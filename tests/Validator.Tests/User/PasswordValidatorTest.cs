using CashFlow.Application.UseCases.User;
using CashFlow.Application.UseCases.User.Register;
using CashFlow.Communication.Requests;
using CommonTestUtilities.Requests;
using FluentAssertions;
using FluentValidation;

namespace Validator.Tests.User;

public class PasswordValidatorTest
{
    [Theory]
    [InlineData("")]
    [InlineData("     ")]
    [InlineData(null)]
    [InlineData("a")]
    [InlineData("aa")]
    [InlineData("aaa")]
    [InlineData("aaaa")]
    [InlineData("aaaaa")]
    [InlineData("aaaaaa")]
    [InlineData("aaaaaaa")]
    [InlineData("aaaaaaaa")]
    [InlineData("AAAAAAAA")]
    [InlineData("Aaaaaaaa")]

    public void Error_Password_Invalid(string password)
    {
        var validator = new PasswordValidator<RequestRegisterUser>();

        var result = validator.IsValid(new ValidationContext<RequestRegisterUser>(new RequestRegisterUser()), password);

        result.Should().BeFalse();
    }
}
