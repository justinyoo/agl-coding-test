using System.Linq;
using System.Threading.Tasks;

using AglCodingTest.Extensions;
using AglCodingTest.Models;
using AglCodingTest.Services.ServiceOptions;

namespace AglCodingTest.Services
{
    /// <summary>
    /// This represents the service entity to load a payload for AGL coding test.
    /// </summary>
    public class AglPayloadProcessingService : IAglPayloadProcessingService
    {
        private bool _disposed;

        /// <inheritdoc />
        public Task InvokeAsync<TOptions>(TOptions options)
            where TOptions : ServiceOptionsBase
        {
            options.ThrowIfNullOrDefault();

            var serviceOptions = options as AglPayloadProcessingServiceOptions;
            serviceOptions.ThrowIfNullOrDefault();

            var groups = serviceOptions.People
                                       .Where(p => p.Pets.Any(q => q.PetType == serviceOptions.PetType))
                                       .Select(p => new
                                                        {
                                                            GenderType = p.GenderType,
                                                            Pets = p.Pets.Where(q => q.PetType == serviceOptions.PetType)
                                                        })
                                       .GroupBy(p => p.GenderType,
                                                p => p.Pets,
                                                (key, group) => new
                                                                    {
                                                                        GenderType = key,
                                                                        Pets = group.SelectMany(q => q)
                                                                    })
                                       .OrderBy(p => p.GenderType)
                                       .Select(p => new
                                                        {
                                                            Gender = p.GenderType,
                                                            Names = p.Pets
                                                                     .OrderBy(q => q.Name)
                                                                     .Select(q => q.Name)
                                                        })
                                       .Select(p => $"<h1>{p.Gender}</h1><ul>{string.Join(string.Empty, p.Names.Select(q => $"<li>{q}</li>"))}</ul>")
                                       .ToList();

            serviceOptions.Groups = groups;
            serviceOptions.IsInvoked = true;

            return Task.CompletedTask;
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
