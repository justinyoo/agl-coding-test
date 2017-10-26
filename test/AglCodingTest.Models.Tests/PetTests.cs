using AglCodingTest.Models.Tests.Fixtures;

using FluentAssertions;

using Newtonsoft.Json;

using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit2;

using Xunit;

namespace AglCodingTest.Models.Tests
{
    /// <summary>
    /// This represents the test entity for <see cref="Pet"/>.
    /// </summary>
    public class PetTests : IClassFixture<ModelFixture>
    {
        private readonly IFixture _fixture;
        private readonly JsonSerializerSettings _settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="PetTests"/> class.
        /// </summary>
        /// <param name="fixture"><see cref="ModelFixture"/> instance.</param>
        public PetTests(ModelFixture fixture)
        {
            this._fixture = fixture.Fixture;
            this._settings = fixture.JsonSerializerSettings;
        }

        /// <summary>
        /// Tests whether the enum value is serialised in a string format or not.
        /// </summary>
        /// <param name="petType"><see cref="PetType"/> value.</param>
        [Theory]
        [AutoData]
        public void Given_Instance_EnumValue_ShouldBe_Serialised_AsString(PetType petType)
        {
            var pet = this._fixture.Build<Pet>()
                                   .With(p => p.PetType, petType)
                                   .Create();


            var serialised = JsonConvert.SerializeObject(pet, this._settings);

            serialised.Should().ContainEquivalentOf(petType.ToString());
        }
    }
}
