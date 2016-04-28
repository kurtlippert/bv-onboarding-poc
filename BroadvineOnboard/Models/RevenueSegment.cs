using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BroadvineOnboard.Models
{
    public class RevenueSegment
    {
        public Guid ClientID { get; set; }
        public int RevenueSegmentID { get; set;}
        [Required]
        public string Name { get; set; }
        [Required]
        [DisplayName("Short Name")]
        public string NameShort { get; set; }

        [Required]
        [DisplayName("Segment Group")]
        public int RevenueSegmentGroupID { get; set; }
        public virtual RevenueSegmentGroup RevenueSegmentGroup { get; set; }

        public RevenueSegment()
        {
            this.ClientID = Helpers.CurrentClientID;
        }
    }
}