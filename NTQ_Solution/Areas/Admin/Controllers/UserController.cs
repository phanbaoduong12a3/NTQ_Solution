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
using Antlr.Runtime.Tree;

namespace NTQ_Solution.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        /// <summary>
        /// Home
        /// </summary>
        /// <param name="active"></param>
        /// <param name="inActive"></param>
        /// <param name="admin"></param>
        /// <param name="user"></param>
        /// <param name="searchString"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ActionResult Index(string active,string inActive,string admin,string user,string searchString, int page = 1, int pageSize = 10)
        {
            try
            {
                var dao = new UserDao();
                var model = dao.ListAllPaging(active, inActive, admin, user, searchString, page, pageSize);
                return View(model);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }
        
        /// <summary>
        /// Create User
        /// </summary>
        /// <returns></returns>
        /// 

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
                    bool checkConfirmPassword = dao.CheckConfirmPassword(registerModel.ConfirmPassword, registerModel.Password);
                    if (result == 1 && checkConfirmPassword)
                    {
                        var user = new User
                        {
                            UserName = registerModel.UserName,
                            PassWord = registerModel.Password,
                            Email = registerModel.Email,
                            CreateAt = DateTime.Now,
                            Role = 0,
                            Status = 1
                        };
                        dao.Insert(user);
                        SetAlert("Create New User Seccess", "success");
                        return RedirectToAction("Index", "ListUser");
                    }
                    if (!checkConfirmPassword) { ModelState.AddModelError("", "Enter ConfirmPassword again"); }
                    else if (result == -1)
                    {
                        ModelState.AddModelError("", "Email is invalid");
                    }
                    else
                    {
                        ModelState.AddModelError("", "UserName is invalid");
                    }
                }
                return View("CreateUser");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet]
        public ActionResult Edit(int id)
        {
            try
            {
                var dao = new UserDao();
                var temp = dao.GetById(id);
                if (temp.Role == 0)
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
                    UpdateAt = temp.UpdateAt,
                    Role = temp.Role,
                    Status = temp.Status
                };
                return View(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        
        [HttpPost]
        public ActionResult Edit(RegisterModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dao = new UserDao();
                    bool checkUserName = dao.CheckUserName(model.UserName);
                    bool checkEmail = dao.CheckEmail(model.Email);
                    bool checkConfirmPassword = dao.CheckConfirmPassword(model.ConfirmPassword, model.Password);
                    var userOld = dao.GetById(model.ID);
                    if(model.UserName == userOld.UserName) checkUserName = true;
                    if(model.Email == userOld.Email) checkEmail = true;
                    if (checkUserName && checkEmail && checkConfirmPassword)
                    {
                        var user = new User
                        {
                            ID = model.ID,
                            Email = model.Email,
                            UserName = model.UserName,
                            PassWord = model.Password,
                            Role = model.Role,
                            Status = model.Status
                        };
                        dao.Update(user);
                        SetAlert("Update Seccess", "success");
                        return RedirectToAction("Index", "ListUser");
                    }
                    if(!checkEmail) { ModelState.AddModelError("", "Email is invalid"); }
                    if (!checkUserName) { ModelState.AddModelError("", "UserName is invalid"); };
                    if (!checkConfirmPassword) { ModelState.AddModelError("", "Enter ConfirmPassword again"); }
                }
                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            try
            {
                new UserDao().Delete(id);
                SetAlert("Delete User Seccess", "success");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

    }
}