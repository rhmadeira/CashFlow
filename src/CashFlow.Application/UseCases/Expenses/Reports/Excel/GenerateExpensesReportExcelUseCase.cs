
using ClosedXML.Excel;

namespace CashFlow.Application.UseCases.Expenses.Reports.Excel;

public class GenerateExpensesReportExcelUseCase : IGenerateExpensesReportExcelUseCase
{
    public async Task<byte[]> Execute(DateOnly period)
    {
        var workbook = new XLWorkbook();

        workbook.Author = "CashFlow";
        workbook.Style.Font.FontName = "Arial";
        workbook.Style.Font.FontSize = 12;

        var workSheet = workbook.Worksheets.Add(period.ToString("Y"));
        InsertHeader(workSheet);

        var file = new MemoryStream();

        //caminho na minha máquina
        workbook.SaveAs(file);

        return file.ToArray();
    }

    private void InsertHeader(IXLWorksheet worksheet)
    {
        worksheet.Cell(1, 1).Value = "Title";
        worksheet.Cell(1, 2).Value = "Date";
        worksheet.Cell(1, 3).Value = "PaymentType";
        worksheet.Cell(1, 4).Value = "Amount";
        worksheet.Cell(1, 5).Value = "Description";

        worksheet.Row(1).Style.Font.Bold = true;
        worksheet.Row(1).Style.Fill.BackgroundColor = XLColor.LightGray;
    }
}
