using MediatR;
using Tourney.Infrastructure.Dtos;
using Tourney.Services.Tournaments.Contracts;

namespace Tourney.Services.Tournaments.Client
{
    public class GetTournamentsPaged : IRequest<PagedResponse<Tournament>>
    {
    }
}
