using System;
using System.ComponentModel;

namespace BroadvineOnboard.Models
{
    public class Area
    {
        public Guid ClientID { get; set; }
        public int AreaID { get; set; }
        [DisplayName("Area")]
        public string Name { get; set; }

        public Area()
        {
            this.ClientID = Helpers.CurrentClientID;
        }
    }
}