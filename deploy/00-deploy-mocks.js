const { network } = require("hardhat")
const ethers = require("ethers")

const BASE_FEE = ethers.utils.parseEther("0.25") // 0.25  is the peremium
const GAS_PRICE_LINK = 1e9 // link per gas.

module.exports = async ({ getNamedAccounts, deployments }) => {
    const { deploy, log } = deployments
    const { deployer } = await getNamedAccounts()
    const args = [BASE_FEE, GAS_PRICE_LINK]
    const chainId = network.config.chainId

    if (chainId == 31337) {
        log("Local network detected! Deploying mocks..")
        // deploy a mock vrfcoordinator....
        await deploy("VRFCoordinatorV2Mock", {
            from: deployer,
            log: true,
            args: args,
        })
        log("Mocks Deployed!!")
        log("-------------------------------------------")
    }
}

module.exports.tags = ["all", "mocks"]