using Nethereum.ABI.FunctionEncoding.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace IPFSPiningService
{
    internal class PollsStruct: IFunctionOutputDTO
    {
        public virtual BigInteger totalAmountContributed { get; set; }
        public virtual uint oracleId { get; set; }
        public virtual uint dateLimit { get; set; }
        public virtual uint receiverId { get; set; }
        public virtual Boolean oracleResolved { get; set; }
        public virtual Boolean oracleResult { get; set; }
        public virtual Boolean disputed { get; set; }
        public virtual Boolean ultimateOracleResolved { get; set; }
        public virtual Boolean ultimateOracleResult { get; set; }
        public virtual Boolean receiverRewardRequested { get; set; }
        public virtual Boolean oracleRewardsRequested { get; set; }
    }
}
