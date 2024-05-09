//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations;
using TASKTRACKERSYSTEM.CustomValidation;



namespace TASKTRACKERSYSTEM.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using TASKTRACKERSYSTEM.CustomValidation;

    public partial class TASK
    {
     

        public int TASKID { get; set; }

        [Required]
        [AlphanumericAttribute(ErrorMessage = "Task Name should allow only alphanumeric characters and be maximum 50 characters.")]
        public string TASKNAME { get; set; }

        [Required(ErrorMessage = "Assigned To should be selected from a list of available team members.")]
        public Nullable<int> ASSIGNEDTO { get; set; }

        [Required(ErrorMessage = "Assigned To should be selected from a list of available team members.")]
        public Nullable<System.DateTime> DUETO { get; set; }
        [Required]
        public int STATUS { get; set; }
  
        public virtual USER USER { get; set; }
        public int UserID { get; internal set; }
    }
    

}
