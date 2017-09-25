using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace sms_pipeLine
{
    public class ExportToExcel
    {
        private static Microsoft.Office.Interop.Excel.Workbook mWorkBook;
        private static Microsoft.Office.Interop.Excel.Sheets mWorkSheets;
        private static Microsoft.Office.Interop.Excel.Worksheet mWSheet1;
        private static Microsoft.Office.Interop.Excel.Application oXL;

        public  void ReadExistingExcel(DataTable dt, string templatePath, string Exportpath, string late1, string late2, string late3)
        {
            try
            {
                //string path = @"C:\Users\zkritsawan.p\Desktop\Project ใช้จริง\testreportMo\TestReport\report\encotemplate.xls";
                string path = templatePath;
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = true;
                oXL.DisplayAlerts = false;
                Microsoft.Office.Interop.Excel.Workbooks workbooks = oXL.Workbooks;
                mWorkBook = workbooks.Open(path, 0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
                //Get all the sheets in the workbook
                mWorkSheets = oXL.Worksheets;
                //Get the allready exists sheet
                mWSheet1 = (Microsoft.Office.Interop.Excel.Worksheet)mWorkSheets.get_Item(1);
                Microsoft.Office.Interop.Excel.Range range = mWSheet1.UsedRange;
                int colCount = range.Columns.Count;

                //int rowCount = range.Rows.Count;
                int rowCount = 5;
                mWSheet1.Cells[rowCount, 7] = late1;
                mWSheet1.Cells[rowCount, 8] = late2;
                mWSheet1.Cells[rowCount, 9] = late3;


                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    mWSheet1.Cells[rowCount + r + 1, 2] = dt.Rows[r]["NAME"].ToString();
                    mWSheet1.Cells[rowCount + r + 1, 7] = dt.Rows[r]["Case1"].ToString();
                    mWSheet1.Cells[rowCount + r + 1, 8] = dt.Rows[r]["Case2"].ToString();
                    mWSheet1.Cells[rowCount + r + 1, 9] = dt.Rows[r]["Case3"].ToString();
                    mWSheet1.Cells[rowCount + r + 1, 6] = string.Format("=E{0}/30", rowCount + r + 1);
                    mWSheet1.Cells[rowCount + r + 1, 11] = string.Format("=E{0}-J{0}", rowCount + r + 1);
                    mWSheet1.Cells[rowCount + r + 1, 10] = string.Format("=((G{0}*F{0})+(H{0}*F{0})+(I{0}*F{0}))", rowCount + r + 1);
                }

                // string pathSave = @"C:\Users\zkritsawan.p\Desktop\Project ใช้จริง\testreportMo\TestReport\report\EncoReport(1).xls";
                string pathSave = Exportpath;
                mWorkBook.SaveAs(pathSave, Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel8,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive,
                Missing.Value, Missing.Value, Missing.Value,
                Missing.Value, Missing.Value);
                mWorkBook.Close(Missing.Value, Missing.Value, Missing.Value);
                mWSheet1 = null;
                mWorkBook = null;
                oXL.Quit();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();

            }
            catch (Exception e)
            {

            }
        }

        public  void ExporttoExcel(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0) return;
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            if (xlApp == null)
            {
                return;
            }

            System.Globalization.CultureInfo CurrentCI = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];
            Microsoft.Office.Interop.Excel.Range range;

            long totalCount = dt.Rows.Count;
            long rowRead = 0;
            float percent = 0;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                worksheet.Cells[1, i + 1] = dt.Columns[i].ColumnName;
                range = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[1, i + 1];
                range.Interior.ColorIndex = 15;
                range.Font.Bold = true;
            }
            for (int r = 0; r < dt.Rows.Count; r++)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    worksheet.Cells[r + 2, i + 1] = dt.Rows[r][i].ToString();
                }
                rowRead++;
                percent = ((float)(100 * rowRead)) / totalCount;
            }
            xlApp.Visible = true;
        }


        public void ExporttoExcelM2(DataTable dt)
        {
            
            if (dt == null || dt.Rows.Count == 0) return;
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            if (xlApp == null)
            {
                return;
            }

            System.Globalization.CultureInfo CurrentCI = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];
            Microsoft.Office.Interop.Excel.Range range;
            worksheet.Cells.NumberFormat = "@";
            long totalCount = dt.Rows.Count;
            long rowRead = 0;
            float percent = 0;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                worksheet.Cells[1, i + 1] = dt.Columns[i].ColumnName;
                range = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[1, i + 1];
                range.Interior.ColorIndex = 15;
                range.Font.Bold = true;
            }
            for (int r = 0; r < dt.Rows.Count; r++)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    worksheet.Cells[r + 2, i + 1] = dt.Rows[r][i].ToString();
                }
                rowRead++;
                percent = ((float)(100 * rowRead)) / totalCount;
            }
            xlApp.Visible = true;

        }
    }
}