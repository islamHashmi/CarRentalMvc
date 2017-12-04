//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CarRental.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DriverAllocation
    {
        public int allocationId { get; set; }
        public long bookingId { get; set; }
        public string carType { get; set; }
        public Nullable<int> carId { get; set; }
        public string carNumber { get; set; }
        public string carRegisterNumber { get; set; }
        public Nullable<int> driverId { get; set; }
        public string driverName { get; set; }
        public string driverMobileNo { get; set; }
        public Nullable<int> slipNumber { get; set; }
        public bool active { get; set; }
        public int entryBy { get; set; }
        public System.DateTime entryDate { get; set; }
        public Nullable<int> updatedBy { get; set; }
        public Nullable<System.DateTime> updatedDate { get; set; }
    
        public virtual Booking Booking { get; set; }
        public virtual Car Car { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
