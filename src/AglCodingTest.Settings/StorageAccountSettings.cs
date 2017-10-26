using System;

namespace AglCodingTest.Settings
{
    /// <summary>
    /// This represents the settings entity for storage account.
    /// </summary>
    public class StorageAccountSettings : ConnectionStringSettings
    {
        private const string AzureWebJobsStorageKey = "AzureWebJobsStorage";

        /// <inheritdoc />
        public override string ConnectionString => Environment.GetEnvironmentVariable(AzureWebJobsStorageKey);
    }
}