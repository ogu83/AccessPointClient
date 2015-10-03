using System.Collections.Generic;

namespace ManagementPanel.Models
{
    public class AccessPointDepartmentModel : BaseModel
    {
        public AccessPointDepartmentModel()
        {
            AccessPoints = new List<AccessPoint>();
            Department = new Department();
        }

        public Department Department { get; set; }

        public int Count { get { return AccessPoints.Count; } }
        public List<AccessPoint> AccessPoints { get; set; }
    }
}