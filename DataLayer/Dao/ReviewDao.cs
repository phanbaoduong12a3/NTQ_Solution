﻿using DataLayer.EF;
using DataLayer.ViewModel;
using PagedList;
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

        public Review GetReviewById(int id)
        {
            return db.Reviews.Find(id);
        }

        public void UpdateReview(Review entity)
        {
            var review = db.Reviews.Find(entity.ID);
            review.Title = entity.Title;
            review.UpdateAt = DateTime.Now;
            db.SaveChanges();
        }

        public IEnumerable<ReviewModel> ListMyReview(int parentID,int userID, int page, int pageSize)
        {
            var model = (from a in db.Reviews
                         join b in db.Users
                         on a.UserID equals b.ID
                         join c in db.Products
                         on a.ProductsID equals c.ID
                         where a.ParentID == parentID && a.UserID == userID
                         select new
                         {
                             ID = a.ID,
                             UserID = a.UserID,
                             ProductID = a.ProductsID,
                             Title = a.Title,
                             ParentID = a.ParentID,
                             UserName = b.UserName,
                             Image = c.Image,
                             ProductName = c.ProductName,
                             Status = a.Status,
                             CreateAt = a.CreateAt,
                             UpdateAt = a.UpdateAt,
                             DeleteAt = a.UpdateAt
                         }).AsEnumerable().Select(x => new ReviewModel()
                         {
                             ID = x.ID,
                             UserID = x.UserID,
                             ProductsID = x.ProductID,
                             Title = x.Title,
                             ParentID = x.ParentID,
                             UserName = x.UserName,
                             Image = x.Image,
                             ProductName = x.ProductName,
                             Status = x.Status,
                             CreateAt = x.CreateAt,
                             UpdateAt = x.UpdateAt,
                             DeleteAt = x.DeleteAt
                         });
            return model.OrderByDescending(x => x.CreateAt).ToPagedList(page, pageSize);
        }

        public IEnumerable<ReviewModel> ListAllPagingReview(int parentID,int page,int pageSize)
        {
           var model = (from a in db.Reviews
                        join b in db.Users
                        on a.UserID equals b.ID
                        join c in db.Products
                        on a.ProductsID equals c.ID
                        where a.ParentID == parentID
                        select new
                        {
                            ID = a.ID,
                            UserID = a.UserID,
                            ProductID = a.ProductsID,
                            Title = a.Title,
                            ParentID = a.ParentID,
                            UserName = b.UserName,
                            Image = c.Image,
                            ProductName = c.ProductName,
                            Status = a.Status,
                            CreateAt = a.CreateAt,
                            UpdateAt = a.UpdateAt,
                            DeleteAt = a.UpdateAt
                        }).AsEnumerable().Select(x => new ReviewModel()
                        {
                            ID= x.ID,
                            UserID = x.UserID,
                            ProductsID = x.ProductID,
                            Title = x.Title,
                            ParentID = x.ParentID,
                            UserName = x.UserName,
                            Image = x.Image,
                            ProductName = x.ProductName,
                            Status = x.Status,
                            CreateAt = x.CreateAt,
                            UpdateAt = x.UpdateAt,
                            DeleteAt = x.DeleteAt
                        });
            return model.OrderByDescending(x => x.CreateAt).ToPagedList(page, pageSize);
        }

        public void Delete(int id)
        {
            try
            {
                var review = db.Reviews.Find(id);
                review.Status = 1;
                review.DeleteAt = DateTime.Now;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// Review Front-end
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="productID"></param>
        /// <returns></returns>
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
