using Common;
using ManagementPanel.Controllers;
using System;

namespace ManagementPanel.Models
{
    public class DashboardViewModel
    {
        public string NameSurname { get; set; }
        public string Role { get { return Enum.GetName(typeof(Roles), RoleType); } }
        public Roles RoleType { get; set; }
    }
}