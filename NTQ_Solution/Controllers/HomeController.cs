using DataLayer.Dao;
using DataLayer.EF;
using DataLayer.ViewModel;
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
        private const string CartSession = "CartSession";
        ProductData productData ;
        ReviewData reviewData ;
        OrderData orderData ;
        public HomeController()
        {
            productData = new ProductData();
            reviewData = new ReviewData();
            orderData = new OrderData();
        }
        // GET: Home
        public ActionResult Index(string trending, string searchString, int page = 1, int pageSize = 8)
        {
            try
            {
                ViewBag.SearchString = searchString;
                var model = productData.ListProductOnSale("", searchString, page, pageSize);
                ViewBag.NewProductModel = productData.ListNewProduct(5);
                ViewBag.HotProductModel = productData.ListHotProduct(3);
                ViewBag.PopularProductModel = productData.ListNewProduct(2);
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
                var product = productData.ViewDetail(id);
                var sessionUser = (UserLogin)Session[Common.CommonConstant.USER_SESSION];
                if(sessionUser != null) { ViewBag.UserID = sessionUser.UserID; }
                ViewBag.ListReview = new ReviewData().ListReviewViewModel(0, id);
                ViewBag.ColorModel = productData.listcolor();
                ViewBag.SizeModel = productData.listsize();
                ViewBag.listcolor = productData.listColor(product.ProductName);
                ViewBag.listsize = productData.listSize(product.ProductName);
                productData.UpdateView(product.ID);
                ViewBag.HotProduct = productData.ListNewProduct(4);
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
                //var dao = new reviewData();
                Review review = new Review();
                review.UserID = userid;
                review.ProductsID = productid;
                review.Title = title;
                review.Status = 0;
                review.CreateAt = DateTime.Now;
                review.ParentID = 0;
                bool addReview = reviewData.InsertReview(review);
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
                var data = reviewData.ListReviewViewModel(0, productid);
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
            /*if(session != null)
            {
                
            }*/
            if(session != null)
            {
                count = productData.CartCount(session.UserID);
                ViewBag.count = count;
            }
            else
            {
                var cart = Session["CartSession"];
                if (cart != null)
                {
                    var list = (List<OrderModel>)cart;
                    ViewBag.count = list.Count;
                }
                else
                {
                    ViewBag.count = count;
                }
            }
            return PartialView();
        }
    }
}