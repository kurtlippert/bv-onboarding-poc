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
using System.IO;

namespace BroadvineOnboard.Controllers
{
    public class PropertyController : Controller
    {
        private BroadvineOnboardContext db = new BroadvineOnboardContext();

        // GET: Property
        public ActionResult Index(string msg = "")
        {
            // TODO: figure out why these 2 lines below here were needed
            //var properties = db.Properties.Include(p => p.Area);
            //properties.All(p => p.ClientID == BroadvineOnboard.Helpers.CurrentClientID);

            if (!string.IsNullOrEmpty(msg)) ViewBag.Error = msg;

            return View(db.Properties.ToList());
        }

        // GET: Property/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Property property = db.Properties.Find(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            return View(property);
        }

        // GET: Property/Create
        public ActionResult Create()
        {
            ViewBag.AreaID = new SelectList(db.Areas, "AreaID", "Name");
            ViewBag.CompanyID = new SelectList(db.Companies, "CompanyID", "Name");
            return View(new Property());
        }

        // POST: Property/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ClientID,Name,Code,Address1,Address2,City,State,PostalCode,BrandCode,SmithTravelCode,TotalRooms,URL,Phone,Email,FoodAndBeverage,ServiceType,StatusType,RelationshipType,MaturityType,CalendarType,AreaID,CompanyID")] Property property)
        {
            if (ModelState.IsValid)
            {
                db.Properties.Add(property);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AreaID = new SelectList(db.Areas, "AreaID", "Name", property.AreaID);
            return View(property);
        }

        // GET: Property/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Property property = db.Properties.Find(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            ViewBag.AreaID = new SelectList(db.Areas, "AreaID", "Name", property.AreaID);
            ViewBag.CompanyID = new SelectList(db.Companies, "CompanyID", "Name");
            return View(property);
        }

        // POST: Property/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ClientID,Name,Code,Address1,Address2,City,State,PostalCode,BrandCode,SmithTravelCode,TotalRooms,URL,Phone,Email,FoodAndBeverage,ServiceType,StatusType,RelationshipType,MaturityType,CalendarType,AreaID,CompanyID")] Property property)
        {
            if (ModelState.IsValid)
            {
                db.Entry(property).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AreaID = new SelectList(db.Areas, "AreaID", "Name", property.AreaID);
            return View(property);
        }

        // GET: Property/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Property property = db.Properties.Find(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            return View(property);
        }

        // POST: Property/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Property property = db.Properties.Find(id);
            db.Properties.Remove(property);
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


        //  Excel spreadsheet stuff ###########################################################################################################################################################################################

        [HttpPost]
        public ActionResult Sheet(FormCollection values)
        {
            List<Property> properties = new List<Property>();

            bool isHeader = true;

            foreach (LinqToExcel.Row row in Helpers.CurrentClientUpload.Rows)
            {
                //  TODO:   Need to take into account the start row of the data.
                //          Currently, it is assumed that the first row is data, whereas it is typically a header row
                if (isHeader) { isHeader = false; continue; }

                //  Check if the value of the first cell in the current row is empty; if it is, then let's assume we've reached the end of records
                if (string.IsNullOrEmpty(row[0].Value.ToString())) break;

                Property property = new Property();
                foreach (System.Reflection.PropertyInfo field in property.GetType().GetProperties())
                {
                    if (!values.AllKeys.Contains(field.Name) || string.IsNullOrEmpty(values[field.Name])) continue;
                    try
                    {
                        int columnNumber = int.Parse(values[field.Name]);
                        if (columnNumber < 1) continue;

                        field.SetValue(property, row[columnNumber].Value.ToString());

                    }
                    catch (Exception e)
                    {
                        Console.Write(e.Message);
                    }
                }

                properties.Add(property);
            }

            //  TODO:   Updating the database since we are currently being redirected to the listings page. 
            //          This will need to change, as we want to present a sample of the data before committing to the database.
            foreach (Property p in properties)
            {
                db.Properties.Add(p);
                db.Entry(p).State = EntityState.Added;
            }

            db.SaveChanges();


            return RedirectToAction("Index");

        }

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

                        ExcelSpreadSheet excel = new ExcelSpreadSheet(folderFilename, "Property");
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
                return RedirectToAction("Index", "Property", new { msg = failedMessage });

            return RedirectToAction("Index");
        }

        public ActionResult WorkSheet()
        {
            var result = (from w in Helpers.CurrentClientUpload.WorkSheetNames select new SelectListItem() { Text = w, Value = w }).ToList();
            ViewBag.WorkSheetNames = result;
            return View();
        }

        //[HttpPost]
        //public ActionResult Sheet(PropertyViewModel viewModel)
        //{
        //    //Helpers.CurrentClientUpload.Columns.
        //    var data = Helpers.CurrentClientUpload.Rows;

        //    int nameCol = int.Parse(viewModel.Name);
        //    int codeCol = int.Parse(viewModel.Code);
        //    int address1Col = int.Parse(viewModel.Address1);
        //    int address2Col = int.Parse(viewModel.Address2);
        //    int cityCol = int.Parse(viewModel.City);
        //    int stateCol = int.Parse(viewModel.State);
        //    int postalCodeCol = int.Parse(viewModel.PostalCode);
        //    int brandCodeCol = int.Parse(viewModel.BrandCode);
        //    int smithTravelCodeCol = int.Parse(viewModel.SmithTravelCode);
        //    int totalRoomsCol = int.Parse(viewModel.TotalRooms);
        //    int address1Col = int.Parse(viewModel.Address1);

        //    List<Property> properties = new List<Property>();
        //    foreach (var row in data)
        //    {
        //        string Name = row[int.Parse(viewModel.Name)].Value.ToString().Trim();
        //        string Code = row[1].Value.ToString().Trim();
        //        string Address1 = row[2].Value.ToString().Trim();
        //        string Address2 = row[3].Value.ToString().Trim();
        //        string City = row[4].Value.ToString().Trim();
        //        string State = row[5].Value.ToString().Trim();
        //        string PostalCode = row[6].Value.ToString().Trim();
        //        string BrandCode = row[7].Value.ToString().Trim();
        //        string SmithTravelCode = row[8].Value.ToString().Trim();
        //        int TotalRooms = row[9].Value;
        //        string URL = row[10].Value.ToString().Trim();
        //        string Phone = row[11].Value.ToString().Trim();
        //        string Email = row[12].Value.ToString().Trim();
        //        bool FoodAndBeverage = (bool)row[13].Value;
        //        Enums.PropertyServiceType ServiceType = (Enums.PropertyServiceType)row[14].Value;
        //        Enums.PropertyStatus StatusType = (Enums.PropertyStatus)row[15].Value;
        //        Enums.PropertyRelationship RelationshipType = (Enums.PropertyRelationship)row[16].Value;
        //        Enums.PropertyMaturity MaturityType = (Enums.PropertyMaturity)row[17].Value;
        //        Enums.PropertyCalendar CalendarType = (Enums.PropertyCalendar)row[18].Value;
        //        Company Company = (Company)row[19].Value;
        //        Area Area = (Area)row[20].Value;

        //        properties.Add(new Property
        //        {
        //            Name = row[0].Value.ToString().Trim(),
        //            Code = row[1].Value.ToString().Trim(),
        //            Address1 = row[2].Value.ToString().Trim(),
        //            Address2 = row[3].Value.ToString().Trim(),
        //            City = row[4].Value.ToString().Trim(),
        //            State = row[5].Value.ToString().Trim(),
        //            PostalCode = row[6].Value.ToString().Trim(),
        //            BrandCode = row[7].Value.ToString().Trim(),
        //            SmithTravelCode = row[8].Value.ToString().Trim(),
        //            TotalRooms = (int)row[9].Value,
        //            URL = row[10].Value.ToString().Trim(),
        //            Phone = row[11].Value.ToString().Trim(),
        //            Email = row[12].Value.ToString().Trim(),
        //            FoodAndBeverage = (bool)row[13].Value,
        //            ServiceType = (Enums.PropertyServiceType)row[14].Value,
        //            StatusType = (Enums.PropertyStatus)row[15].Value,
        //            RelationshipType = (Enums.PropertyRelationship)row[16].Value,
        //            MaturityType = (Enums.PropertyMaturity)row[17].Value,
        //            CalendarType = (Enums.PropertyCalendar)row[18].Value,
        //            Company = (Company)row[19].Value,
        //            Area = (Area)row[20].Value
        //        });            
        //    }

        //    return View("Index", properties.AsEnumerable());
        //} 

        [HttpPost]
        public ActionResult WorkSheet(string worksheetName)
        {
            ExcelSpreadSheet s = Helpers.CurrentClientUpload;
            s.SelectedWorksheet = worksheetName;
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
            return View(new PropertyViewModel());
        }

        public JsonResult GetPropertiesFromClient(int Id)
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
    }
}
