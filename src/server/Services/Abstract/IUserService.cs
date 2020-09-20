using System;
using System.Threading.Tasks;
using gtdpad.Auth;

namespace gtdpad.Services
{
    public interface IUserService
    {
        Task<(bool valid, AuthenticateResponse response)> Authenticate(
            string email,
            string password,
            Func<DateTime> getExpires);
    }
}
