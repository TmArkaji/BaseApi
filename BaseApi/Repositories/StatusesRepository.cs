using BaseApi.Data;
using BaseApi.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BaseApi.Repositories
{
    public class StatusesRepository : GenericRepository<int, Status>, IStatusesRepository
    {
        public StatusesRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, ILogger<StatusesRepository> logger)
            : base(context, httpContextAccessor, logger)
        {
        }
    }
}
