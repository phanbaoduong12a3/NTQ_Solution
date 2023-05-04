using DataLayer.Dao;
using DataLayer.EF;
using DataLayer.ViewModel;
using NTQ_Solution.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTQ_Solution.Controllers
{
    public class OrderController : Controller
    {
        OrderDao OrderDao ;
        public OrderController() 
        {
            OrderDao= new OrderDao();
        }
        // GET: Order
       
        public ActionResult Index(int page = 1, int pageSize = 4)
        {
            try
            {
                var session = (UserLogin)Session[NTQ_Solution.Common.CommonConstant.USER_SESSION];
                if(session != null)
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

        public ActionResult Order(int OrderId,string productCount,string color, string size)
        {
            try
            {
                int count = int.Parse(productCount);
                var model = OrderDao.getOrderModel(OrderId, count, color, size);
                return View(model);
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
                    OrderDao.PaymentSuccess(orderModel,ship,shipMoney);
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
                var session = (UserLogin)Session[NTQ_Solution.Common.CommonConstant.USER_SESSION];
                if (session != null)
                {
                    var userID = session.UserID;
                    var model = OrderDao.OrderDemo(userID, page, pageSize);
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
        
    }
}