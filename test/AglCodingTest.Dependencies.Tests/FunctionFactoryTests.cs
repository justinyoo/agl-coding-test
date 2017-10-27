using AglCodingTest.Dependencies.Tests.Fixtures;

using FluentAssertions;

using Xunit;

namespace AglCodingTest.Dependencies.Tests
{
    /// <summary>
    /// This represents the test entity for the <see cref="FunctionFactory"/> class.
    /// </summary>
    public class FunctionFactoryTests : IClassFixture<DependencyFixture>
    {
        private readonly DependencyFixture _fixture;

        /// <summary>
        /// Initializes a new instance of the <see cref="FunctionFactoryTests"/> class.
        /// </summary>
        /// <param name="fixture"><see cref="DependencyFixture"/> instance.</param>
        public FunctionFactoryTests(DependencyFixture fixture)
        {
            this._fixture = fixture;
        }

        /// <summary>
        /// Tests whether the method should return result or not.
        /// </summary>
        [Fact]
        public void Given_Handler_Function_ShouldBe_Created()
        {
            var handler = this._fixture.ArrangeRegistrationHandler();
            var logger = this._fixture.ArrangeLog();

            var factory = new FunctionFactory(handler);

            var function = factory.Create<IFooFunction>(logger);

            function.Log.Should().NotBeNull();
        }
    }
}
