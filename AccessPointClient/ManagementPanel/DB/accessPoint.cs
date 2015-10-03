//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ManagementPanel.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class accessPoint
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public accessPoint()
        {
            this.accessLog = new HashSet<accessLog>();
            this.department_accessPoint = new HashSet<department_accessPoint>();
            this.role_accessPoint = new HashSet<role_accessPoint>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string IPv4 { get; set; }
        public string IPv6 { get; set; }
        public string Location { get; set; }
        public byte IsOn { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<accessLog> accessLog { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<department_accessPoint> department_accessPoint { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<role_accessPoint> role_accessPoint { get; set; }
    }
}
