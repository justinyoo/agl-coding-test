using System.Collections.Generic;

using AglCodingTest.Models;

namespace AglCodingTest.Services.ServiceOptions
{
    /// <summary>
    /// This represents the options entity for <see cref="AglPayloadLoadingService"/>.
    /// </summary>
    public class AglPayloadLoadingServiceOptions : ServiceOptionsBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AglPayloadLoadingServiceOptions"/> class.
        /// </summary>
        public AglPayloadLoadingServiceOptions()
        {
            this.People = new List<Person>();
        }

        /// <summary>
        /// Gets or sets the list of <see cref="Person"/> entities.
        /// </summary>
        public List<Person> People { get; set; }
    }
}