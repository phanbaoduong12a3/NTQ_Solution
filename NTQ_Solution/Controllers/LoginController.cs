using DataLayer.Dao;
using NTQ_Solution.Areas.Admin.Data;
using NTQ_Solution.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace NTQ_Solution.Controllers
{
    public class LoginController : Controller
    {

        UserDao userDao ;
        public LoginController()
        {
            userDao = new UserDao();
        }
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = userDao.Login(loginModel.Email, loginModel.Password);
                    if (result == 1)
                    {
                        var user = userDao.GetByEmail(loginModel.Email);
                        var userSession = new UserLogin();
                        userSession.UserID = user.ID;
                        userSession.Email = user.Email;
                        userSession.UserName = user.UserName;
                        Session.Add(CommonConstant.USER_SESSION, userSession);
                        var session = (UserLogin)Session[CommonConstant.USER_SESSION];
                        if(user.Role == 0) 
                        { 
                            return RedirectToAction("Profile", "Profile"); 
                        }
                        else
                        {
                            return Redirect("/Admin/Myprofile/Profile");
                        }
                       
                    }
                    else if (result == 0)
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
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }

        }
       
        public ActionResult Logout()
        {
            try
            {
                Session[CommonConstant.USER_SESSION] = null;
                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }
        
    }
}