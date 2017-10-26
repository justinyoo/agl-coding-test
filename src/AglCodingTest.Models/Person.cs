using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AglCodingTest.Models
{
    /// <summary>
    /// This represents the model entity for person.
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Person"/> class.
        /// </summary>
        public Person()
        {
            this.Pets = new List<Pet>();
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Models.GenderType"/> value.
        /// </summary>
        [EnumDataType(typeof(GenderType))]
        public GenderType GenderType { get; set; }

        /// <summary>
        /// Gets or sets the age.
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Gets or sets the list of <see cref="Pet"/> objects.
        /// </summary>
        public List<Pet> Pets { get; set; }
    }
}
