using DataLayer.Dao;
using DataLayer.EF;
using NTQ_Solution.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTQ_Solution.Controllers
{
    public class WishListController : Controller
    {
        WishListDao wishListDao ;
        public WishListController() 
        {
            wishListDao= new WishListDao();
        }
        // GET: WishList
       
        public ActionResult Index(int page = 1, int pageSize = 5)
        {
            try
            {
                var session = (UserLogin)Session[NTQ_Solution.Common.CommonConstant.USER_SESSION];
                if(session != null)
                {
                    var userID = session.UserID;
                    var model = wishListDao.WishListShow(userID, page, pageSize);
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
                wishListDao.Delete(id);
                TempData["success"] = "Delete Product From Wishlist succsee";
                return RedirectToAction("Index","WishList");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}