using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace IPFSPiningService
{
    public static class IPFSPiningService
    {
        [FunctionName("PinFile")]
        public static async Task<IActionResult> PinFile(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            if (req.Query.ContainsKey("hash"))
            {
                string hash = req.Query["hash"];

                InfuraIPFSApi infuraIPFSApi = new();

                if (await infuraIPFSApi.PinFile(hash))
                {
                    return new OkObjectResult("Successfully pinned");
                }
            }

            return new BadRequestObjectResult("There was an error pining the file");
        }

        [FunctionName("AddFile")]
        public static async Task<IActionResult> AddFile(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            if (req.Query.ContainsKey("filecontent"))
            {
                string fileContent = req.Query["filecontent"];

                InfuraIPFSApi infuraIPFSApi = new();
                var addFileResponse = await infuraIPFSApi.AddFile(fileContent);

                if (addFileResponse != null)
                {
                    return new OkObjectResult(addFileResponse.hash);
                }
            }

            return new BadRequestObjectResult("There was an error adding the file");
        }

        [FunctionName("GetFile")]
        public static async Task<IActionResult> GetFile(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            if (req.Query.ContainsKey("hash"))
            {
                string hash = req.Query["hash"];

                InfuraIPFSApi infuraIPFSApi = new();
                var file = await infuraIPFSApi.GetFile(hash);

                if (file != null)
                {
                    return new OkObjectResult(file);
                } 
            }

            return new BadRequestObjectResult("There was an error getting the file");
        }
    }
}
