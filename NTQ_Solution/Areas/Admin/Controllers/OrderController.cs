using DataLayer.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTQ_Solution.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        OrderDao orderDao;
        public OrderController()
        {
            orderDao = new OrderDao();
        }
        // GET: Admin/Order
        public ActionResult Index(string searchString, int page = 1, int pageSize = 4)
        {
            try
            {
                ViewBag.SearchString = searchString;
                var model = orderDao.ListOrderBE(searchString, page, pageSize);
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
                orderDao.UpdateOrderBE(OrderId, userid);
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
                var model = orderDao.ListOrderSuccess(searchString, page, pageSize);
                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}