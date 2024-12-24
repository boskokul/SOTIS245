using FluentResults;
using SOTISProj.DTO;
using SOTISProj.Repo;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace SOTISProj.Util
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly string _key = Environment.GetEnvironmentVariable("JWT_KEY") ?? "YourSuperSecureKeyWithAtLeast32Characters";
        private readonly string _issuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? "explorer";
        private readonly string _audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? "explorer-front.com";

        public Result<AuthenticationTokensDTO> GenerateAccessToken(User user)
        {
            var authenticationResponse = new AuthenticationTokensDTO();

            var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new("id", user.Id.ToString()),
            new("username", user.Username),
            new(ClaimTypes.Role, user.GetPrimaryRoleName())
        };

            var jwt = CreateToken(claims, 60 * 24 * 100);
            authenticationResponse.Id = user.Id;
            authenticationResponse.AccessToken = jwt;

            return authenticationResponse;
        }

        private string CreateToken(IEnumerable<Claim> claims, double expirationTimeInMinutes)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _issuer,
                _audience,
                claims,
                expires: DateTime.Now.AddMinutes(expirationTimeInMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
