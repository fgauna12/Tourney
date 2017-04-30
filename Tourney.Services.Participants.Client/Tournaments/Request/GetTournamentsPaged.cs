using MediatR;
using Tourney.Services.Participants.Contracts;
using Tourney.Services.Participants.Contracts.Tournaments;

namespace Tourney.Services.Participants.Client.Tournaments.Request
{
    public class GetTournamentsPaged : IRequest<PagedResponse<Tournament>>
    {
    }
}
