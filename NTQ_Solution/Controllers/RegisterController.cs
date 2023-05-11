using DataLayer.Dao;
using DataLayer.EF;
using NTQ_Solution.Areas.Admin.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTQ_Solution.Controllers
{
    public class RegisterController : Controller
    {
        UserData userData ;
        public RegisterController()
        {
            userData= new UserData();
        }
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register(RegisterModel registerModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int result = userData.CheckUser(registerModel.UserName, registerModel.Email);
                    bool checkConfirmPassword = userData.CheckConfirmPassword(registerModel.ConfirmPassword, registerModel.Password);
                    if (result == 1)
                    {
                        var user = new User
                        {
                            UserName = registerModel.UserName,
                            PassWord = registerModel.Password,
                            Email = registerModel.Email,
                            CreateAt = DateTime.Now,
                            Role = 0,
                            Status = 1,
                            Address = registerModel.Address,
                            Phone = registerModel.Phone
                        };
                        userData.Insert(user);
                        TempData["success"] = "Dang ki thanh cong";
                        return RedirectToAction("Index", "Login");
                    }
                    if (!checkConfirmPassword) { ModelState.AddModelError("", "Nhập lại xác nhận mật khẩu"); }
                    else if (result == -1)
                    {
                        ModelState.AddModelError("", "Email đã tồn tại");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
                    }
                }
                return View("Index");
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }
    }
}