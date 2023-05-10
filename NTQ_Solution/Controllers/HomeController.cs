using DataLayer.Dao;
using DataLayer.EF;
using NTQ_Solution.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Linq;
using System.Security.Authentication;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace NTQ_Solution.Controllers
{
    public class HomeController : Controller
    {
        ProductDao productDao ;
        ReviewDao reviewDao ;
        OrderDao OrderDao ;
        public HomeController()
        {
            productDao = new ProductDao();
            reviewDao = new ReviewDao();
            OrderDao = new OrderDao();
        }
        // GET: Home
        public ActionResult Index(string trending, string searchString, int page = 1, int pageSize = 8)
        {
            try
            {
                ViewBag.SearchString = searchString;
                var model = productDao.ListProductOnSale(trending, searchString, page, pageSize);
                ViewBag.NewProduct = productDao.ListNewProduct(5);
                ViewBag.HotProduct = productDao.ListHotProduct(3);
                ViewBag.PopularProduct = productDao.ListNewProduct(2);
                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public ActionResult Detail(int id)
        {
            try
            {
                var product = productDao.ViewDetail(id);
                var sessionUser = (UserLogin)Session[Common.CommonConstant.USER_SESSION];
                if(sessionUser != null) { ViewBag.UserID = sessionUser.UserID; }
                ViewBag.ListReview = new ReviewDao().ListReviewViewModel(0, id);
                ViewBag.ColorColor = productDao.listcolor();
                ViewBag.SizeSize = productDao.listsize();
                ViewBag.listcolor = productDao.listColor(product.ProductName);
                ViewBag.listsize = productDao.listSize(product.ProductName);
                productDao.UpdateView(product.ID);
                ViewBag.HotProduct = productDao.ListNewProduct(4);
                return View(product);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        [HttpPost]
        public JsonResult AddNewReview(int productid,int userid,string title)
        {
            try
            {
                //var dao = new ReviewDao();
                Review review = new Review();
                review.UserID = userid;
                review.ProductsID = productid;
                review.Title = title;
                review.Status = 0;
                review.CreateAt = DateTime.Now;
                review.ParentID = 0;
                bool addReview = reviewDao.InsertReview(review);
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
            try
            {
                var data = reviewDao.ListReviewViewModel(0, productid);
                return View(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        
        [ChildActionOnly]
        public PartialViewResult HeaderCart()
        {
            var session = (UserLogin)Session[CommonConstant.USER_SESSION];
            int count = 0;

            if(session != null)
            {
                count = productDao.CartCount(session.UserID);
            }
            ViewBag.count = count;
            return PartialView();
        }
    }
}