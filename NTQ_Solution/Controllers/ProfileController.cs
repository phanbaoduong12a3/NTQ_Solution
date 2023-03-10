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
        UserDao userDao = null;
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
                    bool status;
                    var user = userDao.GetById(model.UserID);
                    if(user.Status == 1) { status = true; }
                    else { status = false; }
                    var result = new RegisterModel
                    {
                        ID = user.ID,
                        UserName = user.UserName,
                        Email = user.Email,
                        Password = user.PassWord,
                        Role = user.Role,
                        Status = status,
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
        public ActionResult Profile(RegisterModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = userDao.GetByEmail(model.Email);
                    model.ID = result.ID;
                    bool checkUserName;
                    bool checkConfirmPassword = userDao.CheckConfirmPassword(model.ConfirmPassword, model.Password);
                    var userOld = userDao.GetById(model.ID);
                    if (model.UserName == userOld.UserName) checkUserName = true;
                    else checkUserName = userDao.CheckUserName(model.UserName);
                    int status;
                    if(model.Status) { status = 1; }
                    else { status = 0; }
                    if (checkUserName && checkConfirmPassword)
                    {
                        var user = new User
                        {
                            ID = model.ID,
                            Email = model.Email,
                            UserName = model.UserName,
                            PassWord = model.Password,
                            Role = model.Role,
                            Status = status
                        };
                        userDao.Update(user);
                        return RedirectToAction("Profile", "Profile");
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