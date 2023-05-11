using Common;
using DataLayer.Dao;
using NTQ_Solution.Areas.Admin.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace NTQ_Solution.Controllers
{
    public class ForgotPasswordController : Controller
    {
        UserData userData;
        public ForgotPasswordController()
        {
            userData = new UserData();
        }
        // GET: ForgotPassword
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SendMail(RegisterModel registerModel)
        {
            var user = userData.GetByEmail(registerModel.Email);
            if(user != null)
            {
                string content = System.IO.File.ReadAllText(Server.MapPath("~/assests/client/teamplate/newpassword.html"));
                content = content.Replace("{{Email}}",user.Email);
                content = content.Replace("{{UserName}}",user.UserName);
                var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                new MailHelper().SendMail(user.Email, "Cấp mật khẩu mới từ Xuu Shop", content);
                new MailHelper().SendMail(toEmail, "Cấp mật khẩu mới từ Xuu Shop", content);

                userData.UpdateNewPassword(user);
                TempData["success"] = "Kiem tra Email de lay mat khau moi";
                return RedirectToAction("Index", "Login");
            }
            else
            {
                TempData["success"] = "Email chua dang ki tai khoan";
                return RedirectToAction("Index","ForgotPassword");
            }
        }
    }
}