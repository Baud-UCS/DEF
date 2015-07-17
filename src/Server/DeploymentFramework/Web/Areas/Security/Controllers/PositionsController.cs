using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Baud.Deployment.BusinessLogic.Domain.Security.Contracts;
using Baud.Deployment.BusinessLogic.Domain.Security.Entities;
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

        public virtual ActionResult Index(IndexFilter filter, PagingData paging)
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

        public virtual ActionResult Disable(short id)
        {
            using (var uow = _securityUow())
            {
                var position = uow.Positions.GetPositionDetail(id);

                if (position == null)
                {
                    return HttpNotFound();
                }

                uow.Positions.Disable(id);
                uow.Commit();

                // TODO add confirmation toast message
                return RedirectToAction(Actions.Index());
            }
        }

        public virtual ActionResult Enable(short id)
        {
            using (var uow = _securityUow())
            {
                var position = uow.Positions.GetPositionDetail(id);

                if (position == null)
                {
                    return HttpNotFound();
                }

                uow.Positions.Enable(id);
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
                var position = uow.Positions.GetPositionDetail(id);

                if (position == null)
                {
                    return HttpNotFound();
                }

                if (!TryUpdateModel(position))
                {
                    return View(position);
                }

                uow.Positions.UpdateName(id);
                uow.Commit();

                // TODO add confirmation toast message
                return RedirectToAction(Actions.Index());
            }
        }

        public virtual ActionResult UpdateName(short id)
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

        public virtual ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Add(FormCollection form)
        {
            using (var uow = _securityUow())
            {
                var position = new Position();

                // TODO Following two lines need fixing (we shouldn't have to type these properties in manually).
                position.Created = DateTime.Now;
                position.CreatedBy = -2;

                if (!TryUpdateModel(position))
                {
                    return View(position);
                }

                uow.Positions.AddPosition(position);
                uow.Commit();

                // TODO add confirmation toast message
                return RedirectToAction(Actions.Index());
            }
        }
    }
}