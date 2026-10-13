using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using ToDo.Application.Services;
using ToDo.Application.Services.Implementations;

namespace ToDo.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IToDoTaskService, ToDoTaskService>();

            return services;
        }

        public static ConfigureHostBuilder AddSerilogLogging(
            this ConfigureHostBuilder host
            )
        {
            host.UseSerilog((context, loggerConfiguration) =>
            {
                loggerConfiguration.ReadFrom.Configuration(context.Configuration);
            });

            return host;
        }
    }     
}
