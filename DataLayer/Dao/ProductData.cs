using DataLayer.EF;
using DataLayer.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Dao
{
    public class ProductData
    {
        NTQDBContext db;
        public ProductData()
        {
            db = new NTQDBContext();
        }

        /// <summary>
        /// Insert Product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public int Insert(Product product)
        {
            try
            {
                db.Products.Add(product);
                db.SaveChanges();
                return product.ID;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        /// <summary>
        /// Danh sách sản phẩm mới
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<Product> ListNewProduct(int top)
        {
            return db.Products.OrderByDescending(x => x.Price).Take(top).ToList();
        }
        /// <summary>
        /// Danh sashc sản phẩm hot
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<Product> ListHotProduct(int top)
        {
            return db.Products.OrderByDescending(x => x.NumberViews).Take(top).ToList();
        }

        /// <summary>
        /// Update Product
        /// </summary>
        /// <param name="product"></param>
        public void Update(Product product)
        {
            try
            {
                var productModel = db.Products.Find(product.ID);
                if (productModel != null)
                {
                    productModel.ProductName = product.ProductName;
                    productModel.NumberViews = product.NumberViews;
                    productModel.Image = product.Image;
                    productModel.Detail = product.Detail;
                    productModel.Slug = product.Slug;
                    productModel.Status = product.Status;
                    productModel.Price = product.Price;
                    productModel.Trending = product.Trending;
                    productModel.UpdateAt = DateTime.Now;
                    productModel.ImportPrice = product.ImportPrice;
                    productModel.Color = product.Color;
                    productModel.Size = product.Size;
                    productModel.CategoryID = product.CategoryID;
                    productModel.SupplierID = product.SupplierID;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }



        /// <summary>
        /// Delete product
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            try
            {
                var product = db.Products.Find(id);
                product.Status = 0;
                product.DeleteAt = DateTime.Now;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Danh sách toàn bộ sản phẩm
        /// </summary>
        /// <param name="trending"></param>
        /// <param name="searchString"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<Product> ListAllPagingProduct(string size, string color,string categoryID, string supplier, string searchString, int page, int pageSize)
        {
            try
            {
                    IQueryable<Product> product = db.Products;
                    if (!string.IsNullOrEmpty(searchString))
                    {
                    product = product.Where(x => x.ProductName.Contains(searchString));
                    }
                    if (!string.IsNullOrEmpty(color))
                    {
                        int Color = int.Parse(color);
                    product = product.Where(x => x.Color == Color);
                    }
                    if (!string.IsNullOrEmpty(size))
                    {
                        int Size = int.Parse(size);
                        product = product.Where(x => x.Size == Size);
                    }
                    if (!string.IsNullOrEmpty(supplier))
                    {
                        int supplierModel = int.Parse(supplier);
                        product = product.Where(x => x.SupplierID == supplierModel);
                    }
                
                    int categoryid = int.Parse(categoryID);
                    if (categoryid == 6)
                    {

                        return product.Where(x => x.CategoryID == 6).OrderByDescending(x => x.Price).ToPagedList(page, pageSize);
                    }
                    return product.Where(x => x.Color != 0 && x.Size != 0 && x.CategoryID == categoryid).OrderByDescending(x => x.Price).ToPagedList(page, pageSize);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Danh sách sản phẩm phía khách hàng
        /// </summary>
        /// <param name="trending"></param>
        /// <param name="searchString"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<Product> ListProductOnSale(string trending, string searchString, int page, int pageSize)
        {
            try
            {
                IQueryable<Product> product = db.Products.Where(x => x.Count > 0);
                if (!string.IsNullOrEmpty(searchString) || !string.IsNullOrEmpty(trending))
                {
                    product = product.Where(x => x.ProductName.Contains(searchString));
                    if (trending != null)
                    {
                        product = product.Where(x => x.Trending == true);
                    }
                    return product.OrderByDescending(x => x.Price).Where(x => x.Status == 1 && x.Color == 0 && x.Size == 0).ToPagedList(page, pageSize);
                }
                return product.OrderByDescending(x => x.Price).Where(x => x.Status == 1 && x.Color == 0 && x.Size == 0).ToPagedList(page, pageSize);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Tìm kiếm sản phẩm theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product GetProductById(int id)
        {
            try
            {
                return db.Products.Find(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Tìm kiếm sản phẩm theo Trending
        /// </summary>
        /// <returns></returns>
        public List<Product> GetProductByTrending()
        {
            try
            {
                return db.Products.Where(x => x.Trending == true).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        // Home Front-End

        

        /// <summary>
        /// Detail about product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product ViewDetail(int id)
        {
            try
            {
                return db.Products.Find(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public void UpdateView(int id)
        {
            try
            {
                var product = db.Products.Find(id);
                product.NumberViews = product.NumberViews + 1;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        /// <summary>
        /// Danh sách danh mục
        /// </summary>
        /// <returns></returns>
        public List<Category> ListCategory()
        {
            return db.Categories.Where(x => x.Status == true).OrderBy(x => x.ID).ToList();
        }
        /// <summary>
        /// Danh sách sản phẩm theo danh mục
        /// </summary>
        /// <param name="categoryID"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<Product> Category(int categoryID, int page, int pageSize)
        {
            try
            {
                IQueryable<Product> product = db.Products.Where(x => x.Count > 0);
                return product.OrderByDescending(x => x.Price).Where(x => x.CategoryID == categoryID && x.Color == 0 && x.Size == 0).ToPagedList(page, pageSize);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        /// <summary>
        /// SỐ lượng sản phẩm trong giỏ hàng
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public int CartCount(int userID)
        {

            return db.Orders.Where(x => x.Status == 1 && x.UserID == userID).Count();

        }
        /// <summary>
        /// Danh sách size
        /// </summary>
        /// <returns></returns>
        public List<Size> listsize()
        {
            return db.Sizes.OrderBy(x=>x.ID).ToList();
        }

        /// <summary>
        /// Danh sách hình ảnh
        /// </summary>
        /// <returns></returns>
        public List<Color> listcolor()
        {
            return db.Colors.OrderBy(x=>x.ID).ToList();
        }
        /// <summary>
        /// Danh sách nhà cung cấp
        /// </summary>
        /// <returns></returns>
        public List<Supplier> listSupplier()
        {
            return db.Suppliers.OrderBy(x => x.ID).ToList();
        }
        /// <summary>
        /// Danh sách size theo sản phẩm
        /// </summary>
        /// <param name="productName"></param>
        /// <returns></returns>
        public List<Product> listSize(string productName)
        {
            return db.Products.Where(x=>x.ProductName == productName && x.Size != 0 && x.Count > 0).OrderBy(x=>x.Size).ToList();
        }
        /// <summary>
        /// Danh sách màu sắc theo sản phẩm
        /// </summary>
        /// <param name="productName"></param>
        /// <returns></returns>
        public List<Product> listColor(string productName)
        {
            return db.Products.Where(x => x.ProductName == productName && x.Color != 0 && x.Count>0).OrderBy(x =>x.Color).ToList();
        }
    }
}
