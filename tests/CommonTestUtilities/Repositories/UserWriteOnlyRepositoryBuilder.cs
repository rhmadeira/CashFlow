using CashFlow.Domain.Repositories.Users;
using Moq;

namespace CommonTestUtilities.Repositories;
public class UserWriteOnlyRepositoryBuilder
{
    public static IUserWriteOnlyRepository Build()
    {
        //var mock = new Mock<IUserWriteOnlyRepository>();
        var mock = Mock.Of<IUserWriteOnlyRepository>();

        //return mock.Object;
        return mock;
    }
}
