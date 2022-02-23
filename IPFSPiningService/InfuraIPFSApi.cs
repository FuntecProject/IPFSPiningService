using System;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace IPFSPiningService
{
    public class InfuraIPFSApi
    {
        private readonly HttpClient client;
        private const string apiBaseUrl = "https://ipfs.infura.io:5001/api/v0/";
        private string projectId;
        private string projectSecret;

        public InfuraIPFSApi()
        {
            projectId = Environment.GetEnvironmentVariable("PROJECT_ID");
            projectSecret = Environment.GetEnvironmentVariable("PROJECT_SECRET");

            client = new HttpClient();
            client.BaseAddress = new Uri(apiBaseUrl);
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
                    "key",
                    $"=Basic {System.Convert.ToBase64String(Encoding.UTF8.GetBytes($"{projectId}:{projectSecret}"))}"
                );
        }

        public async Task<Boolean> DoesFileExist(string hash)
        {
            var response = await client.GetAsync($"cat?arg={hash}");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<string> GetFile(string hash)
        {
            var response = await client.GetAsync($"cat?arg={hash}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return null;
        }

        public async Task<Boolean> PinFile(string hash)
        {
            var response = await client.PostAsync($"pin/add?arg={hash}", null);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<IPFSAddResponse> AddFile(string fileContent)
        {
            MultipartFormDataContent form = new();
            form.Add(new ByteArrayContent(Encoding.ASCII.GetBytes(fileContent)));

            var response = await client.PostAsync("add", form);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = JsonConvert.DeserializeObject<IPFSAddResponse>(await response.Content.ReadAsStringAsync());

                return responseContent;
            }

            return null;
        }
    }
}
