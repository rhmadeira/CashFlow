

using CashFlow.Application.UseCases.Expenses.Register;
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

    [Fact]
    public void ErrorTitleEmpyt()
    {

        var validator = new RegisterExpenseValidator();
        var request = RequestRegisterExpenseBuilder.Build();

        request.Title = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(x => x.ErrorMessage.Equals(ResourceErrorMessages.TITLE_REQUIRED));

    }

}
