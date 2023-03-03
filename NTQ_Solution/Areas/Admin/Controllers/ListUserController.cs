using DataLayer.Dao;
using DataLayer.EF;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace NTQ_Solution.Areas.Admin.Controllers
{
    public class ListUserController : Controller
    {
        // GET: Admin/ListUser
        public ActionResult Index(string searchString, int page = 1, int pageSize = 1)
        {
            var dao = new UserDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        
    }
}