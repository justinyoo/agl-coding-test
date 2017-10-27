using System;

using AglCodingTest.Functions;

using Autofac;

using Microsoft.Azure.WebJobs.Host;

using Moq;

using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace AglCodingTest.Dependencies.Tests.Fixtures
{
    /// <summary>
    /// This represents the fixture entity for function app classes.
    /// </summary>
    public class DependencyFixture : IDisposable
    {
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyFixture"/> class.
        /// </summary>
        public DependencyFixture()
        {
            this.Fixture = new Fixture().Customize(new AutoMoqCustomization());
        }

        /// <summary>
        /// Gets the <see cref="IFixture"/> instance.
        /// </summary>
        public IFixture Fixture { get; }

        /// <summary>
        /// Arranges the <see cref="IContainer"/> instance.
        /// </summary>
        /// <param name="action"><see cref="Action{ContainerBuilder}"/> instance.</param>
        /// <returns>Returns the <see cref="IContainer"/> instance.</returns>
        public IContainer ArrangeContainer(Action<ContainerBuilder> action)
        {
            var builder = new ContainerBuilder();

            action.Invoke(builder);

            var container = builder.Build();

            return container;
        }

        /// <summary>
        /// Arranges the <see cref="RegistrationHandler"/> instance.
        /// </summary>
        /// <returns>Returns the <see cref="RegistrationHandler"/> instance.</returns>
        public RegistrationHandler ArrangeRegistrationHandler()
        {
            var handler = new RegistrationHandler()
                              {
                                  RegisterTypeAsInstancePerDependency = p => p.RegisterType<FooFunction>()
                                                                              .As<IFooFunction>()
                                                                              .InstancePerDependency()
                              };

            return handler;
        }

        /// <summary>
        /// Arranges the <see cref="Mock{TraceWriter}"/> instance.
        /// </summary>
        /// <returns>Returns the <see cref="Mock{TraceWriter}"/> instance.</returns>
        public TraceWriter ArrangeLog()
        {
            var log = this.Fixture.Create<TraceWriter>();

            return log;
        }

        /// <summary>
        /// Arranges the <see cref="Mock{IFunctionFactory}"/> instance.
        /// </summary>
        /// <typeparam name="TFunction">Type of function.</typeparam>
        /// <param name="function"><see cref="Mock{TFunction}"/> instance.</param>
        /// <returns>Returns the <see cref="Mock{IFunctionFactory}"/> instance.</returns>
        public Mock<IFunctionFactory> ArrangeFunctionFactory<TFunction>(out Mock<TFunction> function)
            where TFunction : class, IFunction
        {
            function = new Mock<TFunction>();

            var factory = new Mock<IFunctionFactory>();
            factory.Setup(p => p.Create<TFunction>(It.IsAny<TraceWriter>())).Returns(function.Object);

            return factory;
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
