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
                if (columns == null)
                {
                    //var rows = from d in excel.Worksheet(this.SelectedWorksheet) select d;
                    var rows = Rows;
                    var rowsEnumerable = (IEnumerable<Row>)Rows;
                    Cell[] c = rowsEnumerable.ElementAt(0).ToArray();
                    

                    var result = c.Select(x => x.Value.ToString());

                    columns = result;
                    


                    //var rowWithColumnHeaders = -1;

                    //int currentRow = -1;
                    //foreach (var row in rows)
                    //{
                    //    currentRow++; if (currentRow < RowStart) continue;

                    //}

                    //columns = rows.ElementAt(RowStart).;

                    //List<string> strCols = new List<string>();
                    //foreach (var row in rows)
                    //{
                    //    strCols.Add(row[])
                    //}
                }


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
