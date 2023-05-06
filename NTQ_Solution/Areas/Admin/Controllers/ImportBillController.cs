using DataLayer.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTQ_Solution.Areas.Admin.Controllers
{
    public class ImportBillController : Controller
    {
        ImportBillDao importBillDao;
        public ImportBillController()
        {
            importBillDao = new ImportBillDao();
        }
        // GET: Admin/ImportBill
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            try
            {
                ViewBag.SearchString = searchString;
                var model = importBillDao.ListAllImportBill(searchString, page, pageSize);
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