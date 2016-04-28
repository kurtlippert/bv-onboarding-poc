using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BroadvineOnboard.DAL;
using BroadvineOnboard.Models;

namespace BroadvineOnboard.Controllers
{
    public class RevenueSegmentGroupsController : Controller
    {
        private BroadvineOnboardContext db = new BroadvineOnboardContext();

        // GET: RevenueSegmentGroups
        public ActionResult Index()
        {
            return View(db.RevenueSegmentGroups.ToList());
        }

        // GET: RevenueSegmentGroups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RevenueSegmentGroup revenueSegmentGroup = db.RevenueSegmentGroups.Find(id);
            if (revenueSegmentGroup == null)
            {
                return HttpNotFound();
            }
            return View(revenueSegmentGroup);
        }

        // GET: RevenueSegmentGroups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RevenueSegmentGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RevenueSegmentGroupID,Name,Description")] RevenueSegmentGroup revenueSegmentGroup)
        {
            if (ModelState.IsValid)
            {
                db.RevenueSegmentGroups.Add(revenueSegmentGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(revenueSegmentGroup);
        }

        // GET: RevenueSegmentGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RevenueSegmentGroup revenueSegmentGroup = db.RevenueSegmentGroups.Find(id);
            if (revenueSegmentGroup == null)
            {
                return HttpNotFound();
            }
            return View(revenueSegmentGroup);
        }

        // POST: RevenueSegmentGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RevenueSegmentGroupID,Name,Description")] RevenueSegmentGroup revenueSegmentGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(revenueSegmentGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(revenueSegmentGroup);
        }

        // GET: RevenueSegmentGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RevenueSegmentGroup revenueSegmentGroup = db.RevenueSegmentGroups.Find(id);
            if (revenueSegmentGroup == null)
            {
                return HttpNotFound();
            }
            return View(revenueSegmentGroup);
        }

        // POST: RevenueSegmentGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RevenueSegmentGroup revenueSegmentGroup = db.RevenueSegmentGroups.Find(id);
            db.RevenueSegmentGroups.Remove(revenueSegmentGroup);
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
