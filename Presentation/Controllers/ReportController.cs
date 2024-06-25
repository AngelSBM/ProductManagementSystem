using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;
using ProductManagementSystem.Shared.DTOs.Item;
using ProductManagementSystem.Shared.DTOs.Report;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController(IReportService reportService) : ControllerBase
    {
        [HttpPost]
        [Route("Create/ItemRangeReport")]
        public async Task<IActionResult> Create(CreateCustomerItemRangeReportDto range)
        {
            var report = await reportService.GenerateRangeItemsReportAsync(range);
            return File(report, "application/pdf", "report.pdf");
        }


        [HttpGet]
        [Route("Get/MostExpensiveCustomerItems")]
        public async Task<IActionResult> GetAll()
        {
            var report = await reportService.GenerateExpensiveItemsReport();
            return File(report, "application/pdf", "report.pdf");
        }


    }
}
