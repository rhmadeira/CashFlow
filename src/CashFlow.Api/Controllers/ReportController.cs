using CashFlow.Application.UseCases.Expenses.Reports.Excel;
using CashFlow.Application.UseCases.Expenses.Reports.Pdf;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace CashFlow.Api.Controllers;
[Route("api/[controller]")]
[ApiController]

public class ReportController : ControllerBase
{
    [HttpGet("excel")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]

    public async Task<IActionResult> GetExcel(
        [FromHeader] DateOnly period,
        [FromServices] IGenerateExpensesReportExcelUseCase useCase
        )
    {
        byte[] file = await useCase.Execute(period);
        if (file.Length == 0)
        {
            return NoContent();
        }

        return File(file, MediaTypeNames.Application.Octet, "report.xlsx");
    }

    [HttpGet("pdf")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]

    public async Task<IActionResult> GetPdf(
        [FromHeader] DateOnly period,
        [FromServices] IGenerateExpensesReportPdfUseCase useCase
        )
    {
        byte[] file = await useCase.Execute(period);

        if (file.Length == 0)
        {
            return NoContent();
        }

        return File(file, MediaTypeNames.Application.Pdf, "report.pdf");
    }
}
