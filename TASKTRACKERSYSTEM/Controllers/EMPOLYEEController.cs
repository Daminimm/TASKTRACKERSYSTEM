using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TASKTRACKERSYSTEM.Models;

namespace TASKTRACKERSYSTEM.Controllers
{
    public class EMPOLYEEController : Controller
    {
        //GET: EMPOLYEE
        public ActionResult Index(string SearchData)
        {

            if (TempData["message"] != null)
            {
                ViewBag.Message = TempData["message"];
            }

            using (TASKTRACKEREntities DBContext = new TASKTRACKEREntities())
            {
                IQueryable<TASK> Employe = DBContext.TASKs;

                if (!String.IsNullOrEmpty(SearchData))
                {
                    Employe = Employe.Where(s => s.TASKNAME.Contains(SearchData));
                    List<TaskViewModel> viewModelList = Employe.Select(task => new TaskViewModel
                    {
                        TASKID = task.TASKID,
                        TASKNAME = task.TASKNAME,
                        ASSIGNEDTO = task.ASSIGNEDTO,
                        DUETO = task.DUETO,
                        STATUS = (TaskStatus)task.STATUS, // Convert int to TaskStatus enum
                        USER = task.USER
                    }).ToList();

                    return View(viewModelList);
                }
                else
                {
                    List<TaskViewModel> viewModelList = Employe.Select(task => new TaskViewModel
                    {
                        TASKID = task.TASKID,
                        TASKNAME = task.TASKNAME,
                        ASSIGNEDTO = task.ASSIGNEDTO,
                        DUETO = task.DUETO,
                        STATUS = (TaskStatus)task.STATUS, // Convert int to TaskStatus enum
                        USER = task.USER
                    }).ToList();

                    return View(viewModelList);
                }
            }

        }


        [HttpGet]
        public ActionResult Create()
        {

            TASKTRACKEREntities DBContext = new TASKTRACKEREntities();
            List<USER> UserList = DBContext.USERS.ToList();
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
        [ValidateAntiForgeryToken]
        public ActionResult Create(TASK MODEL, TaskStatus?status)
        {
            TASKTRACKEREntities DBContext = new TASKTRACKEREntities();
            List<USER> UserList = DBContext.USERS.ToList();
            //var duetodate = MODEL.DUETO.Value.Date;

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
                TempData["message"] = "Task Create Successfully";
                return RedirectToAction("Index");
            }
            
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            TASKTRACKEREntities DBContext = new TASKTRACKEREntities();
            TASK data = new TASK(); 
            data = DBContext.TASKs.Where(x => x.TASKID == id).FirstOrDefault();
            List<USER> UserList = DBContext.USERS.ToList();
            ViewBag.UserList = UserList.Select(x => new SelectListItem { Value = x.UserID.ToString(), Text = x.Username.ToString() }).ToList();
            List<SelectListItem> statusList = new List<SelectListItem>
            {
                 new SelectListItem { Text = "Not Started", Value = ((int)TaskStatus.NotStarted).ToString() },
                 new SelectListItem { Text = "In Progress", Value = ((int)TaskStatus.InProgress).ToString() },
                 new SelectListItem { Text = "Completed", Value = ((int)TaskStatus.Completed).ToString() }
            };
            ViewBag.StatusList = new SelectList(statusList, "Value", "Text", data.STATUS.ToString());
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(TASK MODEL, int id)
        {
            TASKTRACKEREntities DBContext = new TASKTRACKEREntities();
            var data = DBContext.TASKs.Where(x => x.TASKID == id).FirstOrDefault();
            List<USER> UserList = DBContext.USERS.ToList();
            ViewBag.UserList = UserList.Select(x => new SelectListItem { Value = x.UserID.ToString(), Text = x.Username.ToString() }).ToList();
            List<SelectListItem> statusList = new List<SelectListItem>
            {
               new SelectListItem { Text = "Not Started", Value = TaskStatus.NotStarted.ToString() },
               new SelectListItem { Text = "In Progress", Value = TaskStatus.InProgress.ToString() },
               new SelectListItem { Text = "Completed", Value = TaskStatus.Completed.ToString() }
            };
            ViewBag.StatusList = new SelectList(statusList, "Value", "Text", data.STATUS.ToString());
            if (data != null)
            {
                data.TASKNAME = MODEL.TASKNAME;
                data.ASSIGNEDTO = MODEL.ASSIGNEDTO;
                data.DUETO = MODEL.DUETO;
                data.STATUS = MODEL.STATUS;

            }

            if (ModelState.IsValid)
            {

                TempData["message"] = "Task Updated Successfully";
                DBContext.SaveChanges();
                return RedirectToAction("Index");

            }

            return View(data);
        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            TASKTRACKEREntities DBContext = new TASKTRACKEREntities();
            var data = DBContext.TASKs.Where(x => x.TASKID == id).FirstOrDefault();
            DBContext.TASKs.Remove(data);
            DBContext.SaveChanges();
            TempData["message"] = "Task Deleted Successfully";
            return RedirectToAction("Index");
        }



    }


}









