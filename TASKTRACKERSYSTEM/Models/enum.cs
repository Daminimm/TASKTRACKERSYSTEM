using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace TASKTRACKERSYSTEM.Models
{

    using System;
    using System.ComponentModel;

    public enum TaskStatus
    {
        [Description("Not Started")]
        NotStarted = 0,
        [Description("In Progress")]
        InProgress = 1,
        [Description("Completed")]
        Completed = 2,
    }

    public static class EnumHelper
    {
        public static string GetDescription(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
            return attribute == null ? value.ToString() : attribute.Description;
        }
    }

    public class Task
    {
        public int STATUS { get; set; }

        public TaskStatus Status // Property to get or set status as enum
        {
            get => (TaskStatus)STATUS;
            set => STATUS = (int)value;
        }

        public string StatusString => EnumHelper.GetDescription(Status);
    }

    //public enum TaskStatus
    //{
    //    //[Description("Not Started")]
    //    NotStarted = 0,
    //    //[Description("In Progress")]
    //    InProgress =1,
    //    //[Description("Completed")]
    //    Completed =2,
    //}

    //public class Task
    //{
    //   public int STATUS { get; set; }

    //    public TaskStatus Status // Property to get or set status as enum
    //   {
    //        get => (TaskStatus)STATUS;
    //       set => STATUS = (int)value;
    //   }

    //   public string StatusString
    //   {
    //        get
    //        {
    //            switch (Status)
    //            {
    //                case TaskStatus.NotStarted:
    //                    return "Not Started";
    //                case TaskStatus.InProgress:
    //                    return "In Progress";
    //                case TaskStatus.Completed:
    //                    return "Completed";
    //                default:
    //                    return "Unknown";
    //            }
    //        }
    //   }


    //}
}
