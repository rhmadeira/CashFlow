using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses.Expense;
using CashFlow.Domain.Entities;

namespace CashFlow.Application.AutoMapper;
public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
    }

    private void RequestToEntity()
    {
        CreateMap<RequestRegisterExpense, Expense>();
        CreateMap<RequestUpdateExpenses, Expense>();
        CreateMap<RequestRegisterUser, User>();
    }

    private void EntityToResponse()
    {
        CreateMap<Expense, ResponseRegisteredExpense>();
        CreateMap<Expense, ResponseShortExpense>();
        CreateMap<Expense, ResponseExpense>();
    }

}
