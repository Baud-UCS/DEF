using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Baud.Deployment.Web.Controllers
{
    public class SecurityController : Controller
    {
        // GET: Users
        public ActionResult Users()
        {
            return View();
        }

        // GET: GroupRoles
        public ActionResult GroupRoles()
        {
            return View();
        }

        // GET: Roles
        public ActionResult Roles()
        {
            return View();
        }
    }
}
