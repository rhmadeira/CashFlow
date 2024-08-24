

using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;

namespace Validator.Tests.Expenses.Register;

public class RegisterExpenseValidatorTests
{

    [Fact]
    public void Success()
    {
        //Arrange - parte onde a gente prepara o ambiente para o teste

        var validator = new RegisterExpenseValidator();
        var request = new RequestRegisterExpense
        {
            Description = "Teste",
            Amount = 10,
            Date = DateTime.Now.AddDays(-1), //Data de ontem
            Title = "Teste",
            PaymentType = PaymentType.CreditCard
        };

        //Act - Ação, executar o método que queremos testar

        var result = validator.Validate(request);

        //Assert - Verificar se o resultado é o esperado

        Assert.True(result.IsValid);

    }
}
