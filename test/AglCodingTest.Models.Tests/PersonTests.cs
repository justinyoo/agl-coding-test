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
    public class PersonTests : IClassFixture<ModelFixture>
    {
        private readonly IFixture _fixture;
        private readonly JsonSerializerSettings _settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonTests"/> class.
        /// </summary>
        /// <param name="fixture"><see cref="ModelFixture"/> instance.</param>
        public PersonTests(ModelFixture fixture)
        {
            this._fixture = fixture.Fixture;
            this._settings = fixture.JsonSerializerSettings;
        }

        /// <summary>
        /// Tests whether the enum value is serialised in a string format or not.
        /// </summary>
        /// <param name="genderType"><see cref="GenderType"/> value.</param>
        [Theory]
        [AutoData]
        public void Given_Instance_EnumValue_ShouldBe_Serialised_AsString(GenderType genderType)
        {
            var pet = this._fixture.Build<Person>()
                                   .With(p => p.GenderType, genderType)
                                   .Without(p => p.Pets)
                                   .Create();

            var serialised = JsonConvert.SerializeObject(pet, this._settings);

            serialised.Should().ContainEquivalentOf(genderType.ToString());
            serialised.Should().NotContainEquivalentOf("genderType");
            serialised.Should().ContainEquivalentOf("\"gender\":");
            serialised.Should().ContainEquivalentOf("\"pets\": []");
        }
    }
}
