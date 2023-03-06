using DataLayer.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTQ_Solution.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        public ActionResult Index(string trending, string searchString, int page=1, int pageSize=10)
        {
            var dao = new ProductDao();
            var model = dao.ListAllPagingProduct(trending, searchString, page, pageSize);
            return View(model);
        }
        public ActionResult CreateProduct()
        {
            return View();
        }
    }
}