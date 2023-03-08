﻿using DataLayer.Dao;
using NTQ_Solution.Areas.Admin.Data;
using NTQ_Solution.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTQ_Solution.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(LoginModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dao = new UserDao();
                    var result = dao.Login(model.Email, model.Password);
                    if (result == 1)
                    {
                        var user = dao.GetByEmail(model.Email);
                        var userSession = new UserLogin();
                        userSession.UserID = user.ID;
                        userSession.Email = user.Email;
                        userSession.UserName = user.UserName;
                        Session.Add(CommonConstant.USER_SESSION, userSession);
                        return RedirectToAction("Index", "Home");
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