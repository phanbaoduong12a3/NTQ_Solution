using DataLayer.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTQ_Solution.Controllers
{
    public class ProductController : Controller
    {
        ProductDao productDao ;
        public ProductController()
        {
            productDao= new ProductDao();
        }
        // GET: Product
        public ActionResult Index(string trending, string searchString, int page = 1, int pageSize = 4)
        {
            try
            {
                ViewBag.SearchString = searchString;
                var model = productDao.ListProductOnSale(trending, searchString, page, pageSize);
                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        [ChildActionOnly]
        public PartialViewResult ProductCategory()
        {
            var model = productDao.ListCategory();
            return PartialView(model);
        }
        public ActionResult Category(int categoryID,int page=1, int pageSize = 4)
        {
            var model = productDao.Category(categoryID, page, pageSize);
            ViewBag.categoryID = categoryID;
            return View(model);
        }
    }
}