using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Raven.Client;
using Raven.Client.Document;

namespace Tourney.Services.Participants.StartUpExtensions
{
    public static class RavenExtensions
    {
        public static IServiceCollection AddRaven(this IServiceCollection services, IConfiguration configuration)
        {
            var ravenConfiguration = configuration.GetSection("ConnectionStrings:Raven").Get<RavenConfiguration>();

            var documentStore = new DocumentStore()
            {
                Url = ravenConfiguration.Url,
                DefaultDatabase = ravenConfiguration.DatabaseName
            };
            documentStore.Initialize();
            services.AddSingleton<IDocumentStore>(documentStore);
            return services;
        }
    }
}