using Nike.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nike.Areas.Admin.Controllers
{
    public class AccountTypeController : Controller
    {
        Entities db = new Entities();
        // GET: Admin/AccountType
        public ActionResult Index()
        {
            var accountType = db.AccountTypes.OrderBy(p => p.AccountTypeId).ToList();
            return View(accountType);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(AccountType accountType)
        {
            db.AccountTypes.Add(accountType);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var accountType = db.AccountTypes.Where(p => p.AccountTypeId == id).FirstOrDefault();

            return View(accountType);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(int id, AccountType Model)
        {
            var accountType = db.AccountTypes.Where(p => p.AccountTypeId == id).FirstOrDefault();
            if (accountType != null)
            {
                accountType.Name = Model.Name;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var accountType = db.AccountTypes.Where(n => n.AccountTypeId == id).FirstOrDefault();
            return View(accountType);
        }
        [HttpPost]
        public ActionResult Remove(int id)
        {
            var accountType = db.AccountTypes.Where(n => n.AccountTypeId == id).FirstOrDefault();

            db.AccountTypes.Remove(accountType);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}