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
    public class WishListDao
    {
		NTQDBContext db = null;
		public WishListDao()
		{
			db = new NTQDBContext();
		}
        public void AddNewWishList(WishList wishList)
        {
			try
			{
				db.WishLists.Add(wishList);
				db.SaveChanges();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
        }
		public IEnumerable<WishListModel> ListAllWishList(int userID, int page,int pageSize)
		{
			try
			{
				var model = (from a in db.WishLists
							 join b in db.Users
							 on a.UserID equals b.ID
							 join c in db.Products
							 on a.ProductsID equals c.ID
							 where a.UserID == userID
							 select new
							 {
								 ID = a.ID,
								 UserName = b.UserName,
								 ProductName = c.ProductName,
								 Image = c.Image,
								 Price = c.Price,
								 CreateAt = a.CreateAt,
								 UpdateAt = a.UpdateAt,
								 DeleteAt = a.DeleteAt,
								 Status = a.Status
							 }).AsEnumerable().Select(x => new WishListModel()
							 {
								 ID= x.ID,
								 UserName= x.UserName,
								 ProductName = x.ProductName,
								 Image= x.Image,
								 Price= x.Price,
								 CreateAt= x.CreateAt,
								 UpdateAt= x.UpdateAt,
								 DeleteAt= x.DeleteAt,
								 Status= x.Status
							 });
				return model.OrderByDescending(x => x.CreateAt).ToPagedList(page, pageSize);
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
                var model = db.WishLists.Find(id);
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
