
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Application.UseCases.Expenses.Reports.Pdf;

public class GenerateExpensesReportPdfUseCase : IGenerateExpensesReportPdfUseCase
{
    private readonly IExpensesReadOnlyRepository _expenseRepository;

    public GenerateExpensesReportPdfUseCase(IExpensesReadOnlyRepository expenseRepository)
    {
        _expenseRepository = expenseRepository;
    }

    public async Task<byte[]> Execute(DateOnly period)
    {
       var exepenses = await _expenseRepository.GetByPeriod(period);

        if(exepenses.Count == 0)
        {
            return [];
        }

        return [];
    }
}
