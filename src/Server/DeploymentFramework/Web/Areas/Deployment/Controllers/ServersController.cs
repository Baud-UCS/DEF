﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Baud.Deployment.BusinessLogic.Domain.Deployment.Contracts;
using Baud.Deployment.Web.Areas.Deployment.Models.Servers;
using Baud.Deployment.Web.Framework.Security;
using Baud.Deployment.Web.Framework.Web;

namespace Baud.Deployment.Web.Areas.Deployment.Controllers
{
    public partial class ServersController : Controller
    {
        private readonly Func<IDeploymentUow> _deploymentUow;

        public ServersController(Func<IDeploymentUow> deploymentUow)
        {
            _deploymentUow = deploymentUow;
        }

        // GET: Deployment/Servers
        public virtual ActionResult Index(IndexFilter filter, PagingData paging)
        {
            paging.PageSize = 2; // only for testing

            ViewBag.Filter = filter;

            using (var uow = _deploymentUow())
            {
                var serversQuery = uow.Servers.GetServers();
                serversQuery = filter.Apply(serversQuery);
                var data = serversQuery.ToPagedList(paging);

                return View(data);
            }
        }

        public virtual ActionResult Detail(int id)
        {
            using (var uow = _deploymentUow())
            {
                var server = uow.Servers.GetServerDetail(id);

                if (server == null)
                {
                    return HttpNotFound();
                }

                return View(server);
            }
        }

        public virtual ActionResult Edit(int id)
        {
            using (var uow = _deploymentUow())
            {
                var server = uow.Servers.GetServerDetail(id);

                if (server == null)
                {
                    return HttpNotFound();
                }

                return View(server);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(int id, FormCollection form)
        {
            using (var uow = _deploymentUow())
            {
                var server = uow.Servers.GetServerDetail(id);

                if (server == null)
                {
                    return HttpNotFound();
                }

                if (!TryUpdateModel(server))
                {
                    return View(server);
                }

                uow.Servers.UpdateServer(id, server);
                uow.Commit();

                // TODO add confirmation toast message
                return RedirectToAction(Actions.Detail(id));
            }
        }
    }
}