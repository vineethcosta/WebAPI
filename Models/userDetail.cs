//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BankAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class userDetail
    {
        public userDetail()
        {
            this.accountDetails = new HashSet<accountDetail>();
        }
    
        public int userId { get; set; }
        public string userName { get; set; }
        public string gender { get; set; }
        public Nullable<System.DateTime> dateOfBirth { get; set; }
        public string address { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public Nullable<int> pincode { get; set; }
        public Nullable<System.DateTime> createdDate { get; set; }
        public Nullable<System.DateTime> editedDate { get; set; }
        public Nullable<int> createdBy { get; set; }
        public Nullable<int> editedBy { get; set; }
        public string emailId { get; set; }
        public string branchId { get; set; }
        public Nullable<int> managerId { get; set; }
        public string phoneNumber { get; set; }
    
        public virtual ICollection<accountDetail> accountDetails { get; set; }
        public virtual branchDetail branchDetail { get; set; }
        public virtual loginDetail loginDetail { get; set; }
    }
}
