using Microsoft.Ajax.Utilities;
using Nike.Models.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nike.Areas.Admin.Controllers
{
    public class ProductTypeController : Controller
    {
        Entities db = new Entities();
        // GET: Admin/ProductType
        public ActionResult Index()
        {
            var productType = db.ProductTypes.OrderBy(p => p.ProductTypeId).ToList();

            return View(productType);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(ProductType productType)
        {
            db.ProductTypes.Add(productType);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var productType = db.ProductTypes.Where(p => p.ProductTypeId == id).FirstOrDefault();

            return View(productType);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(int id, ProductType Model)
        {
            var productType = db.ProductTypes.Where(p => p.ProductTypeId == id).FirstOrDefault();
            if (productType != null)
            {
                productType.ProductTypeName = Model.ProductTypeName;
                productType.Note = Model.Note;
                productType.Active = Model.Active;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var productType = db.ProductTypes.Where(n => n.ProductTypeId == id).FirstOrDefault();
            return View(productType);
        }
        [HttpPost]
        public ActionResult Remove(int id)
        {
            var productType = db.ProductTypes.Where(n => n.ProductTypeId == id).FirstOrDefault();

            db.ProductTypes.Remove(productType);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}