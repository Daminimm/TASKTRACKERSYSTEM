using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using System.ComponentModel;
using System.Reflection;

namespace TASKTRACKERSYSTEM.Models
{
   
    public enum TaskStatus
    {
        NotStarted = 0,
        InProgress=1,
        Completed=2,
    }

    public class Task
    {
       public int STATUS { get; set; }
       
        public TaskStatus Status // Property to get or set status as enum
       {
            get => (TaskStatus)STATUS;
           set => STATUS = (int)value;
       }

       public string StatusString
       {
            get
            {
                switch (Status)
                {
                    case TaskStatus.NotStarted:
                        return "Not Started";
                    case TaskStatus.InProgress:
                        return "In Progress";
                    case TaskStatus.Completed:
                        return "Completed";
                    default:
                        return "Unknown";
                }
            }
       }

     
    }
}