using Common;
using ManagementPanel.DB;
using ManagementPanel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManagementPanel.Controllers
{
    public class AccessLogController : ControllerWithDB
    {
        //
        // GET: /AccessLog/
        public ActionResult Index()
        {
            var user = Session["User"] as DB.user;
            if (user == null)
                return RedirectToAction("Index", "Home");

            var model = new AccessLogModel();
            arrangeBaseModel(model, user);

            var result = Operations.GetAccessLogByUser(user);
            if (result.Success)
                model.Logs = result.ReturnValue.Select(x => new AccessLog(x)).ToList();
            else
            {
                model.ErrorCode = result.ErrorCode;
                model.ErrorMessage = result.Message;
            }

            return View(model);
        }
    }
}