using System.Web.Mvc;

namespace BroadvineOnboard.Controllers
{
    public class TemplateController : Controller
    {
        // GET: Template
        public ActionResult Welcome()
        {
            return View();
        }

    }
}