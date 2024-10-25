using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses.User;

namespace CashFlow.Application.UseCases.User.Register
{
    public interface IRegisterUserUseCase
    {
        Task<ResponseRegisterUser> Execute(RequestRegisterUser request);
    }
}