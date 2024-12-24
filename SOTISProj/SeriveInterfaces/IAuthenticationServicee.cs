using SOTISProj.DTO;
using FluentResults;

namespace SOTISProj.SeriveInterfaces
{
    public interface IAuthenticationServicee
    {
        Result<AuthenticationTokensDTO> Login(CredentialsDTO credentials);
    }
}
