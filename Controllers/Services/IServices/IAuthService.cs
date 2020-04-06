using FubonMailApi.Resources.Request;
using FubonMailApi.Resources.Response;
using System.Threading.Tasks;

namespace FubonMailApi.Controllers.Services.IServices
{
    public interface IAuthService
    {
        Task<LoginOutputResource> LoginAsync(LoginResource resource);
    }
}