using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tourney.Application.Services.Tournament;
using Tourney.Infrastructure.Dtos;

namespace Tourney.Web.Controllers
{
    [Route("api/tournaments")]
    public class TournamentController : Controller
    {
        private readonly ITournamentService _tournamentsService;

        public TournamentController(ITournamentService tournamentsService)
        {
            _tournamentsService = tournamentsService;
        }

        [Route("")]
        [HttpGet]
        public async Task<IActionResult> GetTournaments(PagedRequest pagedRequest)
        {
            var response = await _tournamentsService.GetTournamentsAsync(pagedRequest);
            return Ok(response);
        }
    }
}