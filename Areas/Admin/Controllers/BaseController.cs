using Nike.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Nike.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        Entities db = new Entities();
        // GET: Admin/Base
        public ActionResult Index()
        {
            return View();
        }
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
        public ActionResult LoginAdmin()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult LoginAdmin(Account account)
        {
            var acc = account.AccountName;
            var pass = account.Password;
            var accountCheck = db.Accounts.SingleOrDefault(x => x.AccountName.Equals(acc) && x.Password.Equals(pass));
            
            if (string.IsNullOrEmpty(acc) || string.IsNullOrEmpty(pass))
            {
                return Json(
                new {status = "error", message = "Vui lòng nhập đầy đủ thông tin!"}, JsonRequestBehavior.AllowGet);
            }
            if (accountCheck == null)
            {
                return Json(
                new { status = "error", message = "Tài khoản của bạn không tồn tại vui lòng kiểm tra lại!" }, JsonRequestBehavior.AllowGet);
            }

            Console.WriteLine($"AccountName: {accountCheck.AccountName}, AccountTypeId: {accountCheck.AccountTypeId}");

            // Check if the account has the appropriate role (Assuming 2 is the ID for the admin account type)
            if (accountCheck.AccountTypeId != 2)
            {
                return Json(new { status = "error", message = "Bạn không có quyền truy cập!" });
            }
            Session["Account"] = accountCheck;

            return Json( new { status = "success", message = "Đăng nhập thành công!"}, JsonRequestBehavior.AllowGet);
        }        
    }
}