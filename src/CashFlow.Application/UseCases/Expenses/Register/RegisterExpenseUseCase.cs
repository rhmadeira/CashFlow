using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expenses.Register;
public class RegisterExpenseUseCase : IRegisterExpenseUseCase
{
    private readonly IExpensesRepository _expensesRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterExpenseUseCase(IExpensesRepository expensesRepository, IUnitOfWork unitOfWork)
    {
        _expensesRepository = expensesRepository;
        _unitOfWork = unitOfWork;
    }

    public ResponseRegisteredExpense Execute(RequestRegisterExpense request)
    {
        Validate(request);

        var expense = new Expense
        {
            Title = request.Title,
            Description = request.Description,
            Amount = request.Amount,
            Date = request.Date,
            PaymentType = (Domain.Enums.PaymentType)request.PaymentType,
        };

       _expensesRepository.Add(expense);

        _unitOfWork.Commit();

        return new ResponseRegisteredExpense();
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
