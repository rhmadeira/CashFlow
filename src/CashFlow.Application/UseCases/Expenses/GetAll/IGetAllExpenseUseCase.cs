using CashFlow.Communication.Responses.Expense;

namespace CashFlow.Application.UseCases.Expenses.GetAll;
public interface IGetAllExpenseUseCase
{
    Task<ResponseExpenses> Execute();
}
