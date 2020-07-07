using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using gtdpad.Domain;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Options;
using gtdpad.Support;
using gtdpad.Models;
using gtdpad.Auth;

namespace gtdpad.Services
{
    public class UserService : IUserService
    {
        private readonly Settings _settings;
        private readonly IRepository _repository;

        public UserService(
            IOptionsMonitor<Settings> optionsMonitor,
            IRepository repository)
        {
            Guard.AgainstNull(optionsMonitor, nameof(optionsMonitor));

            _settings = optionsMonitor.CurrentValue;
            _repository = repository;
        }

        public async Task<(bool valid, AuthenticateResponse response)> Authenticate(string email, string password)
        {
            Guard.AgainstNull(email, nameof(email));
            Guard.AgainstNull(password, nameof(password));

            var user = await _repository
                .FindUserByEmail(email)
                .ConfigureAwait(false);

            if (user == null)
            {
                return (false, default);
            }

            var hasher = new PasswordHasher<User>();

            var result = hasher.VerifyHashedPassword(user, user.Password, password);

            if (result == PasswordVerificationResult.Success)
            {
                var token = GenerateJwt(user);

                var response = new AuthenticateResponse(user, token);

                return (true, response);
            }

            return (false, default);
        }

        private string GenerateJwt(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_settings.TokenSecret);

            var signingCredentials = new SigningCredentials(
                key: new SymmetricSecurityKey(key),
                algorithm: SecurityAlgorithms.HmacSha256Signature
            );

            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = GetClaimsIdentity(user.ID, user.Email),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = signingCredentials
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private static ClaimsIdentity GetClaimsIdentity(Guid id, string email)
        {
            var claims = new[] {
                new Claim(ClaimTypes.Sid, id.ToString()),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, "Member")
            };

            return new ClaimsIdentity(claims);
        }

        private static ClaimsPrincipal GetClaimsPrincipal(Guid id, string email)
        {
            var claims = new[] {
                new Claim(ClaimTypes.Sid, id.ToString()),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, "Member")
            };

            var identity = new ClaimsIdentity(
                claims: claims,
                authenticationType: CookieAuthenticationDefaults.AuthenticationScheme,
                nameType: ClaimTypes.Email,
                roleType: ClaimTypes.Role
            );

            return new ClaimsPrincipal(identity);
        }
    }
}
