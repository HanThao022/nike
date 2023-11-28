using Nike.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nike.Areas.Admin.Controllers
{
    public class StaffController : Controller
    {
        Entities db = new Entities();
        // GET: Admin/Staff
        public ActionResult Index()
        {
            var staff = db.Staffs.OrderBy(p => p.StaffId).ToList();
            return View(staff);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(Staff staff)
        {
            staff.CreateDay = DateTime.Now;
            staff.Active = true;
            db.Staffs.Add(staff);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var staff = db.Staffs.Where(p => p.StaffId == id).FirstOrDefault();

            return View(staff);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(int id, Staff Model)
        {
            var staff = db.Staffs.Where(p => p.StaffId == id).FirstOrDefault();
            if (staff != null)
            {
                staff.FullName = Model.FullName;
                staff.Birthday = Model.Birthday;
                staff.Address = Model.Address;
                staff.Phone = Model.Phone;
                staff.Email = Model.Email;
                staff.AccountId = Model.AccountId;
                staff.Active = Model.Active;
                staff.CreateDay = DateTime.Now;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var staff = db.Staffs.Where(n => n.StaffId == id).FirstOrDefault();
            return View(staff);
        }
        [HttpPost]
        public ActionResult Remove(int id)
        {
            var staff = db.Staffs.Where(n => n.StaffId == id).FirstOrDefault();

            db.Staffs.Remove(staff);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}