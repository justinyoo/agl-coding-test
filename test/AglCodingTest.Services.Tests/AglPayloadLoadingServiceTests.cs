using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

using AglCodingTest.Services.ServiceOptions;
using AglCodingTest.Services.Tests.Fixtures;
using AglCodingTest.Settings;

using FluentAssertions;

using Moq;

using Ploeh.AutoFixture;

using Xunit;

namespace AglCodingTest.Services.Tests
{
    /// <summary>
    /// This represents the test entity for <see cref="AglPayloadLoadingService"/>.
    /// </summary>
    public class AglPayloadLoadingServiceTests : IClassFixture<ServiceFixture>
    {
        private readonly IFixture _fixture;

        /// <summary>
        /// Initializes a new instance of the <see cref="AglPayloadLoadingServiceTests"/> class.
        /// </summary>
        /// <param name="fixture"><see cref="ServiceFixture"/> instance.</param>
        public AglPayloadLoadingServiceTests(ServiceFixture fixture)
        {
            this._fixture = fixture.Fixture;
        }

        /// <summary>
        /// Tests whether the constructor should throw an exception or not.
        /// </summary>
        [Fact]
        public void Given_NullParameter_Constructor_ShouldThrow_Exception()
        {
            var settings = new Mock<AppSettings>();
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
            var settings = this._fixture.Create<AppSettings>();
            var client = this._fixture.Create<HttpClient>();

            var service = new AglPayloadLoadingService(settings, client);

            Func<Task> func = async () => await service.InvokeAsync<ServiceOptionsBase>(null).ConfigureAwait(false);
            func.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        /// Tests whether the method should throw an exception or not.
        /// </summary>
        [Fact]
        public void Given_InvalidServiceOptions_InvokeAsync_ShouldThrow_Exception()
        {
            var settings = this._fixture.Create<AppSettings>();
            var client = this._fixture.Create<HttpClient>();

            var service = new AglPayloadLoadingService(settings, client);

            var options = new FooServiceOptions();

            Func<Task> func = async () => await service.InvokeAsync(options).ConfigureAwait(false);
            func.ShouldThrow<ArgumentNullException>();
        }
    }
}
