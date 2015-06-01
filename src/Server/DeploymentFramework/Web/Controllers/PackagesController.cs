using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Baud.Deployment.Web.Controllers
{
    public partial class PackagesController : Controller
    {
        // GET: Packages
        public virtual ActionResult Index()
        {
            return View();
        }
    }
}
