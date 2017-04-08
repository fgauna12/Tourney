using MediatR;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tourney.Infrastructure.Dtos;
using Tourney.Services.Tournaments.Contracts;

namespace Tourney.Services.Tournaments.Client
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
    }
}
