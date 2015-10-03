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
            var users = _entities.user.Where(x => x.Username == username && x.Password == password);
            if (users != null)
                if (users.Count() > 0)
                {
                    var user = users.First();
                    if (user != null)
                    {
                        Session.Timeout = 24 * 60;
                        Session.Add("User", user);
                        return RedirectToAction("Index", "Dashboard");
                    }
                }
            return View();
        }
    }
}