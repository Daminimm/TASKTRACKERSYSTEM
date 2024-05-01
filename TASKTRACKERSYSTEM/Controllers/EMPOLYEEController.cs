﻿using System;
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
        [HttpGet]
        public ActionResult Create(int? id)
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
                ViewBag.Messsage = "Record Create Successfully";
                return RedirectToAction("Index");
            }
          
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int  id)
        {
            TASKTRACKEREntities DBContext = new TASKTRACKEREntities();
            var data = DBContext.TASKs.Where(x => x.TASKID == id).FirstOrDefault();
            //TASK task = DBContext.TASKs.Find(id);
            if (data == null)
            {
                return HttpNotFound();
            }

            List<USER> UserList = DBContext.USERS.ToList();
            ViewBag.UserList = UserList.Select(x => new SelectListItem { Value = x.UserID.ToString(), Text = x.Username.ToString() }).ToList();
            //var statuslist = new List<SelectListItem>
            //{
            //  new SelectListItem { Text = "Not Started", Value = ((int)TaskStatus.NotStarted).ToString() },
            //  new SelectListItem { Text = "In Progress", Value = ((int)TaskStatus.InProgress).ToString() },
            //  new SelectListItem { Text = "Completed", Value = ((int)TaskStatus.Completed).ToString() }
            //};
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
            if (data != null)
            {
                data.TASKNAME = MODEL.TASKNAME;
                data.ASSIGNEDTO = MODEL.ASSIGNEDTO;
                data.DUETO = MODEL.DUETO;
                data.STATUS = MODEL.STATUS;
                DBContext.SaveChanges();
            

            }
            ViewBag.Messsage = "Record Update Successfully";
            return RedirectToAction("Index");

        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            TASKTRACKEREntities DBContext = new TASKTRACKEREntities();
            var data = DBContext.TASKs.Where(x => x.TASKID == id).FirstOrDefault();
            DBContext.TASKs.Remove(data);
            DBContext.SaveChanges();
            ViewBag.Messsage = "Record Remove Successfully";
            return RedirectToAction("Index");
        }


        
    }






}








