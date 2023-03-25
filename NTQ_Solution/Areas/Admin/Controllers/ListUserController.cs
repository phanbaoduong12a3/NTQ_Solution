using DataLayer.Dao;
using DataLayer.EF;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace NTQ_Solution.Areas.Admin.Controllers
{
    public class ListUserController : BaseController 
    {
        UserDao userDao ;
        public ListUserController()
        {
            userDao = new UserDao();
        }
        // GET: Admin/ListUser
        public ActionResult Index(string active, string inActive, string admin, string user, string searchString, int page = 1, int pageSize = 5)
        {
            try
            {
                var model = userDao.ListAllPaging(active, inActive, admin, user, searchString, page, pageSize);
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