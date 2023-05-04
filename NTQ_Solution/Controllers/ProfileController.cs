using DataLayer.Dao;
using DataLayer.EF;
using NTQ_Solution.Areas.Admin.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace NTQ_Solution.Controllers
{
    public class ProfileController : Controller
    {
        UserDao userDao ;
        public ProfileController()
        {
            userDao = new UserDao();
        }
        // GET: Profile
        [HttpGet]
        public ActionResult Profile()
        {
            try
            {
                var model = (NTQ_Solution.Common.UserLogin)Session[NTQ_Solution.Common.CommonConstant.USER_SESSION];
                if (model == null)
                {
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    var user = userDao.GetById(model.UserID);
                    var result = new RegisterModel
                    {
                        ID = user.ID,
                        UserName = user.UserName,
                        Email = user.Email,
                        Password = user.PassWord,
                        Role = user.Role,
                        CreateAt = user.CreateAt,
                        UpdateAt = user.UpdateAt,
                        DeleteAt = user.DeleteAt,
                    };
                    return View(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        [HttpPost]
        public ActionResult Profile(RegisterModel registerModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = userDao.GetByEmail(registerModel.Email);
                    registerModel.ID = result.ID;
                    bool checkUserName;
                    bool checkConfirmPassword = userDao.CheckConfirmPassword(registerModel.ConfirmPassword, registerModel.Password);
                    var userOld = userDao.GetById(registerModel.ID);
                    if (registerModel.UserName == userOld.UserName) checkUserName = true;
                    else checkUserName = userDao.CheckUserName(registerModel.UserName);
                    
                    if (checkUserName && checkConfirmPassword)
                    {
                        var user = new User
                        {
                            ID = registerModel.ID,
                            Email = registerModel.Email,
                            UserName = registerModel.UserName,
                            PassWord = registerModel.Password,
                            Role = registerModel.Role
                        };
                        userDao.Update(user);
                        TempData["success"] = "Sua thanh cong";
                        return RedirectToAction("Profile", "Profile");
                    }
                    if (!checkUserName) { ModelState.AddModelError("", "UserName không chính xác"); };
                    if (!checkConfirmPassword) { ModelState.AddModelError("", "ConfirmPassword không đúng"); }
                }
                return View(registerModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}