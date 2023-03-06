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
        // GET: Admin/MyProfile
        public ActionResult Index()
        {
            var model = (NTQ_Solution.Common.UserLogin)Session[NTQ_Solution.Common.CommonConstant.USER_SESSION];
            var dao = new UserDao();
            var user = dao.GetById(model.UserID);
            var result = new RegisterModel
            {
                ID = user.ID,
                UserName = user.UserName,
                Email = user.Email,
                Password = user.PassWord,
                Role = user.Role,
                Status = user.Status,
                CreateAt = user.CreateAt,
                UpdateAt = user.UpdateAt,
                DeleteAt = user.DeleteAt,
            };
            return View(result);
        }
    }
}