using Nike.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nike.Areas.Admin.Controllers
{
    public class BrandController : Controller
    {
        Entities db = new Entities();
        // GET: Admin/Brand
        public ActionResult Index()
        {
            var brand = db.Brands.OrderBy(p => p.BrandId).ToList();
            return View(brand);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(Brand brand)
        {
            db.Brands.Add(brand);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var brand = db.Brands.Where(p => p.BrandId == id).FirstOrDefault();

            return View(brand);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(int id, Brand Model)
        {
            var brand = db.Brands.Where(p => p.BrandId == id).FirstOrDefault();
            if (brand != null)
            {
                brand.Name = Model.Name;
                brand.Describe = Model.Describe;
                brand.Active = Model.Active;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var brand = db.Brands.Where(n => n.BrandId == id).FirstOrDefault();
            return View(brand);
        }
        [HttpPost]
        public ActionResult Remove(int id)
        {
            var brand = db.Brands.Where(n => n.BrandId == id).FirstOrDefault();

            db.Brands.Remove(brand);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}