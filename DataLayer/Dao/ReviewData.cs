﻿using DataLayer.EF;
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
    public class ReviewData
    {
        NTQDBContext db ;
        public ReviewData()
        {
            db = new NTQDBContext();
        }
        /// <summary>
        /// Thêm mới bình luận
        /// </summary>
        /// <param name="review"></param>
        /// <returns></returns>
        public bool InsertReview(Review review)
        {
            try
            {
                db.Reviews.Add(review);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }
        /// <summary>
        /// Tìm kiếm bình luận theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Review GetReviewById(int id)
        {
            try
            {
                return db.Reviews.Find(id);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }
        /// <summary>
        /// Cập nhật bình luận
        /// </summary>
        /// <param name="review"></param>
        public void UpdateReview(Review review)
        {
            try
            {
                var reviewModel = db.Reviews.Find(review.ID);
                reviewModel.Title = review.Title;
                reviewModel.UpdateAt = DateTime.Now;
                db.SaveChanges();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); throw; }
        }
        /// <summary>
        /// Danh sách sản phẩm theo khách hàng
        /// </summary>
        /// <param name="parentID"></param>
        /// <param name="userID"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<ReviewModel> ListMyReview(int parentID,int userID, int page, int pageSize)
        {
            try
            {
                var reviewModel = (from a in db.Reviews
                             join b in db.Users
                             on a.UserID equals b.ID
                             join c in db.Products
                             on a.ProductsID equals c.ID
                             where a.ParentID == parentID && a.UserID == userID
                             select new ReviewModel
                             {
                                 ID = a.ID,
                                 UserID = a.UserID,
                                 Title = a.Title,
                                 ProductsID = a.ProductsID,
                                 ParentID = a.ParentID,
                                 UserName = b.UserName,
                                 Image = c.Image,
                                 View = c.NumberViews,
                                 ProductName = c.ProductName,
                                 Status = a.Status,
                                 CreateAt = a.CreateAt,
                                 UpdateAt = a.UpdateAt,
                                 DeleteAt = a.UpdateAt
                             });
                return reviewModel.OrderByDescending(x => x.CreateAt).Where(x => x.Status==0).ToPagedList(page, pageSize);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        /// <summary>
        /// Danh sách tất cả bình luận
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="parentID"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<ReviewModel> ListAllPagingReview(string searchString,int parentID,int page,int pageSize)
        {
            try
            {
                var reviewModel = (from a in db.Reviews
                             join b in db.Users
                             on a.UserID equals b.ID
                             join c in db.Products
                             on a.ProductsID equals c.ID
                             where a.ParentID == parentID
                             select new ReviewModel
                             {
                                 ID = a.ID,
                                 UserID = a.UserID,
                                 Title = a.Title,
                                 ProductsID = a.ProductsID,
                                 ParentID = a.ParentID,
                                 UserName = b.UserName,
                                 Image = c.Image,
                                 View = c.NumberViews,
                                 ProductName = c.ProductName,
                                 Status = a.Status,
                                 CreateAt = a.CreateAt,
                                 UpdateAt = a.UpdateAt,
                                 DeleteAt = a.UpdateAt
                             });
                if (!string.IsNullOrEmpty(searchString))
                {
                    return reviewModel.OrderByDescending(x => x.CreateAt).Where(x => x.UserName.Contains(searchString)).ToPagedList(page, pageSize);
                }
                return reviewModel.OrderByDescending(x => x.CreateAt).ToPagedList(page, pageSize);
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
                var review = db.Reviews.Find(id);
                review.Status = 1;
                review.DeleteAt = DateTime.Now;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
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
            try
            {
                return db.Reviews.Where(x => x.ParentID == parentId && x.ProductsID == productID).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        /// <summary>
        /// Danh sách bình luận phía khách hàng
        /// </summary>
        /// <param name="parentID"></param>
        /// <param name="productID"></param>
        /// <returns></returns>
        public List<ReviewModel> ListReviewViewModel(int parentID, int productID)
        {
            try
            {
                var reviewModel = (from a in db.Reviews
                             join b in db.Users
                             on a.UserID equals b.ID
                             where a.ParentID == parentID && a.ProductsID == productID
                             select new ReviewModel
                             {
                                 ID = a.ID,
                                 UserID = a.UserID,
                                 ProductsID = a.ProductsID,
                                 Title = a.Title,
                                 Status = a.Status,
                                 ParentID = a.ParentID,
                                 UserName = b.UserName,
                                 CreateAt = a.CreateAt
                             });
                return reviewModel.OrderByDescending(y => y.ID).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
