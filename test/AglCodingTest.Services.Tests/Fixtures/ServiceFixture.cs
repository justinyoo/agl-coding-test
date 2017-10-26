using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;

using AglCodingTest.Settings;

using Moq;

using Newtonsoft.Json;

using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

using WorldDomination.Net.Http;

namespace AglCodingTest.Services.Tests.Fixtures
{
    /// <summary>
    /// This represents the fixture entity to test services.
    /// </summary>
    public class ServiceFixture : IDisposable
    {
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceFixture"/> class.
        /// </summary>
        public ServiceFixture()
        {
            this.Fixture = new Fixture().Customize(new AutoMoqCustomization());
        }

        /// <summary>
        /// Gets the <see cref="IFixture"/> instance.
        /// </summary>
        public IFixture Fixture { get; }

        /// <summary>
        /// Arranges the <see cref="Mock{AppSettings}"/> instance.
        /// </summary>
        /// <returns>Returns the <see cref="Mock{AppSettings}"/> instance.</returns>
        public Mock<AppSettings> ArrangeAppSettings()
        {
            var endpoint = this.Fixture.Create<string>("http://");

            var agl = new Mock<AglSettings>();
            agl.SetupGet(p => p.Endpoint).Returns(endpoint);

            var settings = new Mock<AppSettings>();
            settings.SetupGet(p => p.Agl).Returns(agl.Object);
            settings.SetupGet(p => p.SerialiserSettings).Returns(new JsonSerializerSettings());
            settings.SetupGet(p => p.Formatter).Returns(new JsonMediaTypeFormatter());

            return settings;
        }

        /// <summary>
        /// Arranges the <see cref="HttpClient"/> instance.
        /// </summary>
        /// <param name="statusCode"><see cref="HttpStatusCode"/> value.</param>
        /// <param name="phrase">Message related to the <see cref="HttpStatusCode"/> value.</param>
        /// <param name="payload">Response payload.</param>
        /// <returns>Returns the <see cref="HttpClient"/> instance.</returns>
        public HttpClient ArrangeHttpClient(HttpStatusCode? statusCode = null, string phrase = null, object payload = null)
        {
            statusCode = statusCode.GetValueOrDefault(this.Fixture.Create<HttpStatusCode>());
            phrase = phrase ?? this.Fixture.Create<string>();

            var content = payload == null
                              ? (StringContent)null
                              : new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

            var res = this.Fixture.Build<HttpResponseMessage>()
                                  .With(p => p.StatusCode, statusCode.Value)
                                  .With(p => p.ReasonPhrase, phrase)
                                  .With(p => p.Content, content)
                                  .Create();

            var messageOptions = new HttpMessageOptions() { HttpResponseMessage = res };
            var options = new[] { messageOptions };
            var handler = new FakeHttpMessageHandler(options);

            var client = new HttpClient(handler);

            return client;
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
