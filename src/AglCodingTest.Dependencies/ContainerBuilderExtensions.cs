using AglCodingTest.Extensions;

using Autofac;
using Autofac.Core;

namespace AglCodingTest.Dependencies
{
    /// <summary>
    /// This represents the extensions entity for <see cref="ContainerBuilder"/>.
    /// </summary>
    public static class ContainerBuilderExtensions
    {
        /// <summary>
        /// Registers a module to the container.
        /// </summary>
        /// <typeparam name="TModule">Type of module.</typeparam>
        /// <param name="builder"><see cref="ContainerBuilder"/> instance.</param>
        /// <param name="handler"><see cref="RegistrationHandler"/> instance.</param>
        /// <returns>Returns <see cref="ContainerBuilder"/> instance.</returns>
        public static ContainerBuilder RegisterModule<TModule>(this ContainerBuilder builder, RegistrationHandler handler)
            where TModule : IModule, new()
        {
            builder.RegisterModule<TModule>();

            if (handler.IsNullOrDefault())
            {
                return builder;
            }

            if (handler.RegisterTypeAsInstancePerDependency.IsNullOrDefault())
            {
                return builder;
            }

            handler.RegisterTypeAsInstancePerDependency.Invoke(builder);

            return builder;
        }
    }
}
