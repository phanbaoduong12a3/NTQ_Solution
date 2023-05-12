using DataLayer.EF;
using DataLayer.ViewModel;
using PagedList;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataLayer.Dao
{
    public class ImportBillData
    {
        NTQDBContext db;
        public ImportBillData()
        {
            db = new NTQDBContext();
        }
        /// <summary>
        /// Danh sách phiếu nhập
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<ImportModel> ListAllImportBill(string searchString, int page, int pageSize)
        {
            try
            {
                var importBillModels = (from a in db.Imports
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
                    var date = searchString.Split('/').ToArray();
                    int day = 0;
                    if (!string.IsNullOrEmpty(date[0]))
                    {
                        day = int.Parse(date[0]);
                    }
                    int month = int.Parse(date[1]);
                    int year = int.Parse(date[2]);
                    if(year > 2019 && year < 2050)
                    {
                       if(month>0 && month < 13)
                        {
                            if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
                            {
                                if (day > 0 && day < 32)
                                {
                                    importBillModels = importBillModels.Where(x => x.CreateAt.Value.Day == day && x.CreateAt.Value.Month == month && x.CreateAt.Value.Year == year);

                                    return importBillModels.OrderByDescending(x => x.CreateAt).ToPagedList(page, pageSize);
                                }
                                if(day == 0)
                                {
                                    importBillModels = importBillModels.Where(x =>  x.CreateAt.Value.Month == month && x.CreateAt.Value.Year == year);
                                }
                                return importBillModels.Where(x => x.CreateAt.Value.Day > 31).OrderByDescending(x => x.CreateAt).ToPagedList(page, pageSize);

                            }
                            else
                            {
                                if(month == 2)
                                {
                                    if(day>0 && day < 30)
                                    {
                                        importBillModels = importBillModels.Where(x => x.CreateAt.Value.Day == day && x.CreateAt.Value.Month == month && x.CreateAt.Value.Year == year);
                                        return importBillModels.OrderByDescending(x => x.CreateAt).ToPagedList(page, pageSize);
                                    }
                                    if (day == 0)
                                    {
                                        importBillModels = importBillModels.Where(x => x.CreateAt.Value.Month == month && x.CreateAt.Value.Year == year);
                                    }
                                    return importBillModels.Where(x => x.CreateAt.Value.Day > 31).OrderByDescending(x => x.CreateAt).ToPagedList(page, pageSize);
                                }
                                else
                                {
                                    if(day>0 && day < 31)
                                    {
                                        importBillModels = importBillModels.Where(x => x.CreateAt.Value.Day == day && x.CreateAt.Value.Month == month && x.CreateAt.Value.Year == year);
                                        return importBillModels.OrderByDescending(x => x.CreateAt).ToPagedList(page, pageSize);
                                    }
                                    if (day == 0)
                                    {
                                        importBillModels = importBillModels.Where(x => x.CreateAt.Value.Month == month && x.CreateAt.Value.Year == year);
                                    }
                                    return importBillModels.Where(x => x.CreateAt.Value.Day > 31).OrderByDescending(x => x.CreateAt).ToPagedList(page, pageSize);
                                }
                            }
                        }
                        return importBillModels.Where(x => x.CreateAt.Value.Month > 12).OrderByDescending(x => x.CreateAt).ToPagedList(page, pageSize);
                    }
                    return importBillModels.Where(x=>x.CreateAt.Value.Year > 2050).OrderByDescending(x => x.CreateAt).ToPagedList(page, pageSize);
                }
                return importBillModels.OrderByDescending(x => x.CreateAt).ToPagedList(page, pageSize);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
