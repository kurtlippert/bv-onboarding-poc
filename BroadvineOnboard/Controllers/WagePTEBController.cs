using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BroadvineOnboard.DAL;
using BroadvineOnboard.Models;

namespace BroadvineOnboard.Controllers
{
    public class WagePTEBController : Controller
    {
        private BroadvineOnboardContext db = new BroadvineOnboardContext();

        // GET: WagePTEB
        public ActionResult Index()
        {
            return View(db.WagePTEBs.ToList());
        }

        // GET: WagePTEB/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WagePTEB wagePTEB = db.WagePTEBs.Find(id);
            if (wagePTEB == null)
            {
                return HttpNotFound();
            }
            return View(wagePTEB);
        }

        // GET: WagePTEB/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WagePTEB/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WagePTEBID,Name")] WagePTEB wagePTEB)
        {
            if (ModelState.IsValid)
            {
                db.WagePTEBs.Add(wagePTEB);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(wagePTEB);
        }

        // GET: WagePTEB/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WagePTEB wagePTEB = db.WagePTEBs.Find(id);
            if (wagePTEB == null)
            {
                return HttpNotFound();
            }
            return View(wagePTEB);
        }

        // POST: WagePTEB/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WagePTEBID,Name")] WagePTEB wagePTEB)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wagePTEB).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(wagePTEB);
        }

        // GET: WagePTEB/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WagePTEB wagePTEB = db.WagePTEBs.Find(id);
            if (wagePTEB == null)
            {
                return HttpNotFound();
            }
            return View(wagePTEB);
        }

        // POST: WagePTEB/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WagePTEB wagePTEB = db.WagePTEBs.Find(id);
            db.WagePTEBs.Remove(wagePTEB);
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
