using BaseApi.Data;
using BaseApi.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BaseApi.Repositories
{
    public class BrandsRepository : GenericRepository<int, Brand>, IBrandsRepository
    {
        public BrandsRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, ILogger<BrandsRepository> logger)
            : base(context, httpContextAccessor, logger)
        {
        }
    }
}
