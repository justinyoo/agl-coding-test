namespace AglCodingTest.Functions.FunctionOptions
{
    /// <summary>
    /// This represents the base options entity for function.
    /// </summary>
    public abstract class FunctionOptionsBase
    {
        /// <summary>
        /// Gets or sets a value indicating whether the service has been invoked or not.
        /// </summary>
        public bool IsInvoked { get; set; }
    }
}
