using CashFlow.Communication.Requests;
using FluentValidation;

namespace CashFlow.Application.UseCases.Expenses.Register;
public class RegisterExpenseValidator : AbstractValidator<RequestRegisterExpense>
{
    public RegisterExpenseValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("The title is required");
        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithMessage("The amount must be greater than zero");
        RuleFor(x => x.Date)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("invalid Date");
        RuleFor(x => x.PaymentType)
            .IsInEnum()
            .WithMessage("Payment type is not valid");
    }
}
