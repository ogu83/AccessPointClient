using Common;
using System;

namespace ManagementPanel.Models
{
    public abstract class BaseModel
    {
        public string NameSurname { get; set; }
        public string Role { get { return Enum.GetName(typeof(Roles), RoleType); } }
        public Roles RoleType { get; set; }

        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}