using Nike.Models;
using Nike.Models.EF;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace Nike.Areas.Admin.Controllers
{
    public class ReportController : Controller
    {
        Entities db = new Entities();
        // GET: Admin/Report
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult List(int page, string fromDate = "", string toDate = "")
        {

            try
            {
                var dateFrom = new DateTime(2023, 11, 11);
                var dateTo = DateTime.Now;
                if (!string.IsNullOrEmpty(fromDate))
                    dateFrom = DateTime.ParseExact(fromDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                if (!string.IsNullOrEmpty(toDate))
                    dateTo = DateTime.ParseExact(toDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                var rps = new List<Report>();
                var records = db.OrderForms.ToList();
                TimeSpan duration = (TimeSpan)(dateTo - dateFrom);
                int numberOfDays = duration.Days;

                for (int i = 0; i <= numberOfDays; i++)
                {

                    var date = dateFrom.AddDays(i);
                    var a = records.ToList();

                    var record = records.Where(r => r.OrderDate >= date && r.OrderDate < date.AddHours(23).AddMinutes(59));

                    if (record.Any())
                    {
                        var report = new Report
                        {
                            DateTime = date,
                            Total = (int)record.Sum(r => r.TotalAmount),
                        };
                        rps.Add(report);
                    }

                }

                //Xử lí phân trang
                var z = records.ToList();
                //Số dữ liệu trên 1 trang
                int pageSize = 10;
                page = (page > 0) ? page : 1;
                int start = (int)(page - 1) * pageSize;

                ViewBag.pageCurrent = page;
                int totalBill = rps.Count();
                float totalNumsize = (totalBill / (float)pageSize);

                int numSize = (int)Math.Ceiling(totalNumsize);

                var rs = rps.OrderByDescending(d => d.DateTime).Skip(start).Take(pageSize);

                var fromto = PaginationExtension.FromTo(totalBill, (int)page, pageSize);

                int from = fromto.Item1;
                int to = fromto.Item2;
                return Json(
                        new
                        {
                            status = 200,
                            rps = rs,
                            pageCurrent = page,
                            numSize,
                            total = totalBill,
                            size = pageSize,
                            from,
                            to
                        }
                        , JsonRequestBehavior.AllowGet
                        );
            }
            catch (Exception e)
            {
                return Json(
                        new
                        {
                            status = 500,
                            message = "Lỗi" + e.Message

                        }
                        , JsonRequestBehavior.AllowGet
                        );
            }
        }
        public ActionResult ExportToExcel(string start = "", string end = "")
        {
            List<Report> data = new List<Report>();
            var dateFrom = new DateTime(2023, 10, 10);
            var dateTo = DateTime.Now;
            if (!string.IsNullOrEmpty(start))
                dateFrom = DateTime.ParseExact(start, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            if (!string.IsNullOrEmpty(end))
                dateTo = DateTime.ParseExact(end, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            var records = db.OrderForms.ToList();
            TimeSpan duration = dateTo - dateFrom;
            int numberOfDays = duration.Days;
            for (int i = 0; i <= numberOfDays; i++)
            {

                var date = dateFrom.AddDays(i);
                var a = records.ToList();

                var record = records.Where(r => r.OrderDate >= date && r.OrderDate < date.AddHours(23).AddMinutes(59));

                if (record.Any())
                {
                    var report = new Report
                    {
                        DateTime = date,
                        Total = (int)record.Sum(r => r.TotalAmount),
                    };
                    data.Add(report);
                }

            }
            // Lấy dữ liệu từ cơ sở dữ liệu
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                // Thêm tiêu đề cho các cột
                worksheet.Cells[1, 1].Value = "Ngày";
                worksheet.Cells[1, 2].Value = "Tổng doanh thu ngày";

                // Thêm dữ liệu từ data vào các ô
                for (int i = 0; i < data.Count; i++)
                {
                    worksheet.Cells[i + 2, 1].Value = data[i].DateTime.ToString("dd/MM/yyyy");
                    worksheet.Cells[i + 2, 2].Value = data[i].Total.ToString("#,##0");
                }
                var headerCells = worksheet.Cells[1, 1, 1, worksheet.Dimension.Columns];
                worksheet.View.FreezePanes(2, 1);
                // Set their text to bold, italic and underline.
                headerCells.Style.Font.Bold = true;
                headerCells.Style.Fill.PatternType = ExcelFillStyle.Solid;
                headerCells.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow);
                worksheet.Cells["A:AZ"].AutoFitColumns();
                var range = worksheet.Cells[worksheet.Dimension.Address];
                range.AutoFilter = true;
                ///Setting thêm cho sheet detail
                //Select only the header cells
                // Lưu package thành file Excel
                var stream = new MemoryStream(package.GetAsByteArray());
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report-data.xlsx");
            }
        }
    }
}