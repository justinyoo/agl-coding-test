using System;
using System.Linq;
using System.Threading.Tasks;

using AglCodingTest.Models;
using AglCodingTest.Services.ServiceOptions;
using AglCodingTest.Services.Tests.Fixtures;

using FluentAssertions;

using Ploeh.AutoFixture.Xunit2;

using Xunit;

namespace AglCodingTest.Services.Tests
{
    /// <summary>
    /// This represents the test entity for <see cref="AglPayloadProcessingService"/>.
    /// </summary>
    public class AglPayloadProcessingServiceTests : IClassFixture<ServiceFixture>
    {
        private readonly ServiceFixture _fixture;

        /// <summary>
        /// Initializes a new instance of the <see cref="AglPayloadProcessingServiceTests"/> class.
        /// </summary>
        /// <param name="fixture"><see cref="ServiceFixture"/> instance.</param>
        public AglPayloadProcessingServiceTests(ServiceFixture fixture)
        {
            this._fixture = fixture;
        }

        /// <summary>
        /// Tests whether the method should throw an exception or not.
        /// </summary>
        [Fact]
        public void Given_NullServiceOptions_InvokeAsync_ShouldThrow_Exception()
        {
            var service = new AglPayloadProcessingService();

            Func<Task> func = async () => await service.InvokeAsync<ServiceOptionsBase>(null).ConfigureAwait(false);
            func.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        /// Tests whether the method should throw an exception or not.
        /// </summary>
        [Fact]
        public void Given_InvalidServiceOptions_InvokeAsync_ShouldThrow_Exception()
        {
            var service = new AglPayloadProcessingService();

            var options = new FooServiceOptions();

            Func<Task> func = async () => await service.InvokeAsync(options).ConfigureAwait(false);
            func.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        /// Tests whether the method should return result or not.
        /// </summary>
        /// <param name="petType"><see cref="PetType"/> value.</param>
        [Theory]
        [AutoData]
        public async void Given_Payload_InvokeAsync_ShouldReturn_Result(PetType petType)
        {
            var service = new AglPayloadProcessingService();

            var people = this._fixture.ArrangePeople();
            var count = people.Count(p => p.Pets.Any(q => q.PetType == petType));

            var options = new AglPayloadProcessingServiceOptions() { People = people, PetType = petType };

            await service.InvokeAsync(options).ConfigureAwait(false);

            options.Groups.Should().HaveCount(count);
            options.IsInvoked.Should().BeTrue();
        }
    }
}