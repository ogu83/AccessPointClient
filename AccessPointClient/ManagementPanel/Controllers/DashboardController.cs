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
    public class DashboardController : ControllerWithDB
    {
        public ActionResult Index()
        {          
            var user = Session["User"] as DB.user;
            if (user == null)
                return RedirectToAction("Index", "Home");

            var model = new DashboardViewModel();
            model.NameSurname = user.FullName;
            model.RoleType = (Roles)user.Role_Id;
            return View(model);
        }

        /// <summary>
        /// Dashboard with Login
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            var username = collection["txtUserName"].ToString();
            var password = collection["txtPassword"].ToString();
            var result = Operations.CheckUserLogin(username, password);
            if (result.Success)
            {
                Session.Timeout = 24 * 60;
                Session.Add("User", result.ReturnValue);
            }
            return Index();
        }

        public ActionResult Logout()
        {
            Session["User"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}