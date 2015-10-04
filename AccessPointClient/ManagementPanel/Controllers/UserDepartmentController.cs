using ManagementPanel.DB;
using ManagementPanel.Models;
using System.Linq;
using System.Web.Mvc;

namespace ManagementPanel.Controllers
{
    public class UserDepartmentController : ControllerWithDB
    {
        //
        // GET: /UserDepartment/
        public ActionResult Index()
        {
            var user = Session["User"] as DB.user;
            if (user == null)
                return RedirectToAction("Index", "Home");

            var model = new UserDepartmentModel();
            arrangeBaseModel(model, user);

            var result = Operations.GetUsersOfManager(user);
            model.Department = new Department(user.department);
            model.Users = result.ReturnValue.Select(x => new User(x) { IsInMyDepartment = x.Department_Id == user.Department_Id }).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult ChangeDepartmentForUser(int employeeId, bool inMyDepartment)
        {
            var user = Session["User"] as DB.user;
            return Json(Operations.ChangeDepartmentForUser(user, employeeId, inMyDepartment).ToJsonResult());
        }
    }
}