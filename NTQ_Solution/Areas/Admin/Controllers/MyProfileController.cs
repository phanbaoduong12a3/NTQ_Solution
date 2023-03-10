using DataLayer.Dao;
using DataLayer.EF;
using NTQ_Solution.Areas.Admin.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTQ_Solution.Areas.Admin.Controllers
{
    public class MyProfileController : BaseController
    {
        UserDao userDao = null;
        public MyProfileController() 
        {
            userDao = new UserDao();
        }
        public ActionResult Index()
        {
            return View();
        }
        // GET: Admin/MyProfile
        [HttpGet]
        public ActionResult Profile()
        {
            try
            {
                var model = (NTQ_Solution.Common.UserLogin)Session[NTQ_Solution.Common.CommonConstant.USER_SESSION];
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        [HttpPost]
        public ActionResult Profile(RegisterModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = userDao.GetByEmail(model.Email);
                    model.ID = result.ID;
                    bool checkUserName ;
                    bool checkConfirmPassword = userDao.CheckConfirmPassword(model.ConfirmPassword, model.Password);
                    var userOld = userDao.GetById(model.ID);
                    if (model.UserName == userOld.UserName) checkUserName = true;
                    else checkUserName = userDao.CheckUserName(model.UserName);
                    if (checkUserName  && checkConfirmPassword)
                    {
                        var user = new User
                        {
                            ID = model.ID,
                            Email = model.Email,
                            UserName = model.UserName,
                            PassWord = model.Password,
                            Role = model.Role,
                        };
                        userDao.Update(user);
                        TempData["success"] = "Update Profile success";
                        return RedirectToAction("Profile", "MyProfile");
                    }
                    if (!checkUserName) { ModelState.AddModelError("", "UserName is invalid"); };
                    if (!checkConfirmPassword) { ModelState.AddModelError("", "ConfirmPassword is not correct"); }
                }
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