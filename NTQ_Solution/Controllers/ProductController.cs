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
        ProductData productData ;
        public ProductController()
        {
            productData= new ProductData();
        }
        // GET: Product
        public ActionResult Index(string trending, string searchString, int page = 1, int pageSize = 9)
        {
            try
            {
                ViewBag.SearchString = searchString;
                var model = productData.ListProductOnSale(trending, searchString, page, pageSize);
                ViewBag.HotProduct = productData.ListNewProduct(4);
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
            var model = productData.ListCategory();
            return PartialView(model);
        }
        public ActionResult Category(int categoryID,int page=1, int pageSize = 8)
        {
            var category = productData.Category(categoryID, page, pageSize);
            ViewBag.HotProduct = productData.ListNewProduct(4);
            ViewBag.categoryID = categoryID;
            return View(category);
        }
    }
}