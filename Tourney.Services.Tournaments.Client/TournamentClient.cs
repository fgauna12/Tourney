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

        async Task<UpdateTournamentResponse> UpdateTournamentAsync(Tournament tournament)
        {
            var response = await _httpClient.PostAsync("/api/tournaments", new JsonContent(tournament));
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UpdateTournamentResponse>(content);
        }
    }

    public class JsonContent : StringContent
    {
        public JsonContent(object content) : this(JsonConvert.SerializeObject(content))
        {}

        public JsonContent(string content) : base(content)
        {
            Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
        }
    }

    public class UpdateTournament : IRequest<Tournament>
    {

    }

    public class UpdateTournamentResponse
    {

    }
}
