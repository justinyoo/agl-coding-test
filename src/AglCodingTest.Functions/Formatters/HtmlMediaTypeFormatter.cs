using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace AglCodingTest.Functions.Formatters
{
    /// <summary>
    /// This represents the media type formatter entity for HTML.
    /// </summary>
    public class HtmlMediaTypeFormatter : MediaTypeFormatter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlMediaTypeFormatter"/> class.
        /// </summary>
        public HtmlMediaTypeFormatter()
        {
            this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/plain"));
        }

        /// <inheritdoc />
        public override bool CanReadType(Type type)
        {
            return false;
        }

        /// <inheritdoc />
        public override bool CanWriteType(Type type)
        {
            return typeof(string) == type;
        }

        /// <inheritdoc />
        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext, CancellationToken cancellationToken)
        {
            using (var writer = new StreamWriter(writeStream))
            {
                writer.WriteLine(value.ToString());
            }

            var tcs = new TaskCompletionSource<Stream>();
            tcs.SetResult(writeStream);

            return tcs.Task;
        }
    }
}
