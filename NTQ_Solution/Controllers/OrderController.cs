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
        OrderDao OrderDao ;
        ProductDao productDao ;
        public OrderController() 
        {
            OrderDao= new OrderDao();
            productDao = new ProductDao();
        }
        public ActionResult AddOrder(string productName, string color, string size)
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
                    int Size = int.Parse(size);
                    var cart = Session[CartSession];
                    var list = new List<OrderModel>();
                    if (!checkProductID)
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
                            Size = Size
                        };
                        OrderDao.AddNewOrder(Order);
                        var model = OrderDao.convertOrderModel(Order, size, color);
                        if(cart != null)
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
                        OrderDao.UpdateOrder(productID);
                    }
                    TempData["success"] = "Them san pham vao gio hang thanh cong";
                    return RedirectToAction("OrderDemo", "Order");
                }
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
                var session = (UserLogin)Session[NTQ_Solution.Common.CommonConstant.USER_SESSION];
                ViewBag.listColor = productDao.listcolor();
                ViewBag.listSize = productDao.listsize();
                if (session != null)
                {
                    var userID = session.UserID;
                    var model = OrderDao.OrderShow(userID, page, pageSize);
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
                OrderDao.Delete(id);
                TempData["success"] = "Xóa sản phẩm khỏi giỏ hàng thành công";
                return RedirectToAction("Index","Order");
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

                ViewBag.listColor = productDao.listcolor();
                ViewBag.listSize = productDao.listsize();
                var cart = Session[CartSession];
                var list = new List<OrderModel>();
                if(cart != null)
                {
                    list = (List<OrderModel>)cart;
                }
                else
                {
                    var sessionUser = (UserLogin)Session[Common.CommonConstant.USER_SESSION];
                    var model = OrderDao.OrderDemo(sessionUser.UserID,1,4);
                    foreach(var item in model)
                    {
                        list.Add(item);
                    }
                    Session[CartSession] = list;
                }
                double? total = 0;
                foreach(var item in list)
                {
                    total += item.Price * item.Count;
                }
                ViewBag.TongTien = total;
                return View(list);
                
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public ActionResult Payment(OrderModel orderModel,string payment,string ship)
        {
            try
            {
                if(payment == "1")
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
                    foreach(var item in list)
                    {
                        if(item.ID == orderModel.ID)
                        {
                            item.Phone = orderModel.Phone;
                            item.Address = orderModel.Address;
                        }
                        OrderDao.PaymentSuccess(item, ship, shipMoney);
                    }
                    
                }
                return RedirectToAction("Index","Order");
            }
            catch(Exception ex )
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            
        }
        public ActionResult OrderDemo(int page=1, int pageSize = 4)
        {
            try
            {
                ViewBag.listColor = productDao.listcolor();
                ViewBag.listSize = productDao.listsize();
                var session = (UserLogin)Session[NTQ_Solution.Common.CommonConstant.USER_SESSION];
                var cart = Session[CartSession];
                if(cart != null)
                {
                    ViewBag.cart = "Yes";
                }
                else
                {
                    ViewBag.cart = "No";
                }
                if (session != null)
                {
                    var userID = session.UserID;
                    var model = OrderDao.OrderDemo(userID, page, pageSize);
                    double? total=0;
                    foreach(var item in model)
                    {
                        total += item.Price * item.Count;
                    }
                    ViewBag.TongTien = total;
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
        public ActionResult UpdateOrder(int OrderId, string productCount)
        {
            OrderDao.UpdateOrderCount(OrderId,productCount);
            return RedirectToAction("OrderDemo", "Order");
        }

                        

    }
}