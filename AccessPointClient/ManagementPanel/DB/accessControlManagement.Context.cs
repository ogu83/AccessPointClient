﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class accessControlManagementEntities : DbContext
    {
        public accessControlManagementEntities()
            : base("name=accessControlManagementEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<accessLog> accessLog { get; set; }
        public virtual DbSet<accessPoint> accessPoint { get; set; }
        public virtual DbSet<department> department { get; set; }
        public virtual DbSet<department_accessPoint> department_accessPoint { get; set; }
        public virtual DbSet<role> role { get; set; }
        public virtual DbSet<role_accessPoint> role_accessPoint { get; set; }
        public virtual DbSet<user> user { get; set; }
        public virtual DbSet<userGroup> userGroup { get; set; }
    }
}
