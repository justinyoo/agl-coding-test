using System.Collections.Generic;

using AglCodingTest.Models;

namespace AglCodingTest.Services.ServiceOptions
{
    /// <summary>
    /// This represents the options entity for <see cref="AglPayloadProcessingService"/>.
    /// </summary>
    public class AglPayloadProcessingServiceOptions : ServiceOptionsBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AglPayloadProcessingServiceOptions"/> class.
        /// </summary>
        public AglPayloadProcessingServiceOptions()
        {
            this.People = new List<Person>();
            this.Groups = new List<string>();
        }

        /// <summary>
        /// Gets or sets the list of <see cref="Person"/> entities.
        /// </summary>
        public List<Person> People { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Models.PetType"/> value.
        /// </summary>
        public PetType PetType { get; set; }

        /// <summary>
        /// Gets or sets the collection of group of pets.
        /// </summary>
        public List<string> Groups { get; set; }
    }
}