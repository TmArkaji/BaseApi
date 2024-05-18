using BaseApi.Data;
using BaseApi.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BaseApi.Repositories
{
    public class StatusRepository : GenericRepository<int, Status>, IStatusRepository
    {
        public StatusRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, ILogger<StatusRepository> logger)
            : base(context, httpContextAccessor, logger)
        {
        }
    }
}
