const { assert, expect } = require("chai")
const { getNamedAccounts, deployments, network, ethers } = require("hardhat")
const { developmentChains, networkConfig } = require("../../helper-hardhat-config")

developmentChains.includes(network.name) 
    ? describe.skip 
    : describe ("Raffle Unit Test",  function() {
        let raffle, raffleEnteranceFee, deployer

        beforeEach(async function() {
            deployer = (await getNamedAccounts()).deployer
            raffle = await ethers.getContract("Raffle", deployer)
            raffleEnteranceFee = await raffle.getEntranceFee()
            accounts = await ethers.getSigners()
        })

        describe("fulfilRandomWords", function(){
            it("works with live Chainlink Keepers and Chainlink VRF, we get a random winner", async function() {
                // enter the raffle 
                const startingTimeStamp = await raffle.getLastTimeStamp()
                // deployer = (await getNamedAccounts()).deployer
                await new Promise(async (resolve, reject) => {
                    raffle.once("WinnerPicked", async () => {
                        console.log("WinnerPicked event fired!!!")
                        try {
                            //add our asserts here
                            const recentWinner = await raffle.getRecentWinner()
                            const raffleState = await raffle.getRaffleState()
                            const winnerEndingBalance = await accounts[0].getBalance()
                            const endingTimeStamp = await raffle.getLastTimeStamp()

                            await expect(raffle.getPlayer(0)).to.be.reverted
                            console.log(recentWinner)
                            assert.equal(recentWinner, accounts[0].address)
                            assert.equal(raffleState, 0)
                            assert.equal(
                                winnerEndingBalance.toString(),
                                winnerStartingBalance.add(raffleEnteranceFee).toString()
                            )
                            assert(endingTimeStamp > startingTimeStamp)
                            resolve() 
                        } catch(error) {
                            console.log(error)
                            reject(e)
                        }
                    })
                    // then entering the raffle
                    console.log("Entering Raffle...")
                      const tx = await raffle.enterRaffle({ value: raffleEnteranceFee })
                      await tx.wait(1)
                      console.log("Ok, time to wait...")
                      const winnerStartingBalance = await accounts[0].getBalance()

                })
            })
        })

    })