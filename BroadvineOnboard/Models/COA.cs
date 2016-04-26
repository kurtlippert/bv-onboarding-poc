using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BroadvineOnboard.Models
{
    public class COA
    {
        public Guid ClientID { get; set; }
        public int COAID { get; set; }

        [Required]
        [DisplayName("Account Code")]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Suffix { get; set; }

        [Required]
        public int DepartmentID { get; set; }
        [DisplayName("Department")]
        public virtual Department Department { get; set; }

        [Required]
        public int AccountTypeID { get; set; }
        [DisplayName("Account Type")]
        public virtual AccountType AccountType { get; set; }

        [Required]
        public int AccountSubTypeID { get; set; }
        [DisplayName("Account Sub Type")]
        public virtual AccountSubType AccountSubType { get; set; }

        [Required]
        public int RevenueSegmentID { get; set; }
        [DisplayName("Revenue Segment")]
        public virtual RevenueSegment RevenueSegment { get; set; }

        public int DriverTypeID { get; set; }
        [DisplayName("Driver Type")]
        public virtual DriverType DriverType { get; set; }
        
        public int WagePTEBID { get; set; }
        [DisplayName("Wage/PTEB")]
        public virtual WagePTEB WagePTEBType { get; set; }

        public int? StatisticalAccountID { get; set; }
        [DisplayName("Statistical Account")]
        public virtual COA StatisticalAccount { get; set; }
        
        public COA()
        {
            this.ClientID = Helpers.CurrentClientID;
        }
    }
}