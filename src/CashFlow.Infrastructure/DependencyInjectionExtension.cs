using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Infrastructure.DataAccess;
using CashFlow.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Infrastructure;
public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection service, IConfiguration configuration)
    {
        AddDbContext(service, configuration);
        AddRepositories(service);

        service.AddScoped<IPasswordEncripter, Security.Cryptography.BCrypt>();
    }

    private static void AddRepositories(IServiceCollection service)
    {
        service.AddScoped<IUnitOfWork, UnitOfWork>();
        service.AddScoped<IExpensesReadOnlyRepository, ExpensesRepository>();
        service.AddScoped<IExpensesWriteOnlyRepository, ExpensesRepository>();
        service.AddScoped<IUserReadOnlyRepository, UserRepository>();
        service.AddScoped<IUserWriteOnlyRepository, UserRepository>();
    }
    private static void AddDbContext(IServiceCollection service, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("connection");

        var version = new Version(8, 0, 39);
        var serverVersion = new MySqlServerVersion(version);
        //optionsBuilder.UseMySql(connectionString, serverVersion);

        service.AddDbContext<CashFlowDbContext>(config => config.UseMySql(connectionString, serverVersion));
    } 
}
