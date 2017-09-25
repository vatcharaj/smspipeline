using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
//using ClosedXML.Excel;

namespace sms_pipeLine
{
    public partial class reciepient : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int is_admin = Convert.ToInt16(Session["ADMIN"]);
                if (is_admin == 0)
                {
                    EditPanel.Visible = false;
                    PPLTabPanel.Visible = false;
                }
            }

        }

        protected void Button1_Click(object sender, ImageClickEventArgs e)
        {      
            OracleQuery2 cc2 = new OracleQuery2();
            DataTable dt = new DataTable();
            dt = cc2.LoadAllTempAIS();
            //ExportToExcel exp = new ExportToExcel();
            //dt.Columns.Add("Test1");
            //dt.Columns.Add("Test2");
            //dt.Rows.Add("0817460808");
            //dt.Rows.Add("0817460808");
            ExporttoExcelM5(dt);


        }
        //private void ExporttoExcelM1(DataTable tbl)
        //{
        //    using (ExcelPackage pck = new ExcelPackage())
        //    {
        //        //Create the worksheet
        //        ExcelWorksheet ws = pck.Workbook.Worksheets.Add("SMSAllTemplAIS");

        //        ws.Cells["A1"].LoadFromDataTable(tbl, true);

        //        using (ExcelRange rng = ws.Cells["A1"])
        //        {
        //            rng.Style.WrapText = true;
        //            rng.Style.Border.BorderAround(ExcelBorderStyle.Medium);
        //        }
        //        //Write it back to the client
        //        var time =DateTime.Now.ToString();
        //        Response.Clear();
        //        //using (var memoryStream = new MemoryStream())
        //        //{
        //            Response.ContentType = "application/vnd.ms-excel";
        //            Response.AddHeader("content-disposition", "attachment;  filename=SMSAllTemplAIS.xls");
        //            Response.BinaryWrite(pck.GetAsByteArray());
        //            pck.SaveAs(memoryStream);
        //            memoryStream.WriteTo(Response.OutputStream);
        //            Response.Flush();
        //            Response.End();
        //        //}
        //    }

        //}

        private void ExporttoExcelM5(DataTable tbl)
        {
            using (ExcelPackage pck = new ExcelPackage())
            {
                //Create the worksheet
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("SMSAllTemplAIS");

                ws.Cells["A1"].LoadFromDataTable(tbl, true);

                using (ExcelRange rng = ws.Cells["A1"])
                {
                    rng.Style.WrapText = true;
                    rng.Style.Border.BorderAround(ExcelBorderStyle.Medium);
                }

                //Write it back to the client
                Response.Clear();
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("content-disposition", "attachment;  filename=SMSAllTemplAIS.xls");
                Response.BinaryWrite(pck.GetAsByteArray());
                Response.End();
            }

        }
       
    }
}