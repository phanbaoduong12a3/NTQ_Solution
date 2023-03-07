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
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        public ActionResult Index(string trending, string searchString, int page=1, int pageSize=15)
        {
            var dao = new ProductDao();
            var model = dao.ListAllPagingProduct(trending, searchString, page, pageSize);
            return View(model);
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
                if(ModelState.IsValid)
                {
                    var dao = new ProductDao();
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
                        Price= productModel.Price,
                        Image = productModel.Image,
                        CreateAt = DateTime.Now
                    };
                    dao.Insert(product);
                    SetAlert("Create New Product Seccess", "success");
                    return RedirectToAction("Index", "Product");
                }
                return View("CreateProduct");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}