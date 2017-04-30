using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Tourney.Services.Participants.Maps;

namespace Tourney.Services.Participants.StartUpExtensions
{
    public static class MapperExtensions
    {
        public static IServiceCollection AddMaps(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<TournamentProfile>());

            IMapper mapper = config.CreateMapper();

            services.AddSingleton(mapper);

            return services;
        }
    }
}