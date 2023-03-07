using DataLayer.EF;
using DataLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Dao
{
    public class ReviewDao
    {
        NTQDBContext db = null;
        public ReviewDao()
        {
            db = new NTQDBContext();
        }

        public bool InsertReview(Review entity)
        {
            db.Reviews.Add(entity);
            db.SaveChanges();
            return true;
        }

        public List<Review> ListReview(int parentId,int productID)
        {
            return db.Reviews.Where(x => x.ParentID == parentId && x.ProductsID == productID).ToList();
        }

        public List<ReviewModel> ListReviewViewModel(int parentID, int productID)
        {
            var model = (from a in db.Reviews
                         join b in db.Users
                         on a.UserID equals b.ID
                         where a.ParentID == parentID && a.ProductsID== productID
                         select new
                         {
                           ID = a.ID,
                           UserID = a.UserID,
                           ProductID = a.ProductsID,
                           Title = a.Title,
                           ParentID = a.ParentID,
                           UserName = b.UserName
                         }).AsEnumerable().Select(x => new ReviewModel()
                         {
                             ID = x.ID,
                             UserID = x.UserID,
                             ProductsID = x.ProductID,
                             Title = x.Title,
                             ParentID = x.ParentID,
                             UserName = x.UserName
                         });
            return model.OrderByDescending(y => y.ID).ToList();
        }
    }
}
