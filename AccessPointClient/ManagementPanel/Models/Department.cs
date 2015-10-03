using ManagementPanel.DB;

namespace ManagementPanel.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Department() { }
        public Department(department department)
        {
            if (department != null)
            {
                Id = department.Id;
                Name = department.Name;
            }
        }
    }
}