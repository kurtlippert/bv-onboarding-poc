using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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