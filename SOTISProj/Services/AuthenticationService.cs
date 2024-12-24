using FluentResults;
using SOTISProj.DTO;
using SOTISProj.RepositoryInterfaces;
using SOTISProj.SeriveInterfaces;
using SOTISProj.Util;


namespace SOTISProj.Services
{
    public class AuthenticationService : IAuthenticationServicee
    {
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IUserRepository userRepository,  ITokenGenerator tokenGenerator)
        {
            _tokenGenerator = tokenGenerator;
            _userRepository = userRepository;
           
        }

        public Result<AuthenticationTokensDTO> Login(CredentialsDTO credentials)
        {
            var user = _userRepository.GetActiveByName(credentials.Username);
            if (user == null || !String.Equals(credentials.Password,user.Password)) return Result.Fail("Not found");
            return _tokenGenerator.GenerateAccessToken(user);
        }

        
    }
}
