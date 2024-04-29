using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TASKTRACKERSYSTEM.Models;
namespace TASKTRACKERSYSTEM.Controllers
{
    public class EMPOLYEEController : Controller
    {
        // GET: EMPOLYEE
        
        public ActionResult Index()
        {
           TASKTRACKEREntities DBContext = new TASKTRACKEREntities();
           List<TASK> EmpList = DBContext.TASKs.ToList();

            List<TaskViewModel> viewModelList = new List<TaskViewModel>();
            foreach (var task in EmpList)
            {
                TaskViewModel viewModel = new TaskViewModel
                {
                    TASKID = task.TASKID,
                    TASKNAME = task.TASKNAME,
                    ASSIGNEDTO = task.ASSIGNEDTO,
                    DUETO = task.DUETO,
                    STATUS = (TaskStatus)task.STATUS, // Convert int to TaskStatus enum
                    USER = task.USER
                };

                viewModelList.Add(viewModel);
            }

            return View(viewModelList);

        }
        public ActionResult Create( )
        {
            TASKTRACKEREntities DBContext = new TASKTRACKEREntities();

            List<USER> UserList = DBContext.USERS.ToList();
            if (UserList == null)
            {
                
                return RedirectToAction("Index");

            }
           
            ViewBag.UserList = UserList.Select(x => new SelectListItem { Value = x.UserID.ToString(), Text = x.Username.ToString() }).ToList();

           var status = new List<SelectListItem>
           {
            new SelectListItem { Text = "Not Started", Value = ((int)TaskStatus.NotStarted).ToString() },
            new SelectListItem { Text = "In Progress", Value = ((int)TaskStatus.InProgress).ToString() },
            new SelectListItem { Text = "Completed", Value = ((int)TaskStatus.Completed).ToString() }
           };

            ViewBag.Status = status;
            return View(new TASK());

        }
        [HttpPost]
        public ActionResult Create(TASK MODEL, TaskStatus status)
        {
            TASKTRACKEREntities DBContext = new TASKTRACKEREntities();
            List<USER> UserList = DBContext.USERS.ToList();
            ViewBag.UserList = UserList.Select(x => new SelectListItem { Value = x.UserID.ToString(), Text = x.Username.ToString() }).ToList();
            var statuslist = new List<SelectListItem>
           {
            new SelectListItem { Text = "Not Started", Value = ((int)TaskStatus.NotStarted).ToString() },
            new SelectListItem { Text = "In Progress", Value = ((int)TaskStatus.InProgress).ToString() },
            new SelectListItem { Text = "Completed", Value = ((int)TaskStatus.Completed).ToString() }
           };


            ViewBag.Status = statuslist;

            if (ModelState.IsValid)
            {
                
                DBContext.TASKs.Add(MODEL);
                DBContext.SaveChanges();
                ViewBag.Messsage = "Record Create Successfully";
                return RedirectToAction("Index");
            }
          
            return View();
        }




    }

      

    
}