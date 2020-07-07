using System;
using gtdpad.Domain;
using gtdpad.Support;

namespace gtdpad.Auth
{
    public class AuthenticateResponse
    {
        public AuthenticateResponse(User user, string token)
        {
            Guard.AgainstNull(user, nameof(user));

            ID = user.ID;
            Email = user.Email;
            Token = token;
        }

        public Guid ID { get; set; }

        public string Email { get; set; }

        public string Token { get; set; }
    }
}
