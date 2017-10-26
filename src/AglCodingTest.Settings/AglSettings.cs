using System;

namespace AglCodingTest.Settings
{
    /// <summary>
    /// This represents the settings entity for AGL coding test.
    /// </summary>
    public class AglSettings
    {
        private const string AglEndpoint = "Agl.Endpoint";

        /// <summary>
        /// Gets the endpoint URL.
        /// </summary>
        public virtual string Endpoint => Environment.GetEnvironmentVariable(AglEndpoint);
    }
}