using DataLayer.EF;
using DataLayer.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Dao
{
    public class ProductDao
    {
        NTQDBContext db;
        public ProductDao()
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
        /// Update Product
        /// </summary>
        /// <param name="product"></param>
        public void Update(Product product)
        {
            try
            {
                var entity = db.Products.Find(product.ID);
                if (entity != null)
                {
                    entity.ProductName = product.ProductName;
                    entity.NumberViews = product.NumberViews;
                    entity.Image = product.Image;
                    entity.Detail = product.Detail;
                    entity.Slug = product.Slug;
                    entity.Status = product.Status;
                    entity.Price = product.Price;
                    entity.Trending = product.Trending;
                    entity.UpdateAt = DateTime.Now;
                    entity.ImportPrice = product.ImportPrice;
                    entity.Color = product.Color;
                    entity.Size = product.Size;
                    entity.CategoryID = product.CategoryID;
                    entity.SupplierID = product.SupplierID;
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
        /// List All Product with Paging
        /// </summary>
        /// <param name="trending"></param>
        /// <param name="searchString"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<Product> ListAllPagingProduct(string size, string color, string supplier, string searchString, int page, int pageSize)
        {
            try
            {
                IQueryable<Product> model = db.Products;
                if (!string.IsNullOrEmpty(searchString))
                {
                    model = model.Where(x => x.ProductName.Contains(searchString));
                }
                if (!string.IsNullOrEmpty(color))
                {
                    model = model.Where(x => x.Color == color);
                }
                if (!string.IsNullOrEmpty(size))
                {
                    model = model.Where(x => x.Size == size);
                }
                if (!string.IsNullOrEmpty(supplier))
                {
                    int supplierModel = int.Parse(supplier);
                    model = model.Where(x => x.SupplierID == supplierModel);
                }
                return model.Where(x => x.Color != null && x.Size != null).OrderByDescending(x => x.Price).ToPagedList(page, pageSize);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// List Product On Sale
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
                IQueryable<Product> model = db.Products.Where(x => x.Count > 0);
                if (!string.IsNullOrEmpty(searchString) || !string.IsNullOrEmpty(trending))
                {
                    model = model.Where(x => x.ProductName.Contains(searchString));
                    if (trending != null)
                    {
                        model = model.Where(x => x.Trending == true);
                    }
                    return model.OrderByDescending(x => x.Price).Where(x => x.Status == 1 && x.Color == null && x.Size == null).ToPagedList(page, pageSize);
                }
                return model.OrderByDescending(x => x.Price).Where(x => x.Status == 1 && x.Color == null && x.Size == null).ToPagedList(page, pageSize);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Get Product By Id
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
        /// Get Product with View
        /// </summary>
        /// <returns></returns>
        public List<Product> GetProductByView()
        {
            try
            {
                return db.Products.OrderByDescending(x => x.NumberViews).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        /// <summary>
        /// Get Product with Trending
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
        /// List All Product
        /// </summary>
        /// <returns></returns>
        public List<Product> GetAllProduct()
        {
            try
            {
                return db.Products.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

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
        public List<Category> ListCategory()
        {
            return db.Categories.Where(x => x.Status == true).OrderBy(x => x.ID).ToList();
        }
        public IEnumerable<Product> Category(int categoryID, int page, int pageSize)
        {
            try
            {
                IQueryable<Product> model = db.Products.Where(x => x.Count > 0);
                return model.OrderByDescending(x => x.Price).Where(x => x.CategoryID == categoryID && x.Color == null && x.Size == null).ToPagedList(page, pageSize);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public List<Product> ListColor(string productName)
        {
            var model = db.Products.Where(x => x.ProductName == productName).ToList();
            return model;
        }
        public List<Product> ListSize(string productName)
        {
            var model = db.Products.Where(x => x.ProductName == productName).ToList();
            return model;
        }
        public int CartCount(int userID)
        {

            return db.Orders.Where(x => x.Status == 1 && x.UserID == userID).Count();

        }

    }
}
