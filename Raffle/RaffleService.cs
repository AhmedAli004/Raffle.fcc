using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Contracts;
using System.Threading;
using HardhatSmartcontractLotteryFcc.Contracts.Raffle.ContractDefinition;

namespace HardhatSmartcontractLotteryFcc.Contracts.Raffle
{
    public partial class RaffleService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, RaffleDeployment raffleDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<RaffleDeployment>().SendRequestAndWaitForReceiptAsync(raffleDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, RaffleDeployment raffleDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<RaffleDeployment>().SendRequestAsync(raffleDeployment);
        }

        public static async Task<RaffleService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, RaffleDeployment raffleDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, raffleDeployment, cancellationTokenSource);
            return new RaffleService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public RaffleService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<CheckUpkeepOutputDTO> CheckUpkeepQueryAsync(CheckUpkeepFunction checkUpkeepFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<CheckUpkeepFunction, CheckUpkeepOutputDTO>(checkUpkeepFunction, blockParameter);
        }

        public Task<CheckUpkeepOutputDTO> CheckUpkeepQueryAsync(byte[] returnValue1, BlockParameter blockParameter = null)
        {
            var checkUpkeepFunction = new CheckUpkeepFunction();
                checkUpkeepFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryDeserializingToObjectAsync<CheckUpkeepFunction, CheckUpkeepOutputDTO>(checkUpkeepFunction, blockParameter);
        }

        public Task<string> EnterRaffleRequestAsync(EnterRaffleFunction enterRaffleFunction)
        {
             return ContractHandler.SendRequestAsync(enterRaffleFunction);
        }

        public Task<string> EnterRaffleRequestAsync()
        {
             return ContractHandler.SendRequestAsync<EnterRaffleFunction>();
        }

        public Task<TransactionReceipt> EnterRaffleRequestAndWaitForReceiptAsync(EnterRaffleFunction enterRaffleFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(enterRaffleFunction, cancellationToken);
        }

        public Task<TransactionReceipt> EnterRaffleRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync<EnterRaffleFunction>(null, cancellationToken);
        }

        public Task<BigInteger> GetEntranceFeeQueryAsync(GetEntranceFeeFunction getEntranceFeeFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetEntranceFeeFunction, BigInteger>(getEntranceFeeFunction, blockParameter);
        }

        
        public Task<BigInteger> GetEntranceFeeQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetEntranceFeeFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> GetIntervalQueryAsync(GetIntervalFunction getIntervalFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetIntervalFunction, BigInteger>(getIntervalFunction, blockParameter);
        }

        
        public Task<BigInteger> GetIntervalQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetIntervalFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> GetLastTimeStampQueryAsync(GetLastTimeStampFunction getLastTimeStampFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetLastTimeStampFunction, BigInteger>(getLastTimeStampFunction, blockParameter);
        }

        
        public Task<BigInteger> GetLastTimeStampQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetLastTimeStampFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> GetNumWordsQueryAsync(GetNumWordsFunction getNumWordsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetNumWordsFunction, BigInteger>(getNumWordsFunction, blockParameter);
        }

        
        public Task<BigInteger> GetNumWordsQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetNumWordsFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> GetNumberOfPlayersQueryAsync(GetNumberOfPlayersFunction getNumberOfPlayersFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetNumberOfPlayersFunction, BigInteger>(getNumberOfPlayersFunction, blockParameter);
        }

        
        public Task<BigInteger> GetNumberOfPlayersQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetNumberOfPlayersFunction, BigInteger>(null, blockParameter);
        }

        public Task<string> GetPlayerQueryAsync(GetPlayerFunction getPlayerFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetPlayerFunction, string>(getPlayerFunction, blockParameter);
        }

        
        public Task<string> GetPlayerQueryAsync(BigInteger index, BlockParameter blockParameter = null)
        {
            var getPlayerFunction = new GetPlayerFunction();
                getPlayerFunction.Index = index;
            
            return ContractHandler.QueryAsync<GetPlayerFunction, string>(getPlayerFunction, blockParameter);
        }

        public Task<byte> GetRaffleStateQueryAsync(GetRaffleStateFunction getRaffleStateFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetRaffleStateFunction, byte>(getRaffleStateFunction, blockParameter);
        }

        
        public Task<byte> GetRaffleStateQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetRaffleStateFunction, byte>(null, blockParameter);
        }

        public Task<string> GetRecentWinnerQueryAsync(GetRecentWinnerFunction getRecentWinnerFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetRecentWinnerFunction, string>(getRecentWinnerFunction, blockParameter);
        }

        
        public Task<string> GetRecentWinnerQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetRecentWinnerFunction, string>(null, blockParameter);
        }

        public Task<BigInteger> GetRequestConfirmationsQueryAsync(GetRequestConfirmationsFunction getRequestConfirmationsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetRequestConfirmationsFunction, BigInteger>(getRequestConfirmationsFunction, blockParameter);
        }

        
        public Task<BigInteger> GetRequestConfirmationsQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetRequestConfirmationsFunction, BigInteger>(null, blockParameter);
        }

        public Task<string> PerformUpkeepRequestAsync(PerformUpkeepFunction performUpkeepFunction)
        {
             return ContractHandler.SendRequestAsync(performUpkeepFunction);
        }

        public Task<TransactionReceipt> PerformUpkeepRequestAndWaitForReceiptAsync(PerformUpkeepFunction performUpkeepFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(performUpkeepFunction, cancellationToken);
        }

        public Task<string> PerformUpkeepRequestAsync(byte[] returnValue1)
        {
            var performUpkeepFunction = new PerformUpkeepFunction();
                performUpkeepFunction.ReturnValue1 = returnValue1;
            
             return ContractHandler.SendRequestAsync(performUpkeepFunction);
        }

        public Task<TransactionReceipt> PerformUpkeepRequestAndWaitForReceiptAsync(byte[] returnValue1, CancellationTokenSource cancellationToken = null)
        {
            var performUpkeepFunction = new PerformUpkeepFunction();
                performUpkeepFunction.ReturnValue1 = returnValue1;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(performUpkeepFunction, cancellationToken);
        }

        public Task<string> RawFulfillRandomWordsRequestAsync(RawFulfillRandomWordsFunction rawFulfillRandomWordsFunction)
        {
             return ContractHandler.SendRequestAsync(rawFulfillRandomWordsFunction);
        }

        public Task<TransactionReceipt> RawFulfillRandomWordsRequestAndWaitForReceiptAsync(RawFulfillRandomWordsFunction rawFulfillRandomWordsFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(rawFulfillRandomWordsFunction, cancellationToken);
        }

        public Task<string> RawFulfillRandomWordsRequestAsync(BigInteger requestId, List<BigInteger> randomWords)
        {
            var rawFulfillRandomWordsFunction = new RawFulfillRandomWordsFunction();
                rawFulfillRandomWordsFunction.RequestId = requestId;
                rawFulfillRandomWordsFunction.RandomWords = randomWords;
            
             return ContractHandler.SendRequestAsync(rawFulfillRandomWordsFunction);
        }

        public Task<TransactionReceipt> RawFulfillRandomWordsRequestAndWaitForReceiptAsync(BigInteger requestId, List<BigInteger> randomWords, CancellationTokenSource cancellationToken = null)
        {
            var rawFulfillRandomWordsFunction = new RawFulfillRandomWordsFunction();
                rawFulfillRandomWordsFunction.RequestId = requestId;
                rawFulfillRandomWordsFunction.RandomWords = randomWords;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(rawFulfillRandomWordsFunction, cancellationToken);
        }
    }
}
