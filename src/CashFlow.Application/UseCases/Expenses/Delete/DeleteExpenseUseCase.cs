
using AutoMapper;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Application.UseCases.Expenses.Delete;
public class DeleteExpenseUseCase : IDeleteExpenseUseCase
{
    private readonly IExpensesWriteOnlyRepository _expensesWriteOnlyRepository;
    private readonly IExpensesReadOnlyRepository _expensesReadOnlyRepository;
    private readonly IMapper _mapper;

    public DeleteExpenseUseCase(IExpensesWriteOnlyRepository expensesWriteOnlyRepository, IMapper mapper, IExpensesReadOnlyRepository expensesReadOnlyRepository)
    {
        _expensesWriteOnlyRepository = expensesWriteOnlyRepository;
        _mapper = mapper;
        _expensesReadOnlyRepository = expensesReadOnlyRepository;
    }

    public async Task Execute(long id)
    {
        var expense = await _expensesReadOnlyRepository.GetById(id);
    }
}
