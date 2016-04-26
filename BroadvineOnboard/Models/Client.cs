using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BroadvineOnboard.Models
{
    public class Client
    {
        public Guid ClientID { get; set; }
        [Required]
        [DisplayName("Challenge Key #1")]
        public Guid ChallengeKey1 { get; set; }
        [Required]
        [DisplayName("Challenge Key #2")]
        public Guid ChallengeKey2 { get; set; }
        [Required]
        [DisplayName("Customer Name")]
        public string ClientName { get; set; }
        [Required]
        [DisplayName("Contact Full Name")]
        public string UserFullName { get; set; }
        [Required]
        [DisplayName("Contact Email Address")]
        public string UserEmailAddress { get; set; }
        public Guid? UserID { get; set; }
        public bool Active { get; set; }

        [DisplayName("Onboarding URL")]
        public string OnboardURL
        {
            get
            {
                return string.Format("{0}?k1={1}&k2={2}", System.Web.Configuration.WebConfigurationManager.AppSettings["OnboardURL"].ToString(), this.ChallengeKey1.ToString(), this.ChallengeKey2.ToString());
            }
        }
        public Client()
        {
            this.ChallengeKey1 = Guid.NewGuid();
            this.ChallengeKey2 = Guid.NewGuid();
            this.Active = true;
        }
    }
}