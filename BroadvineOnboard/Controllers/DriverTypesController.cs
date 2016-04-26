using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BroadvineOnboard.DAL;
using BroadvineOnboard.Models;

namespace BroadvineOnboard.Controllers
{
    public class DriverTypesController : Controller
    {
        private BroadvineOnboardContext db = new BroadvineOnboardContext();

        // GET: DriverTypes
        public ActionResult Index()
        {
            return View(db.DriverTypes.ToList());
        }

        // GET: DriverTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DriverType driverType = db.DriverTypes.Find(id);
            if (driverType == null)
            {
                return HttpNotFound();
            }
            return View(driverType);
        }

        // GET: DriverTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DriverTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DriverTypeID,ClientID,Name,Description")] DriverType driverType)
        {
            if (ModelState.IsValid)
            {
                db.DriverTypes.Add(driverType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(driverType);
        }

        // GET: DriverTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DriverType driverType = db.DriverTypes.Find(id);
            if (driverType == null)
            {
                return HttpNotFound();
            }
            return View(driverType);
        }

        // POST: DriverTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DriverTypeID,ClientID,Name,Description")] DriverType driverType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(driverType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(driverType);
        }

        // GET: DriverTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DriverType driverType = db.DriverTypes.Find(id);
            if (driverType == null)
            {
                return HttpNotFound();
            }
            return View(driverType);
        }

        // POST: DriverTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DriverType driverType = db.DriverTypes.Find(id);
            db.DriverTypes.Remove(driverType);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
