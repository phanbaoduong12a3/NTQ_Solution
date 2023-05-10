using DataLayer.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTQ_Solution.Areas.Admin.Controllers
{
    public class ImportBillController : BaseController
    {
        ImportBillDao importBillDao;
        ProductDao productDao;
        public ImportBillController()
        {
            importBillDao = new ImportBillDao();
            productDao = new ProductDao();  
        }
        // GET: Admin/ImportBill
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            try
            {
                ViewBag.listColor = productDao.listcolor();
                ViewBag.listSize = productDao.listsize();
                ViewBag.SearchString = searchString;
                var model = importBillDao.ListAllImportBill(searchString, page, pageSize);
                double? total = 0;
                foreach(var item in model){
                    total += item.Price;
                }
                ViewBag.total = total;
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