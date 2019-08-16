using System.Threading.Tasks;

namespace LoginSample
{
    public interface IAuthenticationService
    {
        Task<bool> Authorize(string username, string password);
    }
}