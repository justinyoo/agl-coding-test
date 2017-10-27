using System;
using System.Threading.Tasks;

using AglCodingTest.Functions.FunctionOptions;

using Microsoft.Azure.WebJobs.Host;

namespace AglCodingTest.Functions
{
    /// <summary>
    /// This represents the base entity for function classes.
    /// </summary>
    public abstract class FunctionBase : IFunction
    {
        private bool _disposed;

        /// <inheritdoc />
        public TraceWriter Log { get; set; }

        /// <inheritdoc />
        public virtual Task<object> InvokeAsync<TInput, TOptions>(TInput input, TOptions options = default(TOptions))
            where TOptions : FunctionOptionsBase
        {
            return Task.FromResult(default(object));
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">Value indicating whether to dispose managed resources or not.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (this._disposed)
            {
                return;
            }

            if (disposing)
            {
                this.ReleaseManagedResources();
            }

            this.ReleaseUnmanagedResources();

            this._disposed = true;
        }

        /// <summary>
        /// Releases managed resources during the disposing event.
        /// </summary>
        protected virtual void ReleaseManagedResources()
        {
            // Release managed resources here.
        }

        /// <summary>
        /// Releases unmanaged resources during the disposing event.
        /// </summary>
        protected virtual void ReleaseUnmanagedResources()
        {
            // Release unmanaged resources here.
        }
    }
}