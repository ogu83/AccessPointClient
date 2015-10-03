using System;
using System.Linq;

namespace ManagementPanel.Models
{
    public class AccessPoint
    {
        public AccessPoint() { }
        public AccessPoint(DB.accessPoint x)
            : this()
        {
            Id = x.Id;
            Name = x.Name;
            IPv4 = x.IPv4;
            IPv6 = x.IPv6;
            Location = x.Location;
            IsOn = x.IsOn > 0;
            if (x.department_accessPoint.FirstOrDefault() != null)
                Department = new Department(x.department_accessPoint.FirstOrDefault().department);

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