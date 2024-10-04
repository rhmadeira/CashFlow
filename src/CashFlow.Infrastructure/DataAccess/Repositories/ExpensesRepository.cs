﻿using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess.Repositories;
internal class ExpensesRepository : IExpensesReadOnlyRepository, IExpensesWriteOnlyRepository
{
    private readonly CashFlowDbContext _dbContext;

    public ExpensesRepository(CashFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    async Task<Expense?> IExpensesReadOnlyRepository.GetById(long id)
    {
        var expense = await _dbContext.Expenses.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);

        return expense;
    }

    async Task<Expense?> IExpensesWriteOnlyRepository.GetById(long id)
    {
        var expense = await _dbContext.Expenses.FirstOrDefaultAsync(e => e.Id == id);

        return expense;
    }

    public async Task Add(Expense expense)
    {
        await _dbContext.Expenses.AddAsync(expense);
    }

    public async Task<bool> Delete(long id)
    {
        var result = await _dbContext.Expenses.FirstOrDefaultAsync(e => e.Id == id);

        if (result is null)
        {
            return false;
        }

        _dbContext.Expenses.Remove(result);
        return true;
    }

    public async Task<List<Expense>> GetAll()
    {
        var expenses = await _dbContext.Expenses.AsNoTracking().ToListAsync();

        return expenses;
    }

    public void Update(Expense expense)
    {
        _dbContext.Expenses.Update(expense);
    }

    public async Task<List<Expense>> GetByPeriod(DateOnly period)
    {
        // exemplo do professor
        //var startDate = new DateTime(year: period.Year, month: period.Month, day: 1).Date;
        //var daysInMonth = DateTime.DaysInMonth(period.Year, period.Month);
        //var endDate = new DateTime(year: period.Year, month: period.Month, day: daysInMonth, hour:23, minute:59, second:59).Date;

        //var expenses = await _dbContext.Expenses
        //    .AsNoTracking()
        //    .Where(e => e.Date >= startDate && e.Date <= endDate)
        //    .OrderBy(e => e.Date)
        //    .thenBy(e => e.Title)
        //    .ToListAsync();

        // meu exemplo
        var expenses = await _dbContext.Expenses
            .AsNoTracking()
            .Where(e => e.Date.Month == period.Month && e.Date.Year == period.Year)
            .OrderBy(e => e.Date)
            .ThenBy(e => e.Title)
            .ToListAsync();

        return expenses;
    }
}
