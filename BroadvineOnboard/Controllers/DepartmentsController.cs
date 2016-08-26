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
    public class DepartmentsController : Controller
    {
        private BroadvineOnboardContext db = new BroadvineOnboardContext();

        // GET: Departments
        public ActionResult Index(string msg = "")
        {
            var departments = db.Departments.Include(d => d.DepartmentGroup);

            if (!string.IsNullOrEmpty(msg)) ViewBag.Error = msg;

            return View(departments.ToList());
        }

        // POST: Departments
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

                        ExcelSpreadSheet excel = new ExcelSpreadSheet(folderFilename, "Departments");
                        if (excel.WorkSheetNames.Count() == 1) excel.SelectedWorksheet = excel.WorkSheetNames.First().ToString();
                        Helpers.CurrentClientUpload = excel;

                        if (excel.WorkSheetNames.Count() == 1) return RedirectToAction("Import");
                        else return RedirectToAction("WorkSheet");

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
                return RedirectToAction("Index", "Departments", new { msg = failedMessage });

            return RedirectToAction("Index");
        }

        // GET: Departments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // GET: Departments/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentGroupID = new SelectList(db.DepartmentGroups, "DepartmentGroupID", "DepartmentGroupCode");
            return View(new Department());
        }

        // POST: Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DepartmentID,ClientID,DepartmentCode,DepartmentName,DepartmentShortName,SortOrder,ProfitAndLoss,ProfitCenterDistributed,IsActive,DepartmentIncludeInGOPID,DepartmentGroupID")] Department department)
        {
            if (ModelState.IsValid)
            {
                db.Departments.Add(department);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentGroupID = new SelectList(db.DepartmentGroups, "DepartmentGroupID", "DepartmentGroupCode", department.DepartmentGroupID);
            return View(department);
        }

        // GET: Departments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentGroupID = new SelectList(db.DepartmentGroups, "DepartmentGroupID", "DepartmentGroupCode", department.DepartmentGroupID);
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DepartmentID,ClientID,DepartmentCode,DepartmentName,DepartmentShortName,SortOrder,ProfitAndLoss,ProfitCenterDistributed,IsActive,DepartmentIncludeInGOPID,DepartmentGroupID")] Department department)
        {
            if (ModelState.IsValid)
            {
                db.Entry(department).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentGroupID = new SelectList(db.DepartmentGroups, "DepartmentGroupID", "DepartmentGroupCode", department.DepartmentGroupID);
            return View(department);
        }

        // GET: Departments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Department department = db.Departments.Find(id);
            db.Departments.Remove(department);
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
            //List<SelectListItem> list = columnNames.Select(x => new SelectListItem { Text = x, Value = columnNames. }).ToList();
            List<SelectListItem> list = new List<SelectListItem>();
            for (int i = 0; i < columnNames.Length; i++)
            {
                list.Add(new SelectListItem { Text = columnNames[i], Value = i.ToString() });
            }

            ViewBag.Columns = list;

            //Collection
            return View(new Department());
        }

        public JsonResult GetDepartmentsFromClient(Guid Id)
        {
            string[] columnNames = Helpers.CurrentClientUpload.Columns.ToArray();
            //List<SelectListItem> list = columnNames.Select(x => new SelectListItem { Text = x, Value = x }).ToList();
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
