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
    
    public partial class tbl_Orders
    {
        public long Id { get; set; }
        public Nullable<long> VehicleId { get; set; }
        public string UserName { get; set; }
        public string ContactName { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string BoardingPoint { get; set; }
        public string JouneyType { get; set; }
        public Nullable<int> SeatsSelected { get; set; }
        public string DropPoint { get; set; }
        public Nullable<double> UnitPrice { get; set; }
        public Nullable<double> Tax { get; set; }
        public Nullable<double> CouponApplied { get; set; }
        public Nullable<double> NetPay { get; set; }
        public string PaymentMode { get; set; }
        public Nullable<int> PaymentStatus { get; set; }
        public string PaymentStatusinfo { get; set; }
        public string PaymentThrough { get; set; }
        public string CheckSum { get; set; }
        public string Transactionid { get; set; }
        public Nullable<int> Age { get; set; }
        public string Gender { get; set; }
        public string UserAdhar { get; set; }
        public string OTP { get; set; }
        public Nullable<int> TravelStatus { get; set; }
    }
}