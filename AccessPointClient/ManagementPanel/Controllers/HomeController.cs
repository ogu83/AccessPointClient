using ManagementPanel.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManagementPanel.Controllers
{
    public class HomeController : ControllerWithDB
    {
        // GET: /Home/
        public ActionResult Index()
        {            
            return View();
        }

        // POST: /Login/
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var username = collection["txtUserName"].ToString();
            var password = collection["txtPassword"].ToString();
            var result = Operations.CheckUserLogin(username, password);
            if (result.Success)
            {
                Session.Timeout = 24 * 60;
                Session.Add("User", result.ReturnValue);
                return RedirectToAction("Index", "Dashboard");
            }
            return View();
        }
    }
}