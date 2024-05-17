using System.Threading.Tasks;
using BaseApi.Data;

namespace BaseApi.Interfaces
{
    public interface IApiUserLoginRepository
    {
        Task LogUserLoginAsync(ApiUserLogin userLogin);
    }
}
