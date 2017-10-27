using System;

using AglCodingTest.Functions;

using Microsoft.Azure.WebJobs.Host;

namespace AglCodingTest.Dependencies
{
    /// <summary>
    /// This provides interfaces to <see cref="FunctionFactory"/> class.
    /// </summary>
    public interface IFunctionFactory : IDisposable
    {
        /// <summary>
        /// Creates a function.
        /// </summary>
        /// <typeparam name="TFunction">The type of the function.</typeparam>
        /// <param name="log">A <see cref="TraceWriter"/> instance for tracing.</param>
        /// <returns>The function.</returns>
        TFunction Create<TFunction>(TraceWriter log)
            where TFunction : IFunction;
    }
}