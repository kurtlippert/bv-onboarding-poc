using System;
using System.ComponentModel;

namespace BroadvineOnboard.Models
{
    public class RevenueSegmentGroup
    {
        public Guid ClientID { get; set; }
        public int RevenueSegmentGroupID { get; set; }
        [DisplayName("Segment Group Name")]
        public string Name { get; set; }
        public string Description { get; set; }

        public RevenueSegmentGroup()
        {
            this.ClientID = Helpers.CurrentClientID;
        }
    }
}