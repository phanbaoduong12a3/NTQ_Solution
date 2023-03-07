using DataLayer.Dao;
using NTQ_Solution.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTQ_Solution.Areas.Admin.Controllers
{
    public class HomeUserController : Controller
    {
        // GET: Admin/HomeUser
        public ActionResult Index()
        {
            var dao = new ProductDao();
            var model = dao.GetAllProduct();
            return View(model);
        }

        public ActionResult Detail(int id)
        {
            var product = new ProductDao().ViewDetail(id);
            var sessionUser = (UserLogin)Session[Common.CommonConstant.USER_SESSION];
            ViewBag.UserID = sessionUser.UserID;
            ViewBag.ListReview = new ReviewDao().ListReviewViewModel(0, id);
            return View(product);
        }
    }
}