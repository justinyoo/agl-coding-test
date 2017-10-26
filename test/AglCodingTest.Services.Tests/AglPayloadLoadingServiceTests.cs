using System;
using System.Linq;
using System.Net;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web;

using AglCodingTest.Models;
using AglCodingTest.Services.ServiceOptions;
using AglCodingTest.Services.Tests.Fixtures;

using FluentAssertions;

using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit2;

using Xunit;

namespace AglCodingTest.Services.Tests
{
    /// <summary>
    /// This represents the test entity for <see cref="AglPayloadLoadingService"/>.
    /// </summary>
    public class AglPayloadLoadingServiceTests : IClassFixture<ServiceFixture>
    {
        private readonly ServiceFixture _fixture;

        /// <summary>
        /// Initializes a new instance of the <see cref="AglPayloadLoadingServiceTests"/> class.
        /// </summary>
        /// <param name="fixture"><see cref="ServiceFixture"/> instance.</param>
        public AglPayloadLoadingServiceTests(ServiceFixture fixture)
        {
            this._fixture = fixture;
        }

        /// <summary>
        /// Tests whether the constructor should throw an exception or not.
        /// </summary>
        [Fact]
        public void Given_NullParameter_Constructor_ShouldThrow_Exception()
        {
            var settings = this._fixture.ArrangeAppSettings();
            settings.SetupGet(p => p.Formatter).Returns(new JsonMediaTypeFormatter());

            Action action = () => new AglPayloadLoadingService(null, null);
            action.ShouldThrow<ArgumentNullException>();

            action = () => new AglPayloadLoadingService(settings.Object, null);
            action.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        /// Tests whether the method should throw an exception or not.
        /// </summary>
        [Fact]
        public void Given_NullServiceOptions_InvokeAsync_ShouldThrow_Exception()
        {
            var settings = this._fixture.ArrangeAppSettings();
            var client = this._fixture.ArrangeHttpClient();

            var service = new AglPayloadLoadingService(settings.Object, client);

            Func<Task> func = async () => await service.InvokeAsync<ServiceOptionsBase>(null).ConfigureAwait(false);
            func.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        /// Tests whether the method should throw an exception or not.
        /// </summary>
        [Fact]
        public void Given_InvalidServiceOptions_InvokeAsync_ShouldThrow_Exception()
        {
            var settings = this._fixture.ArrangeAppSettings();
            var client = this._fixture.ArrangeHttpClient();

            var service = new AglPayloadLoadingService(settings.Object, client);

            var options = new FooServiceOptions();

            Func<Task> func = async () => await service.InvokeAsync(options).ConfigureAwait(false);
            func.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        /// Tests whether the method should throw an exception or not.
        /// </summary>
        /// <param name="statusCode"><see cref="HttpStatusCode"/> value.</param>
        /// <param name="phrase">Reason phrase value.</param>
        [Theory]
        [InlineAutoData(HttpStatusCode.InternalServerError)]
        public void Given_ErrorResponse_InvokeAsync_ShouldThrow_Exception(HttpStatusCode statusCode, string phrase)
        {
            var settings = this._fixture.ArrangeAppSettings();
            var client = this._fixture.ArrangeHttpClient(statusCode, phrase);
            var options = new AglPayloadLoadingServiceOptions();

            var service = new AglPayloadLoadingService(settings.Object, client);

            Func<Task> func = async () => await service.InvokeAsync(options).ConfigureAwait(false);
            func.ShouldThrow<HttpException>();
        }

        /// <summary>
        /// Tests whether the method should throw an exception or not.
        /// </summary>
        /// <param name="statusCode"><see cref="HttpStatusCode"/> value.</param>
        /// <param name="phrase">Reason phrase value.</param>
        [Theory]
        [InlineAutoData(HttpStatusCode.OK)]
        public async void Given_Response_InvokeAsync_ShouldReturn_Result(HttpStatusCode statusCode, string phrase)
        {
            var settings = this._fixture.ArrangeAppSettings();
            var content = this._fixture.Fixture.CreateMany<Person>().ToList();
            var client = this._fixture.ArrangeHttpClient(statusCode, phrase, content);
            var options = new AglPayloadLoadingServiceOptions();

            var service = new AglPayloadLoadingService(settings.Object, client);

            await service.InvokeAsync(options).ConfigureAwait(false);

            options.People.Should().HaveCount(content.Count);
            options.IsInvoked.Should().BeTrue();
        }
    }
}
