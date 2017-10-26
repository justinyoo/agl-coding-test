using System.Net.Http.Formatting;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace AglCodingTest.Settings
{
    /// <summary>
    /// This represents the app settings entity.
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppSettings"/> class.
        /// </summary>
        public AppSettings()
        {
            var serialiserSettings = new JsonSerializerSettings()
                                         {
                                             ContractResolver = new CamelCasePropertyNamesContractResolver(),
                                             Converters = { new StringEnumConverter() },
                                             Formatting = Formatting.Indented,
                                             NullValueHandling = NullValueHandling.Ignore,
                                             MissingMemberHandling = MissingMemberHandling.Ignore
                                         };
            this.SerialiserSettings = serialiserSettings;
            this.Formatter = new JsonMediaTypeFormatter() { SerializerSettings = serialiserSettings };
        }

        /// <summary>
        /// Gets the <see cref="StorageAccountSettings"/> instance.
        /// </summary>
        public virtual StorageAccountSettings StorageAccount => new StorageAccountSettings();

        /// <summary>
        /// Gets the <see cref="AglSettings"/> instance.
        /// </summary>
        public virtual AglSettings Agl => new AglSettings();

        /// <summary>
        /// Gets the <see cref="JsonSerializerSettings"/> instance.
        /// </summary>
        public virtual JsonSerializerSettings SerialiserSettings { get; }

        /// <summary>
        /// Gets the <see cref="MediaTypeFormatter"/> instance.
        /// </summary>
        public virtual MediaTypeFormatter Formatter { get; }
    }
}