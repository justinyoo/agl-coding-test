using AglCodingTest.Dependencies.Tests.Fixtures;
using AglCodingTest.Settings;

using Autofac;

using FluentAssertions;

using Xunit;

namespace AglCodingTest.Dependencies.Tests
{
    /// <summary>
    /// This represents the test entity for <see cref="AppModule"/>.
    /// </summary>
    public class AppModuleTests : IClassFixture<DependencyFixture>
    {
        private readonly DependencyFixture _fixture;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppModuleTests"/> class.
        /// </summary>
        /// <param name="fixture"><see cref="DependencyFixture"/> instance.</param>
        public AppModuleTests(DependencyFixture fixture)
        {
            this._fixture = fixture;
        }

        /// <summary>
        /// Tests whether the container contains the dependency or not.
        /// </summary>
        [Fact]
        public void Given_Instance_ContainerBuilder_ShouldReturn_Result()
        {
            var container = this._fixture.ArrangeContainer(p => p.RegisterModule<AppModule>());

            var appSettings = container.Resolve<AppSettings>();

            appSettings.Should().NotBeNull();
        }
    }
}
