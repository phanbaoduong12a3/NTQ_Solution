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
    public class ExportBillData
    {
        NTQDBContext db;
        public ExportBillData()
        {
            db = new NTQDBContext();
        }
        /// <summary>
        /// Danh sách phiếu xuất
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<ExportModel> ListAllExportBill(string searchString, int page, int pageSize)
        {
            try
            {
                var exportBillModels = (from a in db.Exports
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
                    exportBillModels = exportBillModels.Where(x => x.ProductName.Contains(searchString));
                    return exportBillModels.OrderByDescending(x => x.CreateAt).ToPagedList(page, pageSize);
                }
                return exportBillModels.OrderByDescending(x => x.CreateAt).ToPagedList(page, pageSize);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
