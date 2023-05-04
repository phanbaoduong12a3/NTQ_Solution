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
                             });
                return model.OrderByDescending(x => x.CreateAt).ToPagedList(page, pageSize);
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
