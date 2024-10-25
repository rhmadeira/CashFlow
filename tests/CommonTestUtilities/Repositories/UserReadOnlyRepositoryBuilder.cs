using CashFlow.Domain.Repositories.Users;
using Moq;

namespace CommonTestUtilities.Repositories;
public class UserReadOnlyRepositoryBuilder
{
    public static IUserReadOnlyRepository Build()
    {
        //var mock = new Mock<IUserWriteOnlyRepository>();
        var mock = Mock.Of<IUserReadOnlyRepository>();

        //return mock.Object;
        return mock;
    }
}
