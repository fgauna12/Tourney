using System.Linq;
using System.Threading.Tasks;
using Raven.Client;
using Tourney.Infrastructure.Dtos;
using Tourney.Infrastructure.Persistance.Raven;

namespace Tourney.Application.Services.Tournament
{
    public interface ITournamentService
    {
        Task<PagedResponse<Domain.Tournament>> GetTournamentsAsync(PagedRequest pagedRequest);
    }

    public class TournamentService : ITournamentService
    {
        private readonly IDocumentStore _documentStore;

        public TournamentService(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        public async Task<PagedResponse<Domain.Tournament>> GetTournamentsAsync(PagedRequest pagedRequest)
        {
            using (var session = _documentStore.OpenAsyncSession())
            {
                return await session
                    .Query<Domain.Tournament>()
                    .ToPagedResponseAsync(pagedRequest);
            }
        }
    }
}