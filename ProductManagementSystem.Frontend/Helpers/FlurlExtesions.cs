using Flurl;
using Flurl.Http;
using ProductManagementSystem.Frontend.Services;

namespace ProductManagementSystem.Frontend.Helpers
{

    public static class FlurlExtensions
    {
        public static async Task<T> GetWithLoadingAsync<T>(this Url url, ILoadingService loadingService)
        {
            try
            {
                loadingService.Show();
                return await url.GetJsonAsync<T>();
            }
            finally
            {
                loadingService.Hide();
            }
        }



        public static async Task<T> PostWithLoadingAsync<T>(this Url url, object data, ILoadingService loadingService)
        {
            try
            {
                loadingService.Show();
                return await url.PostJsonAsync(data).ReceiveJson<T>();
            }
            finally
            {
                loadingService.Hide();
            }
        }

        public static async Task PostWithLoadingAsync(this Url url, object data, ILoadingService loadingService)
        {
            try
            {
                loadingService.Show();
                await url.PostJsonAsync(data);
            }
            finally
            {
                loadingService.Hide();
            }
        }

        public static async Task<T> PutWithLoadingAsync<T>(this Url url, object data, ILoadingService loadingService)
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

        public static async Task DeleteWithLoadingAsync(this Url url, ILoadingService loadingService)
        {
            try
            {
                loadingService.Show();
                await url.DeleteAsync();
            }
            finally
            {
                loadingService.Hide();
            }
        }
    }
}
