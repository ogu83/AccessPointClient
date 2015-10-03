using System.Collections.Generic;

namespace ManagementPanel.Models
{
    public class UserDepartmentModel : BaseModel
    {
        public UserDepartmentModel()
        {
            Users = new List<User>();
            Department = new Department() { Id = 7, Name = "NA" };
        }

        public Department Department { get; set; }

        public int Count { get { return Users.Count; } }
        public List<User> Users { get; set; }
    }
}