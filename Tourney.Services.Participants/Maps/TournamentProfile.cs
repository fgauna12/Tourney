using AutoMapper;
using Tourney.Services.Participants.Core;

namespace Tourney.Services.Participants.Maps
{
    public class TournamentProfile : Profile
    {
        public TournamentProfile()
        {
            CreateMap<Tournament, Contracts.Tournaments.Tournament>()
                .ReverseMap();
        } 
    }
}
