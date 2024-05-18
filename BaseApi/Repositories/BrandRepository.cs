using BaseApi.Data;
using BaseApi.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BaseApi.Repositories
{
    public class BrandRepository : GenericRepository<int, Brand>, IBrandRepository
    {
        public BrandRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, ILogger<BrandRepository> logger)
            : base(context, httpContextAccessor, logger)
        {
        }
    }
}
