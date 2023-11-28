using Newtonsoft.Json;
using Nike.Models;
using Nike.Models.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;

namespace Nike.Controllers
{
    public class CartController : Controller
    {
        Entities db = new Entities();
        // GET: Cart
        public ActionResult Index()
        {
            return View((List<Cart>)Session["cart"]);
        }
        public JsonResult AddToCart(int id, int quantity)
        {
            if (Session["cart"] == null)
            {
                List<Cart> cart = new List<Cart>();
                cart.Add(new Cart { Product = db.Products.Find(id), Quantity = quantity });
                Session["cart"] = cart;
            }
            else
            {
                List<Cart> cart = (List<Cart>)Session["cart"];
                //kiểm tra tồn tại 
                int index = isExist(id);
                if (index != -1)
                {
                    //nếu tồn tại thì cộng thêm số lượng
                    cart[index].Quantity += quantity;                    
                }
                else
                {
                    //nếu không tồn tại thì thêm sản phẩm vào giỏ hàng
                    cart.Add(new Cart { Product = db.Products.Find(id), Quantity = quantity, });
                    //Tính lại số sản phẩm trong giỏ hàng
                    Session["count"] = Convert.ToInt32(Session["count"]) + 1;
                    
                }
                Session["cart"] = cart;

            }
            return Json(new { Message = "Thành công", JsonRequestBehavior.AllowGet });
        }
        private int isExist(int id)
        {
            List<Cart> cart = (List<Cart>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].Product.ProductId.Equals(id))
                    return i;
            return -1;
        }
        public JsonResult Remove(int Id)
        {
            List<Cart> li = (List<Cart>)Session["cart"];
            li.RemoveAll(x => x.Product.ProductId == Id);
            Session["cart"] = li;
            Session["count"] = Convert.ToInt32(Session["count"]) - 1;
            return Json(new { Message = "Thành công", JsonRequestBehavior.AllowGet });
        }

        public JsonResult UpdateTotal(int id, int quantity)
        {
            List<Cart> cart = Session["cart"] as List<Cart>;
            Cart product = cart.FirstOrDefault(m => m.Product.ProductId == id);
            if (product != null)
            {
                product.Quantity = quantity;
            }
            return Json(new { Message = "Thành công", JsonRequestBehavior.AllowGet });
        }
    }
}