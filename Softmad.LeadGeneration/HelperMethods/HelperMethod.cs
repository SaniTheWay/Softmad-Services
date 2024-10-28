using Softmad.Services.Models;
using System.Text.Json;

namespace Softmad.LeadGeneration.HelperMethods
{
    internal class HelperMethod
    {
        public HelperMethod() { }

        public static async Task<T?> HttpResponseConverter<T>(T? result, HttpResponseMessage response)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            result = JsonSerializer.Deserialize<T>(jsonString, options);
            return result;
        }
    }
}
