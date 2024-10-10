using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses.Expense;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expenses.Register;
public class RegisterExpenseUseCase : IRegisterExpenseUseCase
{
    private readonly IExpensesWriteOnlyRepository _expensesWriteRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RegisterExpenseUseCase(IExpensesWriteOnlyRepository expensesRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _expensesWriteRepository = expensesRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseRegisteredExpense> Execute(RequestRegisterExpense request)
    {
        Validate(request);

        var entity = _mapper.Map<Expense>(request);

        await _expensesWriteRepository.Add(entity);
        await _unitOfWork.Commit();

        var response = _mapper.Map<ResponseRegisteredExpense>(entity);

        return response; 
    }
   
    private void Validate(RequestRegisterExpense request)
    {
        var validator = new RegisterExpenseValidator();

        var result = validator.Validate(request);

        if(!result.IsValid) {

            var errorMessages = result.Errors
                .Select(f => f.ErrorMessage)
                .ToList();

            throw new ErrorOnValidationException(errorMessages);
        }

    }
}
