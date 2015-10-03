using Common;
using ManagementPanel.DB;
using ManagementPanel.Models;
using System.Web.Mvc;

namespace ManagementPanel.Controllers
{
    public abstract class ControllerWithDB : Controller
    {
        private static DB.accessControlManagementEntities __entities;
        protected static DB.accessControlManagementEntities _entities
        {
            get
            {
                if (__entities == null)
                    __entities = new DB.accessControlManagementEntities();
                return __entities;
            }
        }

        protected void arrangeBaseModel(BaseModel model, user user)
        {
            model.NameSurname = user.FullName;
            model.RoleType = (Roles)user.Role_Id;
        }
    }
}