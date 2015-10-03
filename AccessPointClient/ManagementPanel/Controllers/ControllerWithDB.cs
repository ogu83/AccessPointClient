using System.Web.Mvc;

namespace ManagementPanel.Controllers
{
    public abstract class ControllerWithDB : Controller
    {
        public enum Roles { Admin = 1, Manager = 2, Employee = 4 }

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
    }
}