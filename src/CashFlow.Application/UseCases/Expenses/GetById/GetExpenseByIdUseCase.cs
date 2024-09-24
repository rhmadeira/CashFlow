using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expenses.GetById;
public class GetExpenseByIdUseCase : IGetExpenseByIdUseCase
{
    private readonly IExpensesReadOnlyRepository _expensesReadRepository;
    private readonly IMapper _mapper;

    public GetExpenseByIdUseCase(IExpensesReadOnlyRepository expensesRepository, IMapper mapper)
    {
        _expensesReadRepository = expensesRepository;
        _mapper = mapper;
    }

    public async Task<ResponseExpense> Execute(long id)
    {
        var result = await _expensesReadRepository.GetById(id);

        if (result is null)
            throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);

        var response = _mapper.Map<ResponseExpense>(result);

        return response;
    }
}
