
using CashFlow.Application.UseCases.Expenses.Reports.Pdf.Fonts;
using CashFlow.Domain.Repositories.Expenses;
using PdfSharp.Fonts;

namespace CashFlow.Application.UseCases.Expenses.Reports.Pdf;

public class GenerateExpensesReportPdfUseCase : IGenerateExpensesReportPdfUseCase
{
    private readonly IExpensesReadOnlyRepository _expenseRepository;

    public GenerateExpensesReportPdfUseCase(IExpensesReadOnlyRepository expenseRepository)
    {
        _expenseRepository = expenseRepository;
        GlobalFontSettings.FontResolver = new ExpensesReportFontResolver();
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
