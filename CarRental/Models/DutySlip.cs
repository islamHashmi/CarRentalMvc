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
    
    public partial class DutySlip
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DutySlip()
        {
            this.DutySlipDetails = new HashSet<DutySlipDetail>();
        }
    
        public long dutySlipId { get; set; }
        public int branchId { get; set; }
        public int slipNumber { get; set; }
        public System.DateTime slipDate { get; set; }
        public int partyId { get; set; }
        public Nullable<long> bookingId { get; set; }
        public string carType { get; set; }
        public Nullable<int> carId { get; set; }
        public string op_carNumber { get; set; }
        public string op_carRegNumber { get; set; }
        public string op_driverName { get; set; }
        public string op_driverMobile { get; set; }
        public Nullable<int> supplierId { get; set; }
        public Nullable<int> driverId { get; set; }
        public Nullable<decimal> driverBalance { get; set; }
        public Nullable<decimal> advanceDriver { get; set; }
        public Nullable<System.TimeSpan> openingTime { get; set; }
        public Nullable<System.TimeSpan> closingTime { get; set; }
        public Nullable<decimal> totalTime { get; set; }
        public Nullable<decimal> openingKM { get; set; }
        public Nullable<decimal> closingKM { get; set; }
        public Nullable<decimal> totalKM { get; set; }
        public Nullable<int> releasePointId { get; set; }
        public string billingType { get; set; }
        public Nullable<decimal> parkingCharge { get; set; }
        public Nullable<decimal> fuelCharge { get; set; }
        public Nullable<decimal> otherCharge { get; set; }
        public string extraChargeName { get; set; }
        public Nullable<decimal> extraAmount { get; set; }
        public Nullable<decimal> paidAmount { get; set; }
        public Nullable<decimal> receivedAmount { get; set; }
        public Nullable<decimal> advanceTaken { get; set; }
        public string route { get; set; }
        public bool payableToDriver { get; set; }
        public bool active { get; set; }
        public int entryBy { get; set; }
        public System.DateTime entryDate { get; set; }
        public Nullable<int> updatedBy { get; set; }
        public Nullable<System.DateTime> updatedDate { get; set; }
    
        public virtual Booking Booking { get; set; }
        public virtual Car Car { get; set; }
        public virtual CompanyBranch CompanyBranch { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Party Party { get; set; }
        public virtual Party Party1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DutySlipDetail> DutySlipDetails { get; set; }
    }
}
