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
        public ActionResult Index(string trending, string searchString, int page = 1, int pageSize = 5)
        {
            try
            {
                var model = productDao.ListProductOnSale(trending, searchString, page, pageSize);
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