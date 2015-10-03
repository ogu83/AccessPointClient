using ManagementPanel.DB;
using ManagementPanel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManagementPanel.Controllers
{
    public class AccessPointDepartmentController : ControllerWithDB
    {
        //
        // GET: /AccessPointDepartment/
        public ActionResult Index()
        {
            var user = Session["User"] as DB.user;
            if (user == null)
                return RedirectToAction("Index", "Home");

            var model = new AccessPointDepartmentModel();
            arrangeBaseModel(model, user);

            var result = Operations.GetAccessPoints(user);
            model.Department = new Department(user.department);
            model.AccessPoints = result.ReturnValue.Select(x => new AccessPoint(x)).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(string name, string ipv4, string ipv6, string location, bool isOn)
        {
            return View();
        }

        [HttpPost]
        public ActionResult TurnOnOffAccessPoint(int accessPointId, bool isOn)
        {
            var user = Session["User"] as DB.user;
            if (user != null)
                return Json(Operations.TurnOnOffAccessPoint(user, accessPointId, isOn).ToJsonResult());
            else
                return null;
        }
    }
}