//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.Office.Interop.Excel;
//using _Excel = Microsoft.Office.Interop.Excel;

//namespace uploadPOC.Classes
//{
//    public class ExcelRead
//    {


//public ExcelRead(string path)
//{
//    int Sheet = 1;
//    this.path = path;
//    wb = excel.Workbooks.Open(path);
//    ws = (Worksheet)wb.Worksheets.get_Item("TIM");
//}

//        public int workSheetCount()
//        {
//            return wb.Worksheets.Count;
//        }
//        public string getNameOfSheet()
//        {
//            return ws.Name;
//        }

//        public string ReadCell(int i, int j)
//        {
//            Range rng = ws.Range["A1"];
//            if (rng != null)
//            {
//                if (ws.get_Range("A1", Type.Missing).Value2 != null)
//                {
//                    wb.Close();
//                    return rng.Value2.ToString();
//                }
//                else
//                {
//                    wb.Close();
//                    return "Returned a null value in the cell";
//                }
//            }
//            else
//            {
//                wb.Close();
//                return "Bollocks";
//            }
//        }
//    }
//}


using System;
using System.Data;
using System.Data.OleDb;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;

namespace uploadPOC.Classes
{
    class ExcelRead
    {
        string path = "";
        _Application excel = new _Excel.Application();
        Workbook wb;
        Worksheet ws;

        public ExcelRead(string path)
        {
            this.path = path;
            wb = excel.Workbooks.Open(path);
            ws = (Worksheet)wb.Worksheets.get_Item(1);
        }

        public int workSheetCount()
        {
            return wb.Worksheets.Count;
        }
        public string getNameOfSheet()
        {
            return ws.Name;
        }

        private System.Data.DataTable GetDataTable(string sql, string connectionString)
        {
            System.Data.DataTable dt = null;

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    using (OleDbDataReader rdr = cmd.ExecuteReader())
                    {
                        dt.Load(rdr);
                        return dt;
                    }
                }
            }
        }

        public void GetExcel(string path)
        {
            string fullPathToExcel = path; //ie C:\Temp\YourExcel.xls
            string connString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0;HDR=yes'", fullPathToExcel);
            System.Data.DataTable dt = GetDataTable("SELECT top 1 from [TIM]", connString);

            foreach (DataRow dr in dt.Rows)
            {
                var row1Col0 = dr[0];
                Console.WriteLine(row1Col0);
            }
        }
    }
}