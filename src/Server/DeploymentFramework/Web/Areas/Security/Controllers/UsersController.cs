using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Baud.Deployment.Web.Areas.Security.Controllers
{
    public class UsersController : Controller
    {
        // GET: Security/Users
        public ActionResult Index()
        {
            return View();
        }
    }
}