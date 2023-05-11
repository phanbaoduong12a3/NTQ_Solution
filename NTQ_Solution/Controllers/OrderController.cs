using DataLayer.Dao;
using DataLayer.EF;
using DataLayer.ViewModel;
using NTQ_Solution.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace NTQ_Solution.Controllers
{
    public class OrderController : Controller
    {
        private const string CartSession = "CartSession";
        OrderData orderData;
        ProductData productData;
        public OrderController()
        {
            orderData = new OrderData();
            productData = new ProductData();
        }
        public ActionResult AddOrder(string productName, string color, string size)
        {
            try
            {
                var sessionUser = (UserLogin)Session[Common.CommonConstant.USER_SESSION];
                var product = orderData.findProductOrder(productName, size, color);
                int productID = product.ID;
                bool checkProductID = orderData.checkProductID(productID);
                var cart = Session[CartSession];
                var list = new List<OrderModel>();
                if (!checkProductID)
                {
                    var Order = new Order
                    {
                        ProductsID = productID,
                        CreateAt = DateTime.Now,
                        UserID = 0,
                        Status = 1,
                        Count = 1,
                        Color = product.Color,
                        Size = product.Size
                    };
                    orderData.AddNewOrder(Order);
                    var model = orderData.convertOrderModel(Order, size, color);
                    if (cart != null)
                    {
                        list = (List<OrderModel>)cart;
                        list.Add(model);
                        Session[CartSession] = list;
                    }
                    list.Add(model);
                    Session[CartSession] = list;
                }
                else
                {
                    orderData.UpdateOrder(productID);
                }
                TempData["success"] = "Them san pham vao gio hang thanh cong";
                return RedirectToAction("OrderDemo", "Order");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        // GET: Order

        public ActionResult Index(int page = 1, int pageSize = 4)
        {
            try
            {
                Session[CartSession] = null;
                var session = (UserLogin)Session[NTQ_Solution.Common.CommonConstant.USER_SESSION];
                ViewBag.listColor = productData.listcolor();
                ViewBag.listSize = productData.listsize();
                if (session != null)
                {
                    var userID = session.UserID;
                    var model = orderData.OrderShow(userID, page, pageSize);
                    return View(model);
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                orderData.Delete(id);
                TempData["success"] = "Xoa san pham khoi gio hang thanh cong";
                return RedirectToAction("Index", "Order");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public ActionResult DeleteOrder(int id)
        {
            try
            {
                orderData.DeleteOrder(id);
                var cart = Session[CartSession];
                if (cart != null)
                {
                    var list = (List<OrderModel>)cart;
                
                    for(int i=0; i < list.Count; i++)
                    {
                        if (list[i].ID == id)
                        {
                            list.Remove(list[i]);
                        }
                    }
                    Session[CartSession] = list;
                }

                TempData["success"] = "Huy don hang thanh cong";
                return RedirectToAction("Index", "Order");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public ActionResult Order(/*int OrderId,string productCount,string color, string size*/)
        {
            try
            {
                var session = (UserLogin)Session[NTQ_Solution.Common.CommonConstant.USER_SESSION];
                if (session == null)
                {
                    return RedirectToAction("Index", "Login");
                }
                ViewBag.listColor = productData.listcolor();
                ViewBag.listSize = productData.listsize();
                var cart = Session[CartSession];
                var list = new List<OrderModel>();

                var sessionUser = (UserLogin)Session[Common.CommonConstant.USER_SESSION];
                var orderModels = orderData.OrderDemo(sessionUser.UserID, 1, 4);
                foreach (var item in orderModels)
                {
                    list.Add(item);
                }
                Session[CartSession] = list;

                double? total = 0;
                foreach (var item in list)
                {
                    total += item.Price * item.Count;
                }
                ViewBag.TongTien = total;
                return View(list);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public ActionResult Payment(OrderModel orderModel, string payment, string ship)
        {
            try
            {
                if (payment == "1")
                {
                    int shipMoney;
                    if (ship == "1")
                    {
                        ship = "Giao hàng tiết kiệm";
                        shipMoney = 20000;
                    }
                    else
                    {
                        ship = "Giao hàng hỏa tốc";
                        shipMoney = 25000;
                    }
                    var cart = Session[CartSession];
                    var list = (List<OrderModel>)cart;
                    foreach (var item in list)
                    {
                        if (item.ID == orderModel.ID)
                        {
                            item.Phone = orderModel.Phone;
                            item.Address = orderModel.Address;
                        }
                        orderData.PaymentSuccess(item, ship, shipMoney);
                    }

                }
                return RedirectToAction("Index", "Order");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }
        public ActionResult OrderDemo(int page = 1, int pageSize = 4)
        {
            try
            {

                ViewBag.listColor = productData.listcolor();
                ViewBag.listSize = productData.listsize();
                var session = (UserLogin)Session[NTQ_Solution.Common.CommonConstant.USER_SESSION];
                var cart = Session[CartSession];
                if (cart == null) { orderData.Remove(); }
                if (session == null)
                {

                    var orderModels = orderData.OrderDemo(0, page, pageSize);
                    if (orderModels != null)
                    {
                        double? total = 0;
                        foreach (var item in orderModels)
                        {
                            total += item.Price * item.Count;
                        }
                        ViewBag.TongTien = total;
                    }
                    return View(orderModels);
                }
                else
                {
                    var orderdemo = orderData.OrderDemo(session.UserID, page, pageSize);
                    if (cart == null)
                    {
                        if (orderdemo != null) return View(orderdemo);
                        var orderModel = orderData.OrderDemo(0, page, pageSize);
                        return View(orderModel);
                    }
                    else
                    {
                        List<OrderModel> list = new List<OrderModel>();
                        list = (List<OrderModel>)cart;
                        for (int i = 0; i < list.Count; i++)
                        {
                            orderData.UpdateOrderUser(session.UserID);
                        }
                        var orderModels = orderData.OrderDemo(session.UserID, page, pageSize);

                        /*if (orderModels != null)
                        {
                            foreach (var item in orderModels)
                            {
                                list.Add(item);
                            }
                            Session[CartSession] = list;
                        }*/
                        double? total = 0;
                        foreach (var item in orderModels)
                        {
                            total += item.Price * item.Count;
                        }
                        ViewBag.TongTien = total;
                        return View(orderModels);
                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public ActionResult UpdateOrder(List<int> OrderId, List<string> productCount)
        {
            for (int i = 0; i < OrderId.Count; i++)
            {
                if (productCount[i] == "")
                {
                    TempData["success"] = "So luong khong duoc de trong";
                    return RedirectToAction("OrderDemo", "Order");
                }
                orderData.UpdateOrderCount(OrderId[i], productCount[i]);
            }
            TempData["success"] = "Cap nhat gio hang thanh cong";
            return RedirectToAction("OrderDemo", "Order");
        }



    }
}