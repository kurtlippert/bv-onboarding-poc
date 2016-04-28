using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BroadvineOnboard.Models
{
    public class DepartmentGroup
    {
        public Guid ClientID { get; set; }
        public int DepartmentGroupID { get; set; }
        
        [Required]
        [DisplayName("Group Code")]
        public string DepartmentGroupCode { get; set; }
        [Required]
        [DisplayName("Group Name")]
        public string GroupName { get; set; }
        [Required]
        [DisplayName("Short Name")]
        public string ShortName { get; set; }

        public DepartmentGroup()
        {
            this.ClientID = Helpers.CurrentClientID;
        }
    }
}