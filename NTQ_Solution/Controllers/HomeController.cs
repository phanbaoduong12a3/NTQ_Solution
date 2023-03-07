using DataLayer.Dao;
using DataLayer.EF;
using NTQ_Solution.Common;
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
            var sessionUser = (UserLogin)Session[Common.CommonConstant.USER_SESSION];
            ViewBag.UserID = sessionUser.UserID;
            ViewBag.ListReview = new ReviewDao().ListReviewViewModel(0, id);
            return View(product);
        }
        [HttpPost]
        public JsonResult AddNewReview(int productid,int userid,string title)
        {
            try
            {
                var dao = new ReviewDao();
                Review review = new Review();
                review.UserID = userid;
                review.ProductsID = productid;
                review.Title = title;
                review.CreateAt = DateTime.Now;
                bool addReview = dao.InsertReview(review);
                if(addReview)
                {
                    return Json(new { status = true });
                }
                else
                {
                    return Json(new { status = false });
                }
            }
            catch 
            {
                return Json(new { status = false });
            }
        }

        public ActionResult GetReview(int productid)
        {
            var data = new ReviewDao().ListReviewViewModel(0,productid);
            return View(data);
        }
    }
}