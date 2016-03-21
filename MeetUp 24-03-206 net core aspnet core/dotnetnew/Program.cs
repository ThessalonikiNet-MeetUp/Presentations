using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var host = new WebHostBuilder()
                        .UseServer("Microsoft.AspNetCore.Server.Kestrel")
                        .UseContentRoot(Directory.GetCurrentDirectory())
                        .UseDefaultConfiguration(args)
                        .UseStartup<Startup>()
                        .Build();
            host.Run();
        }
    }
}
