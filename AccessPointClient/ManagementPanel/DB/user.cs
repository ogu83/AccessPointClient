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
    
    public partial class user
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public user()
        {
            this.accessLog = new HashSet<accessLog>();
        }
    
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Role_Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public int Group_Id { get; set; }
        public long Phone { get; set; }
        public System.TimeSpan WorkStartTime { get; set; }
        public System.TimeSpan WorkEndTime { get; set; }
        public int Department_Id { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<accessLog> accessLog { get; set; }
        public virtual department department { get; set; }
        public virtual role role { get; set; }
        public virtual userGroup userGroup { get; set; }
    }
}
