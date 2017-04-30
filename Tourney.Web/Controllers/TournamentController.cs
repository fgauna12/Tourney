using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Tourney.Services.Participants.Client.Tournaments.Request;

namespace Tourney.Web.Controllers
{
    [Route("api/tournaments")]
    public class TournamentController : Controller
    {
        private readonly IMediator _mediator;

        public TournamentController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [Route("")]
        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetTournaments(GetTournamentsPaged tournaments)
        {
            var response = await _mediator.Send(tournaments);
            return Ok(response);
        }
    }
}