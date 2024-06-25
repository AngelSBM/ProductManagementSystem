using Flurl;
using Flurl.Http;
using ProductManagementSystem.Frontend.Services;

namespace ProductManagementSystem.Frontend.Helpers
{

    public static class FlurlExtensions
    {
        public static async Task<T> GetWithLoadingAsync<T>(this Url url, string token, ILoadingService loadingService)
        {
            try
            {
                loadingService.Show();
                return await url
                    .WithHeader("Authorization", $"bearer {token}")
                    .GetJsonAsync<T>();
            }
            finally
            {
                loadingService.Hide();
            }
        }



        public static async Task<T> PostWithLoadingAsync<T>(this Url url, object data, string token, ILoadingService loadingService)
        {
            try
            {
                loadingService.Show();
                return await url
                    .WithHeader("Authorization", $"bearer {token}")
                    .PostJsonAsync(data)
                    .ReceiveJson<T>();
            }
            finally
            {
                loadingService.Hide();
            }
        }

        public static async Task PostWithLoadingAsync(this Url url, object data, string token, ILoadingService loadingService)
        {
            try
            {
                loadingService.Show();
                await url.WithHeader("Authorization", $"bearer {token}").PostJsonAsync(data);
            }
            finally
            {
                loadingService.Hide();
            }
        }

        public static async Task<T> PutWithLoadingAsync<T>(this Url url, object data, string token, ILoadingService loadingService)
        {
            try
            {
                loadingService.Show();
                return await url.PutJsonAsync(data).ReceiveJson<T>();
            }
            finally
            {
                loadingService.Hide();
            }
        }

        public static async Task DeleteWithLoadingAsync(this Url url, string token, ILoadingService loadingService)
        {
            try
            {
                loadingService.Show();
                await url.WithHeader("Authorization", $"bearer {token}").DeleteAsync();
            }
            finally
            {
                loadingService.Hide();
            }
        }
    }
}
