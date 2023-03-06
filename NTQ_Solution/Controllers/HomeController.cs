using DataLayer.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTQ_Solution.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var dao = new ProductDao();
            var model = dao.GetAllProduct();
            return View(model);
        }
        public ActionResult Detail(int id)
        {
            var product = new ProductDao().ViewDetail(id);
            return View(product);
        }
    }
}