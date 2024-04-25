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
           return View(EmpList);
        }
        public ActionResult Create()
        {
            TASKTRACKEREntities DBContext = new TASKTRACKEREntities();

            List<USER> UserList = DBContext.USERS.ToList();
            if (UserList == null)
            {
                
                return RedirectToAction("Index");

            }
           
            ViewBag.UserList = UserList.Select(x => new SelectListItem { Value = x.UserID.ToString(), Text = x.Username.ToString() }).ToList();
            return View(new TASK());

        }
        [HttpPost]
        public ActionResult Create(TASK MODEL)
        {
            TASKTRACKEREntities DBContext = new TASKTRACKEREntities();
            List<USER> UserList = DBContext.USERS.ToList();
            ViewBag.UserList = UserList.Select(x => new SelectListItem { Value = x.UserID.ToString(), Text = x.Username.ToString() }).ToList();
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