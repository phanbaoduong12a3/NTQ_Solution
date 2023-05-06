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
    public class ShipDao
    {
        NTQDBContext db;
        public ShipDao()
        {
            db = new NTQDBContext();
        }
        public IEnumerable<ShippingModel> ListShip(int page, int pageSize)
        {
            try
            {
                var model = (from a in db.Shippings
                             join d in db.Orders
                             on a.OrderID equals d.ID
                             join b in db.Users 
                             on d.UserID equals b.ID
                             join c in db.Products
                             on d.ProductsID equals c.ID
                             select new ShippingModel
                             {
                                 ID = a.ID,
                                 Status = a.Status,
                                 CreateAt = a.CreateAt,
                                 EndAt= a.EndAt,
                                 OrderID = a.OrderID,
                                 SupplierID = a.SupplierID,
                                 Address = a.Address,
                                 ShipMoney = a.ShipMoney,
                                 ShipMode = a.ShipMode,
                                 UserName = b.UserName,
                                 ProductName = c.ProductName,
                                 Image = c.Image,
                                 Price = c.Price,
                                 Count = d.Count,
                                 Color = d.Color,
                                 Size = d.Size

                             });
                return model.Where(x=>x.Status==false).OrderByDescending(x => x.CreateAt).ToPagedList(page, pageSize);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public void UpdateShip(int shipID)
        {
            var ship = db.Shippings.Find(shipID);
            ship.Status = true;
            var order = db.Orders.Find(ship.OrderID);
            order.Status = 4;
            db.SaveChanges();
        }
    }
}
