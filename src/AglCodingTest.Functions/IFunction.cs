using System;
using System.Threading.Tasks;

using AglCodingTest.Functions.FunctionOptions;

using Microsoft.Azure.WebJobs.Host;

namespace AglCodingTest.Functions
{
    /// <summary>
    /// This provides interfaces to all classes in the function layer.
    /// </summary>
    public interface IFunction : IDisposable
    {
        /// <summary>
        /// Gets or sets the <see cref="TraceWriter"/> instance.
        /// </summary>
        TraceWriter Log { get; set; }

        /// <summary>
        /// Invokes the function.
        /// </summary>
        /// <typeparam name="TInput">Type of input instance.</typeparam>
        /// <typeparam name="TOptions">Type of function options instance.</typeparam>
        /// <param name="input"><see cref="TIn"/> instance.</param>
        /// <param name="options"><see cref="TOptions"/> instance.</param>
        /// <returns>Returns output instance.</returns>
        Task<object> InvokeAsync<TInput, TOptions>(TInput input, TOptions options = default(TOptions))
            where TOptions : FunctionOptionsBase;
    }
}