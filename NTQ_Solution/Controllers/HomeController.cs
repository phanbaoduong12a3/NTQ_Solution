using DataLayer.Dao;
using DataLayer.EF;
using NTQ_Solution.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Web;
using System.Web.Mvc;

namespace NTQ_Solution.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
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

        public ActionResult Detail(int id)
        {
            try
            {
                var dao = new ProductDao();
                var product = dao.ViewDetail(id);
                var sessionUser = (UserLogin)Session[Common.CommonConstant.USER_SESSION];
                if(sessionUser != null) { ViewBag.UserID = sessionUser.UserID; }
                ViewBag.ListReview = new ReviewDao().ListReviewViewModel(0, id);
                dao.UpdateView(product.ID);
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
                var dao = new ReviewDao();
                Review review = new Review();
                review.UserID = userid;
                review.ProductsID = productid;
                review.Title = title;
                review.Status = 0;
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
            try
            {
                var data = new ReviewDao().ListReviewViewModel(0, productid);
                return View(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public ActionResult WishList(int productID)
        {
            try
            {
                var dao = new WishListDao();
                var sessionUser = (UserLogin)Session[Common.CommonConstant.USER_SESSION];
                if (sessionUser == null)
                {
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    var userID = sessionUser.UserID;
                    var wishList = new WishList
                    {
                        ProductsID = productID,
                        UserID = userID,
                        CreateAt = DateTime.Now,
                        Status = 1
                    };
                    dao.AddNewWishList(wishList);
                    return RedirectToAction("Index", "WishList");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}