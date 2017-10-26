namespace AglCodingTest.Settings
{
    /// <summary>
    /// This represents the base settings entity for connection strings.
    /// </summary>
    public abstract class ConnectionStringSettings
    {
        /// <summary>
        /// Gets the connection string value.
        /// </summary>
        public virtual string ConnectionString => string.Empty;
    }
}
