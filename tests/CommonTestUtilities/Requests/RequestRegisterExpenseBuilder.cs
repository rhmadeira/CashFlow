
using Bogus;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;

namespace CommonTestUtilities.Requests;

public class RequestRegisterExpenseBuilder
{
    public static RequestRegisterExpense Build() 
    {

        //Opção 1

        //var faker = new Faker();

        //var request = new RequestRegisterExpense
        //{
        //    Title = faker.Lorem.Sentence(),
        //    Description = faker.Lorem.Paragraph(),
        //    Amount = faker.Random.Decimal(),
        //    Date = faker.Date.Past(),
        //    PaymentType = faker.PickRandom<PaymentType>()
        //};

        //Opção 2

      var request =  new Faker<RequestRegisterExpense>()
            .RuleFor(x => x.Title, f => f.Commerce.ProductName())
            .RuleFor(x => x.Description, f => f.Lorem.Paragraph())
            .RuleFor(x => x.Amount, f => f.Random.Decimal(min: 1, max:1000))
            .RuleFor(x => x.Date, f => f.Date.Past())
            .RuleFor(x => x.PaymentType, f => f.PickRandom<PaymentType>());

        return request;
    
    }
}
