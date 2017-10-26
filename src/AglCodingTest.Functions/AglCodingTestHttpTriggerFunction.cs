using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using AglCodingTest.Extensions;
using AglCodingTest.Functions.FunctionOptions;
using AglCodingTest.Services;
using AglCodingTest.Services.ServiceOptions;

namespace AglCodingTest.Functions
{
    /// <summary>
    /// This represents the function entity to process for AGL coding test.
    /// </summary>
    public class AglCodingTestHttpTriggerFunction : IAglCodingTestHttpTriggerFunction
    {
        private readonly IAglPayloadLoadingService _loadingService;
        private readonly IAglPayloadProcessingService _processingService;

        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="AglCodingTestHttpTriggerFunction"/> class.
        /// </summary>
        /// <param name="loadingService"><see cref="IAglPayloadLoadingService"/> instance.</param>
        /// <param name="processingService"><see cref="IAglPayloadProcessingService"/> instance.</param>
        public AglCodingTestHttpTriggerFunction(IAglPayloadLoadingService loadingService, IAglPayloadProcessingService processingService)
        {
            this._loadingService = loadingService.ThrowIfNullOrDefault();
            this._processingService = processingService.ThrowIfNullOrDefault();
        }

        /// <inheritdoc />
        public async Task<object> InvokeAsync<TInput, TOptions>(TInput input, TOptions options = default(TOptions))
            where TOptions : FunctionOptionsBase
        {
            var req = input as HttpRequestMessage;
            req.ThrowIfNullOrDefault();

            options.ThrowIfNullOrDefault();

            // STEP #1: Load payload.
            var loadingServiceOptions = new AglPayloadLoadingServiceOptions();

            await this._loadingService.InvokeAsync(loadingServiceOptions).ConfigureAwait(false);

            if (!loadingServiceOptions.IsInvoked)
            {
                return req.CreateErrorResponse(HttpStatusCode.InternalServerError, "Payload couldn't be loaded");
            }

            // STEP #2: Process payload.
            var processingServiceOptions = new AglPayloadProcessingServiceOptions()
                                               {
                                                   People = loadingServiceOptions.People
                                               };

            await this._processingService.InvokeAsync(processingServiceOptions).ConfigureAwait(false);

            if (!processingServiceOptions.IsInvoked)
            {
                return req.CreateErrorResponse(HttpStatusCode.InternalServerError, "Payload couldn't be processed");
            }

            // STEP #3: Create response.
            var html = new StringBuilder();
            html.AppendLine("<html><body>");
            html.AppendLine(string.Join(string.Empty, processingServiceOptions.Groups));
            html.AppendLine("</body></html>");

            return req.CreateResponse(HttpStatusCode.OK, html.ToString(), "text/html");
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (this._disposed)
            {
                return;
            }

            this._disposed = true;
        }
    }
}
