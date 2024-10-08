
using CashFlow.Domain.Enums;
using CashFlow.Domain.Repositories.Expenses;
using ClosedXML.Excel;

namespace CashFlow.Application.UseCases.Expenses.Reports.Excel;

public class GenerateExpensesReportExcelUseCase : IGenerateExpensesReportExcelUseCase
{
    private const string CURRENCY_FORMAT = "R$";
    private readonly IExpensesReadOnlyRepository _expenseRepository;

    public GenerateExpensesReportExcelUseCase(IExpensesReadOnlyRepository expenseRepository)
    {
        _expenseRepository = expenseRepository;
    }

    public async Task<byte[]> Execute(DateOnly period)
    {
        var expenses = await _expenseRepository.GetByPeriod(period);

        if (expenses.Count == 0) 
        {
            return [];
        }

       using var workbook = new XLWorkbook();

        workbook.Author = "CashFlow";
        workbook.Style.Font.FontName = "Arial";
        workbook.Style.Font.FontSize = 12;

        var workSheet = workbook.Worksheets.Add(period.ToString("Y"));

        InsertHeader(workSheet);

        foreach (var expense in expenses)
        {
            workSheet.Cell(workSheet.LastRowUsed().RowNumber() + 1, 1).Value = expense.Title;
            workSheet.Cell(workSheet.LastRowUsed().RowNumber(), 2).Value = expense.Date;
            workSheet.Cell(workSheet.LastRowUsed().RowNumber(), 3).Value = ConvertPaymentType(expense.PaymentType);
            workSheet.Cell(workSheet.LastRowUsed().RowNumber(), 4).Value = expense.Amount;
            workSheet.Cell(workSheet.LastRowUsed().RowNumber(), 4).Style.NumberFormat.Format = $"-{CURRENCY_FORMAT} #,##0.00";
            workSheet.Cell(workSheet.LastRowUsed().RowNumber(), 5).Value = expense.Description;
        }

        workSheet.Columns().AdjustToContents();

        var file = new MemoryStream();
        //caminho na minha máquina
        workbook.SaveAs(file);

        return file.ToArray();
    }

    private string ConvertPaymentType(PaymentType paymentType)
    {
        return paymentType switch
        {
            PaymentType.Cash => "Cash",
            PaymentType.CreditCard => "Credit Card",
            PaymentType.DebitCard => "Debit Card",
            PaymentType.EletronicTransfer => "Eletronic Transfer",
            _ => string.Empty
        };
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
