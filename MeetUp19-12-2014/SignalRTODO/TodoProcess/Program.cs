using Microsoft.AspNet.SignalR.Client;
using SignalRTODO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoProcess
{
    class Program
    {
        static string server = @"http://localhost:49220/";
         static void Main(string[] args)
         {



             var hubConnection = new HubConnection(server);

             //string username = username;// "venue1";
             // string password = password;//"venue1";
             // stockTickerHubProxy.On<Stock>("UpdateStockPrice", stock => Console.WriteLine("Stock update for {0} new price {1}", stock.Symbol, stock.Price));

             IHubProxy promosHubProxy = hubConnection.CreateHubProxy("TodoHub");
             hubConnection.Error += hubConnection_Error;

             hubConnection.Start().ContinueWith(task =>
             {
                 if (task.IsFaulted)
                 {
                     Console.WriteLine("There was an error opening the connection:{0}",
                                       task.Exception.GetBaseException());
                 }
                 else
                 {
                     Console.WriteLine("Connected");
                 }
             }
                 ).Wait();
             promosHubProxy.On("showList", (r) =>
             {
                 foreach (var t in r)
                 {
                     
                     Console.WriteLine(t.Task);
                 }



             });
             
             Console.ReadLine();
         }

         static void hubConnection_Error(Exception obj)
         {
             Console.WriteLine(obj.Message);
         }
    }
}
