using DataLayer.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTQ_Solution.Areas.Admin.Controllers
{
    public class ShippingController : Controller
    {
        ShipDao shipDao;
        public ShippingController()
        {
            shipDao = new ShipDao();
        }
        // GET: Admin/Order
        public ActionResult Index(int page = 1, int pageSize = 4)
        {
            try
            {
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