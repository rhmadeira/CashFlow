using CashFlow.Domain.Repositories;

namespace CashFlow.Infrastructure.DataAccess;

internal class UnitOfWork : IUnitOfWork
{
    private readonly CashFlowDbContext _cashFlowDbContext;
    public UnitOfWork(CashFlowDbContext cashFlowDbContext)
    {
      _cashFlowDbContext = cashFlowDbContext;
    }
    public void Commit() => _cashFlowDbContext.SaveChanges();
    
}
