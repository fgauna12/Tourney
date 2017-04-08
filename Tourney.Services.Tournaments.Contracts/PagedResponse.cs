using System.Collections.Generic;

namespace Tourney.Infrastructure.Dtos
{
    public class PagedResponse<T>
    {
        public int Total { get; set; }
        public IList<T> Results { get; set; }
    }
}