namespace AglCodingTest.Services.ServiceOptions
{
    /// <summary>
    /// This represents the base options entity for service.
    /// </summary>
    public abstract class ServiceOptionsBase
    {
        /// <summary>
        /// Gets or sets a value indicating whether the service has been invoked or not.
        /// </summary>
        public bool IsInvoked { get; set; }
    }
}
