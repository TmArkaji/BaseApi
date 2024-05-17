using BaseApi.Data;

namespace BaseApi.Interfaces
{
    public interface IApiUserRepository
    {
        Task<ApiUser> ValidateUserAsync(string userName, string passwordHash, string apiKey, string countryA2);
    }
}
