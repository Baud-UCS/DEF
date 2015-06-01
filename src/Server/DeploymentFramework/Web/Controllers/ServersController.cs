using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Baud.Deployment.Web.Controllers
{
    public partial class ServersController : Controller
    {
        // GET: Servers
        public virtual ActionResult Index()
        {
            return View();
        }

        // GET: Servers/Details/5
        public virtual ActionResult Details(int id)
        {
            return View();
        }

        // GET: Servers/Create
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create()
        {
            return View();
        }

        // POST: Servers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(FormCollection collection)
        {
            try
            {
                //// TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Servers/Edit/5
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Servers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                //// TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Servers/Delete/5
        [ValidateAntiForgeryToken]
        public virtual ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Servers/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                //// TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
