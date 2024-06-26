using System.Text.Json;

namespace ProductManagementSystem.Frontend.Helpers
{
    public static class ErrorMessageFormat
    {
        public static string FormatErrorMessage(Exception ex)
        {
            return JsonSerializer.Serialize(new
            {
                StatusCode = 500,
                Message = "Request failed!",
                Errors = new[] { ex.Message } 
            });
        }
    }
}
