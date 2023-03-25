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
		NTQDBContext db ;
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
                             select new WishListModel
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
                             });
				return model.OrderByDescending(x => x.CreateAt).ToPagedList(page, pageSize);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
		}

        public IEnumerable<WishListModel> WishListShow(int userID, int page, int pageSize)
        {
            try
            {
                var model = (from a in db.WishLists
                             join b in db.Users
                             on a.UserID equals b.ID
                             join c in db.Products
                             on a.ProductsID equals c.ID
                             where a.UserID == userID
                             select new WishListModel
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
