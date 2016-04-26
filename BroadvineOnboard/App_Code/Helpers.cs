using System;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using BroadvineOnboard.DAL;
using System.Data.Entity;
using System.Net.Mail;

namespace BroadvineOnboard
{
    public class Helpers
    {


        public static string CurrentUserID
        {
            get
            {
                return HttpContext.Current.User.Identity.GetUserId();
            }
        }

        public static Guid CurrentClientID
        {
            get
            {
                if (HttpContext.Current.Session[CurrentUserID] == null)
                {
                    BroadvineOnboardContext db = new BroadvineOnboardContext();
                    var client = db.Clients.FirstOrDefault(c => c.UserID.ToString() == CurrentUserID);
                    if (client != null) HttpContext.Current.Session[CurrentUserID] = client.UserID.ToString();
                }

                return Guid.Parse(CurrentUserID.ToString());
            }
        }

        public static ExcelSpreadSheet CurrentClientUpload
        {
            get
            {
                return ((ExcelSpreadSheet)HttpContext.Current.Session[string.Format("{0}-excel", CurrentClientID)]);
            }
            set
            {
                HttpContext.Current.Session[string.Format("{0}-excel", CurrentClientID)] = value;
            }
        }
        
        public static bool AssignCurrentUserToClient(string userid, string clientKey1, string clientKey2)
        {
            BroadvineOnboardContext db = new BroadvineOnboardContext();
            var client = db.Clients.FirstOrDefault(c => c.ChallengeKey1.ToString() == clientKey1 &&
                                                        c.ChallengeKey2.ToString() == clientKey2);
            if (client != null)
            {
                client.UserID = Guid.Parse(userid);
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
            }

            return true;
        }

        public static void SendEmail(string emailTo, string subject, string body)
        {
            using (MailMessage message = new MailMessage())
            {
                if (!string.IsNullOrEmpty(emailTo))
                {
                    message.To.Add(new MailAddress(emailTo));
                    foreach (string emailAddress in System.Web.Configuration.WebConfigurationManager.AppSettings["EmailTo"].ToString().Split(';'))
                    {
                        if (!string.IsNullOrEmpty(emailAddress)) message.Bcc.Add(new MailAddress(emailAddress));
                    }
                }

                message.From = new MailAddress(System.Web.Configuration.WebConfigurationManager.AppSettings["EmailFrom"].ToString());
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient(System.Web.Configuration.WebConfigurationManager.AppSettings["EmailServer"].ToString());
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(System.Web.Configuration.WebConfigurationManager.AppSettings["EmailUserName"].ToString(), System.Web.Configuration.WebConfigurationManager.AppSettings["EmailPassword"].ToString());
                smtp.Port = 25;
                smtp.Send(message);
            }
        }
    }
}