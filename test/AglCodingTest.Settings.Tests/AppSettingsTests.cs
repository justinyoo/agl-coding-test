using AglCodingTest.Settings.Tests.Fixtures;

using FluentAssertions;

using Ploeh.AutoFixture.Xunit2;

using Xunit;

namespace AglCodingTest.Settings.Tests
{
    /// <summary>
    /// This represents the test entity for <see cref="AppSettings"/>.
    /// </summary>
    public class AppSettingsTests : IClassFixture<SettingsFixture>
    {
        private readonly SettingsFixture _fixture;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppSettingsTests"/> class.
        /// </summary>
        /// <param name="fixture"><see cref="SettingsFixture"/> instance.</param>
        public AppSettingsTests(SettingsFixture fixture)
        {
            this._fixture = fixture;
        }

        /// <summary>
        /// Tests whether the instance should return result or not.
        /// </summary>
        /// <param name="connString">Connection string value.</param>
        /// <param name="endpoint">Endpoint value.</param>
        [Theory]
        [AutoData]
        public void Given_Instance_AppSettings_ShouldReturn_Result(string connString, string endpoint)
        {
            this._fixture.SetEnvironmentVariable("AzureWebJobsStorage", connString);
            this._fixture.SetEnvironmentVariable("Agl.Endpoint", endpoint);

            var settings = new AppSettings();

            settings.StorageAccount.ConnectionString.Should().BeEquivalentTo(connString);
            settings.Agl.Endpoint.Should().BeEquivalentTo(endpoint);
        }
    }
}
