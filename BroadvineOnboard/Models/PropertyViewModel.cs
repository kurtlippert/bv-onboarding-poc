using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BroadvineOnboard.Models
{
    public class PropertyViewModel
    {
        public int ClientID { get; set; }

        public int ID { get; set; }

        [Required]
        [DisplayName("Property Name")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Property Code")]
        public string Code { get; set; }

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

        [DisplayName("Brand Code")]
        public string BrandCode { get; set; }

        [DisplayName("Smith Travel Code")]
        public string SmithTravelCode { get; set; }

        [Required]
        [DisplayName("Total Rooms")]
        public int TotalRooms { get; set; }

        [DisplayName("Website")]
        public string URL { get; set; }

        [DisplayName("Phone Number")]
        public string Phone { get; set; }

        [DisplayName("Email Address")]
        public string Email { get; set; }

        [DisplayName("Food And Beverage")]
        public bool FoodAndBeverage { get; set; }

        [Required]
        [DisplayName("Service Type")]
        public string ServiceType { get; set; }

        [Required]
        [DisplayName("Property Status")]
        public string StatusType { get; set; }

        [Required]
        [DisplayName("Owned or Operated")]
        public string RelationshipType { get; set; }

        [Required]
        [DisplayName("Maturity Type")]
        public string MaturityType { get; set; }

        [Required]
        [DisplayName("Calendar Type")]
        public string CalendarType { get; set; }

        [Required]
        [DisplayName("Company Name")]
        public string CompanyName { get; set; }

        [Required]
        [DisplayName("Area Name")]
        public string AreaName { get; set; }
    }
}