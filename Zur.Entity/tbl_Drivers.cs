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
    
    public partial class tbl_Drivers
    {
        public long Id { get; set; }
        public string DriverName { get; set; }
        public string DriverUserName { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string AdharCard { get; set; }
        public string Photo { get; set; }
        public string PanNumber { get; set; }
        public Nullable<long> VehicleId { get; set; }
        public Nullable<long> RootNumber { get; set; }
        public Nullable<long> TravelId { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string DrivingLicensecopy { get; set; }
        public string License { get; set; }
        public string DriverPic { get; set; }
        public Nullable<int> Age { get; set; }
        public string MaritalStatus { get; set; }
        public string BatchNumber { get; set; }
        public Nullable<System.DateTime> DrivingLicenseExpiredDate { get; set; }
        public Nullable<System.DateTime> LicenseIssuedDate { get; set; }
        public string Driversmoke { get; set; }
        public string DriverDrink { get; set; }
    }
}