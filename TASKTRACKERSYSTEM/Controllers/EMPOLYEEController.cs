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
        public ActionResult Create(TASK MODEL, TaskStatus?status)
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
                TempData["message"] = "Record Create Successfully";    
            }
            
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int  id)
        {
            TASKTRACKEREntities DBContext = new TASKTRACKEREntities();
            var data = DBContext.TASKs.Where(x => x.TASKID == id).FirstOrDefault();
           
            if (data == null)
            {
                return HttpNotFound();
            }
            

            List<USER> UserList = DBContext.USERS.ToList();
            ViewBag.UserList = UserList.Select(x => new SelectListItem { Value = x.UserID.ToString(), Text = x.Username.ToString() }).ToList();
            //ViewBag.DueTo = data.DUETO.HasValue ? data.DUETO.Value.ToString("dd-MM-yyyy") : null;
           

            List<SelectListItem> statusList = new List<SelectListItem>
            {
                 new SelectListItem { Text = "Not Started", Value = ((int)TaskStatus.NotStarted).ToString() },
                 new SelectListItem { Text = "In Progress", Value = ((int)TaskStatus.InProgress).ToString() },
                 new SelectListItem { Text = "Completed", Value = ((int)TaskStatus.Completed).ToString() }
            };

             ViewBag.StatusList = new SelectList(statusList, "Value", "Text", data.STATUS.ToString());
            //var shortDueTo = data.DUETO.HasValue ? data.DUETO.Value.ToString("d") : null;
   


                return View(data);
        }
        [HttpPost]
        public ActionResult Edit(TASK MODEL, int id)
        {
            TASKTRACKEREntities DBContext = new TASKTRACKEREntities();
            var data = DBContext.TASKs.Where(x => x.TASKID == id).FirstOrDefault();
            if (data == null)
            {
                return HttpNotFound();
            }
            

            List<USER> UserList = DBContext.USERS.ToList();
            ViewBag.UserList = UserList.Select(x => new SelectListItem { Value = x.UserID.ToString(), Text = x.Username.ToString() }).ToList();
            List<SelectListItem> statusList = new List<SelectListItem>
            {
               new SelectListItem { Text = "Not Started", Value = TaskStatus.NotStarted.ToString() },
               new SelectListItem { Text = "In Progress", Value = TaskStatus.InProgress.ToString() },
               new SelectListItem { Text = "Completed", Value = TaskStatus.Completed.ToString() }
            };
            ViewBag.StatusList = new SelectList(statusList, "Value", "Text", data.STATUS.ToString());
            if (ModelState.IsValid)
           {
                if (data != null)
                {
                    data.TASKNAME = MODEL.TASKNAME;
                    data.ASSIGNEDTO = MODEL.ASSIGNEDTO;
                    if (MODEL.DUETO.HasValue)
                    {
                        data.DUETO = MODEL.DUETO.Value.Date;
                    }
                    //data.DUETO = MODEL.DUETO;
                    data.STATUS = MODEL.STATUS;
                    DBContext.SaveChanges();
                    //ViewBag.Message = "Record Update Successfully";
                    TempData["message"] = "Record Updated Successfully";
                  
                }
                
            }
           
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            TASKTRACKEREntities DBContext = new TASKTRACKEREntities();
            var data = DBContext.TASKs.Where(x => x.TASKID == id).FirstOrDefault();
            DBContext.TASKs.Remove(data);
            DBContext.SaveChanges();
            TempData["message"] = "Record Deleted Successfully";
            return RedirectToAction("Index");
        }


        
    }


}









