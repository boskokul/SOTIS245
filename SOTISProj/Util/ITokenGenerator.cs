using FluentResults;
using SOTISProj.DTO;
using SOTISProj.Repo;

namespace SOTISProj.Util
{
    public interface ITokenGenerator
    {
        Result<AuthenticationTokensDTO> GenerateAccessToken(User user);
    }
}
