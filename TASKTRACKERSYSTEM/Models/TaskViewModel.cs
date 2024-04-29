﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TASKTRACKERSYSTEM.CustomValidation;

namespace TASKTRACKERSYSTEM.Models
{
    public class TaskViewModel
    {
        public int TASKID { get; set; }
        [Required]
        [Alphanumeric(ErrorMessage = "Task Name should allow only alphanumeric characters and be maximum 50 characters.")]
        public string TASKNAME { get; set; }
        public Nullable<int> ASSIGNEDTO { get; set; }

        public Nullable<System.DateTime> DUETO { get; set; }
        public TaskStatus STATUS { get; set; }

        public virtual USER USER { get; set; }
    }
}