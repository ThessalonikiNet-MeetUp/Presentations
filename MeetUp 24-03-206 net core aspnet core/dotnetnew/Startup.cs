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
            
            app.UseMvc();
            
            app.Run(context =>
            {
                return context.Response.WriteAsync("Hello World");
            });
        }
    }
}