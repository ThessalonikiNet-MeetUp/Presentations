using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ConsoleApplication
{
    public class ProcessingTimeMiddleware  
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ProcessingTimeMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<ProcessingTimeMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            var watch = new Stopwatch();
            watch.Start();

            await _next(context);
            _logger.LogInformation($"Request: {context.Request.Path} executed in {watch.ElapsedMilliseconds.ToString()} ms");
        }
    }

}