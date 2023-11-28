using Nike.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nike.Areas.Admin.Controllers
{
    public class CustomerController : Controller
    {
        Entities db = new Entities();
        // GET: Admin/Customer
        public ActionResult Index()
        {
            var cus = db.Customers.OrderBy(p => p.CustomerId).ToList();
            return View(cus);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(Customer cus)
        {
            cus.CreateDay = DateTime.Now;
            cus.Active = true;
            db.Customers.Add(cus);
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