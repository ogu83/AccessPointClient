using System;
using System.Linq;

namespace ManagementPanel.Models
{
    public class AccessPoint
    {
        public AccessPoint() { }
        public AccessPoint(DB.accessPoint accessPoint)
            : this()
        {
            Id = accessPoint.Id;
            Name = accessPoint.Name;
            IPv4 = accessPoint.IPv4;
            IPv6 = accessPoint.IPv6;
            Location = accessPoint.Location;
            IsOn = accessPoint.IsOn > 0;
            if (accessPoint.department_accessPoint.FirstOrDefault() != null)
                Department = new Department(accessPoint.department_accessPoint.FirstOrDefault().department);
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string IPv4 { get; set; }
        public string IPv6 { get; set; }
        public string Location { get; set; }
        public bool IsOn { get; set; }
        public Department Department { get; set; }
    }
}