﻿using DataLayer.Dao;
using DataLayer.EF;
using DataLayer.ViewModel;
using Microsoft.Ajax.Utilities;
using NTQ_Solution.Areas.Admin.Data;
using System;
using System.Web.Mvc;

namespace NTQ_Solution.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        ProductData productData ;
        public ProductController()
        {
            productData = new ProductData();
        }
        // GET: Admin/Product
        
        public ActionResult Index(int categoryID, string size, string color,  string supplierID, string searchString, int page = 1, int pageSize = 5)
        {
            try
            {
                ViewBag.listColor = productData.listcolor();
                ViewBag.listSize = productData.listsize();
                ViewBag.listSupplier = productData.listSupplier();
                ViewBag.SearchString = searchString;
                ViewBag.Size = size;
                ViewBag.Color = color;
                ViewBag.Supplier = supplierID;
                ViewBag.Category = categoryID;
                var model = productData.ListAllPagingProduct(categoryID, size, color,  supplierID, searchString, page, pageSize);
                return View(model);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        [HttpGet]
        public ActionResult CreateProduct(int id)
        {
            var listSize = productData.listsize();
            SelectList sizeList = new SelectList(listSize, "ID", "SizeName");
            ViewBag.SizeList = sizeList;
            var listColor = productData.listcolor();
            SelectList colorList = new SelectList(listColor, "ID", "ColorName");
            ViewBag.ColorList = colorList;
            var listCategory = productData.ListCategory();
            var listSupplier = new SupplierData().ListSupplier();
            SelectList cateList = new SelectList(listCategory, "ID", "CategoryName");
            ViewBag.CategoryList = cateList;
            SelectList supList = new SelectList(listSupplier, "ID", "SupplierName");
            ViewBag.SupplierList = supList;
            ViewBag.Category = id;
            return View();
        }

        [HttpPost]
        public ActionResult CreateProduct(ProductModel productModel,string category,string supplier,string size,string color)
        {
            try
            {
                var listSize = productData.listsize();
                SelectList sizeList = new SelectList(listSize, "ID", "SizeName");
                ViewBag.SizeList = sizeList;
                var listColor = productData.listcolor();
                SelectList colorList = new SelectList(listColor, "ID", "ColorName");
                ViewBag.ColorList = colorList;
                var listCategory = productData.ListCategory();
                var listSupplier = new SupplierData().ListSupplier();
                SelectList cateList = new SelectList(listCategory, "ID", "CategoryName");
                ViewBag.CategoryList = cateList;
                SelectList supList = new SelectList(listSupplier, "ID", "SupplierName");
                ViewBag.SupplierList = supList;
                if (ModelState.IsValid)
                {
                    int Size =  0;
                    int Color = 0;
                    if (color!= null) { Size = int.Parse(color); }
                    if(size != null) { Size = int.Parse(size); }
                    int Category = int.Parse(category);
                    int Supplier = int.Parse(supplier);
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
                        Count=30,
                        Trending = trending,
                        Price = productModel.Price,
                        Image = productModel.Image,
                        CreateAt = DateTime.Now,
                        ImportPrice = productModel.ImportPrice,
                        Color = Color,
                        Size = Size,
                        CategoryID = Category,
                        SupplierID = Supplier
                    };
                    productData.Insert(product);
                    TempData["success"] = "Them san pham thanh cong";
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
                var temp = productData.GetProductById(id);
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
                    UpdateAt = DateTime.Now,
                    ImportPrice = temp.ImportPrice,
                    Color = temp.Color,
                    Size = temp.Size,
                    CategoryID = temp.CategoryID,
                    SupplierID = temp.SupplierID
                };
                var listSize = productData.listsize();
                SelectList sizeList = new SelectList(listSize, "ID", "SizeName");
                ViewBag.SizeList = sizeList;
                var listColor = productData.listcolor();
                SelectList colorList = new SelectList(listColor, "ID", "ColorName");
                ViewBag.ColorList = colorList;
                var listCategory = productData.ListCategory();
                var listSupplier = new SupplierData().ListSupplier();
                SelectList cateList = new SelectList(listCategory, "ID", "CategoryName");
                ViewBag.CategoryList = cateList;
                SelectList supList = new SelectList(listSupplier, "ID", "SupplierName");
                ViewBag.SupplierList = supList;
                return View(product);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        [HttpPost]
        public ActionResult UpdateProduct(ProductModel productModel, string category, string supplier,string color, string size)
        {
            try
            {
                var listSize = productData.listsize();
                SelectList sizeList = new SelectList(listSize, "ID", "SizeName");
                ViewBag.SizeList = sizeList;
                var listColor = productData.listcolor();
                SelectList colorList = new SelectList(listColor, "ID", "ColorName");
                ViewBag.ColorList = colorList;
                var listCategory = productData.ListCategory();
                var listSupplier = new SupplierData().ListSupplier();
                SelectList cateList = new SelectList(listCategory, "ID", "CategoryName");
                ViewBag.CategoryList = cateList;
                SelectList supList = new SelectList(listSupplier, "ID", "SupplierName");
                ViewBag.SupplierList = supList;
                int status;
                int Category = int.Parse(category);
                int Supplier = int.Parse(supplier);
                int Color = int.Parse(color);
                int Size = int.Parse(size);
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
                        UpdateAt = DateTime.Now,
                        ImportPrice = productModel.ImportPrice,
                        Color = Color,
                        Size = Size,
                        CategoryID = Category,
                        SupplierID = Supplier
                    };
                    productData.Update(product);
                    TempData["success"] = "Sua san pham thanh cong";
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
                productData.Delete(id);
                TempData["success"] = "Xoa san pham thanh cong";
                return RedirectToAction("Index");
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }
        [ChildActionOnly]
        public ActionResult ProductMenu()
        {
            var model = productData.ListCategory();
            return View(model);
        }
    }
}