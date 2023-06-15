using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts;
using System.Threading;

namespace HardhatSmartcontractLotteryFcc.Contracts.Raffle.ContractDefinition
{


    public partial class RaffleDeployment : RaffleDeploymentBase
    {
        public RaffleDeployment() : base(BYTECODE) { }
        public RaffleDeployment(string byteCode) : base(byteCode) { }
    }

    public class RaffleDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "61016060405234801561001157600080fd5b50604051610d38380380610d388339810160408190526100309161007e565b6001600160a01b0395909516608081905260a05260e092909252610120526001600160401b039190911660c052610140526003805460ff191690554260005563ffffffff1661010052610109565b60008060008060008060c0878903121561009757600080fd5b86516001600160a01b03811681146100ae57600080fd5b60208801519096506001600160401b03811681146100cb57600080fd5b8095505060408701519350606087015192506080870151915060a087015163ffffffff811681146100fb57600080fd5b809150509295509295509295565b60805160a05160c05160e051610100516101205161014051610bc16101776000396000818160d6015261030701526000818161020a01526105d5015260006104d001526000610472015260006104980152600061050c01526000818161028301526102c50152610bc16000f3fe6080604052600436106100c25760003560e01c806353a2c19a1161007f57806391ad27b41161005957806391ad27b4146101fb578063c1c244e81461022e578063e55ae4e814610243578063fd6673f51461026357600080fd5b806353a2c19a146101a55780635f1b0fd8146101b95780636e04ff0d146101cd57600080fd5b806309bc33a7146100c7578063115cbaf5146101095780631fe543e3146101295780632cfcc5391461014b5780634585e33b14610153578063473f1ddc14610173575b600080fd5b3480156100d357600080fd5b507f00000000000000000000000000000000000000000000000000000000000000005b6040519081526020015b60405180910390f35b34801561011557600080fd5b5060035460ff16604051610100919061087a565b34801561013557600080fd5b506101496101443660046108e9565b610278565b005b610149610305565b34801561015f57600080fd5b5061014961016e36600461099b565b6103eb565b34801561017f57600080fd5b506001546001600160a01b03165b6040516001600160a01b039091168152602001610100565b3480156101b157600080fd5b5060016100f6565b3480156101c557600080fd5b5060036100f6565b3480156101d957600080fd5b506101ed6101e8366004610a0d565b6105ad565b604051610100929190610aa2565b34801561020757600080fd5b507f00000000000000000000000000000000000000000000000000000000000000006100f6565b34801561023a57600080fd5b506000546100f6565b34801561024f57600080fd5b5061018d61025e366004610afa565b610652565b34801561026f57600080fd5b506002546100f6565b336001600160a01b037f000000000000000000000000000000000000000000000000000000000000000016146102f75760405163073e64fd60e21b81523360048201526001600160a01b037f00000000000000000000000000000000000000000000000000000000000000001660248201526044015b60405180910390fd5b6103018282610682565b5050565b7f00000000000000000000000000000000000000000000000000000000000000003410156103465760405163181c600560e11b815260040160405180910390fd5b600060035460ff16600181111561035f5761035f610864565b1461037d5760405163349289c560e21b815260040160405180910390fd5b6002805460018101825560009182527f405787fa12a823e0f2b7631cc41b3ba8828b3321ca811111fa75cd3aa3bb5ace0180546001600160a01b0319163390811790915560405190917f0805e1d667bddb8a95f0f09880cf94f403fb596ce79928d9f29b74203ba284d491a2565b6000610405604051806020016040528060008152506105ad565b509050806104545760025460035447919060ff16600181111561042a5761042a610864565b604051632c2193d560e11b81526004810193909352602483019190915260448201526064016102ee565b6003805460ff1916600190811782556040516305d3b1d360e41b81527f000000000000000000000000000000000000000000000000000000000000000060048201527f000000000000000000000000000000000000000000000000000000000000000067ffffffffffffffff16602482015260448101929092527f000000000000000000000000000000000000000000000000000000000000000063ffffffff16606483015260848201526000906001600160a01b037f00000000000000000000000000000000000000000000000000000000000000001690635d3b1d309060a4016020604051808303816000875af1158015610555573d6000803e3d6000fd5b505050506040513d601f19601f820116820180604052508101906105799190610b13565b60405190915081907fcd6e45c8998311cab7e9d4385596cac867e20a0587194b954fa3a731c93ce78b90600090a250505050565b600354600090606090829060ff1660018111156105cc576105cc610864565b600014905060007f0000000000000000000000000000000000000000000000000000000000000000600054426106029190610b2c565b6002549110915015154715158280156106185750835b80156106215750805b801561062a5750815b60408051808201909152600381526203078360ec1b6020820152909890975095505050505050565b60006002828154811061066757610667610b53565b6000918252602090912001546001600160a01b031692915050565b60006002805490508260008151811061069d5761069d610b53565b60200260200101516106af9190610b69565b90506000600282815481106106c6576106c6610b53565b6000918252602082200154600180546001600160a01b0319166001600160a01b039092169182179055915060405190808252806020026020018201604052801561071a578160200160208202803683370190505b50805161072f916002916020909101906107ea565b506003805460ff191690554260009081556040516001600160a01b0383169047908381818185875af1925050503d8060008114610788576040519150601f19603f3d011682016040523d82523d6000602084013e61078d565b606091505b50509050806107af5760405163a1d04b3960e01b815260040160405180910390fd5b6040516001600160a01b038316907f5b690ec4a06fe979403046eaeea5b3ce38524683c3001f662c8b5a829632f7df90600090a25050505050565b82805482825590600052602060002090810192821561083f579160200282015b8281111561083f57825182546001600160a01b0319166001600160a01b0390911617825560209092019160019091019061080a565b5061084b92915061084f565b5090565b5b8082111561084b5760008155600101610850565b634e487b7160e01b600052602160045260246000fd5b602081016002831061089c57634e487b7160e01b600052602160045260246000fd5b91905290565b634e487b7160e01b600052604160045260246000fd5b604051601f8201601f1916810167ffffffffffffffff811182821017156108e1576108e16108a2565b604052919050565b600080604083850312156108fc57600080fd5b8235915060208084013567ffffffffffffffff8082111561091c57600080fd5b818601915086601f83011261093057600080fd5b813581811115610942576109426108a2565b8060051b91506109538483016108b8565b818152918301840191848101908984111561096d57600080fd5b938501935b8385101561098b57843582529385019390850190610972565b8096505050505050509250929050565b600080602083850312156109ae57600080fd5b823567ffffffffffffffff808211156109c657600080fd5b818501915085601f8301126109da57600080fd5b8135818111156109e957600080fd5b8660208285010111156109fb57600080fd5b60209290920196919550909350505050565b60006020808385031215610a2057600080fd5b823567ffffffffffffffff80821115610a3857600080fd5b818501915085601f830112610a4c57600080fd5b813581811115610a5e57610a5e6108a2565b610a70601f8201601f191685016108b8565b91508082528684828501011115610a8657600080fd5b8084840185840137600090820190930192909252509392505050565b821515815260006020604081840152835180604085015260005b81811015610ad857858101830151858201606001528201610abc565b506000606082860101526060601f19601f830116850101925050509392505050565b600060208284031215610b0c57600080fd5b5035919050565b600060208284031215610b2557600080fd5b5051919050565b81810381811115610b4d57634e487b7160e01b600052601160045260246000fd5b92915050565b634e487b7160e01b600052603260045260246000fd5b600082610b8657634e487b7160e01b600052601260045260246000fd5b50069056fea26469706673582212204f4f79184e7c99a082aae4817c7f3c929e40c0bcee839403bc465af02dcd004d64736f6c63430008130033";
        public RaffleDeploymentBase() : base(BYTECODE) { }
        public RaffleDeploymentBase(string byteCode) : base(byteCode) { }
        [Parameter("address", "vrfCoordinatorV2", 1)]
        public virtual string VrfCoordinatorV2 { get; set; }
        [Parameter("uint64", "subscriptionId", 2)]
        public virtual ulong SubscriptionId { get; set; }
        [Parameter("bytes32", "gasLane", 3)]
        public virtual byte[] GasLane { get; set; }
        [Parameter("uint256", "interval", 4)]
        public virtual BigInteger Interval { get; set; }
        [Parameter("uint256", "entranceFee", 5)]
        public virtual BigInteger EntranceFee { get; set; }
        [Parameter("uint32", "callbackGasLimit", 6)]
        public virtual uint CallbackGasLimit { get; set; }
    }

    public partial class CheckUpkeepFunction : CheckUpkeepFunctionBase { }

    [Function("checkUpkeep", typeof(CheckUpkeepOutputDTO))]
    public class CheckUpkeepFunctionBase : FunctionMessage
    {
        [Parameter("bytes", "", 1)]
        public virtual byte[] ReturnValue1 { get; set; }
    }

    public partial class EnterRaffleFunction : EnterRaffleFunctionBase { }

    [Function("enterRaffle")]
    public class EnterRaffleFunctionBase : FunctionMessage
    {

    }

    public partial class GetEntranceFeeFunction : GetEntranceFeeFunctionBase { }

    [Function("getEntranceFee", "uint256")]
    public class GetEntranceFeeFunctionBase : FunctionMessage
    {

    }

    public partial class GetIntervalFunction : GetIntervalFunctionBase { }

    [Function("getInterval", "uint256")]
    public class GetIntervalFunctionBase : FunctionMessage
    {

    }

    public partial class GetLastTimeStampFunction : GetLastTimeStampFunctionBase { }

    [Function("getLastTimeStamp", "uint256")]
    public class GetLastTimeStampFunctionBase : FunctionMessage
    {

    }

    public partial class GetNumWordsFunction : GetNumWordsFunctionBase { }

    [Function("getNumWords", "uint256")]
    public class GetNumWordsFunctionBase : FunctionMessage
    {

    }

    public partial class GetNumberOfPlayersFunction : GetNumberOfPlayersFunctionBase { }

    [Function("getNumberOfPlayers", "uint256")]
    public class GetNumberOfPlayersFunctionBase : FunctionMessage
    {

    }

    public partial class GetPlayerFunction : GetPlayerFunctionBase { }

    [Function("getPlayer", "address")]
    public class GetPlayerFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "index", 1)]
        public virtual BigInteger Index { get; set; }
    }

    public partial class GetRaffleStateFunction : GetRaffleStateFunctionBase { }

    [Function("getRaffleState", "uint8")]
    public class GetRaffleStateFunctionBase : FunctionMessage
    {

    }

    public partial class GetRecentWinnerFunction : GetRecentWinnerFunctionBase { }

    [Function("getRecentWinner", "address")]
    public class GetRecentWinnerFunctionBase : FunctionMessage
    {

    }

    public partial class GetRequestConfirmationsFunction : GetRequestConfirmationsFunctionBase { }

    [Function("getRequestConfirmations", "uint256")]
    public class GetRequestConfirmationsFunctionBase : FunctionMessage
    {

    }

    public partial class PerformUpkeepFunction : PerformUpkeepFunctionBase { }

    [Function("performUpkeep")]
    public class PerformUpkeepFunctionBase : FunctionMessage
    {
        [Parameter("bytes", "", 1)]
        public virtual byte[] ReturnValue1 { get; set; }
    }

    public partial class RawFulfillRandomWordsFunction : RawFulfillRandomWordsFunctionBase { }

    [Function("rawFulfillRandomWords")]
    public class RawFulfillRandomWordsFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "requestId", 1)]
        public virtual BigInteger RequestId { get; set; }
        [Parameter("uint256[]", "randomWords", 2)]
        public virtual List<BigInteger> RandomWords { get; set; }
    }

    public partial class RaffleEnterEventDTO : RaffleEnterEventDTOBase { }

    [Event("RaffleEnter")]
    public class RaffleEnterEventDTOBase : IEventDTO
    {
        [Parameter("address", "player", 1, true )]
        public virtual string Player { get; set; }
    }

    public partial class RequestedRaffleWinnerEventDTO : RequestedRaffleWinnerEventDTOBase { }

    [Event("RequestedRaffleWinner")]
    public class RequestedRaffleWinnerEventDTOBase : IEventDTO
    {
        [Parameter("uint256", "requestId", 1, true )]
        public virtual BigInteger RequestId { get; set; }
    }

    public partial class WinnerPickedEventDTO : WinnerPickedEventDTOBase { }

    [Event("WinnerPicked")]
    public class WinnerPickedEventDTOBase : IEventDTO
    {
        [Parameter("address", "player", 1, true )]
        public virtual string Player { get; set; }
    }

    public partial class OnlyCoordinatorCanFulfillError : OnlyCoordinatorCanFulfillErrorBase { }

    [Error("OnlyCoordinatorCanFulfill")]
    public class OnlyCoordinatorCanFulfillErrorBase : IErrorDTO
    {
        [Parameter("address", "have", 1)]
        public virtual string Have { get; set; }
        [Parameter("address", "want", 2)]
        public virtual string Want { get; set; }
    }







    public partial class Raffle__UpkeepNotNeededError : Raffle__UpkeepNotNeededErrorBase { }

    [Error("Raffle__UpkeepNotNeeded")]
    public class Raffle__UpkeepNotNeededErrorBase : IErrorDTO
    {
        [Parameter("uint256", "currentBalance", 1)]
        public virtual BigInteger CurrentBalance { get; set; }
        [Parameter("uint256", "numPlayers", 2)]
        public virtual BigInteger NumPlayers { get; set; }
        [Parameter("uint256", "raffleState", 3)]
        public virtual BigInteger RaffleState { get; set; }
    }

    public partial class CheckUpkeepOutputDTO : CheckUpkeepOutputDTOBase { }

    [FunctionOutput]
    public class CheckUpkeepOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "upkeepNeeded", 1)]
        public virtual bool UpkeepNeeded { get; set; }
        [Parameter("bytes", "", 2)]
        public virtual byte[] ReturnValue2 { get; set; }
    }



    public partial class GetEntranceFeeOutputDTO : GetEntranceFeeOutputDTOBase { }

    [FunctionOutput]
    public class GetEntranceFeeOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class GetIntervalOutputDTO : GetIntervalOutputDTOBase { }

    [FunctionOutput]
    public class GetIntervalOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class GetLastTimeStampOutputDTO : GetLastTimeStampOutputDTOBase { }

    [FunctionOutput]
    public class GetLastTimeStampOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class GetNumWordsOutputDTO : GetNumWordsOutputDTOBase { }

    [FunctionOutput]
    public class GetNumWordsOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class GetNumberOfPlayersOutputDTO : GetNumberOfPlayersOutputDTOBase { }

    [FunctionOutput]
    public class GetNumberOfPlayersOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class GetPlayerOutputDTO : GetPlayerOutputDTOBase { }

    [FunctionOutput]
    public class GetPlayerOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class GetRaffleStateOutputDTO : GetRaffleStateOutputDTOBase { }

    [FunctionOutput]
    public class GetRaffleStateOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint8", "", 1)]
        public virtual byte ReturnValue1 { get; set; }
    }

    public partial class GetRecentWinnerOutputDTO : GetRecentWinnerOutputDTOBase { }

    [FunctionOutput]
    public class GetRecentWinnerOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class GetRequestConfirmationsOutputDTO : GetRequestConfirmationsOutputDTOBase { }

    [FunctionOutput]
    public class GetRequestConfirmationsOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }




}
