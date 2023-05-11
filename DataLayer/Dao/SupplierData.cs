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
    public class SupplierData
    {
        NTQDBContext db;
        public SupplierData()
        {
            db = new NTQDBContext();
        }
        /// <summary>
        /// Danh sách sản phẩm theo nhà cung cấp
        /// </summary>
        /// <param name="size"></param>
        /// <param name="color"></param>
        /// <param name="supplierID"></param>
        /// <param name="searchString"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<ProductModel> GetProductOfSupplier(string size, string color, int supplierID, string searchString, int page, int pageSize)
        {
            try
            {
                var productModels = (from a in db.Products
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
                    productModels = productModels.Where(x => x.ProductName.Contains(searchString));
                }
                if (!string.IsNullOrEmpty(size))
                {
                    int Size = int.Parse(size);
                    productModels = productModels.Where(x => x.Size == Size);
                }
                if (!string.IsNullOrEmpty(color))
                {
                    int Color = int.Parse(color);
                    productModels = productModels.Where(x => x.Size == Color);
                }
                return productModels.Where(x=>x.Color != 0 && x.Size != 0).OrderByDescending(x => x.NumberViews).ToPagedList(page, pageSize);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        /// <summary>
        /// Cập nhật khi nhập hàng
        /// </summary>
        /// <param name="productModel"></param>
        /// <param name="userID"></param>
        public void UpdateImport(ProductModel productModel, int userID)
        {
            var product = db.Products.Find(productModel.ID);
            if(product != null) 
            {
                product.ImportPrice = productModel.ImportPrice;
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
        /// <summary>
        /// Danh sách nhà cung cấp
        /// </summary>
        /// <returns></returns>
        public List<Supplier> ListSupplier()
        {
            return db.Suppliers.OrderBy(x => x.ID).ToList();
        }
    }
}
