using System;
using System.Threading.Tasks;

using AglCodingTest.Services.ServiceOptions;

namespace AglCodingTest.Functions
{
    /// <summary>
    /// This provides interfaces to all classes in the function layer.
    /// </summary>
    public interface IFunction : IDisposable
    {
        /// <summary>
        /// Invokes the function.
        /// </summary>
        /// <typeparam name="TIn">Type of input instance.</typeparam>
        /// <typeparam name="TOptions">Type of service options instance.</typeparam>
        /// <param name="input"><see cref="TIn"/> instance.</param>
        /// <param name="options"><see cref="TOptions"/> instance.</param>
        /// <returns>Returns <see cref="TOut"/> instance.</returns>
        Task<object> InvokeAsync<TIn, TOptions>(TIn input, TOptions options)
            where TOptions : ServiceOptionsBase;
    }
}