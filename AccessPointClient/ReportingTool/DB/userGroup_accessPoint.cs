//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ReportingTool.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class userGroup_accessPoint
    {
        public int Id { get; set; }
        public int AccessPoint_Id { get; set; }
        public int Group_Id { get; set; }
    
        public virtual accessPoint accessPoint { get; set; }
        public virtual userGroup userGroup { get; set; }
    }
}
