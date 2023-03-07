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

        public int Insert(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
            return product.ID;
        }

        public List<Product> GetAllProduct()
        {
            return db.Products.ToList();
        }

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

        public List<Product> GetProductByView() 
        {
            return db.Products.OrderByDescending(x => x.NumberViews).ToList();
        }

        public List<Product> GetProductByTrending()
        {
            return db.Products.Where(x => x.Trending == true).ToList();
        }

        public Product ViewDetail(int id)
        {
            return db.Products.Find(id);
        }
    }
}
