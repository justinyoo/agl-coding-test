using System;

using AglCodingTest.Functions;

using Autofac;

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

    /// <summary>
    /// This represents the factory entity for functions.
    /// </summary>
    public class FunctionFactory : IFunctionFactory
    {
        private readonly IContainer _container;

        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="FunctionFactory"/> class.
        /// </summary>
        public FunctionFactory()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FunctionFactory"/> class.
        /// </summary>
        /// <param name="handler"><see cref="RegistrationHandler"/> instance.</param>
        public FunctionFactory(RegistrationHandler handler = null)
        {
            this._container = new ContainerBuilder()
                                  .RegisterModule<AppModule>(handler)
                                  .Build();
        }

        /// <inheritdoc />
        public TFunction Create<TFunction>(TraceWriter log)
            where TFunction : IFunction
        {
            var function = this._container.Resolve<TFunction>();
            function.Log = log;

            return function;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (this._disposed)
            {
                return;
            }

            this._disposed = true;
        }
    }
}
