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
    public class RevenueSegmentsController : Controller
    {
        private BroadvineOnboardContext db = new BroadvineOnboardContext();

        // GET: RevenueSegments
        public ActionResult Index()
        {
            var revenueSegments = db.RevenueSegments.Include(r => r.RevenueSegmentGroup);
            return View(revenueSegments.ToList());
        }

        // GET: RevenueSegments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RevenueSegment revenueSegment = db.RevenueSegments.Find(id);
            if (revenueSegment == null)
            {
                return HttpNotFound();
            }
            return View(revenueSegment);
        }

        // GET: RevenueSegments/Create
        public ActionResult Create()
        {
            ViewBag.RevenueSegmentGroupID = new SelectList(db.RevenueSegmentGroups, "RevenueSegmentGroupID", "Name");
            return View(new RevenueSegment());
        }

        // POST: RevenueSegments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RevenueSegmentID,ClientID,Name,NameShort,RevenueSegmentGroupID")] RevenueSegment revenueSegment)
        {
            if (ModelState.IsValid)
            {
                db.RevenueSegments.Add(revenueSegment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RevenueSegmentGroupID = new SelectList(db.RevenueSegmentGroups, "RevenueSegmentGroupID", "Name", revenueSegment.RevenueSegmentGroupID);
            return View(revenueSegment);
        }

        // GET: RevenueSegments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RevenueSegment revenueSegment = db.RevenueSegments.Find(id);
            if (revenueSegment == null)
            {
                return HttpNotFound();
            }
            ViewBag.RevenueSegmentGroupID = new SelectList(db.RevenueSegmentGroups, "RevenueSegmentGroupID", "Name", revenueSegment.RevenueSegmentGroupID);
            return View(revenueSegment);
        }

        // POST: RevenueSegments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RevenueSegmentID,ClientID,Name,NameShort,RevenueSegmentGroupID")] RevenueSegment revenueSegment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(revenueSegment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RevenueSegmentGroupID = new SelectList(db.RevenueSegmentGroups, "RevenueSegmentGroupID", "Name", revenueSegment.RevenueSegmentGroupID);
            return View(revenueSegment);
        }

        // GET: RevenueSegments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RevenueSegment revenueSegment = db.RevenueSegments.Find(id);
            if (revenueSegment == null)
            {
                return HttpNotFound();
            }
            return View(revenueSegment);
        }

        // POST: RevenueSegments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RevenueSegment revenueSegment = db.RevenueSegments.Find(id);
            db.RevenueSegments.Remove(revenueSegment);
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
