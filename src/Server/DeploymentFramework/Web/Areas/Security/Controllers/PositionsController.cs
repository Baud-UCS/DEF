using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Baud.Deployment.BusinessLogic.Domain.Security.Contracts;
using Baud.Deployment.Web.Areas.Security.Models.Positions;
using Baud.Deployment.Web.Framework.Web;

namespace Baud.Deployment.Web.Areas.Security.Controllers
{
    public partial class PositionsController : Controller
    {
        private readonly Func<ISecurityUow> _securityUow;

        public PositionsController(Func<ISecurityUow> securityUow)
        {
            _securityUow = securityUow;
        }

        public ActionResult Index(IndexFilter filter, PagingData paging)
        {
            paging.PageSize = 2; // only for testing

            ViewBag.Filter = filter;

            using (var uow = _securityUow())
            {
                var positionsQuery = uow.Positions.GetPositions();
                positionsQuery = filter.Apply(positionsQuery);
                var data = positionsQuery.ToPagedList(paging);

                return View(data);
            }
        }

        public virtual ActionResult Detail(short id)
        {
            using (var uow = _securityUow())
            {
                var position = uow.Positions.GetPositionDetail(id);

                if (position == null)
                {
                    return HttpNotFound();
                }

                return View(position);
            }
        }

        public virtual ActionResult Edit(short id)
        {
            using (var uow = _securityUow())
            {
                var position = uow.Positions.GetPositionDetail(id);

                if (position == null)
                {
                    return HttpNotFound();
                }

                return View(position);
            }
        }
        // TODO: T4 template for Actions
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(short id, FormCollection form)
        {
            using (var uow = _securityUow())
            {
                var position = uow.Positions.GetPositionDetail(id);

                if (position == null)
                {
                    return HttpNotFound();
                }

                if (!TryUpdateModel(position))
                {
                    return View(position);
                }

                uow.Positions.UpdatePosition(id, position);
                uow.Commit();

                // TODO add confirmation toast message
                return RedirectToAction(Actions.Detail(id));
            }
        }
    }
}