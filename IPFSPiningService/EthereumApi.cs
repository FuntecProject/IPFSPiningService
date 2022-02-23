using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nethereum.Web3;
using Nethereum.Contracts;
using System.Net.Http;
using Newtonsoft.Json;

namespace IPFSPiningService
{
    public class EthereumApi
    {
        private readonly Web3 web3;
        private readonly string providerUrl;
        private ContractsMetadata contractsMetadata;
        private Contract contract;

        public EthereumApi()
        {
            providerUrl = Environment.GetEnvironmentVariable("INFURA_URL");
            web3 = new (providerUrl);
        }

        public async Task BuildContractInstance()
        {
            using HttpClient client = new();
            var response = await client.GetAsync("https://www.funtec.xyz/etc/contractsMetadata.json");
            contractsMetadata = JsonConvert.DeserializeObject<ContractsMetadata>(await response.Content.ReadAsStringAsync());
            contract = web3.Eth.GetContract(contractsMetadata.pollRewardsABI, contractsMetadata.pollRewardsAddress);
        }
    }
}
