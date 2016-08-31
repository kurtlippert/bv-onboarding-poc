using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BroadvineOnboard.DAL;
using BroadvineOnboard.Models;
using System.Web;
using System.IO;
using System;
using System.Collections.Generic;

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

        // POST: COA
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            string failedMessage = "";

            if (file != null && file.ContentLength > 0)
            {
                string filename = file.FileName.ToLower();
                if (filename.EndsWith("xls") || filename.EndsWith("xlsx"))
                {
                    try
                    {
                        var folderFilename = Path.Combine(Server.MapPath(System.Web.Configuration.WebConfigurationManager.AppSettings["UploadFolder"]), Path.GetFileName(file.FileName));
                        file.SaveAs(folderFilename);

                        ExcelSpreadSheet excel = new ExcelSpreadSheet(folderFilename, "COA");
                        if (excel.WorkSheetNames.Count() == 1) excel.SelectedWorksheet = excel.WorkSheetNames.First().ToString();
                        Helpers.CurrentClientUpload = excel;

                        return RedirectToAction("WorkSheet");

                    }
                    catch (Exception ex)
                    {
                        failedMessage = ex.Message;
                    }

                }
                else failedMessage = "Only valid excel spreadsheets are excepted";

            }
            else failedMessage = "The file contained no data.";

            if (!string.IsNullOrEmpty(failedMessage))
                return RedirectToAction("Index", "COA", new { msg = failedMessage });

            return RedirectToAction("Index");
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


        public ActionResult WorkSheet()
        {
            var result = (from w in Helpers.CurrentClientUpload.WorkSheetNames select new SelectListItem() { Text = w, Value = w }).ToList();
            ViewBag.WorkSheetNames = result;
            return View();
        }

        [HttpPost]
        public ActionResult WorkSheet(FormCollection values)
        {
            ExcelSpreadSheet s = Helpers.CurrentClientUpload;
            s.SelectedWorksheet = values["worksheetName"];
            s.RowStart = int.Parse(values["rowNumber"]);
            Helpers.CurrentClientUpload = s;

            return RedirectToAction("Import");
        }

        //  Import 
        public ActionResult Import()
        {
            string[] columnNames = Helpers.CurrentClientUpload.Columns.ToArray();
            ViewBag.AreaID = new SelectList(db.Areas, "AreaID", "Name");
            ViewBag.CompanyID = new SelectList(db.Companies, "CompanyID", "Name");

            Array.IndexOf(columnNames, "");
            List<SelectListItem> list = new List<SelectListItem>();
            for (int i = 0; i < columnNames.Length; i++)
            {
                list.Add(new SelectListItem { Text = columnNames[i], Value = i.ToString() });
            }

            ViewBag.Columns = list;

            //Collection
            return View(new COA());
        }

        public JsonResult GetCOAFromClient(Guid Id)
        {
            string[] columnNames = Helpers.CurrentClientUpload.Columns.ToArray();
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "(Don't Map)", Value = "-1" });
            for (int i = 0; i < columnNames.Length; i++)
            {
                list.Add(new SelectListItem { Text = columnNames[i], Value = i.ToString() });
            }

            return Json(list, JsonRequestBehavior.AllowGet);
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
