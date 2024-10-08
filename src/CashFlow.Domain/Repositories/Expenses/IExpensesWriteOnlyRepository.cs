﻿using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses;

public interface IExpensesWriteOnlyRepository
{
    Task Add(Expense expense);

    /// <summary>
    /// This function return true if the deletion was successful
    /// </summary>
    /// <param name="expense"></param>
    /// <returns></returns>
    Task<bool> Delete(long id);

    Task<Expense?> GetById(long id);
    void Update(Expense expense);
}
