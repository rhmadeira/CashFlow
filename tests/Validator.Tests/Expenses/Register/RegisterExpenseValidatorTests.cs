using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Enums;
using CashFlow.Exception;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace Validator.Tests.Expenses.Register;

public class RegisterExpenseValidatorTests
{

    [Fact]
    public void Success()
    {
        //Arrange - parte onde a gente prepara o ambiente para o teste

        var validator = new RegisterExpenseValidator();
        var request = RequestRegisterExpenseBuilder.Build();

        //Act - Ação, executar o método que queremos testar

        var result = validator.Validate(request);

        //Assert - Verificar se o resultado é o esperado

        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData("     ")]
    [InlineData(null)]
    public void ErrorTitleEmpyt(string title)
    {

        var validator = new RegisterExpenseValidator();
        var request = RequestRegisterExpenseBuilder.Build();

        request.Title = title;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(x => x.ErrorMessage.Equals(ResourceErrorMessages.TITLE_REQUIRED));

    }

    [Fact]
    public void ErrorDateFuture()
    {

        var validator = new RegisterExpenseValidator();
        var request = RequestRegisterExpenseBuilder.Build();

        request.Date = DateTime.Now.AddDays(1);

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(x => x.ErrorMessage.Equals(ResourceErrorMessages.INVALIDE_DATE));

    }

    [Fact]
    public void ErrorPaymentTypeInvalid()
    {

        var validator = new RegisterExpenseValidator();
        var request = RequestRegisterExpenseBuilder.Build();

        request.PaymentType = (PaymentType)700;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(x => x.ErrorMessage.Equals(ResourceErrorMessages.PAYMENT_TYPE_INVALIDE));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void ErrorAmountInValid(decimal amount)
    {

        var validator = new RegisterExpenseValidator();
        var request = RequestRegisterExpenseBuilder.Build();

        request.Amount = amount;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(x => x.ErrorMessage.Equals(ResourceErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_ZERO));
    }
}
