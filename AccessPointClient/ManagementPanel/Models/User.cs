
using ManagementPanel.DB;
using System;
namespace ManagementPanel.Models
{
    public class User
    {
        public User() { }
        public User(user user)
        {
            Id = user.Id;
            Username = user.Username;
            RoleId = user.Role_Id;
            RoleName = user.role.Name;
            Email = user.Email;
            FullName = user.FullName;
            Phone = user.Phone;
            WorkStartTime = user.WorkStartTime;
            WorkEndTime = user.WorkEndTime;
            Department = new Department(user.department);
        }

        public int Id { get; set; }
        public string Username { get; set; }

        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public string Email { get; set; }
        public string FullName { get; set; }

        public long Phone { get; set; }

        public TimeSpan WorkStartTime { get; set; }
        public TimeSpan WorkEndTime { get; set; }

        public Department Department { get; set; }

        public bool IsInMyDepartment { get; set; }
    }
}