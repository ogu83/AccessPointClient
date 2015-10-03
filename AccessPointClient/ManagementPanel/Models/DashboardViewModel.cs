using ManagementPanel.Controllers;
using System;

namespace ManagementPanel.Models
{
    public class DashboardViewModel
    {
        public string NameSurname { get; set; }
        public string Role { get { return Enum.GetName(typeof(ControllerWithDB.Roles), RoleType); } }
        public ControllerWithDB.Roles RoleType { get; set; }
    }
}