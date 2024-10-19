using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses.Login;

namespace CashFlow.Application.UseCases.Login;

public interface ILoginUseCase
{
    Task<ResponseLogin> Execute(RequestLogin request);
}