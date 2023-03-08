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
        // GET: Admin/ListReview
        public ActionResult Index(int parentID=0,int page=1, int pageSize = 10)
        {
            try
            {
                var dao = new ReviewDao();
                var model = dao.ListAllPagingReview(parentID, page, pageSize);
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
                var dao = new ReviewDao();
                var model = dao.GetReviewById(id);
                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        [HttpPost]
        public ActionResult UpdateReview(Review model)
        {
            try
            {
                var dao = new ReviewDao();
                dao.UpdateReview(model);
                SetAlert("Update Success", "success");
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
                new ReviewDao().Delete(id);
                SetAlert("Delete success", "success");
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