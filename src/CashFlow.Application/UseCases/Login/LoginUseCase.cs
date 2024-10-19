using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses.Login;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Login
{
    public class LoginUseCase : ILoginUseCase
    {
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IPasswordEncripter _passwordEncripter;
        private readonly IAccessTokenGenerator _accessTokenGenerator;

        public LoginUseCase(
            IUserReadOnlyRepository userReadOnlyRepository, 
            IPasswordEncripter passwordEncripter, 
            IAccessTokenGenerator accessTokenGenerator)
        {
            _userReadOnlyRepository = userReadOnlyRepository;
            _passwordEncripter = passwordEncripter;
            _accessTokenGenerator = accessTokenGenerator;
        }

        public async Task<ResponseLogin> Execute(RequestLogin request)
        {
           var user = await _userReadOnlyRepository.GetUserByEmail(request.Email);

            if (user is null)
            {
                throw new InvalidLoginException();
            }

           var passwordMatch = _passwordEncripter.Verify(request.Password, user.Password);
            if(passwordMatch == false)
            {
                throw new InvalidLoginException();
            }

            {
                return new ResponseLogin
                {
                    Name = user.Name,
                    Token = _accessTokenGenerator.Generate(user)
                };
            }
        }
    }
}
