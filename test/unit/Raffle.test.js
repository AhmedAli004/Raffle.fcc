const { getNamedAccounts, deployments, network, ethers } = require("hardhat")
//const ethers = require("ethers");
const { developmentChains, networkConfig } = require("../../helper-hardhat-config")
const { assert, expect } = require("chai")

!developmentChains.includes(network.name) 
    ? describe.skip 
    : describe ("Raffle Unit Test",  function() {
        let raffle, vrfCoordinatorV2Mock, raffleEnteranceFee, deployer, interval
        const chaindId = network.config.chainId
        beforeEach(async function () {
            await deployments.fixture(["all"])
            raffle = await ethers.getContract("Raffle", deployer)
            vrfCoordinatorV2Mock = await ethers.getContract("VRFCoordinatorV2Mock", deployer)
            raffleEnteranceFee = await raffle.getEntranceFee();
            interval = await raffle.getInterval()
            accounts = await ethers.getSigners()
            deployer = accounts[0]
        })

        describe("constructor",  function () {
            it("Initilizes the raffle correctly", async function () {
                const raffleState = await raffle.getRaffleState()
                assert.equal(raffleState.toString(), "0")
            })
            it("Checking Interval", async function () {
                assert.equal(interval.toString(), networkConfig[chaindId]["interval"])
            })
        })
        describe ("enterRaflle",  function () {
            it("reverts when you dont pay enough", async function() {
                await expect(raffle.enterRaffle()).to.be.revertedWith(
                    "Raffle__SendMoreToEnterRaffle"
                    )
            })
            it("records players when they enter", async function () {
                await raffle.enterRaffle({value: raffleEnteranceFee})
                const playerFromContract = await raffle.getPlayer(0)
                assert.equal(playerFromContract, deployer.address)
            })
            it("emits event on enter", async function () {

                await expect(raffle.enterRaffle({value: raffleEnteranceFee})).to.emit(
                    raffle,
                    "RaffleEnter"
                )
            })
            it("doesnt allow enterance when raffle is calculating", async function () {
                await raffle.enterRaffle({value: raffleEnteranceFee})
                await network.provider.send("evm_increaseTime", [+interval + 1])
                await network.provider.send("evm_mine", [])
                //We predent to be Chainlink keeper
                await raffle.performUpkeep([])
                await expect(raffle.enterRaffle({value: raffleEnteranceFee})).to.be.revertedWith(
                    "Raffle__NotOpen"
                    )
            })
        })
        describe("checkUpkeep",  function() {
            it("returns false if people haven't send any ETH", async function() {
                await network.provider.send("evm_increaseTime", [+interval + 1])
                await network.provider.send("evm_mine", [])
                const { upkeepNeeded } = await raffle.callStatic.checkUpkeep([])
                assert(!upkeepNeeded)
            })
            it("returns false if raffle isn't open", async function() {
                await raffle.enterRaffle({ value: raffleEnteranceFee })
                await network.provider.send("evm_increaseTime", [+interval + 1])
                await network.provider.send("evm_mine", [])             
                await raffle.performUpkeep([]) // changes the state to calculating
                const raffleState = await raffle.getRaffleState() // stores the new state
                const { upkeepNeeded } = await raffle.callStatic.checkUpkeep("0x") // upkeepNeeded = (timePassed && isOpen && hasBalance && hasPlayers)
                assert.equal(raffleState.toString() == "1", upkeepNeeded == false)
            })
            it("returns false if enough time hasn't passed", async function() {
                await raffle.enterRaffle({ value: raffleEnteranceFee })
                await network.provider.send("evm_increaseTime", [+interval - 5])
                await network.provider.send("evm_mine", [])
                const { upkeepNeeded } = await raffle.callStatic.checkUpkeep([])
                assert(!upkeepNeeded)
            })
            it("returns true if enough time has passed, has players, eth, and is open", async function() {
                await raffle.enterRaffle({ value: raffleEnteranceFee})
                await network.provider.send("evm_increaseTime", [+interval + 1])
                await network.provider.send("evm_mine", [])
                const { upkeepNeeded } = await raffle.callStatic.checkUpkeep([])
                assert(upkeepNeeded)
            })
        })
        describe("performUpkeep", function() {
            it("it can only run if checkupkeep is true", async function() {
                await raffle.enterRaffle({ value: raffleEnteranceFee })
                await network.provider.send("evm_increaseTime", [+interval + 1])
                await network.provider.send("evm_mine", [])
                const tx = await raffle.performUpkeep([])
                assert(tx)
            })
            it("reverts when checkupkeep is false", async function() {
                await expect(raffle.performUpkeep([])).to.be.revertedWith(
                    "Raffle__UpkeepNotNeeded"
                )
            })
            it("updates the raffle state, emits and event, and calls the vrf coordinator", async function(){
                await raffle.enterRaffle({ value: raffleEnteranceFee })
                await network.provider.send("evm_increaseTime", [+interval + 1])
                await network.provider.send("evm_mine", [])
                const txResponse = await raffle.performUpkeep([])
                const txReceipt = await txResponse.wait(1)
                const requestId = txReceipt.events[1].args.requestId
                const raffleState = await raffle.getRaffleState()
                assert(requestId.toNumber() > 0)
                assert(raffleState == 1)
            })
        })
        describe("fulfillRandomWords", function() {
            beforeEach(async function() {
                await raffle.enterRaffle({value: raffleEnteranceFee})
                await network.provider.send("evm_increaseTime", [+interval + 1])
                await network.provider.send("evm_mine", [])
            })
            it("can only be called after performUpkeep", async function() {
                await expect(vrfCoordinatorV2Mock.fulfillRandomWords(0, raffle.address)
                ).to.be.revertedWith("nonexistent request")
                await expect(vrfCoordinatorV2Mock.fulfillRandomWords(1, raffle.address)
                ).to.be.revertedWith("nonexistent request")
            })
            // Masive Test 
            it("picks a winner, reset the lottery, and sends money", async () => {
                const additionalEnterants = 3
                const startingAccountIndex = 1 // deployer = 0
                for(
                    let i = startingAccountIndex; 
                    i < startingAccountIndex + additionalEnterants;
                    i++
                ) {
                    const accountConnectedRaffle = raffle.connect(accounts[i])
                    await accountConnectedRaffle.enterRaffle ({ value: raffleEnteranceFee })
                }
                const startingTimeStamp = await raffle.getLastTimeStamp() 

                // performUpkeep (mock being chainlink keepers)
                // fulfilRandomWords (mock being the chainlink VRF)
                // We will have to wait for the fulfillRandomWords to be called
                await new Promise(async (resolve, reject) => {
                    raffle.once("WinnerPicked",async () => {
                        console.log("Found the event!!")
                        try {
                        
                            const  recentWinner = await raffle.getRecentWinner()
                            const raffleState = await raffle.getRaffleState()
                            const endingTimeStamp = await raffle.getLastTimeStamp
                            const numPlayers = await raffle.getNumberOfPlayers()
                            const winnerEndingBalance = await accounts[1].getBalance()
                            assert.equal(numPlayers.toString(), "0")
                            assert.equal(raffleState.toString(), "0")
                            assert(endingTimeStamp > startingTimeStamp)
                            assert(winnerEndingBalance.toString(),
                                   winnerStartingBalance
                                   .add(raffleEnteranceFee
                                    .mul(additionalEnterants)
                                    .add(raffleEnteranceFee).toString())
                            )
                            
                        } catch (e) {
                            reject (e)
                        }
                        resolve()
                    })
                    const tx = await raffle.performUpkeep([])
                    const txReceipt = await tx.wait(1)
                    const winnerStartingBalance = await accounts[1].getBalance()
                    await vrfCoordinatorV2Mock.fulfillRandomWords(
                        txReceipt.events[1].args.requestId,
                        raffle.address
                    )
                })
            })
            
        })
    })







