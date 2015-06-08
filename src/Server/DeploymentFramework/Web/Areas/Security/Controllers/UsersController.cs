using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Baud.Deployment.BusinessLogic.Domain.Security.Contracts;
using Baud.Deployment.Web.Areas.Security.Models.Users;
using Baud.Deployment.Web.Framework.Security;
using Baud.Deployment.Web.Framework.Web;

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
        public virtual ActionResult Index(IndexFilter filter, PagingData paging)
        {
            paging.PageSize = 2; // only for testing

            ViewBag.Filter = filter;

            using (var uow = _securityUow())
            {
                var usersQuery = uow.Users.GetUsers();
                usersQuery = filter.Apply(usersQuery);
                var data = usersQuery.ToPagedList(paging);

                return View(data);
            }
        }

        public virtual ActionResult Detail(short id)
        {
            using (var uow = _securityUow())
            {
                var user = uow.Users.GetUserDetail(id);

                if (user == null)
                {
                    return HttpNotFound();
                }

                return View(user);
            }
        }

        public virtual ActionResult Edit(short id)
        {
            using (var uow = _securityUow())
            {
                var user = uow.Users.GetUserDetail(id);

                if (user == null)
                {
                    return HttpNotFound();
                }

                return View(user);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(short id, FormCollection form)
        {
            using (var uow = _securityUow())
            {
                var user = uow.Users.GetUserDetail(id);

                if (user == null)
                {
                    return HttpNotFound();
                }

                if (!TryUpdateModel(user))
                {
                    return View(user);
                }

                uow.Users.UpdateUser(id, user);
                uow.Commit();

                // TODO add confirmation toast message
                return RedirectToAction(Actions.Detail(id));
            }
        }
    }
}