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
        // GET: Product
        public ActionResult Index(string trending, string searchString, int page = 1, int pageSize = 8)
        {
            try
            {
                var dao = new ProductDao();
                var model = dao.ListAllPagingProduct(trending, searchString, page, pageSize);
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