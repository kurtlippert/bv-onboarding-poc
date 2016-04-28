using System.Text;
using BroadvineOnboard.Models;

namespace BroadvineOnboard
{
    public class EmailTemplate
    {
        public static string WelcomeEmail(Client c)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(string.Format("<h1>Welcome {0}</h1>", c.UserFullName));
            sb.Append("<p>To begin the onboarding process, please use the link below</p>");
            sb.Append(string.Format("<a href='{0}'>{0}</a>", c.OnboardURL));

            return sb.ToString(); 
        }
    }
}