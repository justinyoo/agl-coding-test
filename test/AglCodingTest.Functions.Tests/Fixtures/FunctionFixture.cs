using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;

using AglCodingTest.Functions.FunctionOptions;
using AglCodingTest.Models;
using AglCodingTest.Services;

using Moq;

using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace AglCodingTest.Functions.Tests.Fixtures
{
    /// <summary>
    /// This represents the fixture entity to test services.
    /// </summary>
    public class FunctionFixture : IDisposable
    {
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="FunctionFixture"/> class.
        /// </summary>
        public FunctionFixture()
        {
            this.Fixture = new Fixture().Customize(new AutoMoqCustomization());
        }

        /// <summary>
        /// Gets the <see cref="IFixture"/> instance.
        /// </summary>
        public IFixture Fixture { get; }

        /// <summary>
        /// Arranges the <see cref="Mock{IAglPayloadLoadingService}"/> instance.
        /// </summary>
        /// <returns>Returns the <see cref="Mock{IAglPayloadLoadingService}"/> instance.</returns>
        public Mock<IAglPayloadLoadingService> ArrangeLoadingService()
        {
            var service = this.Fixture.Create<Mock<IAglPayloadLoadingService>>();

            return service;
        }

        /// <summary>
        /// Arranges the <see cref="Mock{IAglPayloadProcessingService}"/> instance.
        /// </summary>
        /// <returns>Returns the <see cref="Mock{IAglPayloadProcessingService}"/> instance.</returns>
        public Mock<IAglPayloadProcessingService> ArrangeProcessingService()
        {
            var service = this.Fixture.Create<Mock<IAglPayloadProcessingService>>();

            return service;
        }

        /// <summary>
        /// Arranges the <see cref="AglCodingTestHttpTriggerFunctionOptions"/> instance.
        /// </summary>
        /// <param name="petType"><see cref="PetType"/> value.</param>
        /// <returns>Returns the <see cref="AglCodingTestHttpTriggerFunctionOptions"/> instance.</returns>
        public AglCodingTestHttpTriggerFunctionOptions ArrangeAglCodingTestHttpTriggerFunctionOptions(PetType petType)
        {
            var options = this.Fixture
                              .Build<AglCodingTestHttpTriggerFunctionOptions>()
                              .With(p => p.PetType, petType)
                              .Create();

            return options;
        }

        /// <summary>
        /// Arranges the <see cref="HttpRequestMessage"/> instance.
        /// </summary>
        /// <returns>Returns the <see cref="HttpRequestMessage"/> instance.</returns>
        public HttpRequestMessage ArrangeHttpRequestMessage()
        {
            var config = new HttpConfiguration();

            var context = new HttpRequestContext() { Configuration = config };
            var request = new HttpRequestMessage()
                              {
                                  Properties =
                                      {
                                          { HttpPropertyKeys.RequestContextKey, context }
                                      }
                              };

            return request;
        }

        /// <summary>
        /// Arranges the list of <see cref="Person"/> entities.
        /// </summary>
        /// <returns>Returns the list of <see cref="Person"/> entities.</returns>
        public List<Person> ArrangePeople()
        {
            var people = this.Fixture.CreateMany<Person>().ToList();

            return people;
        }

        /// <summary>
        /// Arranges the list of groups in string.
        /// </summary>
        /// <returns>Returns the list of groups in string.</returns>
        public List<string> ArrangeGroups()
        {
            var groups = this.Fixture.CreateMany<string>().ToList();

            return groups;
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
