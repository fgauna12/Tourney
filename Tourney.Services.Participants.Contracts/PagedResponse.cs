using System.Collections.Generic;

namespace Tourney.Services.Participants.Contracts
{
    public class PagedResponse<T>
    {
        public int Total { get; set; }
        public IList<T> Results { get; set; }
    }
}