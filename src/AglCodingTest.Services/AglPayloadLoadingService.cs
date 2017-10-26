using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web;

using AglCodingTest.Extensions;
using AglCodingTest.Models;
using AglCodingTest.Services.ServiceOptions;
using AglCodingTest.Settings;

namespace AglCodingTest.Services
{
    /// <summary>
    /// This represents the service entity to load a payload for AGL coding test.
    /// </summary>
    public class AglPayloadLoadingService : IAglPayloadLoadingService
    {
        private readonly AppSettings _appSettings;
        private readonly HttpClient _httpClient;
        private readonly List<MediaTypeFormatter> _formatters;

        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="AglPayloadLoadingService"/> class.
        /// </summary>
        /// <param name="appSettings"><see cref="AppSettings"/> instance.</param>
        /// <param name="httpClient"><see cref="HttpClient"/> instance.</param>
        public AglPayloadLoadingService(AppSettings appSettings, HttpClient httpClient)
        {
            this._appSettings = appSettings.ThrowIfNullOrDefault();
            this._httpClient = httpClient.ThrowIfNullOrDefault();
            this._formatters = new[] { this._appSettings.Formatter }.ToList();
        }

        /// <inheritdoc />
        public async Task InvokeAsync<TOptions>(TOptions options)
            where TOptions : ServiceOptionsBase
        {
            options.ThrowIfNullOrDefault();

            var serviceOptions = options as AglPayloadLoadingServiceOptions;
            serviceOptions.ThrowIfNullOrDefault();

            var requestUri = this._appSettings.Agl.Endpoint;
            using (var response = await this._httpClient.GetAsync(requestUri).ConfigureAwait(false))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpException((int)response.StatusCode, response.ReasonPhrase);
                }

                var people = await response.Content
                                           .ReadAsAsync<List<Person>>(this._formatters)
                                           .ConfigureAwait(false);

                serviceOptions.People = people;
                serviceOptions.IsInvoked = true;
            }
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
