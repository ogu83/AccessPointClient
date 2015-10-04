using Common;
using ManagementPanel.DB;
using ManagementPanel.Models;
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
            arrangeBaseModel(model, user);

            return View(model);
        }

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

        public ActionResult SyncLDAP()
        {
            var user = Session["User"] as DB.user;
            if (user == null)
                return RedirectToAction("Index", "Home");

            var users = LDAPHelper.GetADUsers();
            foreach (var u in users)
            {
                var dbUser = new user { FullName = u.DisplayName, Username = u.UserName, Email = u.Email };
                Operations.AddUser(dbUser);
            }

            return RedirectToAction("Index");
        }
    }
}