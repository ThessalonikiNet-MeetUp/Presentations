using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using SignalRTODO.Models;

namespace SignalRTODO.Hubs
{
    public class TodoHub : Hub
    {

        public void GetList()
        {
            Clients.All.showList(TodoDB.ActiveTodo);
        }
        public void AddToList(string task)
        {
            TodoDB.ActiveTodo.Add(new TodoModel(task));
            IList<TodoModel> delta = new List<TodoModel>();
            delta.Add(new TodoModel(task));


            Clients.All.showList(delta);
        }
    }




}




#region DB
public class TodoDB
{
    static TodoDB()
    {
        ActiveTodo = new List<TodoModel>();
    }
    public static List<TodoModel> ActiveTodo { get; set; }

}
#endregion