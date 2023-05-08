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
        ExportBillDao exportBillDao;
        ProductDao productDao;
        public ExportBillController()
        {
            exportBillDao = new ExportBillDao();
            productDao = new ProductDao();
        }
        // GET: Admin/ExportBill
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            try
            {
                ViewBag.listColor = productDao.listcolor();
                ViewBag.listSize = productDao.listsize();
                ViewBag.SearchString = searchString;
                var model = exportBillDao.ListAllExportBill(searchString, page, pageSize);
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