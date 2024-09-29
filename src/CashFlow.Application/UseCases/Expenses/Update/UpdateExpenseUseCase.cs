
using AutoMapper;
using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Requests;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expenses.Update;
public class UpdateExpenseUseCase : IUpdateExpenseUseCase
{
    private readonly IExpensesWriteOnlyRepository _expensesWriteOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateExpenseUseCase(IExpensesWriteOnlyRepository expensesWriteOnlyRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _expensesWriteOnlyRepository = expensesWriteOnlyRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Execute(long id, RequestUpdateExpenses request)
    {
        Validade(request);

        var expense = await _expensesWriteOnlyRepository.GetById(id);

        if(expense is null)
        {
            throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);
        }

        _mapper.Map(request, expense);

        _expensesWriteOnlyRepository.Update(expense);

        await _unitOfWork.Commit();

    }

    public void Validade(RequestUpdateExpenses request)
    {
        var validator = new UpdateExpenseValidator();

        var result = validator.Validate(request);

        if(!result.IsValid)
        {
            var errosMessages = result.Errors.Select(x => x.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errosMessages);
        }    
    }
}
