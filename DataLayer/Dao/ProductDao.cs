using DataLayer.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Dao
{
    public class ProductDao
    {
        NTQDBContext db ;
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
                if(entity != null)
                {
                    entity.ProductName = product.ProductName;
                    entity.NumberViews = product.NumberViews;
                    entity.Image = product.Image;
                    entity.Detail = product.Detail;
                    entity.Slug = product.Slug;
                    entity.Status = product.Status;
                    entity.Price = product.Price;
                    entity.Trending = product.Trending;
                    entity.UpdateAt= DateTime.Now;
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
            catch(Exception ex)
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
        public IEnumerable<Product> ListAllPagingProduct(string trending, string searchString, int page, int pageSize)
        {
            try
            {
                IQueryable<Product> model = db.Products;
                if (!string.IsNullOrEmpty(searchString) || !string.IsNullOrEmpty(trending))
                {
                    model = model.Where(x => x.ProductName.Contains(searchString));
                    if (trending != null)
                    {
                        model = model.Where(x => x.Trending == true);
                    }
                    return model.OrderByDescending(x => x.NumberViews).ToPagedList(page, pageSize);
                }
                return model.OrderByDescending(x => x.NumberViews).ToPagedList(page, pageSize);
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
                IQueryable<Product> model = db.Products;
                if (!string.IsNullOrEmpty(searchString) || !string.IsNullOrEmpty(trending))
                {
                    model = model.Where(x => x.ProductName.Contains(searchString));
                    if (trending != null)
                    {
                        model = model.Where(x => x.Trending == true);
                    }
                    return model.OrderByDescending(x => x.NumberViews).Where(x => x.Status == 1).ToPagedList(page, pageSize);
                }
                return model.OrderByDescending(x => x.NumberViews).Where(x => x.Status == 1).ToPagedList(page, pageSize);
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


    }
}
