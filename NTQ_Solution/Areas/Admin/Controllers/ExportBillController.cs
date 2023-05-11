using DataLayer.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTQ_Solution.Areas.Admin.Controllers
{
    public class ExportBillController : BaseController
    {
        ExportBillData exportBillData;
        ProductData productData;
        public ExportBillController()
        {
            exportBillData = new ExportBillData();
            productData = new ProductData();
        }
        // GET: Admin/ExportBill
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            try
            {
                ViewBag.listColor = productData.listcolor();
                ViewBag.listSize = productData.listsize();
                ViewBag.SearchString = searchString;
                var model = exportBillData.ListAllExportBill(searchString, page, pageSize);
                double? total = 0;
                double? total2 = 0;
                foreach(var item in model )
                {
                    total += (item.Price-20000);
                    total2 += (item.Price - item.Count * item.ImportPrice - 20000);
                }
                ViewBag.total = total;
                ViewBag.total2 = total2;
                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }
    }
}