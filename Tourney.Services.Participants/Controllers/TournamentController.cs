using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Raven.Client;
using Raven.Client.Linq;
using Tourney.Services.Participants.Contracts;
using Tourney.Services.Participants.Core;
using TournamentDto = Tourney.Services.Participants.Contracts.Tournaments.Tournament;
using Bogus;
using System.Linq;

namespace Tourney.Services.Participants.Controllers
{
    [Route("/api/tournaments")]
    public class TournamentController : Controller
    {
        private readonly IDocumentStore _documentStore;
        private readonly IMapper _mapper;

        public TournamentController(IDocumentStore documentStore, IMapper mapper)
        {
            _documentStore = documentStore;
            _mapper = mapper;
        }

        [Route("")]
        [HttpGet]
        public async Task<PagedResponse<TournamentDto>> GetTournamentsAsync(PagedRequest pagedRequest)
        {
            //using (var session = _documentStore.OpenAsyncSession())
            //{
            //    RavenQueryStatistics stats;
            //    var list = await session
            //        .Query<Tournament>()
            //        .Statistics(out stats)
            //        .Skip(pagedRequest.PageNumber * pagedRequest.PageSize)
            //        .Take(pagedRequest.PageSize)
            //        .ToListAsync();

            //    var results = _mapper.Map<List<TournamentDto>>(list);
            //    return new PagedResponse<TournamentDto>()
            //    {
            //        Results = results,
            //        Total = stats.TotalResults - stats.SkippedResults
            //    };
            //}

            var faker = new Faker<TournamentDto>()
                .RuleFor(x => x.Name, opt => opt.Company.CompanyName())
                .RuleFor(x => x.Description, opt => opt.Company.CatchPhrase())
                .RuleFor(x => x.Start, opt => opt.Date.Future())
                .RuleFor(x => x.End, (opt, t) => t.Start.AddDays(1))
                .RuleFor(x => x.Id, opt => opt.UniqueIndex.ToString());

            var results = faker.Generate(50).ToList();

            return new PagedResponse<TournamentDto>()
            {
                Results = results,
                Total = results.Count
            };
        }

        [Route("")]
        [HttpPost]
        public async Task<IActionResult> CreateTournamentAsync([FromBody]Tournament tournament)
        {
            using (var session = _documentStore.OpenAsyncSession())
            {
                await session.StoreAsync(tournament);
                await session.SaveChangesAsync();
            }
            return Ok(tournament);
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdateTournamentAsync([FromRoute] string id, [FromBody]Tournament tournament)
        {
            if (id != tournament.Id)
                return BadRequest("Invalid Tournament ID");
            
            using (var session = _documentStore.OpenAsyncSession())
            {
                await session.StoreAsync(tournament);
                await session.SaveChangesAsync();
            }
            return Ok(tournament);
        }
    }
}