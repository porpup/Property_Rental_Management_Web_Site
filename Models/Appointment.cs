//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Property_Rental_Management_Web_Site.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Appointment
    {
        public int AppointmentID { get; set; }
        public int ApartmentID { get; set; }
        public int ManagerID { get; set; }
        public int TenantID { get; set; }
        public System.DateTime Date { get; set; }
        public string Status { get; set; }
    
        public virtual Apartment Apartment { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}