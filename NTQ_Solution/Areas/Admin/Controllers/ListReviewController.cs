using DataLayer.Dao;
using DataLayer.EF;
using DataLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTQ_Solution.Areas.Admin.Controllers
{
    public class ListReviewController : BaseController
    {
        ReviewData reviewData ;
        public ListReviewController()
        {
            reviewData = new ReviewData();
        }
        // GET: Admin/ListReview
        public ActionResult Index(string searchString, int parentID=0,int page=1, int pageSize = 5)
        {
            try
            {
                var model = reviewData.ListAllPagingReview(searchString,parentID, page, pageSize);
                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        [HttpGet]
        public ActionResult UpdateReview(int id)
        {
            try
            {
                var model = reviewData.GetReviewById(id);
                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        [HttpPost]
        public ActionResult UpdateReview(Review review)
        {
            try
            {
                reviewData.UpdateReview(review);
                TempData["success"] = "Update Review success";
                return RedirectToAction("Index", "ListReview");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                reviewData.Delete(id);
                TempData["success"] = "Delete Review success";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}