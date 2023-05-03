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
    public class OrderDao
    {
		NTQDBContext db ;
		public OrderDao()
		{
			db = new NTQDBContext();
		}
        public void AddNewOrder(Order Order)
        {
			try
			{
				db.Orders.Add(Order);
				db.SaveChanges();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
        }
		public IEnumerable<OrderModel> ListAllOrder(int userID, int page,int pageSize)
		{
			try
			{
                var model = (from a in db.Orders
                             join b in db.Users
                             on a.UserID equals b.ID
                             join c in db.Products
                             on a.ProductsID equals c.ID
                             where a.UserID == userID
                             select new OrderModel
                             {
                                 ID = a.ID,
                                 UserName = b.UserName,
                                 ProductName = c.ProductName,
                                 Image = c.Image,
                                 Price = c.Price,
                                 Count = a.Count,
                                 CreateAt = a.CreateAt,
                                 UpdateAt = a.UpdateAt,
                                 DeleteAt = a.DeleteAt,
                                 Status = a.Status
                             });
				return model.OrderByDescending(x => x.CreateAt).ToPagedList(page, pageSize);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
		}

        public bool checkProductID(int productID)
        {
            var result = db.Orders.FirstOrDefault(x=>x.ProductsID == productID && x.Status == 1);
            if (result == null) return false;
            return true;
        }

        public void UpdateOrder(int productID)
        {
            var result = db.Orders.FirstOrDefault(x => x.ProductsID == productID && x.Status == 1);
            result.Count += 1;
            db.SaveChanges();
        }

        public IEnumerable<OrderModel> getOrderModel(int OrderId)
        {
            var model = (from a in db.Orders
                         join b in db.Users
                         on a.UserID equals b.ID
                         join c in db.Products
                         on a.ProductsID equals c.ID
                         where a.ID == OrderId
                         select new OrderModel
                         {
                             ID = a.ID,
                             UserName = b.UserName,
                             ProductName = c.ProductName,
                             Image = c.Image,
                             Price = c.Price,
                             Count = a.Count,
                             CreateAt = a.CreateAt,
                             UpdateAt = a.UpdateAt,
                             DeleteAt = a.DeleteAt,
                             Status = a.Status,
                             Payment = "Thanh toán khi nhận hàng"
                         });
            return model;
        }

        public IEnumerable<OrderModel> OrderShow(int userID, int page, int pageSize)
        {
            try
            {
                var model = (from a in db.Orders
                             join b in db.Users
                             on a.UserID equals b.ID
                             join c in db.Products
                             on a.ProductsID equals c.ID
                             where a.UserID == userID
                             select new OrderModel
                             {
                                 ID = a.ID,
                                 UserName = b.UserName,
                                 ProductName = c.ProductName,
                                 Image = c.Image,
                                 Price = c.Price,
                                 Count = a.Count,
                                 CreateAt = a.CreateAt,
                                 UpdateAt = a.UpdateAt,
                                 DeleteAt = a.DeleteAt,
                                 Status = a.Status
                             });

                return model.OrderByDescending(x => x.CreateAt).Where(x=>x.Status == 1).ToPagedList(page, pageSize);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public void Delete(int id)
		{
			try
			{
                var model = db.Orders.Find(id);
                model.Status = 0;
                db.SaveChanges();
            }
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
		}
    }
}
