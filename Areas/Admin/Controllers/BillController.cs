using Nike.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nike.Areas.Admin.Controllers
{
    public class BillController : Controller
    {
        Entities db = new Entities();
        // GET: Admin/Bill
        public ActionResult Index()
        {
            return View();
        }
        //[HttpPost]
        //public ActionResult Create(Collection_invoice coInvoice)
        //{
        //    if (coInvoice.Invoice != null)
        //    {
        //        Invoice inv = new Invoice();
        //        inv.inv_date = coInvoice.Invoice.inv_date;
        //        inv.net = coInvoice.Invoice.net;
        //        inv.total = coInvoice.Invoice.total;
        //        inv.discount = coInvoice.Invoice.discount;

        //        int code = inv.AddNewInvoice(inv);

        //        coInvoice.InvoiceDetail.code = code;
        //        coInvoice.InvoiceDetail.itemName = db.Items.SingleOrDefault(im => im.id == coInvoice.InvoiceDetail.itemId).name;
        //        db.InvoiceDetails.Add(coInvoice.InvoiceDetail);
        //        db.SaveChanges();
        //    }
        //    else
        //    {
        //        var VendorList = db.Vendors.ToList();
        //        var invoice_detalis = new Models.Collection_invoice
        //        {
        //            Vendor = VendorList
        //        };
        //        return View("Create", invoice_detalis);
        //    }
        //    int? page = null;
        //    return View("List", db.Invoices.ToList().ToPagedList(page ?? 1, 3));
        //}
    }
}