using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using AglCodingTest.Extensions;
using AglCodingTest.Models;
using AglCodingTest.Services;
using AglCodingTest.Services.ServiceOptions;
using AglCodingTest.Settings;

namespace AglCodingTest.Functions
{
    /// <summary>
    /// This represents the function entity to process for AGL coding test.
    /// </summary>
    public class AglCodingTestHttpTriggerFunction : IAglCodingTestHttpTriggerFunction
    {
        private readonly AppSettings _appSettings;
        private readonly IAglPayloadLoadingService _service;

        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="AglCodingTestHttpTriggerFunction"/> class.
        /// </summary>
        /// <param name="appSettings"><see cref="AppSettings"/> instance.</param>
        /// <param name="service"><see cref="IAglPayloadLoadingService"/> instance.</param>
        public AglCodingTestHttpTriggerFunction(AppSettings appSettings, IAglPayloadLoadingService service)
        {
            this._appSettings = appSettings.ThrowIfNullOrDefault();
            this._service = service.ThrowIfNullOrDefault();
        }

        /// <inheritdoc />
        public async Task<object> InvokeAsync<TIn, TOptions>(TIn input, TOptions options)
            where TOptions : ServiceOptionsBase
        {
            var req = input as HttpRequestMessage;
            req.ThrowIfNullOrDefault();

            options.ThrowIfNullOrDefault();

            var serviceOptions = options as AglPayloadLoadingServiceOptions;
            serviceOptions.ThrowIfNullOrDefault();

            await this._service.InvokeAsync(serviceOptions).ConfigureAwait(false);

            var catOwners = serviceOptions.People
                                          .Where(p => p.Pets.Any(q => q.PetType == PetType.Cat))
                                          .OrderBy(p => p.GenderType)
                                          .Select(p => new { Gender = p.GenderType, Names = p.Pets.Where(q => q.PetType == PetType.Cat).OrderBy(q => q.Name).Select(q => q.Name) })
                                          .Select(p => $"<h1>{p.Gender}</h1><ul>{string.Join(string.Empty, p.Names.Select(q => $"<li>{q}</li>"))}</ul>")
                                          .ToList();

            var html = new StringBuilder();
            html.AppendLine("<html><body>");
            html.AppendLine(string.Join(string.Empty, catOwners));
            html.AppendLine("</body></html>");

            var res = req.CreateResponse(HttpStatusCode.OK, html.ToString(), "text/html");

            return res;
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
