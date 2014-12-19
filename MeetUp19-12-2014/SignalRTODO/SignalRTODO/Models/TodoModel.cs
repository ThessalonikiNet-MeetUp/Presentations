using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRTODO.Models
{
    public class TodoModel
    {
        public string Task { get; set; }
        public string Category { get; set; }
        public int Status { get; set; }
        public TodoModel(string task)
        {
            Task = task;
            Status = 0;
            Category = "urgent";
        }
    }
}