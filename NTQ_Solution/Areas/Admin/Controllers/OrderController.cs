using DataLayer.Dao;
using DataLayer.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTQ_Solution.Areas.Admin.Controllers
{
    public class OrderController : BaseController
    {
        OrderData orderData;
        ProductData productData;
        public OrderController()
        {
            orderData = new OrderData();
            productData = new ProductData();
        }
        // GET: Admin/Order
        public ActionResult Index(string searchString, int page = 1, int pageSize = 4)
        {
            try
            {
                ViewBag.listColor = productData.listcolor();
                ViewBag.listSize = productData.listsize();
                ViewBag.SearchString = searchString;
                var model = orderData.ListOrderBE(searchString, page, pageSize);
                return View(model);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public ActionResult ConfirmOrder(int OrderId, int userid)
        {
            try
            {
                orderData.UpdateOrderBE(OrderId, userid);
                TempData["success"] = "Xac nhan don hang thanh cong";
                return RedirectToAction("Index","Order");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public ActionResult OrderSuccess(string searchString, int page = 1, int pageSize = 4)
        {
            try
            {
                ViewBag.listColor = productData.listcolor();
                ViewBag.listSize = productData.listsize();
                var model = orderData.ListOrderSuccess(searchString, page, pageSize);
                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        [ChildActionOnly]
        public ActionResult StatusOrder()
        {
            return View();
        }
    }
}