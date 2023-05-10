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
        public List<Product> ListNewProduct(int top)
        {
            return db.Products.OrderByDescending(x => x.Price).Take(top).ToList();
        }
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
        public IEnumerable<Product> ListAllPagingProduct(string size, string color,string categoryID, string supplier, string searchString, int page, int pageSize)
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
                        int Color = int.Parse(color);
                        model = model.Where(x => x.Color == Color);
                    }
                    if (!string.IsNullOrEmpty(size))
                    {
                        int Size = int.Parse(size);
                        model = model.Where(x => x.Size == Size);
                    }
                    if (!string.IsNullOrEmpty(supplier))
                    {
                        int supplierModel = int.Parse(supplier);
                        model = model.Where(x => x.SupplierID == supplierModel);
                    }
                
                    int categoryid = int.Parse(categoryID);
                    if (categoryid == 6)
                    {

                        return model.Where(x => x.CategoryID == 6).OrderByDescending(x => x.Price).ToPagedList(page, pageSize);
                    }
                    return model.Where(x => x.Color != 0 && x.Size != 0 && x.CategoryID == categoryid).OrderByDescending(x => x.Price).ToPagedList(page, pageSize);

                

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
                    return model.OrderByDescending(x => x.Price).Where(x => x.Status == 1 && x.Color == 0 && x.Size == 0).ToPagedList(page, pageSize);
                }
                return model.OrderByDescending(x => x.Price).Where(x => x.Status == 1 && x.Color == 0 && x.Size == 0).ToPagedList(page, pageSize);
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
                return model.OrderByDescending(x => x.Price).Where(x => x.CategoryID == categoryID && x.Color == 0 && x.Size == 0).ToPagedList(page, pageSize);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public int CartCount(int userID)
        {

            return db.Orders.Where(x => x.Status == 1 && x.UserID == userID).Count();

        }
        public List<Size> listsize()
        {
            return db.Sizes.OrderBy(x=>x.ID).ToList();
        }
        public List<Color> listcolor()
        {
            return db.Colors.OrderBy(x=>x.ID).ToList();
        }
        public List<Supplier> listSupplier()
        {
            return db.Suppliers.OrderBy(x => x.ID).ToList();
        }
        public List<Product> listSize(string productName)
        {
            return db.Products.Where(x=>x.ProductName == productName && x.Size != 0 && x.Count > 0).OrderBy(x=>x.Size).ToList();
        }
        public List<Product> listColor(string productName)
        {
            return db.Products.Where(x => x.ProductName == productName && x.Color != 0 && x.Count>0).OrderBy(x =>x.Color).ToList();
        }
    }
}
