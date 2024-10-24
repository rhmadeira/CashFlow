using CashFlow.Communication.Requests;
using CashFlow.Exception;
using FluentValidation;

namespace CashFlow.Application.UseCases.User.Register;

public class RegisterUserValidator : AbstractValidator<RequestRegisterUser>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.NAME_REQUIRED);

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.EMAIL_REQUIRED)
            .EmailAddress()
            .When(user => !string.IsNullOrEmpty(user.Email))
            .WithMessage(ResourceErrorMessages.INVALID_EMAIL);

        RuleFor(user => user.Password)
            .SetValidator(new PasswordValidator<RequestRegisterUser>());
    }
}
