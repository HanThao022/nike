using Nike.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nike.Areas.Admin.Controllers
{
    public class SalesController : Controller
    {
        Entities db = new Entities();
        // GET: Admin/Sales
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(DateTime? fromDate, DateTime? toDate)
        {
            if (fromDate == null || toDate == null)
            {
                ViewBag.Message = "Vui lòng nhập ngày bắt đầu và ngày kết thúc";
                return View();
            }
            else
            {
                using (var db = new Entities())
                {
                    // Lấy danh sách sản phẩm bán được trong khoảng thời gian
                    var products = db.OrderDetails
                        .Where(od => od.OrderForm.OrderDate >= fromDate && od.OrderForm.OrderDate <= toDate)
                        .GroupBy(od => od.Product.ProductName)
                        .Select(g => new { ProductName = g.Key, Quantity = g.Sum(od => od.Quantity) })
                        .ToList();

                    // Tạo danh sách tên sản phẩm và số lượng để truyền cho Highcharts
                    var productNames = products.Select(p => p.ProductName).ToArray();
                    var productQuantities = products.Select(p => p.Quantity).ToArray();

                    // Gán dữ liệu cho ViewBag để sử dụng trong View
                    ViewBag.ProductNames = productNames;
                    ViewBag.ProductQuantities = productQuantities;
                    ViewBag.FromDate = fromDate.Value.ToString("dd/MM/yyyy");
                    ViewBag.ToDate = toDate.Value.ToString("dd/MM/yyyy");
                }
                return View();
            }
        }
    }
}