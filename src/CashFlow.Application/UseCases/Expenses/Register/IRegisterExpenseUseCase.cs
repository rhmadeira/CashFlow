using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses.Expense;

namespace CashFlow.Application.UseCases.Expenses.Register;
public interface IRegisterExpenseUseCase
{
    Task<ResponseRegisteredExpense> Execute(RequestRegisterExpense request);
}
