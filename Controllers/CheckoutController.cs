using Nike.Models;
using Nike.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;

namespace Nike.Controllers
{
    public class CheckoutController : Controller
    {
        Entities db = new Entities();
        // GET: Checkout
        public ActionResult Index()
        {
            
            return View((List<Cart>)Session["cart"]);

        }
        [HttpGet]
        public ActionResult Checkout(string tt) 
        {
            //gán
            tt = tt.Replace(",", "");
            OrderForm form = new OrderForm();
            Account acc = (Account)Session["Account"];

            Customer customerInfo = db.Customers.FirstOrDefault(c => c.AccountId == acc.AccountId);

            if (customerInfo != null)
            {
                // Sao chép thông tin từ Customer vào OrderForm
                form.Customer = customerInfo;
                form.OrderDate = DateTime.Now;
                form.TotalAmount = float.Parse(tt);
                form.Active = true;

                // Lưu OrderForm vào cơ sở dữ liệu
                db.OrderForms.Add(form);
                db.SaveChanges();

                int orderFormId = form.OrderFormId;
                var lstCart = (List<Cart>)Session["cart"];
                List<OrderDetail> lstOD = new List<OrderDetail>();

                foreach (var item in lstCart)
                {
                    OrderDetail detail = new OrderDetail();
                    detail.Quantity = item.Quantity;
                    detail.OrderFormId = orderFormId;
                    detail.ProductId = item.Product.ProductId;
                    detail.Price = float.Parse(tt);
                    lstOD.Add(detail);
                }

                // Lưu OrderDetails vào cơ sở dữ liệu
                db.OrderDetails.AddRange(lstOD);
                db.SaveChanges();

                return View();
            }
            else
            {
                // Xử lý trường hợp không tìm thấy thông tin khách hàng
                return RedirectToAction("Error");
            }
        }
    }
}
     