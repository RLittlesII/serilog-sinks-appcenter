using System;
using System.Threading.Tasks;

namespace LoginSample
{
    public class AuthenticationService : IAuthenticationService
    {
        public async Task<bool> Authorize(string username, string password)
        {
            throw new Exception("Some Custom exception");

            if (!(username?.ToLower() == "admin" && password?.ToLower() == "admin"))
            {
                throw new NotAuthorizedException();
            }

            return true;
        }
    }
}
