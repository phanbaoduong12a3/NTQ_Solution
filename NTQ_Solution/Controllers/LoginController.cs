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
                        userSession.Role = user.Role;
                        userSession.AccountName = user.AccountName;
                        Session.Add(CommonConstant.USER_SESSION, userSession);
                        var session = (UserLogin)Session[CommonConstant.USER_SESSION];
                        if(user.Role == 0) 
                        { 
                            return RedirectToAction("Index", "Home"); 
                        }
                        else
                        {
                            return Redirect("/Admin/Order/Index");
                        }
                       
                    }
                    else if (result == 0)
                    {
                        ModelState.AddModelError("", "Không tìm thấy Email");
                    }
                    else if (result == -1)
                    {
                        ModelState.AddModelError("", "Tài khoản bị khóa");
                    }
                    else if (result == -2)
                    {
                        ModelState.AddModelError("", "Mật khẩu không chính xác");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Đăng nhập thất bại");
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