using DataLayer.Dao;
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
        ProductDao productDao ;
        public ProductController()
        {
            productDao = new ProductDao();
        }
        // GET: Admin/Product
        
        public ActionResult Index(string size, string color, string categoryID, string supplier, string searchString, int page = 1, int pageSize = 5)
        {
            try
            {
                ViewBag.listColor = productDao.listcolor();
                ViewBag.listSize = productDao.listsize();
                ViewBag.listSupplier = productDao.listSupplier();
                ViewBag.SearchString = searchString;
                ViewBag.Size = size;
                ViewBag.Color = color;
                ViewBag.Supplier = supplier;
                ViewBag.Category = categoryID;
                var model = productDao.ListAllPagingProduct(size, color, categoryID, supplier, searchString, page, pageSize);
                return View(model);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }

        [HttpGet]
        public ActionResult CreateProduct()
        {
            var listSize = productDao.listsize();
            SelectList sizeList = new SelectList(listSize, "ID", "SizeName");
            ViewBag.SizeList = sizeList;
            var listColor = productDao.listcolor();
            SelectList colorList = new SelectList(listColor, "ID", "ColorName");
            ViewBag.ColorList = colorList;
            var listCategory = productDao.ListCategory();
            var listSupplier = new SupplierDao().ListSupplier();
            SelectList cateList = new SelectList(listCategory, "ID", "CategoryName");
            ViewBag.CategoryList = cateList;
            SelectList supList = new SelectList(listSupplier, "ID", "SupplierName");
            ViewBag.SupplierList = supList;
            return View();
        }

        [HttpPost]
        public ActionResult CreateProduct(ProductModel productModel,string category,string supplier,string size,string color)
        {
            try
            {
                var listSize = productDao.listsize();
                SelectList sizeList = new SelectList(listSize, "ID", "SizeName");
                ViewBag.SizeList = sizeList;
                var listColor = productDao.listcolor();
                SelectList colorList = new SelectList(listColor, "ID", "ColorName");
                ViewBag.ColorList = colorList;
                var listCategory = productDao.ListCategory();
                var listSupplier = new SupplierDao().ListSupplier();
                SelectList cateList = new SelectList(listCategory, "ID", "CategoryName");
                ViewBag.CategoryList = cateList;
                SelectList supList = new SelectList(listSupplier, "ID", "SupplierName");
                ViewBag.SupplierList = supList;
                if (ModelState.IsValid)
                {
                    int Size = int.Parse(size);
                    int Color = int.Parse(color);
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
                    productDao.Insert(product);
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
                    UpdateAt = DateTime.Now,
                    ImportPrice = temp.ImportPrice,
                    Color = temp.Color,
                    Size = temp.Size,
                    CategoryID = temp.CategoryID,
                    SupplierID = temp.SupplierID
                };
                var listSize = productDao.listsize();
                SelectList sizeList = new SelectList(listSize, "ID", "SizeName");
                ViewBag.SizeList = sizeList;
                var listColor = productDao.listcolor();
                SelectList colorList = new SelectList(listColor, "ID", "ColorName");
                ViewBag.ColorList = colorList;
                var listCategory = productDao.ListCategory();
                var listSupplier = new SupplierDao().ListSupplier();
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
                var listSize = productDao.listsize();
                SelectList sizeList = new SelectList(listSize, "ID", "SizeName");
                ViewBag.SizeList = sizeList;
                var listColor = productDao.listcolor();
                SelectList colorList = new SelectList(listColor, "ID", "ColorName");
                ViewBag.ColorList = colorList;
                var listCategory = productDao.ListCategory();
                var listSupplier = new SupplierDao().ListSupplier();
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
                    productDao.Update(product);
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
                productDao.Delete(id);
                TempData["success"] = "Xoa san pham thanh cong";
                return RedirectToAction("Index");
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }
        [ChildActionOnly]
        public ActionResult ProductMenu()
        {
            var model = productDao.ListCategory();
            return View(model);
        }
    }
}