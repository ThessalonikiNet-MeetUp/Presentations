using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace ConsoleApplication
{
    public class Startup {
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }
        
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory) {
            
            loggerFactory.AddConsole(LogLevel.Debug);
            
            var logger = loggerFactory.CreateLogger("Custom Logging");
            app.Use(async (context, next) =>
            {
                logger.LogInformation("Handling request.");
                await next.Invoke();
                logger.LogInformation("Finished handling request.");
            });
            
            app.UseMiddleware<ProcessingTimeMiddleware>();

            app.UseMvc();
            
            app.Run(context =>
            {
                return context.Response.WriteAsync("Hello World");
            });
        }
    }
}