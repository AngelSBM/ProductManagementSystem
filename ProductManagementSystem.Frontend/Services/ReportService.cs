using Flurl;
using Flurl.Http;
using Flurl.Http.Content;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using ProductManagementSystem.Frontend.Helpers;
using ProductManagementSystem.Shared.DTOs.Report;

namespace ProductManagementSystem.Frontend.Services
{
    public interface IReportService 
    {
        Task<byte[]> GetItemRangeReport(CreateCustomerItemRangeReportDto range);
        Task<byte[]> GetMostExpensiveItemsPerCustomerReport();
    }

    public class ReportService(ILoadingService loadingService, IAccessTokenProvider tokenProvider, ConfigurationSettings configurationSettings) : IReportService
    {
        private const string _reportPath = "/Report";

        public async Task<string> GetTokenAsync()
        {
            string token = "";
            var tokenResult = await tokenProvider.RequestAccessToken();
            if (tokenResult.TryGetToken(out var tokenResponse))
            {
                token = tokenResponse.Value;
            }

            return token;
        }

        public async Task<byte[]> GetItemRangeReport(CreateCustomerItemRangeReportDto range)
        {
            var token = await GetTokenAsync();

            var result = await configurationSettings.BaseUrl
                .AppendPathSegment(_reportPath)
                .AppendPathSegment("Create/ItemRangeReport")
                .WithHeader("Authorization", $"bearer {token}")
                .PostJsonAsync(range)
                .ReceiveBytes();

            return result;
         }

        public async Task<byte[]> GetMostExpensiveItemsPerCustomerReport()
        {
            var token = await GetTokenAsync();
            var result = await configurationSettings.BaseUrl
                .AppendPathSegment(_reportPath)
                .AppendPathSegment("Get/MostExpensiveCustomerItems")
                .WithHeader("Authorization", $"bearer {token}")
                .GetBytesAsync();

            return result;
        }
    }
}
