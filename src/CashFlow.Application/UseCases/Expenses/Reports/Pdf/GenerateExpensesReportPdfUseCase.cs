
using CashFlow.Application.UseCases.Expenses.Reports.Pdf.Fonts;
using CashFlow.Domain.Reports;
using CashFlow.Domain.Repositories.Expenses;
using MigraDoc.DocumentObjectModel;
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

        var document = CreateDocument(period);

        return [];
    }

    private Document CreateDocument(DateOnly month)
    {
        var document = new Document();
        document.Info.Title = $"{ResourceReportGenerationMessages.TITLE} {month:Y}";
        document.Info.Author = "Rafael Madeira";

        var style = document.Styles["Normal"];
        style!.Font.Name = FontHelper.RALEWAY_REGULAR;

        return document;
    }
}
