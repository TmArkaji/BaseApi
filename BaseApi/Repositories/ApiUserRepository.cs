using System.Threading.Tasks;
using BaseApi.Data;
using BaseApi.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BaseApi.Repositories
{
    public class ApiUserRepository : IApiUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ApiUserRepository> _logger;

        public ApiUserRepository(ApplicationDbContext context, ILogger<ApiUserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ApiUser> ValidateUserAsync(string userName, string passwordHash, string apiKey, string countryA2)
        {
            return await _context.ApiUsers
                .FirstOrDefaultAsync(u =>
                    u.UserName == userName &&
                    u.PasswordHash == passwordHash &&
                    u.ApiKey == apiKey &&
                    u.CountryA2 == countryA2);
        }
    }
}
