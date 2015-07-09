using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Baud.Deployment.BusinessLogic.Domain.Security.Contracts;
using Baud.Deployment.Web.Areas.Security.Models.Roles;
using Baud.Deployment.Web.Framework.Security;
using Baud.Deployment.Web.Framework.Web;

namespace Baud.Deployment.Web.Areas.Security.Controllers
{
    public partial class RolesController : Controller
    {
        private readonly Func<ISecurityUow> _securityUow;

        public RolesController(Func<ISecurityUow> securityUow)
        {
            _securityUow = securityUow;
        }

        public virtual ActionResult Index(IndexFilter filter, PagingData paging)
        {
            paging.PageSize = 2; // only for testing

            ViewBag.Filter = filter;

            using (var uow = _securityUow())
            {
                var rolesQuery = uow.Roles.GetRoles();
                rolesQuery = filter.Apply(rolesQuery);
                var data = rolesQuery.ToPagedList(paging);

                return View(data);
            }
        }

        public virtual ActionResult Detail(short id)
        {
            using (var uow = _securityUow())
            {
                var role = uow.Roles.GetRoleDetail(id);

                if (role == null)
                {
                    return HttpNotFound();
                }

                return View(role);
            }
        }

        public virtual ActionResult Disable(short id)
        {
            using (var uow = _securityUow())
            {
                var role = uow.Roles.GetRoleDetail(id);

                if (role == null)
                {
                    return HttpNotFound();
                }

                if (!TryUpdateModel(role))
                {
                    return View(role);
                }

                uow.Roles.Disable(id);
                uow.Commit();

                // TODO add confirmation toast message
                return RedirectToAction(Actions.Index());
            }
        }

        public virtual ActionResult Enable(short id)
        {
            using (var uow = _securityUow())
            {
                var role = uow.Roles.GetRoleDetail(id);

                if (role == null)
                {
                    return HttpNotFound();
                }

                if (!TryUpdateModel(role))
                {
                    return View(role);
                }

                uow.Roles.Enable(id);
                uow.Commit();

                // TODO add confirmation toast message
                return RedirectToAction(Actions.Index());
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual ActionResult UpdateName(short id, string name)
        {
            using (var uow = _securityUow())
            {
                var role = uow.Roles.GetRoleDetail(id);

                if (role == null)
                {
                    return HttpNotFound();
                }

                if (!TryUpdateModel(role))
                {
                    return View(role);
                }

                uow.Roles.UpdateName(id, name);
                uow.Commit();

                // TODO add confirmation toast message
                return RedirectToAction(Actions.Index());
            }
        }

        public virtual ActionResult UpdateName(short id)
        {
            using (var uow = _securityUow())
            {
                var role = uow.Roles.GetRoleDetail(id);

                if (role == null)
                {
                    return HttpNotFound();
                }

                return View(role);
            }
        }
    }
}