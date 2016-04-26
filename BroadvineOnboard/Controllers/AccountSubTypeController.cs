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
    public class AccountSubTypeController : Controller
    {
        private BroadvineOnboardContext db = new BroadvineOnboardContext();

        // GET: AccountSubType
        public ActionResult Index()
        {
            return View(db.AccountSubTypes.ToList());
        }

        // GET: AccountSubType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountSubType accountSubType = db.AccountSubTypes.Find(id);
            if (accountSubType == null)
            {
                return HttpNotFound();
            }
            return View(accountSubType);
        }

        // GET: AccountSubType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountSubType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccountSubTypeID,ClientID,Name")] AccountSubType accountSubType)
        {
            if (ModelState.IsValid)
            {
                db.AccountSubTypes.Add(accountSubType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(accountSubType);
        }

        // GET: AccountSubType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountSubType accountSubType = db.AccountSubTypes.Find(id);
            if (accountSubType == null)
            {
                return HttpNotFound();
            }
            return View(accountSubType);
        }

        // POST: AccountSubType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccountSubTypeID,ClientID,Name")] AccountSubType accountSubType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accountSubType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(accountSubType);
        }

        // GET: AccountSubType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountSubType accountSubType = db.AccountSubTypes.Find(id);
            if (accountSubType == null)
            {
                return HttpNotFound();
            }
            return View(accountSubType);
        }

        // POST: AccountSubType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccountSubType accountSubType = db.AccountSubTypes.Find(id);
            db.AccountSubTypes.Remove(accountSubType);
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
