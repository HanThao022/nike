using Nike.Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Nike.Areas.Admin.Controllers
{    
    public class ProductController : Controller
    {
        Entities db = new Entities();
        // GET: Admin/Product
        public ActionResult Index(string curentFilter, string search, int? page)
        {
            var product = db.Products.OrderBy(p => p.ProductId).ToList();
            if (search != null)
            {
                page = 1;
            }
            else
            {
                search = curentFilter;
            }
            if (!string.IsNullOrEmpty(search))
            {
                product = db.Products.Where(n => n.ProductName.Contains(search)).ToList();
            }
            else
            {
                product = db.Products.ToList();
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(product.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            List<ProductType> cate = db.ProductTypes.ToList();

            // Tạo SelectList
            SelectList cateList = new SelectList(cate, "ProductTypeId", "ProductTypeName");

            // Set vào ViewBag
            ViewBag.CategoryList = cateList;
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(Product product, HttpPostedFileBase Image)
        {
            try
            {
                if (Image != null)
                {
                    string path = Path.Combine(Server.MapPath("~/Asset/Client/image"), Path.GetFileName(Image.FileName));
                    Image.SaveAs(path);
                }
                ViewBag.FileStatus = "Tải lên thành công";
            }
            catch (Exception)
            {
                ViewBag.FileStatus = "Error"; 
            }
            product.CreateBy = "Admin";
            product.CreateDay = DateTime.Now;
            product.Active = true;
            db.Products.Add(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var product = db.Products.Where(p => p.ProductId == id).FirstOrDefault();
            List<ProductType> cate = db.ProductTypes.ToList();

            SelectList cateList = new SelectList(cate, "ProductTypeId", "ProductTypeName");

            ViewBag.CategoryList = cateList;
            return View(product);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(int id, Product Model, HttpPostedFileBase Image)
        {
            var product = db.Products.Where(p => p.ProductId == id).FirstOrDefault();
            if(product != null)
            {
                product.ProductName = Model.ProductName;
                product.Describe = Model.Describe;
                try
                { //có hình ảnh
                    if (Image != null)
                    {
                        string path = Path.Combine(Server.MapPath("~/Asset/Client/image"), Path.GetFileName(Image.FileName));
                        Image.SaveAs(path);
                        product.Image = Image.FileName;
                    }
                       
                    ViewBag.FileStatus = "Tải lên thành công";
                    
                }
                catch (Exception)
                {
                    ViewBag.FileStatus = "Error"; ;
                }
                //product.Image = Image.FileName;
                product.Warranty = Model.Warranty;
                product.Size = Model.Size;
                product.Color = Model.Color;
                product.Price = Model.Price;
                product.ProductTypeId = Model.ProductTypeId;
                product.Active = Model.Active;
                db.SaveChanges();
            }
            
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var product = db.Products.Where(n => n.ProductId == id).FirstOrDefault();
            return View(product);
        }
        [HttpPost]
        public ActionResult Remove(int id)
        {
            var product = db.Products.Where(n => n.ProductId == id).FirstOrDefault();

            db.Products.Remove(product);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}