using docHttpTrigger.CloudStorage;
using docHttpTrigger.CloudStorage.AspNetCoreWebApp.CloudStorage;
using Google.Apis.Logging;
using Google.Cloud.Functions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace docHttpTrigger
{
    public class Startup : FunctionsStartup
    {
        public override void ConfigureServices(WebHostBuilderContext context, IServiceCollection services) =>
           services
               .AddSingleton<ICloudStorage, GoogleCloudStorage>();

        public override void ConfigureAppConfiguration(WebHostBuilderContext context, IConfigurationBuilder configuration) =>
            configuration
            .AddJsonFile("appsettings.json");

    }
}
