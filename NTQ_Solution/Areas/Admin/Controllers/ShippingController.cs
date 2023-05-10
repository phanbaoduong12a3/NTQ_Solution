using DataLayer.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTQ_Solution.Areas.Admin.Controllers
{
    public class ShippingController : BaseController
    {
        ShipDao shipDao;
        ProductDao productDao;
        public ShippingController()
        {
            shipDao = new ShipDao();
            productDao = new ProductDao();
        }
        // GET: Admin/Order
        public ActionResult Index(int page = 1, int pageSize = 4)
        {
            try
            {
                ViewBag.listColor = productDao.listcolor();
                ViewBag.listSize = productDao.listsize();
                var model = shipDao.ListShip(page, pageSize);
                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public ActionResult ConfirmShip(int id)
        {
            try
            {
                shipDao.UpdateShip(id);
                TempData["success"] = "Giao hang thanh cong";
                return RedirectToAction("Index", "Shipping");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}