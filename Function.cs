using docHttpTrigger.CloudStorage;
using docHttpTrigger.CloudStorage.AspNetCoreWebApp.CloudStorage;
using Google.Cloud.Functions.Framework;
using Google.Cloud.Functions.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace docHttpTrigger
{
    [FunctionsStartup(typeof(Startup))]
    public class Function : IHttpFunction
    {
        private readonly ILogger _logger;

        private readonly ICloudStorage _cloudStorage;

        //public Function(ICloudStorage cloudStorage)
        //{
        //    _cloudStorage = cloudStorage;
        //}
        public Function(ILogger<Function> logger, ICloudStorage cloudStorage)
        {
            _logger = logger;
            _cloudStorage = cloudStorage;
        }

        public async Task HandleAsync(HttpContext context)
        {
            HttpRequest request = context.Request;
            using TextReader reader = new StreamReader(request.Body);
            string dco = await reader.ReadToEndAsync();
            if (dco.Length > 0)
            {
                try
                {
                    //JsonDocument jdoc = JsonDocument.Parse(text);
                    JsonElement json = JsonSerializer.Deserialize<JsonElement>(dco);
                    if (json.TryGetProperty("id", out JsonElement id))
                    {
                        json.TryGetProperty("type", out JsonElement type);
                        await _cloudStorage.UploadFileAsync( string.Concat(type.ToString(),"_", id.ToString(), ".json"), dco);
                    }
                }
                catch (JsonException parseException)
                {
                    _logger.LogError(parseException, "Error parsing JSON request");
                }
            }
 
            await context.Response.WriteAsync($"Hello!");
        }
    }
}
