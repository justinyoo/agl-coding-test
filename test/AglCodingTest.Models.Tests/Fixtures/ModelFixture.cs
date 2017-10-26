using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace AglCodingTest.Models.Tests.Fixtures
{
    /// <summary>
    /// This represents the fixture entity to test models.
    /// </summary>
    public class ModelFixture
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelFixture"/> class.
        /// </summary>
        public ModelFixture()
        {
            this.Fixture = new Fixture().Customize(new AutoMoqCustomization());
            this.JsonSerializerSettings = new JsonSerializerSettings
                                              {
                                                  ContractResolver = new CamelCasePropertyNamesContractResolver(),
                                                  Converters = { new StringEnumConverter() },
                                                  Formatting = Formatting.Indented,
                                                  NullValueHandling = NullValueHandling.Ignore,
                                                  MissingMemberHandling = MissingMemberHandling.Ignore
                                              };
        }

        /// <summary>
        /// Gets the <see cref="IFixture"/> instance.
        /// </summary>
        public IFixture Fixture { get; }

        /// <summary>
        /// Gets the <see cref="Newtonsoft.Json.JsonSerializerSettings"/> instance.
        /// </summary>
        public JsonSerializerSettings JsonSerializerSettings { get; }
    }
}
