using Flurl;
using Flurl.Http;
using Flurl.Http.Content;
using ProductManagementSystem.Shared.DTOs.Report;

namespace ProductManagementSystem.Frontend.Services
{
    public interface IReportService 
    {
        Task<byte[]> GetItemRangeReport(CreateCustomerItemRangeReportDto range);
        Task<byte[]> GetMostExpensiveItemsPerCustomerReport();
    }

    public class ReportService : IReportService
    {
        private const string _reportPath = "/Report";
        private const string _baseUrl = "https://localhost:44307";
        public async Task<byte[]> GetItemRangeReport(CreateCustomerItemRangeReportDto range)
        {
            var result = await _baseUrl
                .AppendPathSegment(_reportPath)
                .AppendPathSegment("Create/ItemRangeReport")
                .PostJsonAsync(range)
                .ReceiveBytes();

            return result;
         }

        public async Task<byte[]> GetMostExpensiveItemsPerCustomerReport()
        {
            var result = await _baseUrl
                .AppendPathSegment(_reportPath)
                .AppendPathSegment("Get/MostExpensiveCustomerItems")
                .GetBytesAsync();

            return result;
        }
    }
}
