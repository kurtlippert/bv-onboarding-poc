using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using BroadvineOnboard.Models;
using LinqToExcel;

namespace BroadvineOnboard
{
    public class ExcelSpreadSheet
    {
        public string SelectedWorksheet { get; set; }
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
                if (columns == null) columns = excel.GetColumnNames(SelectedWorksheet);
                return columns;
            }
        }

    }
}
