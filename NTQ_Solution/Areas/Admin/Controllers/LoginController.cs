using DataLayer.Dao;
using NTQ_Solution.Areas.Admin.Data;
using NTQ_Solution.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.HtmlControls;

namespace NTQ_Solution.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(LoginModel model)
        {
            if(ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(model.Email, model.Password);
                if(result==1)
                {
                    var user = dao.GetByEmail(model.Email);
                    var userSession = new UserLogin();
                    userSession.UserID = user.ID;
                    userSession.Email = user.Email;
                    userSession.UserName = user.UserName;
                    Session.Add(CommonConstant.USER_SESSION, userSession);
                    if (user.Role == 1)
                    {
                        return RedirectToAction("Profile", "MyProfile");
                    }
                    else
                    {
                        var product = new ProductDao().GetAllProduct();
                        return RedirectToAction("Index", "HomeUser");

                    }
                }
                else if(result == 0)
                {
                    ModelState.AddModelError("", "Don't see Email");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Account is InActive");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Password is incorect");
                }
                else
                {
                    ModelState.AddModelError("", "Login fail");
                }
            }
            return View("Index");

        }
    }
}