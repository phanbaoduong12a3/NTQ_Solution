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
        ShipData shipData;
        ProductData productData;
        public ShippingController()
        {
            shipData = new ShipData();
            productData = new ProductData();
        }
        // GET: Admin/Order
        public ActionResult Index(int page = 1, int pageSize = 4)
        {
            try
            {
                ViewBag.listColor = productData.listcolor();
                ViewBag.listSize = productData.listsize();
                var model = shipData.ListShip(page, pageSize);
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
                shipData.UpdateShip(id);
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