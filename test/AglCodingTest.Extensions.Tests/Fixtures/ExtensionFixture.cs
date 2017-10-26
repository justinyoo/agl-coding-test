using System;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace AglCodingTest.Extensions.Tests.Fixtures
{
    /// <summary>
    /// This represents the fixture entity to test extensions.
    /// </summary>
    public class ExtensionFixture : IDisposable
    {
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtensionFixture"/> class.
        /// </summary>
        public ExtensionFixture()
        {
            this.Fixture = new Fixture().Customize(new AutoMoqCustomization());
        }

        /// <summary>
        /// Gets the <see cref="IFixture"/> instance.
        /// </summary>
        public IFixture Fixture { get; }

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
