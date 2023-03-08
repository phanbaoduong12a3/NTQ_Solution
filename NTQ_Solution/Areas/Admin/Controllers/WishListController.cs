using DataLayer.Dao;
using NTQ_Solution.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTQ_Solution.Areas.Admin.Controllers
{
    public class WishListController : BaseController
    {
        // GET: Admin/WishList
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            try
            {
                var dao = new WishListDao();
                var session = (UserLogin)Session[NTQ_Solution.Common.CommonConstant.USER_SESSION];
                if (session != null)
                {
                    var userID = session.UserID;
                    var model = dao.ListAllWishList(userID, page, pageSize);
                    return View(model);
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                var dao = new WishListDao();
                dao.Delete(id);
                return RedirectToAction("Index", "WishList");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}