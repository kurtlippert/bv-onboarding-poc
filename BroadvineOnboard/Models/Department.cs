using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BroadvineOnboard.Models
{
    public class Department
    {
        public Guid ClientID { get; set; }
        public int DepartmentID { get; set; }
        [Required]
        [DisplayName("Deparment Code")]
        public string DepartmentCode { get; set; }
        [Required]
        [DisplayName("Name")]
        public string DepartmentName { get; set; }
        [Required]
        [DisplayName("Short Name")]
        public string DepartmentShortName { get; set; }
        public string SortOrder { get; set; }
        [DisplayName("Profit & Loss")]
        public bool ProfitAndLoss { get; set; }
        [DisplayName("Profit Center")]
        public bool ProfitCenterDistributed { get; set; }
        public bool IsActive { get; set; }
        public int DepartmentIncludeInGOPID { get; set; }

        public int DepartmentGroupID { get; set; }
        [DisplayName("Department Group")]
        public virtual DepartmentGroup DepartmentGroup { get; set; }
        
        public Department()
        {
            this.ClientID = Helpers.CurrentClientID;
            this.IsActive = true;
        }
    }
}