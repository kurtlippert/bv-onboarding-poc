﻿using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BroadvineOnboard.DAL;
using BroadvineOnboard.Models;
using System;
using System.Collections.Generic;
using System.Web;
using System.IO;

namespace BroadvineOnboard.Controllers
{
    public class CompaniesController : Controller
    {
        private BroadvineOnboardContext db = new BroadvineOnboardContext();

        // GET: Companies
        public ActionResult Index()
        {
            return View(db.Companies.ToList());
        }

        // POST: Companies
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

                        ExcelSpreadSheet excel = new ExcelSpreadSheet(folderFilename, "Companies");
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
                return RedirectToAction("Index", "Companies", new { msg = failedMessage });

            return RedirectToAction("Index");
        }

        // GET: Companies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // GET: Companies/Create
        public ActionResult Create()
        {
            return View(new Company());
        }

        // POST: Companies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CompanyID,ClientID,CompanyCode,Name,NameShort,NameLegal,Address1,Address2,City,State,PostalCode,Phone,Fax,URL,IsManagementCompany,IsOwnerCompany")] Company company)
        {
            if (ModelState.IsValid)
            {
                db.Companies.Add(company);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(company);
        }

        // GET: Companies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CompanyID,ClientID,CompanyCode,Name,NameShort,NameLegal,Address1,Address2,City,State,PostalCode,Phone,Fax,URL,IsManagementCompany,IsOwnerCompany")] Company company)
        {
            if (ModelState.IsValid)
            {
                db.Entry(company).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(company);
        }

        // GET: Companies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Company company = db.Companies.Find(id);
            db.Companies.Remove(company);
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
            return View(new Company());
        }

        public JsonResult GetCompaniesFromClient(Guid Id)
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
