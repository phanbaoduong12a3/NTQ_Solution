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
        UserDao userDao ;
        public UserController() 
        {
            userDao = new UserDao();
        }
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
        public ActionResult Index(string status,string role, string searchString, int page = 1, int pageSize = 5)
        {
            try
            {
                ViewBag.SearchString = searchString;
                ViewBag.Status = status;
                ViewBag.Role = role;
                var model = userDao.ListAllPaging(status, role, searchString, page, pageSize);
                
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
                    int result = userDao.CheckUser(registerModel.UserName, registerModel.Email);
                    bool checkConfirmPassword = userDao.CheckConfirmPassword(registerModel.ConfirmPassword, registerModel.Password);
                    if (result == 1 && checkConfirmPassword)
                    {
                        var user = new User
                        {
                            UserName = registerModel.UserName,
                            PassWord = registerModel.Password,
                            Email = registerModel.Email,
                            CreateAt = DateTime.Now,
                            Role = 1,
                            Status = 1
                        };
                        userDao.Insert(user);
                        TempData["success"] = "Tao moi thanh cong";
                        return RedirectToAction("Index", "ListUser");
                    }
                    if (!checkConfirmPassword) { ModelState.AddModelError("", "Nhập lại ConfirmPassword"); }
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
                var temp = userDao.GetById(id);
                bool status;
                if(temp.Status == 1)
                {
                    status = true;
                }
                else
                {
                    status = false;
                }
                if (temp.Role == 0)
                {
                    ViewBag.Role = "Khách hàng";
                }
                if (temp.Role == 1)
                {
                    ViewBag.Role = "Nhân viên";
                }
                var registerModel = new RegisterModel
                {
                    ID = temp.ID,
                    UserName = temp.UserName,
                    Email = temp.Email,
                    Password = temp.PassWord,
                    UpdateAt = temp.UpdateAt,
                    Role = temp.Role,
                    Status = status
                };
                return View(registerModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        
        [HttpPost]
        public ActionResult Edit(RegisterModel registerModel,string role)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int Role = registerModel.Role;
                    if (role != "") Role = int.Parse(role);
                    bool checkUserName = userDao.CheckUserName(registerModel.UserName);
                    bool checkEmail = userDao.CheckEmail(registerModel.Email);
                    bool checkConfirmPassword = userDao.CheckConfirmPassword(registerModel.ConfirmPassword, registerModel.Password);
                    var userOld = userDao.GetById(registerModel.ID);
                    if(registerModel.UserName == userOld.UserName) checkUserName = true;
                    if(registerModel.Email == userOld.Email) checkEmail = true;
                    int status;
                    if (registerModel.Status) { status = 1; }
                    else status = 0;
                    if (checkUserName && checkEmail && checkConfirmPassword)
                    {
                        var user = new User
                        {
                            ID = registerModel.ID,
                            Email = registerModel.Email,
                            UserName = registerModel.UserName,
                            PassWord = registerModel.Password,
                            Role = Role,
                            Status = status
                        };
                        userDao.Update(user);
                        TempData["success"] = "Sua thanh cong";
                        return RedirectToAction("Index", "ListUser");
                    }
                    if(!checkEmail) { ModelState.AddModelError("", "Email đã tồn tại"); }
                    if (!checkUserName) { ModelState.AddModelError("", "UserName đã tồn tại"); };
                    if (!checkConfirmPassword) { ModelState.AddModelError("", "Nhập lại ConfirmPassword"); }
                }
                return View(registerModel);
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
                userDao.Delete(id);
                TempData["success"] = "Xoa thanh cong";
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