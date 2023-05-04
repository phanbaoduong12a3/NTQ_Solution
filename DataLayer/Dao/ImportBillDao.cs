using DataLayer.EF;
using DataLayer.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Dao
{
    public class ImportBillDao
    {
        NTQDBContext db;
        public ImportBillDao()
        {
            db = new NTQDBContext();
        }
        public IEnumerable<ImportModel> ListAllImportBill(string searchString, int page, int pageSize)
        {
            try
            {
                var model = (from a in db.Imports
                             join b in db.Suppliers on a.SupplierID equals b.ID
                             join c in db.Users on a.UserID equals c.ID
                             join d in db.Products on a.ProductID equals d.ID
                             select new ImportModel
                             {
                                 ID = a.ID,
                                 ProductID = a.ProductID,
                                 Count = a.Count,
                                 Price = a.Price,
                                 CreateAt = a.CreateAt,
                                 SupplierID = a.SupplierID,
                                 UserID = a.UserID,
                                 ProductName = d.ProductName,
                                 SupplierName = b.SupplierName,
                                 UserName = c.UserName,
                                 Image = d.Image,
                                 Color = d.Color,
                                 Size = d.Size
                             });
                if (!string.IsNullOrEmpty(searchString))
                {
                    model = model.Where(x => x.ProductName.Contains(searchString));
                    return model.OrderByDescending(x => x.CreateAt).ToPagedList(page, pageSize);
                }
                return model.OrderByDescending(x => x.CreateAt).ToPagedList(page, pageSize);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
