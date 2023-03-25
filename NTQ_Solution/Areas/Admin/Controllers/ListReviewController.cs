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
        ReviewDao reviewDao ;
        public ListReviewController()
        {
            reviewDao = new ReviewDao();
        }
        // GET: Admin/ListReview
        public ActionResult Index(string searchString, int parentID=0,int page=1, int pageSize = 5)
        {
            try
            {
                var model = reviewDao.ListAllPagingReview(searchString,parentID, page, pageSize);
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
                var model = reviewDao.GetReviewById(id);
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
                reviewDao.UpdateReview(review);
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
                reviewDao.Delete(id);
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