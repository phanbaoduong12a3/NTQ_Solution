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
    public class UserController : Controller
    {
        // GET: Admin/User
        public ActionResult Index(int page = 1, int pageSize = 1)
        {
            var dao = new UserDao();
            var model = dao.ListAllPaging(page,pageSize);
            return View(model);
        }
        [HttpGet]
        public ActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateUser(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                int result = dao.CheckUser(registerModel.UserName, registerModel.Email);
                if (result == 1)
                {
                    var user = new User
                    {
                        UserName = registerModel.UserName,
                        PassWord = Encryptor.MD5Hash(registerModel.Password),
                        Email = registerModel.Email,
                        Create_at = DateTime.Now,
                        Role = 0,
                        Status = 1
                    };
                    dao.Insert(user);
                    return RedirectToAction("Index", "ListUser");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Email đã tồn tại");
                }
                else
                {
                    ModelState.AddModelError("", "UserName đã tồn tại");
                }
            }
            return View("CreateUser");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user = new UserDao().GetById(id);
            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                int check = dao.CheckUser(user.UserName, user.Email);
                if (check == 1)
                {
                    user.PassWord = Encryptor.MD5Hash(user.PassWord);
                    var result = dao.Update(user);
                    if(result)
                    {
                        return RedirectToAction("Index", "ListUser");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Cập nhật không thành công");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "UserName đã tồn tại");
                }
            }
            return View("Edit");
        }
    }
}