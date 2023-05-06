using DataLayer.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTQ_Solution.Areas.Admin.Controllers
{
    public class ExportBillController : Controller
    {
        ExportBillDao exportBillDao;
        public ExportBillController()
        {
            exportBillDao = new ExportBillDao();
        }
        // GET: Admin/ExportBill
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            try
            {
                ViewBag.SearchString = searchString;
                var model = exportBillDao.ListAllExportBill(searchString, page, pageSize);
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