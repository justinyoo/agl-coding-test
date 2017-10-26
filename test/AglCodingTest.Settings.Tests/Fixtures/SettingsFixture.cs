using System;

using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace AglCodingTest.Settings.Tests.Fixtures
{
    /// <summary>
    /// This represents the fixture entity to test services.
    /// </summary>
    public class SettingsFixture : IDisposable
    {
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsFixture"/> class.
        /// </summary>
        public SettingsFixture()
        {
            this.Fixture = new Fixture().Customize(new AutoMoqCustomization());
        }

        /// <summary>
        /// Gets the <see cref="IFixture"/> instance.
        /// </summary>
        public IFixture Fixture { get; }

        /// <summary>
        /// Sets the environment variables.
        /// </summary>
        /// <param name="key">Environment key.</param>
        /// <param name="value">Environment value.</param>
        public void SetEnvironmentVariable(string key, string value)
        {
            Environment.SetEnvironmentVariable(key, value, EnvironmentVariableTarget.Process);
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
