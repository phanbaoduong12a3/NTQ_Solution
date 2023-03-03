using DataLayer.Dao;
using DataLayer.EF;
using NTQ_Solution.Areas.Admin.Data;
using NTQ_Solution.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace NTQ_Solution.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        // GET: Admin/User
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new UserDao();
            var model = dao.ListAllPaging(searchString, page,pageSize);
            ViewBag.SearchString = searchString;
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
            try
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
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var dao = new UserDao();
            var temp = dao.GetById(id);
            if(temp.Role == 0)
            {
                ViewBag.Role = "User";
            }
            else
            {
                ViewBag.Role = "Admin";
            }
            var user = new RegisterModel
            {
                ID = temp.ID,
                UserName = temp.UserName,
                Email = temp.Email,
                Password = temp.PassWord,
                Update_at = temp.Update_at
                
            };
            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(RegisterModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dao = new UserDao();
                    var user = new User
                    {
                        ID = model.ID,
                        UserName = model.UserName,
                        PassWord = Encryptor.MD5Hash(model.Password)
                    };
                    dao.Update(user);
                    return RedirectToAction("Index", "ListUser");
                }
                return View("Edit");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public ActionResult Delete(int id)
        {
            new UserDao().Delete(id);
            return RedirectToAction("Index");
        }

    }
}