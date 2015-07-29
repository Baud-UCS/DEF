using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Baud.Deployment.BusinessLogic.Domain.Deployment.Contracts;
using Baud.Deployment.BusinessLogic.Domain.Deployment.Entities;
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
            paging.PageSize = 10; // only for testing

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

        public virtual ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Add(FormCollection form)
        {
            using (var uow = _deploymentUow())
            {
                var server = new Server();

                if (!TryUpdateModel(server))
                {
                    return View();
                }

                uow.Servers.AddServer(server);
                uow.Commit();

                // TODO add confirmation toast message
                return RedirectToAction(Actions.Detail(server.ID));
            }
        }

        // TODO Add a repository method, make this work
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult EditParameters(int serverID, FormCollection form)
        {
            using (var uow = _deploymentUow())
            {
                // Three items (ID, Name, Value) for each Parameter passed, hence / 3.
                for (int i = 0; i < form.Count / 3; i++)
                {
                    var id = int.Parse(form.Get("Server.Parameters[" + i + "].ID"));
                    var value = form.Get("Server.Parameters[" + i + "].Value");

                    var parameter = uow.Servers.GetParameterByID(id);
                    parameter.Value = value;

                    uow.Servers.UpdateParameters(parameter);
                }

                uow.Commit();
                var server = uow.Servers.GetServerDetail(serverID);
                return View(new ServerParameterModel { Server = server, ServerParameter = new ServerParameter() });
            }
        }

        public virtual ActionResult EditParameters(int serverID)
        {
            using (var uow = _deploymentUow())
            {
                var server = uow.Servers.GetServerDetail(serverID);

                if (server == null)
                {
                    return HttpNotFound();
                }

                return View(new ServerParameterModel { Server = server, ServerParameter = new ServerParameter() });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult AddParameter(int serverID, FormCollection form)
        {
            using (var uow = _deploymentUow())
            {
                var server = uow.Servers.GetServerDetail(serverID);

                if (server == null)
                {
                    return HttpNotFound();
                }

                var parameter = new ServerParameter
                {
                    Name = form.Get("ServerParameter.Name"),
                    Value = form.Get("ServerParameter.Value"),
                    ServerID = serverID
                };

                uow.Servers.AddParameter(parameter);
                uow.Commit();

                // TODO add confirmation toast message
                return RedirectToAction(Actions.EditParameters(serverID));
            }
        }
    }
}