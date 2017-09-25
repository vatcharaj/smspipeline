using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Text;
using System.IO;
using System.Net;
using System.ComponentModel;

namespace sms_pipeLine.controls
{
    public partial class ReportControl : System.Web.UI.UserControl
    {
      

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.ExcelBtn);
            if (!IsPostBack)
            {
                startLabel.Text = DateTime.Now.ToString("dd/MM/yyyy");
                endLabel.Text = DateTime.Now.ToString("dd/MM/yyyy");
                staCalendar.SelectedDate = DateTime.Today.Date;
                endCalendar.SelectedDate = DateTime.Today.Date;
                bindGroupList();
                bindReportList();
                bindTime();
                bindmonthlyreport();
                if (ViewState["ReportResultView"]!=null)
                {
                    ReportResultView.DataSource = ViewState["ReportResultView"] as DataTable;
                    ReportResultView.DataBind();
                   
                }
            }
        }

        private void bindmonthlyreport()
        {
            OracleQuery2 cc2 = new OracleQuery2();
            DataTable dt = cc2.LoadMonthlyReport();
            MonthlyList.DataSource = dt;
            MonthlyList.DataBind();
           
        }
        protected void MonthlyList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "LdReport_CMD")
            {
                string pathfiles = e.CommandArgument.ToString();
               string ff=   HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.ApplicationPath;
               Response.Redirect(ff + pathfiles);
            }
        }
       
        private void bindTime()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Time");
            dt.Columns.Add("Hrs");
            DateTime dd = DateTime.Today.Date;
            for (int i = 0; i < 24; i++) 
            {
                dt.Rows.Add(dd.AddHours(i).ToString("HH:00"), i);
            }

            StaTime.DataSource = dt;
            EndTime.DataSource = dt;
            StaTime.DataTextField = "Time";
            EndTime.DataTextField = "Time";
            StaTime.DataValueField = "Hrs";
            EndTime.DataValueField = "Hrs";
            StaTime.DataBind();
            EndTime.DataBind();

            StaTime.SelectedValue = "0";
            EndTime.SelectedValue = "23";
        }

        private void bindReportList()
        {
            OracleQuery2 cc2 = new OracleQuery2();
            DataTable dt = cc2.LoadReport();
            ReportList.DataSource = dt;
            ReportList.DataTextField = "REPORT_NAME";
            ReportList.DataValueField = "REPORT_ID";
            ReportList.DataBind();
            ReportList.Items.Insert(0, new ListItem("--เลือกรายงาน--", "-1"));
            ReportList.SelectedIndex = 0;
            //ReportList.Items.FindByValue("4").Enabled = false;
        }

        private void bindGroupList()
        {
            OracleQuery2 cc2 = new OracleQuery2();
            DataTable dtgroup = cc2.LoadAllGroups();
            dtgroup.Columns.Add("text");
            foreach (DataRow r in dtgroup.Rows)
            {
                r["text"] = r["DEPARTMENT"] + " - " + r["GROUP_NAME"];
            }
            GroupList.DataSource = dtgroup;
            GroupList.DataTextField = "text";
            GroupList.DataValueField = "GROUP_ID";
            GroupList.DataBind();
            GroupList.Items.Insert(0, new ListItem("--ทั้งหมด--", "-1"));
            GroupList.SelectedIndex = 0;
        }

        protected void StaBtn_Click(object sender, ImageClickEventArgs e)
        {
            staCalendar.Visible = !staCalendar.Visible;
            endCalendar.Visible = false;
        }

        protected void EndBtn_Click(object sender, ImageClickEventArgs e)
        {
            endCalendar.Visible = !endCalendar.Visible;
            staCalendar.Visible = false;
        }

        protected void staCalendar_SelectionChanged(object sender, EventArgs e)
        {
            DateTime stDate = staCalendar.SelectedDate;
            DateTime endDate = endCalendar.SelectedDate;//Convert.ToDateTime(endLabel.Text);
            if (endDate < stDate || stDate > DateTime.Now)
                startLabel.Text = endDate.ToString("dd/MM/yyyy");
            else if ((endDate - stDate).TotalDays > 30)
            {
                startLabel.Text = endDate.AddDays(-30).ToString("dd/MM/yyyy");
                staCalendar.SelectedDate = endDate.AddDays(-30);
            }
            else
                startLabel.Text = stDate.ToString("dd/MM/yyyy");


            staCalendar.Visible = false;
        }

        protected void endCalendar_SelectionChanged(object sender, EventArgs e)
        {
            DateTime stDate = staCalendar.SelectedDate;// Convert.ToDateTime(startLabel.Text);
            DateTime endDate = endCalendar.SelectedDate;
            if (endDate < stDate)
                endLabel.Text = stDate.ToString("dd/MM/yyyy");
            else if ((endDate - stDate).TotalDays > 30)
            {
                endLabel.Text = stDate.AddDays(30).ToString("dd/MM/yyyy");
                endCalendar.SelectedDate = stDate.AddDays(30);
            }
            else
                endLabel.Text = endDate.ToString("dd/MM/yyyy");

            endCalendar.Visible = false;
        }

        protected void GenReport_Click(object sender, EventArgs e)
        {
            if (ReportList.SelectedValue != "4" )
                bindreportgrid();
            else
                bindgraphsum();

        }

        private void bindManualSend()
        {
            throw new NotImplementedException();
        }

        private void bindgraphsum()
        {
            OracleQuery2 cc2 = new OracleQuery2();
            DataTable dt = cc2.LoadSumReport();

            if (ReportList.SelectedValue != "-1")
            {
                errorLabel.Visible = false;
                grpPanel.Visible = true;
                reportPanel.Visible = true;
                ReportResultView.Visible = false;
                SumChart.Visible = true;
                ExcelBtn.Visible = false;
                NodataLabel.Visible = false;
                ReportNameLabel.Text = ReportList.SelectedItem.Text;
                SumChart.Series.Add("SUM");
                double succ = Convert.ToDouble(dt.Rows[0]["STATUS_SENT"]);
                double err = Convert.ToDouble(dt.Rows[0]["STATUS_ERROR"]);
                double tatal = Convert.ToDouble(dt.Rows[0]["SENT_TOTAL"]);
                string[] xValues = { "Success", "Error" };
                double[] yValues = { succ, err };
                SumChart.Series["SUM"].Points.DataBindXY(xValues, yValues);
                SumChart.Series["SUM"].ChartType = SeriesChartType.Pie;
                SumChart.Series["SUM"].Points[0].Color = Color.MediumSeaGreen;
                SumChart.Series["SUM"].Points[1].Color = Color.PaleGreen;
                for (int i = 0; i < yValues.Count(); i++)
                {
                    SumChart.Series["SUM"].Points[i].Label = xValues[i] + " : " + yValues[i];
                    SumChart.Series["SUM"].Points[i].LegendText = xValues[i];
                }
                SumChart.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                SumChart.Legends[0].Enabled = true;
                SumChart.Titles.Add("Sent Total : " + tatal);
            }
        }

        private void bindreportgrid()
        {
            DataTable dt = new DataTable();
            if (ReportList.SelectedValue != "-1")
            {
                errorLabel.Visible = false;
                grpPanel.Visible = true;
                reportPanel.Visible = true;
                ReportResultView.Visible = true;
                SumChart.Visible = false;
                ExcelBtn.Visible = true;
                ReportNameLabel.Text = ReportList.SelectedItem.Text;
                int stahrs = Convert.ToInt32(StaTime.SelectedValue);
                int endhrs = Convert.ToInt32(EndTime.SelectedValue);
                DateTime st_date = staCalendar.SelectedDate.AddHours(stahrs);// Convert.ToDateTime(startLabel.Text);
                DateTime end_date = endCalendar.SelectedDate.AddHours(endhrs);//Convert.ToDateTime(endLabel.Text).AddHours(24);

                if (end_date < st_date)
                {
                    end_date = st_date;
                    EndTime.SelectedValue = StaTime.SelectedValue;

                }
                OracleQuery2 cc2 = new OracleQuery2();
                string groupid = GroupList.SelectedValue;

                if (ReportList.SelectedValue == "1")
                {
                    dt = cc2.LoadAllSMSReport(st_date, end_date, groupid);
                }
                else if (ReportList.SelectedValue == "2")
                {
                    dt = cc2.LoadSMSReport(st_date, end_date, 1, groupid);
                }
                else if (ReportList.SelectedValue == "3")
                {
                    dt = cc2.LoadSMSReport(st_date, end_date, 0, groupid);
                }
                else if (ReportList.SelectedValue == "5")
                {
                    dt = cc2.LoadManualSMSReport(st_date, end_date);
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    dt.Columns.Add("SEND_DATE_STR") ;
                    foreach (DataRow r in dt.Rows) {
                        r["SEND_DATE_STR"] = ((DateTime)r["SEND_DATE"]).ToString("dd/MM/yyyy HH:mm:ss");

                    }

                    ReportResultView.DataSource = dt;
                    ReportResultView.DataBind();
                    NodataLabel.Visible = false;
                }
                else
                {
                    ReportResultView.DataSource = null;
                    ReportResultView.DataBind();
                    NodataLabel.Visible = true;
                }



            }
            else
            {
                errorLabel.Visible = true;
            }

            ViewState["ReportResultView"] = dt;
        }

        protected void ClosePanel_Click(object sender, EventArgs e)
        {
            grpPanel.Visible = false;
            reportPanel.Visible = false;
        }
        protected void ReportResultView_Sorting(object sender, GridViewSortEventArgs e)
        {
            SumChart.Visible = false;
            DataTable sourceTable = ViewState["ReportResultView"] as DataTable;

            SortGrid(ReportResultView, sourceTable, e.SortExpression);
            DataView view = ReportResultView.DataSource as DataView;
            ViewState["ReportResultView"] = view.ToTable();
        }
        private void SortGrid(GridView GridViewToSort, DataTable sourceTable, string SortExpression)
        {
            DataView view = new DataView(sourceTable);
            string[] sortData = ViewState["sortExpression"] == null ? (SortExpression + " " + "ASC").ToString().Trim().Split(' ') : ViewState["sortExpression"].ToString().Trim().Split(' ');
            if (SortExpression == sortData[0])
            {
                if (sortData[1] == "ASC")
                {
                    view.Sort = SortExpression + " " + "DESC";
                    this.ViewState["sortExpression"] = SortExpression + " " + "DESC";
                }
                else
                {
                    view.Sort = SortExpression + " " + "ASC";
                    this.ViewState["sortExpression"] = SortExpression + " " + "ASC";
                }
            }
            else
            {
                view.Sort = SortExpression + " " + "ASC";
                this.ViewState["sortExpression"] = SortExpression + " " + "ASC";
            }
            GridViewToSort.DataSource = view;
            GridViewToSort.DataBind();
        }
        protected void ReportResultView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SumChart.Visible = false;
           ReportResultView.DataSource= ViewState["ReportResultView"] as DataTable;
            ReportResultView.PageIndex = e.NewPageIndex;
            ReportResultView.DataBind();
          // ViewState["ReportResultView"] = ReportResultView.DataSource as DataTable;
        }
        protected void ReportResultView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int pindex = ReportResultView.PageIndex * ReportResultView.PageSize;
            if (e.CommandName=="resend")
            {
                DataTable dt = ViewState["ReportResultView"] as DataTable;
                int index = Convert.ToInt32(e.CommandArgument.ToString()) + pindex;
                string mobile = dt.Rows[index]["mobile"].ToString();
                string msg = dt.Rows[index]["MESSAGE"].ToString();
                string logid = dt.Rows[index]["LOGID"].ToString();
                SendSMS ss = new SendSMS();
                ss.RetrySend(mobile, msg, logid);
                bindreportgrid();
            }
        }

        protected void CancelBtn_Click(object sender, EventArgs e)
        {
            ReportList.ClearSelection();
            GroupList.ClearSelection();
            ReportList.Items.FindByValue("-1").Selected = true;
            GroupList.Items.FindByValue("-1").Selected = true;
            endLabel.Text = DateTime.Now.ToString("dd/MM/yyyy");
            startLabel.Text = DateTime.Now.ToString("dd/MM/yyyy");
            endCalendar.SelectedDate = DateTime.Today.Date;
            staCalendar.SelectedDate = DateTime.Today.Date;
        }

        protected void ExcelBtn_Click(object sender, ImageClickEventArgs e)
        {
            SumChart.Visible = false;
            DataTable dt = ViewState["ReportResultView"] as DataTable;
           

            ExporttoExcel(dt);
           // ExporttoCSV(dt);
            //ExportToExcel ec = new ExportToExcel();
            //ec.ExporttoExcel(dt);
        }

        private void ExporttoExcel(DataTable tbl)
        {
            using (ExcelPackage pck = new ExcelPackage())
            {
                //Create the worksheet
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("SMSREPORT");

                ws.Cells["A1"].LoadFromDataTable(tbl, true);

                using (ExcelRange rng = ws.Cells["A1"])
                {
                    rng.Style.WrapText = true;
                    rng.Style.Border.BorderAround(ExcelBorderStyle.Medium);
                }

                //Write it back to the client
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;  filename=Report_" + "SMSREPORT" + ".xlsx");
                Response.BinaryWrite(pck.GetAsByteArray());
                Response.End();
            }

        }
        private void ExporttoCSV(DataTable dtTable)
        {
           

              Response.Clear();
            Response.Charset = "windows-874";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding(874);
            Response.ContentType = "text/csv";

            Response.AddHeader("content-disposition", "attachment;filename=Report_SMSREPORT.csv");


            StringBuilder sbldr = new StringBuilder();

            if (dtTable.Columns.Count != 0)
            {

                foreach (DataColumn col in dtTable.Columns)
                {

                    
                    Response.Write(col.ColumnName + ',');

                }

               // sbldr.Append("\r\n");

                foreach (DataRow row in dtTable.Rows)
                {

                    foreach (DataColumn column in dtTable.Columns)
                    {

                       
                        Response.Write( row[column].ToString() + ',');
                    }

                    Response.Write(Environment.NewLine);


                }

            }
          

            

          

            Response.End();


        }

     
        protected void EditLB_Click(object sender, EventArgs e)
        {
            LinkButton lb = (LinkButton)sender;
            GridViewRow thisGridViewRow = (GridViewRow)lb.Parent.Parent;
            int row = thisGridViewRow.RowIndex;
           
            LinkButton savelb = thisGridViewRow.FindControl("SaveLB") as LinkButton;
            LinkButton cancellb = thisGridViewRow.FindControl("CancelLB") as LinkButton;
            Label MOBILELabel = thisGridViewRow.FindControl("MOBILELabel") as Label;
            TextBox MOBILETextBox = thisGridViewRow.FindControl("MOBILETextBox") as TextBox;
            lb.Visible = false;
            savelb.Visible = true;
            cancellb.Visible = true;
            MOBILELabel.Visible = false;
            MOBILETextBox.Visible = true; 
            SumChart.Visible = false;
        }
        protected void CancelLB_Click(object sender, EventArgs e)
        {
            LinkButton lb = (LinkButton)sender;
            GridViewRow thisGridViewRow = (GridViewRow)lb.Parent.Parent;
            int row = thisGridViewRow.RowIndex;
            LinkButton savelb = thisGridViewRow.FindControl("SaveLB") as LinkButton;
            LinkButton editlb = thisGridViewRow.FindControl("EditLB") as LinkButton;
            Label MOBILELabel = thisGridViewRow.FindControl("MOBILELabel") as Label;
            TextBox MOBILETextBox = thisGridViewRow.FindControl("MOBILETextBox") as TextBox;
            StaBtn.Enabled = true;
            EndBtn.Enabled = true;
            GroupList.Enabled = true;
            lb.Visible = false;
            savelb.Visible = false;
            editlb.Visible = true;
            MOBILELabel.Visible = true;
            MOBILETextBox.Visible = false;
            SumChart.Visible = false;
        }
        protected void SaveLB_Click(object sender, EventArgs e)
        {
            LinkButton lb = (LinkButton)sender;
            GridViewRow thisGridViewRow = (GridViewRow)lb.Parent.Parent;
            int row = thisGridViewRow.RowIndex;
            LinkButton cancellb = thisGridViewRow.FindControl("CancelLB") as LinkButton;
            LinkButton editlb = thisGridViewRow.FindControl("EditLB") as LinkButton;
            Label MOBILELabel = thisGridViewRow.FindControl("MOBILELabel") as Label;
            TextBox MOBILETextBox = thisGridViewRow.FindControl("MOBILETextBox") as TextBox;
            Label LOGIDLabel = thisGridViewRow.FindControl("LOGIDLabel") as Label;
            lb.Visible = false;
            cancellb.Visible = false;
            editlb.Visible = true;
            MOBILELabel.Visible = true;
            MOBILETextBox.Visible = false;
            SumChart.Visible = false;
            string logid = LOGIDLabel.Text;
            string mobile = MOBILETextBox.Text;
            OracleQuery2 cc2 = new OracleQuery2();
            cc2.UpdateLogMobile(mobile, logid);


            bindreportgrid();

        }

        protected void ReportList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ReportList.SelectedValue == "4")
            {
                StaBtn.Enabled = false;
                EndBtn.Enabled = false;
                GroupList.Enabled = false;
            }
            else if (ReportList.SelectedValue == "5")
            {
                GroupList.Enabled = false;
            }
            else 
            {
                StaBtn.Enabled = true;
                EndBtn.Enabled = true;
                GroupList.Enabled = true;
            }
        }

 
      

     
       
    }
}