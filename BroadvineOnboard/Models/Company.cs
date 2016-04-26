using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BroadvineOnboard.Models
{
    public class Company
    {
        public Guid ClientID { get; set; }
        public int CompanyID { get; set; }
        [Required]
        [DisplayName("Company Code")]
        public string CompanyCode { get; set; }
        [Required]
        [DisplayName("Company Name")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Short Name")]
        public string NameShort{ get; set; }
        [Required]
        [DisplayName("Legal Name")]
        public string NameLegal{ get; set; }
        [Required]
        [DisplayName("Address")]
        public string Address1 { get; set; }
        [DisplayName(" ")]
        public string Address2 { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        [DisplayName("Postal Code")]
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string URL { get; set; }
        [DisplayName("Management Company")]
        public bool IsManagementCompany { get; set; }
        [DisplayName("Owner Company")]
        public bool IsOwnerCompany { get; set; }

        public Company()
        {
            this.ClientID = Helpers.CurrentClientID;
        }
    }
}