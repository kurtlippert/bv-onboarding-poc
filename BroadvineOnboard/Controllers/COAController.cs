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
    public class COAController : Controller
    {
        private BroadvineOnboardContext db = new BroadvineOnboardContext();

        // GET: COA
        public ActionResult Index()
        {
            var cOAs = db.COAs.Include(c => c.AccountSubType).Include(c => c.AccountType).Include(c => c.Department).Include(c => c.DriverType).Include(c => c.RevenueSegment).Include(c => c.WagePTEBType);
            return View(cOAs.ToList());
        }

        // GET: COA/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COA cOA = db.COAs.Find(id);
            if (cOA == null)
            {
                return HttpNotFound();
            }
            return View(cOA);
        }

        // GET: COA/Create
        public ActionResult Create()
        {
            ViewBag.AccountSubTypeID = new SelectList(db.AccountSubTypes, "AccountSubTypeID", "Name");
            ViewBag.AccountTypeID = new SelectList(db.AccountTypes, "AccountTypeID", "Name");
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepartmentCode");
            ViewBag.DriverTypeID = new SelectList(db.DriverTypes, "DriverTypeID", "Name");
            ViewBag.RevenueSegmentID = new SelectList(db.RevenueSegments, "RevenueSegmentID", "Name");
            ViewBag.WagePTEBID = new SelectList(db.WagePTEBs, "WagePTEBID", "Name");
            return View(new COA());
        }

        // POST: COA/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "COAID,ClientID,Code,Name,Suffix,DepartmentID,AccountTypeID,AccountSubTypeID,RevenueSegmentID,DriverTypeID,WagePTEBID,StatisticalAccountID")] COA cOA)
        {
            if (ModelState.IsValid)
            {
                db.COAs.Add(cOA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccountSubTypeID = new SelectList(db.AccountSubTypes, "AccountSubTypeID", "Name", cOA.AccountSubTypeID);
            ViewBag.AccountTypeID = new SelectList(db.AccountTypes, "AccountTypeID", "Name", cOA.AccountTypeID);
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepartmentCode", cOA.DepartmentID);
            ViewBag.DriverTypeID = new SelectList(db.DriverTypes, "DriverTypeID", "Name", cOA.DriverTypeID);
            ViewBag.RevenueSegmentID = new SelectList(db.RevenueSegments, "RevenueSegmentID", "Name", cOA.RevenueSegmentID);
            ViewBag.WagePTEBID = new SelectList(db.WagePTEBs, "WagePTEBID", "Name", cOA.WagePTEBID);
            return View(cOA);
        }

        // GET: COA/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COA cOA = db.COAs.Find(id);
            if (cOA == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountSubTypeID = new SelectList(db.AccountSubTypes, "AccountSubTypeID", "Name", cOA.AccountSubTypeID);
            ViewBag.AccountTypeID = new SelectList(db.AccountTypes, "AccountTypeID", "Name", cOA.AccountTypeID);
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepartmentCode", cOA.DepartmentID);
            ViewBag.DriverTypeID = new SelectList(db.DriverTypes, "DriverTypeID", "Name", cOA.DriverTypeID);
            ViewBag.RevenueSegmentID = new SelectList(db.RevenueSegments, "RevenueSegmentID", "Name", cOA.RevenueSegmentID);
            ViewBag.WagePTEBID = new SelectList(db.WagePTEBs, "WagePTEBID", "Name", cOA.WagePTEBID);
            return View(cOA);
        }

        // POST: COA/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "COAID,ClientID,Code,Name,Suffix,DepartmentID,AccountTypeID,AccountSubTypeID,RevenueSegmentID,DriverTypeID,WagePTEBID,StatisticalAccountID")] COA cOA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cOA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccountSubTypeID = new SelectList(db.AccountSubTypes, "AccountSubTypeID", "Name", cOA.AccountSubTypeID);
            ViewBag.AccountTypeID = new SelectList(db.AccountTypes, "AccountTypeID", "Name", cOA.AccountTypeID);
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepartmentCode", cOA.DepartmentID);
            ViewBag.DriverTypeID = new SelectList(db.DriverTypes, "DriverTypeID", "Name", cOA.DriverTypeID);
            ViewBag.RevenueSegmentID = new SelectList(db.RevenueSegments, "RevenueSegmentID", "Name", cOA.RevenueSegmentID);
            ViewBag.WagePTEBID = new SelectList(db.WagePTEBs, "WagePTEBID", "Name", cOA.WagePTEBID);
            return View(cOA);
        }

        // GET: COA/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COA cOA = db.COAs.Find(id);
            if (cOA == null)
            {
                return HttpNotFound();
            }
            return View(cOA);
        }

        // POST: COA/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            COA cOA = db.COAs.Find(id);
            db.COAs.Remove(cOA);
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
