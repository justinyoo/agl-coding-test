using System.ComponentModel.DataAnnotations;

namespace AglCodingTest.Models
{
    /// <summary>
    /// This represents the model entity for pet.
    /// </summary>
    public class Pet
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Models.PetType"/> value.
        /// </summary>
        [EnumDataType(typeof(PetType))]
        public PetType PetType { get; set; }
    }
}