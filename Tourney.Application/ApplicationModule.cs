using System;
using Autofac;
using Raven.Client;
using Raven.Client.Document;
using Tourney.Application.Services.Tournament;

namespace Tourney.Application
{
    public class ApplicationModule : Module
    {
        public RavenConfiguration RavenConfiguration { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            if (RavenConfiguration == null) throw new ArgumentNullException(nameof(RavenConfiguration));

            var documentStore = new DocumentStore()
            {
                Url = RavenConfiguration.Url,
                DefaultDatabase = RavenConfiguration.DatabaseName
            };
            documentStore.Initialize();
            builder.RegisterInstance(documentStore).As<IDocumentStore>();

            builder.RegisterType<TournamentService>().As<ITournamentService>();
        }
    }

    public class RavenConfiguration
    {
        public string Url { get; set; }
        public string DatabaseName { get; set; }
    }
}