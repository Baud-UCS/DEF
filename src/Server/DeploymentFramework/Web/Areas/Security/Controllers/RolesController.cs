﻿using System;
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

        public ActionResult Index(IndexFilter filter, PagingData paging)
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

        public virtual ActionResult Edit(short id)
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
        // TODO: T4 template for Actions
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(short id, FormCollection form)
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

                uow.Roles.UpdateRole(id, role);
                uow.Commit();

                // TODO add confirmation toast message
                return RedirectToAction(Actions.Detail(id));
            }
        }
    }
}