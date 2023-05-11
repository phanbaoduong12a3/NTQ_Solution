using DataLayer.Dao;
using DataLayer.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTQ_Solution.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        CategoryData categoryData;
        public CategoryController()
        {
            categoryData = new CategoryData();
        }
        // GET: Admin/Category
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            try
            {
                ViewBag.SearchString = searchString;
                var model = categoryData.ListCategory(searchString, page, pageSize);
                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        [HttpGet]
        public ActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateCategory(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool check = categoryData.CheckCategoryName(category.CategoryName);
                    if(check)
                    {
                        category.Status = true;
                        categoryData.InsertCategory(category);
                        TempData["success"] = "Tao moi thanh cong";
                        return RedirectToAction("Index", "Category");
                    }
                    ModelState.AddModelError("", "Tên danh mục đã tồn tại");
                }
                return View("CreateCategory");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            try
            {
                var model = categoryData.GetByID(id);
                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        [HttpPost]
        public ActionResult Edit(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool check = categoryData.CheckCategoryName(category.CategoryName);
                    var oldCategory = categoryData.GetByID(category.ID);
                    if(oldCategory.CategoryName == category.CategoryName) check = true;
                    if (check)
                    {
                        categoryData.UpdateCategory(category);
                        TempData["success"] = "Sua thanh cong";
                        return RedirectToAction("Index", "Category");
                    }
                    ModelState.AddModelError("", "Tên danh mục đã tồn tại");
                }
                return View("Edit");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                categoryData.Delete(id);
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