
using Bogus;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;

namespace CommonTestUtilities.Requests;

public class RequestRegisterUserBuilder
{
    public static RequestRegisterUser Build() 
    {

        var request = new Faker<RequestRegisterUser>()
              .RuleFor(x => x.Name, f => f.Person.FirstName)
              .RuleFor(x => x.Email, (f, user) => f.Internet.Email(user.Name))
              .RuleFor(x => x.Password, f => f.Internet.Password(prefix:"A@a1"));


        return request;
    
    }
}
