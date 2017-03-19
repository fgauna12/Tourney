using System.Linq;
using System.Threading.Tasks;
using Raven.Client;
using Raven.Client.Linq;
using Tourney.Infrastructure.Dtos;

namespace Tourney.Infrastructure.Persistance.Raven
{
    public static class RavenQueryableExtensions
    {
        public static async Task<PagedResponse<T>> ToPagedResponseAsync<T>(this IRavenQueryable<T> queryable, PagedRequest request)
        {
            RavenQueryStatistics stats;
            var list = await queryable.Statistics(out stats).Skip(request.PageNumber * request.PageSize).Take(request.PageSize).ToListAsync();
            return new PagedResponse<T>()
            {
                Results = list,
                Total = stats.TotalResults - stats.SkippedResults
            };
        }
    }
}