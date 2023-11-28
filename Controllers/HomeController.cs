using Nike.Models.EF;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;

namespace Nike.Controllers
{
    public class HomeController : Controller
    {
        Entities db = new Entities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

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
        public ActionResult Register()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register (Account account)
        {
            if (ModelState.IsValid)
            {
                var check = db.Accounts.FirstOrDefault(a => a.AccountId == account.AccountId);
                if (check == null)
                {
                    account.AccountTypeId = 1;
                    account.Password = GetMD5(account.Password);
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.Accounts.Add(account);
                    db.SaveChanges();

                    // Thêm thông tin từ Account vào Customer
                    Customer customer = new Customer
                    {
                        AccountId = account.AccountId,
                        CreateDay = DateTime.Now,
                        Active = true,
                    };

                    db.Customers.Add(customer);
                    db.SaveChanges();

                    return RedirectToAction("Login");
                }
                else
                {
                    //ViewBag.error = "Email already exists";
                    return View("Register");
                }
            }
            return View();
        }
        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Account account)
        {
            var acc = account.AccountName;
            var pass = GetMD5(account.Password);
            var accountCheck = db.Accounts.SingleOrDefault(x => x.AccountName.Equals(acc) && x.Password.Equals(pass));

            if (accountCheck != null )
            { 
                
                if (accountCheck.AccountTypeId == 1)
                {
                    Session["Account"] = accountCheck;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View("Login");
                }
            }
            else
            {
                return View("Login");
            }
        }
        public ActionResult Logout()
        {
            Session["Account"] = null;
            Session["Cart"] = null;
            return RedirectToAction("Index", "Home");
        }

        
    }
}