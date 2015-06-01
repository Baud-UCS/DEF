using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Baud.Deployment.BusinessLogic.Domain.Security.Contracts;
using Baud.Deployment.Web.Framework.Security;

namespace Baud.Deployment.Web.Areas.Security.Controllers
{
    public partial class UsersController : Controller
    {
        private readonly Func<ISecurityUow> _securityUow;

        public UsersController(Func<ISecurityUow> securityUow)
        {
            _securityUow = securityUow;
        }

        ////[RequirePermission(Permissions.Security.UserRead)]
        public virtual ActionResult Index()
        {
            using (var uow = _securityUow())
            {
                var users = uow.Users.GetUsers().ToList();
            }

            return View();
        }
    }
}