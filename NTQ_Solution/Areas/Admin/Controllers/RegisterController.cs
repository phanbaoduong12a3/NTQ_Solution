using DataLayer.Dao;
using DataLayer.EF;
using NTQ_Solution.Areas.Admin.Data;
using NTQ_Solution.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTQ_Solution.Areas.Admin.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Admin/Register
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register(RegisterModel registerModel)
        {
            if(ModelState.IsValid)
            {
                var dao = new UserDao();
                int result = dao.CheckUser(registerModel.UserName, registerModel.Email);
                bool checkConfirmPassword = dao.CheckConfirmPassword(registerModel.ConfirmPassword, registerModel.Password);
                if (result == 1)
                {
                    var user = new User
                    {
                        UserName = registerModel.UserName,
                        PassWord =registerModel.Password,
                        Email = registerModel.Email,
                        CreateAt = DateTime.Now,
                        Role = 0,
                        Status=1
                    };
                    dao.Insert(user);
                    return RedirectToAction("Index","Login");
                }
                if (!checkConfirmPassword) { ModelState.AddModelError("", "Enter ConfirmPassword again"); }
                else if(result == -1) 
                {
                    ModelState.AddModelError("", "Email is invalid");
                }
                else
                {
                    ModelState.AddModelError("", "UserName is invalid");
                }
            }
            return View("Index");
        }
    }
}