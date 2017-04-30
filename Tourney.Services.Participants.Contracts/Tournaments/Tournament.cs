using System;

namespace Tourney.Services.Participants.Contracts.Tournaments
{
    public class Tournament
    {
        public string Id { get; set; }

        public DateTimeOffset Start { get; set; }
        public DateTimeOffset End { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
