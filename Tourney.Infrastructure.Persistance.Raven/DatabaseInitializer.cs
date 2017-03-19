using System;
using Raven.Client.Document;
using Raven.Client.Indexes;
using Raven.Abstractions.Extensions;

namespace Tourney.Infrastructure.Persistance.Raven
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
