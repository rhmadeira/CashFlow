using AutoMapper;
using CashFlow.Communication.Responses.Expense;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Application.UseCases.Expenses.GetAll;

public class GetAllExpenseUseCase : IGetAllExpenseUseCase
{
    private readonly IExpensesReadOnlyRepository _expensesReadRepository;
    private readonly IMapper _mapper;

    public GetAllExpenseUseCase(IExpensesReadOnlyRepository expensesRepository, IMapper mapper)
    {
        _expensesReadRepository = expensesRepository;
        _mapper = mapper;
    }

    public async Task<ResponseExpenses> Execute()
    {
        var result = await _expensesReadRepository.GetAll();

        return new ResponseExpenses
        {
            Expenses = _mapper.Map<List<ResponseShortExpense>>(result)
        };

    }
}
