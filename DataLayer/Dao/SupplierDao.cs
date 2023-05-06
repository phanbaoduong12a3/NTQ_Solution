using DataLayer.EF;
using DataLayer.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Dao
{
    public class SupplierDao
    {
        NTQDBContext db;
        public SupplierDao()
        {
            db = new NTQDBContext();
        }
        public IEnumerable<ProductModel> GetProductOfSupplier(string size, string color, int supplierID, string searchString, int page, int pageSize)
        {
            try
            {
                var model = (from a in db.Products
                             where a.SupplierID == supplierID
                             select new ProductModel
                             {
                                 ID = a.ID,
                                 ProductName = a.ProductName,
                                 CategoryID = a.CategoryID,
                                 SupplierID = a.SupplierID,
                                 Slug = a.Slug,
                                 ImportCount = 1,
                                 ImportPrice = a.ImportPrice,
                                 ExportCount = 1,
                                 Price = a.Price,
                                 Size = a.Size,
                                 Color = a.Color,
                                 Image = a.Image,
                                 NumberViews = a.NumberViews,
                                 Detail = a.Detail,
                                 Count = a.Count
                             }) ;
                if (!string.IsNullOrEmpty(searchString))
                {
                    model = model.Where(x => x.ProductName.Contains(searchString));
                }
                if (!string.IsNullOrEmpty(size))
                {
                    model = model.Where(x => x.Size == size);
                }
                if (!string.IsNullOrEmpty(color))
                {
                    model = model.Where(x => x.Color == color);
                }
                return model.Where(x=>x.Color != null && x.Size != null).OrderByDescending(x => x.NumberViews).ToPagedList(page, pageSize);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public void UpdateImport(ProductModel productModel, int userID)
        {
            var product = db.Products.Find(productModel.ID);
            if(product != null) 
            {
                product.Count += productModel.ImportCount;
            }
            var import = new Import
            {
                ProductID= productModel.ID,
                Count= productModel.ImportCount,
                Price= productModel.ImportPrice * productModel.ImportCount,
                CreateAt = DateTime.Now,
                SupplierID = productModel.SupplierID,
                UserID = userID
            };
            db.Imports.Add(import);
            db.SaveChanges();
        }
        public List<Supplier> ListSupplier()
        {
            return db.Suppliers.OrderBy(x => x.ID).ToList();
        }
    }
}
