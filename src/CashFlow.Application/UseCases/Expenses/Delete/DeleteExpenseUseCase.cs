
using AutoMapper;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expenses.Delete;
public class DeleteExpenseUseCase : IDeleteExpenseUseCase
{
    private readonly IExpensesWriteOnlyRepository _expensesWriteOnlyRepository;
    private readonly IExpensesReadOnlyRepository _expensesReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DeleteExpenseUseCase(IExpensesWriteOnlyRepository expensesWriteOnlyRepository, IMapper mapper, IExpensesReadOnlyRepository expensesReadOnlyRepository,
        IUnitOfWork uni)
    {
        _expensesWriteOnlyRepository = expensesWriteOnlyRepository;
        _mapper = mapper;
        _expensesReadOnlyRepository = expensesReadOnlyRepository;
        _unitOfWork = uni;
    }

    public async Task Execute(long id)
    {

        var result = await _expensesWriteOnlyRepository.Delete(id);

        if (result is false)
        {
            throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);
        }

        await _unitOfWork.Commit();

    }
}
