using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using AglCodingTest.Functions;
using AglCodingTest.Functions.FunctionOptions;
using AglCodingTest.Models;
using AglCodingTest.Services;
using AglCodingTest.Settings;

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace AglCodingTest.FunctionApp
{
    /// <summary>
    /// This represents the HTTP trigger entity for index.
    /// </summary>
    public static class IndexHttpTrigger
    {
        /// <summary>
        /// Runs the function.
        /// </summary>
        /// <param name="req"><see cref="HttpRequestMessage"/> instance.</param>
        /// <param name="log"><see cref="TraceWriter"/> instance.</param>
        /// <returns>Returns the <see cref="HttpResponseMessage"/> instance.</returns>
        [FunctionName("IndexHttpTrigger")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "pets")]HttpRequestMessage req, TraceWriter log)
        {
            var appSettings = new AppSettings();
            var httpClient = new HttpClient();
            var loadingService = new AglPayloadLoadingService(appSettings, httpClient);
            var processingService = new AglPayloadProcessingService();
            var function = new AglCodingTestHttpTriggerFunction(loadingService, processingService);

            var pet = req.GetQueryNameValuePairs()
                         .SingleOrDefault(p => p.Key.Equals("type", StringComparison.CurrentCultureIgnoreCase))
                         .Value;
            var petType = string.IsNullOrWhiteSpace(pet)
                              ? PetType.Cat
                              : (PetType)Enum.Parse(typeof(PetType), pet, true);

            var options = new AglCodingTestHttpTriggerFunctionOptions() { PetType = petType };

            var result = await function.InvokeAsync(req, options).ConfigureAwait(false);

            return result as HttpResponseMessage;
        }
    }
}
