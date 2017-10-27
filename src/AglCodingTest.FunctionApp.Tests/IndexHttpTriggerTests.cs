using System.Net.Http;

using AglCodingTest.FunctionApp.Tests.Fixtures;
using AglCodingTest.Functions;
using AglCodingTest.Functions.FunctionOptions;

using FluentAssertions;

using Moq;

using Xunit;

namespace AglCodingTest.FunctionApp.Tests
{
    /// <summary>
    /// This represents the test entity for <see cref="IndexHttpTrigger"/>.
    /// </summary>
    public class IndexHttpTriggerTests : IClassFixture<FunctionAppFixture>
    {
        private readonly FunctionAppFixture _fixture;

        /// <summary>
        /// Initializes a new instance of the <see cref="IndexHttpTriggerTests"/> class.
        /// </summary>
        /// <param name="fixture"><see cref="FunctionAppFixture"/> instance.</param>
        public IndexHttpTriggerTests(FunctionAppFixture fixture)
        {
            this._fixture = fixture;
        }

        /// <summary>
        /// Tests whether the method should return result or not.
        /// </summary>
        [Fact]
        public async void Given_Factory_Run_ShouldReturn_Result()
        {
            var log = this._fixture.ArrangeLog();
            var factory = this._fixture.ArrangeFunctionFactory(out Mock<IAglCodingTestHttpTriggerFunction> function);
            var req = new HttpRequestMessage();
            var res = new HttpResponseMessage();

            function.SetupGet(p => p.Log).Returns(log);
            function.Setup(p => p.InvokeAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<FunctionOptionsBase>()))
                    .ReturnsAsync(res);

            IndexHttpTrigger.FunctionFactory = factory.Object;

            var result = await IndexHttpTrigger.Run(req, log).ConfigureAwait(false);

            result.Should().NotBeNull();
        }
    }
}
