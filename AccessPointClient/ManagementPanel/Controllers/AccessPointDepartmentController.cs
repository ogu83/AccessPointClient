using ManagementPanel.DB;
using ManagementPanel.Models;
using System.Linq;
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

        public ActionResult Edit(int id)
        {
            var user = Session["User"] as DB.user;
            if (user == null)
                return RedirectToAction("Index", "Home");

            var model = new AccessPointEditModel();
            arrangeBaseModel(model, user);

            var result = Operations.GetAccessPoint(user, id);
            if (result.Success)
                model.AccessPoint = new AccessPoint(result.ReturnValue);
            else
            {
                model.ErrorCode = result.ErrorCode;
                model.ErrorMessage = result.Message;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(int id, AccessPointEditModel model)
        {
            var user = Session["User"] as DB.user;
            if (user == null)
                return RedirectToAction("Index", "Home");

            var result = Operations.EditAccessPoint(user, model.AccessPoint);
            if (result.Success)
                return RedirectToAction("Index");
            else
            {
                model.ErrorCode = result.ErrorCode;
                model.ErrorMessage = result.Message;
                return View(model);
            }
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
            return Json(Operations.TurnOnOffAccessPoint(user, accessPointId, isOn).ToJsonResult());
        }
    }
}