using CashFlow.Communication.Responses.Expense;

namespace CashFlow.Application.UseCases.Expenses.GetById;
public interface IGetExpenseByIdUseCase
{
    Task<ResponseExpense> Execute(long id);
}
