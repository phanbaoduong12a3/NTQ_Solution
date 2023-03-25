using DataLayer.Dao;
using DataLayer.EF;
using Microsoft.Ajax.Utilities;
using NTQ_Solution.Areas.Admin.Data;
using System;
using System.Web.Mvc;

namespace NTQ_Solution.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        ProductDao productDao ;
        public ProductController()
        {
            productDao = new ProductDao();
        }
        // GET: Admin/Product
        
        public ActionResult Index(string trending, string searchString, int page = 1, int pageSize = 5)
        {
            try
            {
                var model = productDao.ListAllPagingProduct(trending, searchString, page, pageSize);
                return View(model);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        [HttpGet]
        public ActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateProduct(ProductModel productModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool trending;
                    if (productModel.Trending == true) trending = true;
                    else trending = false;
                    var product = new Product
                    {
                        ProductName = productModel.ProductName,
                        Slug = productModel.Slug,
                        Detail = productModel.Detail,
                        Status = 1,
                        NumberViews = 0,
                        Trending = trending,
                        Price = productModel.Price,
                        Image = productModel.Image,
                        CreateAt = DateTime.Now
                    };
                    productDao.Insert(product);
                    TempData["success"] = "Create New Product success";
                    return RedirectToAction("Index", "Product");
                }
                return View(productModel);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        [HttpGet]
        public ActionResult UpdateProduct(int id)
        {
            try
            {
                var temp = productDao.GetProductById(id);
                bool checkTrending,status;
                if(temp.Status == 1) status = true;
                else status = false;
                if (temp.Trending == true) checkTrending = true;
                else checkTrending = false;
                var product = new ProductModel
                {
                    ProductName = temp.ProductName,
                    Slug = temp.Slug,
                    Detail = temp.Detail,
                    Status = status,
                    NumberViews = temp.NumberViews,
                    Trending = checkTrending,
                    Price = temp.Price,
                    Image = temp.Image,
                    UpdateAt = DateTime.Now
                };
                return View(product);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        [HttpPost]
        public ActionResult UpdateProduct(ProductModel productModel)
        {
            try
            {
                int status;
                if (productModel.Status) status = 1;
                else status = 0;
                if(ModelState.IsValid)
                {
                    var product = new Product
                    {
                        ID = productModel.ID,
                        ProductName = productModel.ProductName,
                        Slug = productModel.Slug,
                        Detail = productModel.Detail,
                        Status = status,
                        NumberViews = productModel.NumberViews,
                        Trending = productModel.Trending,
                        Price = productModel.Price,
                        Image = productModel.Image,
                        UpdateAt = DateTime.Now
                    };
                    productDao.Update(product);
                    TempData["success"] = "Update Product success";
                    return RedirectToAction("Index", "Product");
                }
                return View(productModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                productDao.Delete(id);
                TempData["success"] = "Delete Product success";
                return RedirectToAction("Index");
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }
    }
}