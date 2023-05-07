using DataLayer.Dao;
using NTQ_Solution.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NTQ_Solution.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        // GET: Admin/Base
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (UserLogin)Session[CommonConstant.USER_SESSION];
            if (session == null)
            {
                    filterContext.Result = new RedirectToRouteResult(new
                        RouteValueDictionary(new { controller = "ShowWarning", action = "Index", Area = "Admin" }));
            }
            else if(session.Role == 0)
            {
                filterContext.Result = new RedirectToRouteResult(new
                        RouteValueDictionary(new { controller = "ShowWarning", action = "Index", Area = "Admin" }));
            }
            else
            {
                base.OnActionExecuting(filterContext);
            }
            
        }
        protected void SetAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            if(type == "success")
            {
                TempData["AlertType"] = "alert-success";
            }
            else if (type == "warning")
            {
                TempData["AlertType"] = "alert-warning";
            }
            else if(type == "error")
            {
                TempData["AlertType"] = "alert-danger";
            }
        }
    }
}