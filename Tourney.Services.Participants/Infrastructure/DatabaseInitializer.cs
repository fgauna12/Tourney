using Raven.Abstractions.Extensions;
using Raven.Client.Document;
using Raven.Client.Indexes;

namespace Tourney.Services.Participants.Infrastructure
{
    public static class DatabaseInitializer
    {
        public static void Initialize(this DocumentStore documentStore)
        {
            documentStore.Initialize();

            //Create/update update indexes upon start
            IndexCreation.CreateIndexes(typeof(DatabaseInitializer).Assembly(), documentStore: documentStore);
        }
    }
}
