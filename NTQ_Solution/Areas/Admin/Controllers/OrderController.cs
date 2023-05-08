using DataLayer.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTQ_Solution.Areas.Admin.Controllers
{
    public class OrderController : BaseController
    {
        OrderDao orderDao;
        ProductDao productDao;
        public OrderController()
        {
            orderDao = new OrderDao();
            productDao = new ProductDao();
        }
        // GET: Admin/Order
        public ActionResult Index(string searchString, int page = 1, int pageSize = 4)
        {
            try
            {
                ViewBag.listColor = productDao.listcolor();
                ViewBag.listSize = productDao.listsize();
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
                ViewBag.listColor = productDao.listcolor();
                ViewBag.listSize = productDao.listsize();
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