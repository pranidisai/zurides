//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Zur.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_Registration
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<System.DateTime> Registerdate { get; set; }
        public string CurrentLocation { get; set; }
        public string gender { get; set; }
        public Nullable<int> Age { get; set; }
        public string Image { get; set; }
    }
}
