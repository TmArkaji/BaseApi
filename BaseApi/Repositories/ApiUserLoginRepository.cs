using System.Threading.Tasks;
using BaseApi.Data;
using BaseApi.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BaseApi.Repositories
{
    public class ApiUserLoginRepository : IApiUserLoginRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ApiUserLoginRepository> _logger;

        public ApiUserLoginRepository(ApplicationDbContext context, ILogger<ApiUserLoginRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task LogUserLoginAsync(ApiUserLogin userLogin)
        {
            await _context.ApiUserLogins.AddAsync(userLogin);
            await _context.SaveChangesAsync();
        }
    }
}
