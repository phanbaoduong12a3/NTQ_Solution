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
        NTQDBContext db = null;
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
            db.Products.Add(product);
            db.SaveChanges();
            return product.ID;
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
                    entity.Slug = product.Slug;
                    entity.Status = product.Status;
                    entity.Price = product.Price;
                    entity.Trending = product.Trending;
                    entity.UpdateAt= DateTime.Now;
                    db.SaveChanges();
                }
            }
            catch 
            {
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
            IQueryable<Product> model = db.Products;
            if(!string.IsNullOrEmpty(searchString) || !string.IsNullOrEmpty(trending))
            {
                model = model.Where(x => x.ProductName.Contains(searchString));
                if(trending != null)
                {
                    model = model.Where(x => x.Trending == true);
                }
                if(model == null)
                {
                    return null;
                }
                return model.OrderByDescending(x => x.NumberViews).ToPagedList(page,pageSize);
            }
            return model.OrderByDescending(x => x.NumberViews).ToPagedList(page, pageSize);
        }

        /// <summary>
        /// Get Product By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product GetProductById(int id)
        {
            return db.Products.Find(id);
        }

        /// <summary>
        /// Get Product with View
        /// </summary>
        /// <returns></returns>
        public List<Product> GetProductByView() 
        {
            return db.Products.OrderByDescending(x => x.NumberViews).ToList();
        }
        /// <summary>
        /// Get Product with Trending
        /// </summary>
        /// <returns></returns>
        public List<Product> GetProductByTrending()
        {
            return db.Products.Where(x => x.Trending == true).ToList();
        }



        // Home Front-End

        /// <summary>
        /// List All Product
        /// </summary>
        /// <returns></returns>
        public List<Product> GetAllProduct()
        {
            return db.Products.ToList();
        }

        /// <summary>
        /// Detail about product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product ViewDetail(int id)
        {
            return db.Products.Find(id);
        }
    }
}
