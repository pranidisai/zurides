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
    
    public partial class tbl_Ratings
    {
        public long Id { get; set; }
        public Nullable<long> VehicleId { get; set; }
        public Nullable<long> DriverID { get; set; }
        public string DriverName { get; set; }
        public string Review { get; set; }
        public Nullable<double> Rating { get; set; }
    }
}