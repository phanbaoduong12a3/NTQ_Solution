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
                ViewBag.ListColor = new ProductDao().ListColor(product.ProductName);
                ViewBag.ListSize = new ProductDao().ListSize(product.ProductName);
                productDao.UpdateView(product.ID);
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
        public ActionResult Order(string productName, string color, string size)
        {
            try
            {
                var sessionUser = (UserLogin)Session[Common.CommonConstant.USER_SESSION];
                if (sessionUser == null)
                {
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    var product = OrderDao.findProductOrder(productName, size, color);
                    int productID = product.ID;
                    bool checkProductID = OrderDao.checkProductID(productID);
                    if(!checkProductID) 
                    {
                        var userID = sessionUser.UserID;
                        var Order = new Order
                        {
                            ProductsID = productID,
                            UserID = userID,
                            CreateAt = DateTime.Now,
                            Status = 1,
                            Count = 1,
                            Color = product.Color,
                            Size = size
                        };
                        OrderDao.AddNewOrder(Order);
                    }
                    else
                    {
                        OrderDao.UpdateOrder(productID);
                    }
                    TempData["success"] = "Them san pham vao gio hang thanh cong";
                    return RedirectToAction("Index", "Order");
                }
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