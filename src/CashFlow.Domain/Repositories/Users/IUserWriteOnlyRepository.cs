using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Users;

public interface IUserWriteOnlyRepository
{
    Task AddUser(User user);
    Task UpdateUser(User user);
    Task DeleteUser(User user);
}
