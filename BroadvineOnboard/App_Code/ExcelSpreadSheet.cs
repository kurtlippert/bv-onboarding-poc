using System.Collections.Generic;
using LinqToExcel;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace BroadvineOnboard
{
    public class ExcelSpreadSheet
    {
        public string SelectedWorksheet { get; set; }

        public int RowStart { get; set; }

        public string ControllerName { get; set; }

        private ExcelQueryFactory excel = null;

        private string excelFileName = null;
        
        private IEnumerable<string> worksheetNames = null;
        public IEnumerable<string> WorkSheetNames
        {
            get
            {
                if (worksheetNames == null) worksheetNames = excel.GetWorksheetNames();
                return worksheetNames;
            }
        }

        public ExcelSpreadSheet(string folderFilename, string controllerName)
        {
            this.ControllerName = controllerName;
            excel = new ExcelQueryFactory(folderFilename);
            excelFileName = folderFilename;
            excel.ReadOnly = true;
        }

        private IEnumerable<string> columns;
        public IEnumerable<string> Columns
        {
            //get
            //{
            //    if (columns == null)
            //    {
            //        TODO: Figure Out how to get this code to work :/
            //        //columns = RowsNoHeader.Skip(6).First().ToArray().Select(x => x.Value.ToString());
            //        //columns = RowsNoHeader.Skip(RowStart).First().ToArray().Select(x => x.Value.ToString());
            //        var row = RowsNoHeader.Skip(6).First();

            //        List<string> cols = new List<string>();
            //        foreach (var cell in row.ToArray())
            //        {
            //            cols.Add(cell.Value.ToString());
            //        }
            //    }

            //    return columns;
            //}

            get
            {
                if (columns == null)
                {
                    columns = ReadExcelFileSAX(excelFileName);    // SAX
                }

                return columns;
            }
        }

        private IQueryable<LinqToExcel.RowNoHeader> rowsNoHeader;
        public IQueryable<LinqToExcel.RowNoHeader> RowsNoHeader
        {
            get
            {
                if (rowsNoHeader == null)
                {
                    rowsNoHeader = from d in excel.WorksheetNoHeader(this.SelectedWorksheet) select d;
                }

                return rowsNoHeader;
            }
        }

        private IQueryable<LinqToExcel.Row> rows;
        public IQueryable<LinqToExcel.Row> Rows
        {
            get
            {
                if (rows == null)
                {
                    rows = from d in excel.Worksheet(this.SelectedWorksheet) select d;
                }

                return rows;
            }
        }

        // The SAX approach.
        private IEnumerable<string> ReadExcelFileSAX(string fileName)
        {
            using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(fileName, false))
            {
                WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;
                WorksheetPart worksheetPart = GetWorksheetPart(workbookPart, this.SelectedWorksheet);

                DocumentFormat.OpenXml.Spreadsheet.Row row = worksheetPart.Worksheet.Descendants<DocumentFormat.OpenXml.Spreadsheet.Row>()
                    .Where(r => this.RowStart == r.RowIndex).FirstOrDefault();

                List<string> cells = new List<string>();
                foreach (var cell in row.Descendants<DocumentFormat.OpenXml.Spreadsheet.Cell>())
                {
                    if (cell.CellValue != null)
                    {
                        string cellValue;

                        //if (cell.DataType != null && cell.DataType == CellValues.SharedString)
                        //{
                            SharedStringItem ssi = workbookPart.SharedStringTablePart.SharedStringTable.Elements<SharedStringItem>().ElementAt(int.Parse(cell.CellValue.InnerText));

                            cellValue = ssi.InnerText;
                        //}
                        //else
                        //{
                        //    cellValue = cell.CellValue.InnerText;
                        //}

                        cells.Add(cellValue);
                    }
                }

                return cells;
            }
        }

        private WorksheetPart GetWorksheetPart(WorkbookPart workbookPart, string sheetName)
        {
            string relId = workbookPart.Workbook.Descendants<Sheet>().First(s => sheetName.Equals(s.Name)).Id;
            return (WorksheetPart)workbookPart.GetPartById(relId);
        }
    }
}
