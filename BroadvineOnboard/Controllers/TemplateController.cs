using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
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