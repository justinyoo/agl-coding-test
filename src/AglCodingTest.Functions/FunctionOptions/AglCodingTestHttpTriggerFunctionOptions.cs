using AglCodingTest.Models;

namespace AglCodingTest.Functions.FunctionOptions
{
    /// <summary>
    /// This represents the options entity for <see cref="AglCodingTestHttpTriggerFunction"/>.
    /// </summary>
    public class AglCodingTestHttpTriggerFunctionOptions : FunctionOptionsBase
    {
        /// <summary>
        /// Gets or sets the <see cref="Models.PetType"/> value.
        /// </summary>
        public PetType PetType { get; set; }
    }
}