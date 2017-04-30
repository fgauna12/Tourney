using System.Net.Http;
using System.Threading.Tasks;
using MediatR;
using Newtonsoft.Json;
using Tourney.Services.Participants.Client.Infrastructure;
using Tourney.Services.Participants.Client.Tournaments.Request;
using Tourney.Services.Participants.Client.Tournaments.Response;
using Tourney.Services.Participants.Contracts;
using Tourney.Services.Participants.Contracts.Tournaments;

namespace Tourney.Services.Participants.Client.Tournaments
{
    public class TournamentsHandler : IAsyncRequestHandler<GetTournamentsPaged, PagedResponse<Tournament>>
    {
        private readonly HttpClient _httpClient;
        public TournamentsHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        async Task<PagedResponse<Tournament>> IAsyncRequestHandler<GetTournamentsPaged, PagedResponse<Tournament>>.Handle(GetTournamentsPaged message)
        {
            var response = await _httpClient.GetAsync("/api/tournaments");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PagedResponse<Tournament>>(content);
        }

        async Task<UpdateTournamentResponse> UpdateTournamentAsync(Tournament tournament)
        {
            var response = await _httpClient.PostAsync("/api/tournaments", new JsonContent(tournament));
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UpdateTournamentResponse>(content);
        }
    }
}
