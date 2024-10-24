using CashFlow.Application.UseCases.User.Register;
using CashFlow.Exception;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace Validator.Tests.User.Register;

public class RegisterUserValidatorTests
{

    [Fact]
    public void Success()
    {
        var request = RequestRegisterUserBuilder.Build();
        var validator = new RegisterUserValidator();

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData("     ")]
    [InlineData(null)] 
    public void Error_Name_Empty(string name)
    {
        var request = RequestRegisterUserBuilder.Build();
        var validator = new RegisterUserValidator();
        request.Name = name;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(x => x.ErrorMessage.Equals(ResourceErrorMessages.NAME_REQUIRED));
    }

    [Theory]
    [InlineData("")]
    [InlineData("     ")]
    [InlineData(null)]
    public void Error_Email_Empty(string email)
    {
        var request = RequestRegisterUserBuilder.Build();
        var validator = new RegisterUserValidator();
        request.Email = email;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(x => x.ErrorMessage.Equals(ResourceErrorMessages.EMAIL_REQUIRED));
    }

    [Fact]
    public void Error_Email_Invalid()
    {
        var request = RequestRegisterUserBuilder.Build();
        var validator = new RegisterUserValidator();
        request.Email = "teste.com";

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(x => x.ErrorMessage.Equals(ResourceErrorMessages.INVALID_EMAIL));
    }

    [Fact]
    public void Error_Password_Empty()
    {
        var request = RequestRegisterUserBuilder.Build();
        var validator = new RegisterUserValidator();
        request.Password = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(x => x.ErrorMessage.Equals(ResourceErrorMessages.INVALID_PASSWORD));
    }

}
