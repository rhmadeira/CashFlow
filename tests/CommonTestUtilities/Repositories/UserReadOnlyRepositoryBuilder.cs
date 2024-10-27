using CashFlow.Domain.Repositories.Users;
using Moq;

namespace CommonTestUtilities.Repositories;
public class UserReadOnlyRepositoryBuilder
{
    private readonly Mock<IUserReadOnlyRepository> _repository;

    public UserReadOnlyRepositoryBuilder()
    {
        _repository = new Mock<IUserReadOnlyRepository>();
    }

    public void ExistActiveUserWithEmail()
    {
        _repository.Setup(user => user.ExistActiveUserWithEmail(It.IsAny<string>())).ReturnsAsync(true);
    }

    public IUserReadOnlyRepository Build() => _repository.Object;
    
}
