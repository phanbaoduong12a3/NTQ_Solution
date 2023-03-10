﻿using DataLayer.Dao;
using DataLayer.EF;
using NTQ_Solution.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTQ_Solution.Controllers
{
    public class ReviewController : Controller
    {
        ReviewDao reviewDao = null;
        public ReviewController()
        {
            reviewDao = new ReviewDao();
        }
        // GET: Review
        public ActionResult Index(int parentID = 0, int page = 1, int pageSize = 10)
        {
            try
            {
                var session = (UserLogin)Session[Common.CommonConstant.USER_SESSION];
                if(session == null) { return RedirectToAction("Index", "Login"); }
                else
                {
                    int userID = session.UserID;
                    var model = reviewDao.ListMyReview(parentID, userID, page, pageSize);
                    return View(model);
                }
                
            }
            catch (Exception)
            {

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
        public ActionResult UpdateReview(Review model)
        {
            try
            {
                reviewDao.UpdateReview(model);
                /*SetAlert("Update Success", "success");*/
                return RedirectToAction("Index", "Review");
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
                /*SetAlert("Delete success", "success");*/
                return RedirectToAction("Index","Review");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}