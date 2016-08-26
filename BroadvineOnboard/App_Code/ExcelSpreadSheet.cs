using System.Collections.Generic;
using LinqToExcel;
using System.Linq;

namespace BroadvineOnboard
{
    public class ExcelSpreadSheet
    {
        public string SelectedWorksheet { get; set; }

        public int RowStart { get; set; }

        public string ControllerName { get; set; }

        private ExcelQueryFactory excel = null;
        
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
            excel.ReadOnly = true;
        }

        private IEnumerable<string> columns;
        public IEnumerable<string> Columns
        {
            get
            {
                if (columns == null) columns = Rows.Skip(0).First().ToArray().Select(x => x.Value.ToString());
                return columns;
            }
        }

        private IQueryable<Row> rows;
        public IQueryable<Row> Rows
        {
            get
            {
                //rows = excel.Worksheet(this.SelectedWorksheet).
                if (rows == null) rows = from d in excel.Worksheet(this.SelectedWorksheet) select d;
                return rows;
            }
        }

    }
}
