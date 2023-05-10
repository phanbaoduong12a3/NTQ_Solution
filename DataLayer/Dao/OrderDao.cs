using DataLayer.EF;
using DataLayer.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
                                 Status = a.Status,
                                 Color = a.Color,
                                 Size = a.Size,
                                 ShipMode = a.ShipMode
                             });
				return model.OrderByDescending(x => x.CreateAt).ToPagedList(page, pageSize);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
		}
        public Order GetOrderByProductID(int productID)
        {
            return db.Orders.Where(x => x.ProductsID == productID).FirstOrDefault();
        }
        public bool checkProductID(int productID)
        {
            var result = db.Orders.FirstOrDefault(x=>x.ProductsID == productID && x.Status == 1);
            if (result == null) return false;
            return true;
        }
        public Product findProductOrder(string productName, string size, string color)
        {
            try
            {
                int Size = 0;
                if (size != null) int.Parse(size);
                int Color = 0;
                if (color != null) int.Parse(color);
                var product = db.Products.Where(x => x.ProductName == productName && x.Size == Size && x.Color == Color).FirstOrDefault();
                return product;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public void UpdateOrder(int productID)
        {
            var result = db.Orders.FirstOrDefault(x => x.ProductsID == productID && x.Status == 1);
            result.Count += 1;
            db.SaveChanges();
        }

        public IEnumerable<OrderModel> getOrderModel(int OrderId,int count,string color, string size)
        {
            int Size = 0;
            if (size != null) int.Parse(size);
            int Color = 0;
            if (color != null) int.Parse(color);
            var model = (from a in db.Orders
                         join b in db.Users
                         on a.UserID equals b.ID
                         join c in db.Products
                         on a.ProductsID equals c.ID
                         where a.ID == OrderId
                         select new OrderModel
                         {
                             ID = a.ID,
                             Address= b.Address,
                             Phone = b.Phone,
                             Email = b.Email,
                             Color = Color,
                             Size = Size,
                             UserName = b.UserName,
                             ProductName = c.ProductName,
                             Image = c.Image,
                             Price = c.Price,
                             Count = count, 
                             CreateAt = a.CreateAt,
                             UpdateAt = a.UpdateAt,
                             DeleteAt = a.DeleteAt,
                             Status = a.Status,
                             Payment = ""
                         });
            return model;
        }
        public OrderModel convertOrderModel(Order Order, string color, string size)
        {
            int Size = 0;
            if (size != null) int.Parse(size);
            int Color = 0;
            if (color != null) int.Parse(color);
            var model = (from a in db.Orders
                         join b in db.Users
                         on a.UserID equals b.ID
                         join c in db.Products
                         on a.ProductsID equals c.ID
                         where a.ID == Order.ID
                         select new OrderModel
                         {
                             ID = a.ID,
                             Address = b.Address,
                             Phone = b.Phone,
                             Email = b.Email,
                             Color = Color,
                             Size = Size,
                             UserName = b.UserName,
                             ProductName = c.ProductName,
                             Image = c.Image,
                             Price = c.Price,
                             Count = Order.ID,
                             CreateAt = a.CreateAt,
                             UpdateAt = a.UpdateAt,
                             DeleteAt = a.DeleteAt,
                             Status = a.Status,
                             Payment = ""
                         });
            return model.FirstOrDefault(x=>x.ID == Order.ID);
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
                                 Color = c.Color,
                                 CreateAt = a.CreateAt, 
                                 UpdateAt = a.UpdateAt,
                                 DeleteAt = a.DeleteAt,
                                 Size = c.Size,
                                 Image = c.Image,
                                 Price = c.Price,
                                 Count = a.Count,
                                 Status = a.Status,
                                 Address = b.Address,
                                 Phone = b.Phone,
                                 Email = b.Email,
                                 ShipMode = a.ShipMode,
                             });

                return model.OrderBy(x => x.Status).Where(x=>x.Status >1).ToPagedList(page, pageSize);
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
        public void DeleteOrder(int id)
        {
            try
            {
                var model = db.Orders.Find(id);
                model.Status = 5;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public void PaymentSuccess(OrderModel orderModel,string shipMode,int shipMoney)
        {
            var order = db.Orders.Find(orderModel.ID);
            order.Count = orderModel.Count;
            order.Price = orderModel.Count*orderModel.Price + shipMoney;
            order.Phone = orderModel.Phone;
            order.Address = orderModel.Address;
            order.Color = orderModel.Color;
            order.Size = orderModel.Size;
            order.Status = 2;
            order.ShipMode = shipMode;
            db.SaveChanges();
        }


        public IEnumerable<OrderModel> ListOrderBE(string searchString, int page, int pageSize)
        {
            try
            {
                var model = (from a in db.Orders
                             join b in db.Users
                             on a.UserID equals b.ID
                             join c in db.Products
                             on a.ProductsID equals c.ID
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
                                 Color = a.Color,
                                 Size = a.Size
                             });
                if(!string.IsNullOrEmpty(searchString) )
                {
                    model = model.Where(x=>x.ProductName.Contains(searchString));
                    return model.OrderByDescending(x => x.CreateAt).Where(x => x.Status==2).ToPagedList(page, pageSize);
                }
                return model.OrderByDescending(x => x.CreateAt).Where(x=>x.Status == 2).ToPagedList(page, pageSize);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public IEnumerable<OrderModel> ListOrderSuccess(string searchString, int page, int pageSize)
        {
            try
            {
                var model = (from a in db.Orders
                             join b in db.Users
                             on a.UserID equals b.ID
                             join c in db.Products
                             on a.ProductsID equals c.ID
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
                                 Color = a.Color,
                                 Size = a.Size
                             });
                if (!string.IsNullOrEmpty(searchString))
                {
                    model = model.Where(x => x.ProductName.Contains(searchString));
                    return model.OrderByDescending(x => x.CreateAt).Where(x => x.Status == 4).ToPagedList(page, pageSize);
                }
                return model.OrderByDescending(x => x.CreateAt).Where(x => x.Status == 4).ToPagedList(page, pageSize);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public void UpdateOrderBE(int orderID,int userid)
        {
            var order = db.Orders.Find(orderID);
            order.Status = 3;
            int shipMoney;
            if (order.ShipMode == "Giao hàng tiết kiệm") shipMoney = 20000;
            else shipMoney = 25000;
            var ship = new Shipping
            {
                OrderID = orderID,
                Address = order.Address,
                CreateAt = DateTime.Now,
                EndAt= DateTime.Now.AddDays(2),
                Status = false,
                ShipMode = order.ShipMode,
                ShipMoney = shipMoney
            };
            var product = db.Products.Find(order.ProductsID);
            product.Count -= order.Count;
            
            var export = new Export
            {
                ProductID = order.ProductsID,
                Count = order.Count,
                Price = order.Price,
                UserID = userid,
                OrderID = orderID,
                CreateAt = DateTime.Now,
            };
            db.Exports.Add(export);
            db.Shippings.Add(ship);
            db.SaveChanges();
        }
        public void UpdateOrderCount(int orderId, string productCount)
        {
            var order = db.Orders.Where(x=>x.ID==orderId).FirstOrDefault();
            order.Count = int.Parse(productCount);
            db.SaveChanges();
        }
        public IEnumerable<OrderModel> OrderDemo(int userID, int page, int pageSize)
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
                                 Color = c.Color,
                                 CreateAt = a.CreateAt,
                                 UpdateAt = a.UpdateAt,
                                 DeleteAt = a.DeleteAt,
                                 Size = c.Size,
                                 Image = c.Image,
                                 Price = c.Price,
                                 Count = a.Count,
                                 Status = a.Status,
                                 Address = b.Address,
                                 Phone = b.Phone,
                                 Email = b.Email,
                                 ProductCount = c.Count
                             });

                return model.OrderByDescending(x => x.CreateAt).Where(x => x.Status == 1).ToPagedList(page, pageSize);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
