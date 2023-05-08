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
    public class ExportBillDao
    {
        NTQDBContext db;
        public ExportBillDao()
        {
            db = new NTQDBContext();
        }
        public IEnumerable<ExportModel> ListAllExportBill(string searchString, int page, int pageSize)
        {
            try
            {
                var model = (from a in db.Exports
                             join c in db.Users on a.UserID equals c.ID
                             join d in db.Products on a.ProductID equals d.ID
                             select new ExportModel
                             {
                                 ID = a.ID,
                                 ProductID = a.ProductID,
                                 Count = a.Count,
                                 Price = a.Price,
                                 CreateAt = a.CreateAt,
                                 UserID = a.UserID,
                                 ProductName = d.ProductName,
                                 UserName = c.UserName,
                                 Image = d.Image,
                                 Color = d.Color,
                                 Size = d.Size,
                                 ImportPrice = d.ImportPrice
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
