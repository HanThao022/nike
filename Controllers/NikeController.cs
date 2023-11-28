using Nike.Models;
using Nike.Models.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.Security.Cryptography;
using System.Runtime.CompilerServices;
using System.Reflection;

namespace Nike.Controllers
{
    public class NikeController : Controller
    {
        Entities db = new Entities();
        // GET: Nike
        public ActionResult Index()
        {
            var listProduct = db.Products.ToList();
            var listPType = db.ProductTypes.ToList();

            Product_ProductType product_ProductType = new Product_ProductType();

            product_ProductType.ListProduct = listProduct;
            product_ProductType.ListProductTypes = listPType;
            //viewBag truyền cho view
            ViewBag.ListProductTypes = listPType;

            return View(product_ProductType);
        }

        [HttpGet]
        public JsonResult NewProduct()
        {
            try
            {
                var products = db.Products.Select(a => new
                {
                    a.ProductId,
                    a.ProductName,
                    a.Describe,
                    a.Image,
                    a.Warranty,
                    a.Size,
                    a.Color,
                    a.Price,
                    a.ProductTypeId,
                    a.CreateBy,
                    a.CreateDay,
                    a.Active
                }).OrderByDescending(a => a.CreateDay).Take(8);

                return Json(new
                {
                    status = 200,
                    products
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = 500, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Details (int id)
        {
            var product = db.Products.Where( x => x.ProductId == id).FirstOrDefault();
            return View(product);
        }
        [HttpGet]
        public JsonResult Different(string id)
        {
            try
            {
                var Id = int.Parse(id);
                var dff = db.Products.Where(d => d.ProductTypeId == Id && d.ProductId != Id).Select(a => new
                {
                    a.ProductId,
                    a.ProductName,
                    a.Describe,
                    a.Image,
                    a.Warranty,
                    a.Size,
                    a.Color,
                    a.Price,
                    a.ProductTypeId,
                    a.CreateBy,
                    a.CreateDay,
                    a.Active
                }).OrderBy(a => a.ProductTypeId ).ToList();
                return Json(new
                {
                    status = 200,
                    dff
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = 500, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult Sell()
        {
            try
            {
                var sell = db.Products.Where(a => a.OrderDetails.Select(b => b.Quantity).Sum()>5).Select(a => new
                {
                    a.ProductId,
                    a.ProductName,
                    a.Describe,
                    a.Image,
                    a.Warranty,
                    a.Size,
                    a.Color,
                    a.Price,
                    a.ProductTypeId,
                    a.CreateBy,
                    a.CreateDay,
                    a.Active
                });
                return Json(new
                {
                    status = 200,
                    sell
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = 500, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult CDCategories(string search, int id = 0, int page = 0)
        {
            try
            {
                var products = db.Products.Select(p => new
                {
                    p.ProductName,
                    p.ProductId,
                    p.Image,
                    p.Price,
                    p.CreateDay,
                    p.ProductTypeId
                });

                if(id != 0)
                {
                    products = products.Where(x => x.ProductTypeId == id);
                }
                if (!String.IsNullOrEmpty(search))
                {
                    ViewBag.txtSearch = search;
                    products = products.Where(p => p.ProductName.Contains(search));
                }

                //Phân trang
                int pageSize = 9;
                page = (page > 0) ? page : 1;
                int start = (int)(page - 1) * pageSize;
               
                float totalNumsize = (products.Count() / (float)pageSize);
                bool isFull = (int)Math.Ceiling(totalNumsize) <= page;
                int numSize = (int)Math.Ceiling(totalNumsize);
                products = products.OrderByDescending(d => d.CreateDay).Skip(start).Take(pageSize);


                return Json(new { status = true, data = products, numSize, isFull }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex }, JsonRequestBehavior.AllowGet);
            }
        }        
    }
}