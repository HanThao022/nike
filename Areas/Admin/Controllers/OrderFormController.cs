using Nike.Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Nike.Areas.Admin.Controllers
{
    public class OrderFormController : Controller
    {
        Entities db = new Entities();
        // GET: Admin/OrderForm
        public ActionResult Index(string search, DateTime? fromDate, DateTime? toDate)
        {
            var order = from x in db.OrderForms select x;
            if(search != null)
            {
                if (fromDate != null && toDate != null)
                {
                    //order = order.Where(x => x.OrderDate >= fromDate && x.OrderDate <= toDate);
                    return View(order.Where(x => x.OrderDate >= fromDate && x.OrderDate <= toDate));
                }
                else
                {
                    return View(order);
                    //return View(order.Where(x => x.AccountName.Contains(search)));
                }
            }  
            return View(order);
        }
        [HttpGet]
        public ActionResult Edit(int id) 
        {
            var form = db.OrderForms.Where(p => p.OrderFormId == id).FirstOrDefault();
            return View(form);
        }
        [HttpPost]
        public ActionResult Edit(int id, OrderForm Model)
        {
            var form = db.OrderForms.Where(p => p.OrderFormId == id).FirstOrDefault();
            if (form != null)
            {     
                form.ShipDate = Model.ShipDate;
                form.Active = false;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }
}