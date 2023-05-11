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
    public class OrderData
    {
		NTQDBContext db ;
		public OrderData()
		{
			db = new NTQDBContext();
		}
        /// <summary>
        /// Thêm mới đơn hàng
        /// </summary>
        /// <param name="Order"></param>
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
        /// <summary>
        /// Kiểm tra sản phẩm đã có trong giỏ hàng chưa
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public bool checkProductID(int productID)
        {
            var model = db.Orders.FirstOrDefault(x=>x.ProductsID == productID && x.Status == 1);
            if (model == null) return false;
            return true;
        }
        /// <summary>
        /// Tìm kiếm đơn hàng theo tên sản phẩm
        /// </summary>
        /// <param name="productName"></param>
        /// <param name="size"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public Product findProductOrder(string productName, string size, string color)
        {
            try
            {
                int Size = 0;
                if (size != null) Size = int.Parse(size);
                int Color = 0;
                if (color != null) Color = int.Parse(color);
                var product = db.Products.Where(x => x.ProductName == productName && x.Size == Size && x.Color == Color).FirstOrDefault();
                return product;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        /// <summary>
        /// Cập nhật đơn hàng
        /// </summary>
        /// <param name="productID"></param>
        public void UpdateOrder(int productID)
        {
            var model = db.Orders.FirstOrDefault(x => x.ProductsID == productID && x.Status == 1);
            model.Count += 1;
            db.SaveChanges();
        }
        public OrderModel convertOrderModel(Order Order, string color, string size)
        {
            int Size = 0;
            if (size != null) Size = int.Parse(size);
            int Color = 0;
            if (color != null) Color = int.Parse(color);
            var orderModels = (from a in db.Orders
                               join b in db.Products
                               on a.ProductsID equals b.ID
                               where a.UserID == Order.UserID
                               select new OrderModel
                               {
                                   ID = a.ID,
                                   ProductName = b.ProductName,
                                   Color = Color,
                                   CreateAt = a.CreateAt,
                                   UpdateAt = a.UpdateAt,
                                   DeleteAt = a.DeleteAt,
                                   Size = Size,
                                   Image = b.Image,
                                   Price = b.Price,
                                   Count = a.Count,
                                   Status = a.Status,
                                   ProductCount = b.Count
                               });
            return orderModels.FirstOrDefault(x=>x.ID == Order.ID);
        }
        /// <summary>
        /// Danh sách đơn hàng phía khách hàng
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<OrderModel> OrderShow(int userID, int page, int pageSize)
        {
            try
            {
                var orderModels = (from a in db.Orders
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

                return orderModels.OrderBy(x => x.Status).Where(x=>x.Status >1).ToPagedList(page, pageSize);
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
                var orderModels = db.Orders.Find(id);
                orderModels.Status = 0;
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
                var orderModels = db.Orders.Find(id);
                orderModels.Status = 5;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        /// <summary>
        /// Xử lý đặt hàng thành công
        /// </summary>
        /// <param name="orderModel"></param>
        /// <param name="shipMode"></param>
        /// <param name="shipMoney"></param>
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
        /// <summary>
        /// Danh sách đơn hàng hiển thị trong trang quản lý
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>

        public IEnumerable<OrderModel> ListOrderBE(string searchString, int page, int pageSize)
        {
            try
            {
                var orderModels = (from a in db.Orders
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
                    orderModels = orderModels.Where(x=>x.ProductName.Contains(searchString));
                    return orderModels.OrderByDescending(x => x.CreateAt).Where(x => x.Status==2).ToPagedList(page, pageSize);
                }
                return orderModels.OrderByDescending(x => x.CreateAt).Where(x=>x.Status == 2).ToPagedList(page, pageSize);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        /// <summary>
        /// Danh sách đơn hàng giao thành công
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<OrderModel> ListOrderSuccess(string searchString, int page, int pageSize)
        {
            try
            {
                var orderModels = (from a in db.Orders
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
                    orderModels = orderModels.Where(x => x.ProductName.Contains(searchString));
                    return orderModels.OrderByDescending(x => x.CreateAt).Where(x => x.Status == 4).ToPagedList(page, pageSize);
                }
                return orderModels.OrderByDescending(x => x.CreateAt).Where(x => x.Status == 4).ToPagedList(page, pageSize);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        /// <summary>
        /// Function cập nhật đơn hàng trong trang quản lý
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="userid"></param>
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
        /// <summary>
        /// Cập nhật số lượng sản phẩm
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="productCount"></param>
        public void UpdateOrderCount(int orderId, string productCount)
        {
            var orderModels = db.Orders.Where(x=>x.ID==orderId).FirstOrDefault();
            orderModels.Count = int.Parse(productCount);
            db.SaveChanges();
        }
        /// <summary>
        /// Danh sách giỏ hàng
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<OrderModel> OrderDemo(int userID, int page, int pageSize)
        {
            try
            {
                if(userID == 0)
                {
                    var model = (from a in db.Orders
                                 join b in db.Products
                                 on a.ProductsID equals b.ID
                                 where a.UserID == userID
                                 select new OrderModel
                                 {
                                     ID = a.ID,
                                     ProductName = b.ProductName,
                                     Color = b.Color,
                                     CreateAt = a.CreateAt,
                                     UpdateAt = a.UpdateAt,
                                     DeleteAt = a.DeleteAt,
                                     Size = b.Size,
                                     Image = b.Image,
                                     Price = b.Price,
                                     Count = a.Count,
                                     Status = a.Status,
                                     ProductCount = b.Count
                                 }
                                 );
                    return model.OrderByDescending(x => x.CreateAt).Where(x => x.Status == 1).ToPagedList(page, pageSize);

                }
                var orderModels = (from a in db.Orders
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

                return orderModels.OrderByDescending(x => x.CreateAt).Where(x => x.Status == 1).ToPagedList(page, pageSize);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public void UpdateOrderUser(int userID)
        {
           
            var order = db.Orders.Where(x => x.UserID == 0).ToList();
            for(int i=0; i<order.Count; i++)
            {
                order[i].UserID = userID;
            }
            db.SaveChanges();
        }
        public void Remove()
        {
            var model = db.Orders.Where(x => x.UserID == 0);
            foreach(var item in model)
            {
                db.Orders.Remove(item);
            }
            db.SaveChanges();
        }
    }
}
