using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPFSPiningService
{
    public class ContractsMetadata
    {
        public string accountsStorageAddress { get; set; }
        public string accountsStorageTxHash { get; set; }
        public string accountsStorageABI { get; set; }
        public string accountsStorageBlockDeployed { get; set; }
        public string pollRewardsAddress { get; set; }
        public string pollRewardsTxHash { get; set; }
        public string pollRewardsABI { get; set; }
        public string pollRewardsBlockDeployed { get; set; }
    }
}
