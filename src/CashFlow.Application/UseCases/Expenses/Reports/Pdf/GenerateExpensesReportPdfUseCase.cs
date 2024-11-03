
using CashFlow.Application.UseCases.Expenses.Reports.Pdf.Fonts;
using CashFlow.Domain.Reports;
using CashFlow.Domain.Repositories.Expenses;
using MigraDoc.DocumentObjectModel;
using PdfSharp.Fonts;

namespace CashFlow.Application.UseCases.Expenses.Reports.Pdf;

public class GenerateExpensesReportPdfUseCase : IGenerateExpensesReportPdfUseCase
{
    private const string CURRENCY_SYMBOL = "€";
    private readonly IExpensesReadOnlyRepository _expenseRepository;

    public GenerateExpensesReportPdfUseCase(IExpensesReadOnlyRepository expenseRepository)
    {
        _expenseRepository = expenseRepository;
        GlobalFontSettings.FontResolver = new ExpensesReportFontResolver();
    }

    public async Task<byte[]> Execute(DateOnly period)
    {
        var exepenses = await _expenseRepository.GetByPeriod(period);

        if (exepenses.Count == 0)
        {
            return [];
        }

        var document = CreateDocument(period);
        var page = CreatePage(document);

        Paragraph paragraph = page.AddParagraph();
        var title = string.Format(ResourceReportGenerationMessages.TOTAL_SPENT_IN, period.ToString("Y"));

        paragraph.AddFormattedText(title, new Font { Name = FontHelper.RALEWAY_REGULAR, Size = 15 });

        paragraph.AddLineBreak();
        var totalAmount = exepenses.Sum(ex => ex.Amount);
        paragraph.AddFormattedText($"{totalAmount} {CURRENCY_SYMBOL}", new Font { Name = FontHelper.WORK_SANS_BLACK, Size = 50 });

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

    private Section CreatePage(Document document)
    {
        var section = document.AddSection();
        section.PageSetup = document.DefaultPageSetup.Clone();

        section.PageSetup.PageFormat = PageFormat.A4;
        section.PageSetup.LeftMargin = 40;
        section.PageSetup.RightMargin = 40;
        section.PageSetup.TopMargin = 80;
        section.PageSetup.BottomMargin = 80;

        return section;
    }
}
