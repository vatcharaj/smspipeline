﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace sms_pipeLine.controls.PPL
{
    public partial class PTTPPLControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                Grid_ppl.DataSource = ViewState["CurrentPTTPPL"] as DataTable;
                Grid_ppl.DataBind();
            }
        }
        public void BindPPLDetail(string group_id, int level)
        {
            groupLabel.Text = group_id;
            LevelLabel.Text = level.ToString();
            OracleQuery2 cc2 = new OracleQuery2();
            SQLServerQuery ss = new SQLServerQuery();
            DataTable dt = cc2.LoadPTTPPL(group_id);
            string result = "";
            foreach (DataRow r in dt.Rows)
            {

                result = result + r["EMPLOYEE_ID"].ToString() + ",";

            }
            result = result.TrimEnd(',');
            
            DataTable dt_pis = ss.LoadINPIS("", result);
            DataTable dtppl = new DataTable();
            dtppl.Columns.Add("EMPLOYEE_ID");
            dtppl.Columns.Add("NAME");
            dtppl.Columns.Add("POSITION");
            dtppl.Columns.Add("COMPANY");
            dtppl.Columns.Add("MOBILE");
            dtppl.Columns.Add("GROUP_KEY");
            if (dt != null && dt_pis != null && dt.Rows.Count > 0 && dt_pis.Rows.Count > 0)
            {
                var results = from table1 in dt.AsEnumerable()
                              join table2 in dt_pis.AsEnumerable() on table1["EMPLOYEE_ID"].ToString() equals table2["P_ID"].ToString()
                              select new
                              {
                                  EMPLOYEE_ID = table1["EMPLOYEE_ID"].ToString(),
                                  FULLNAMETH = table2["FULLNAMETH"].ToString(),
                                  POSNAME = table2["POSNAME"].ToString(),
                                  unitname = table2["unitname"].ToString(),
                                  mobile = !string.IsNullOrEmpty(table2["mobile"].ToString()) ? table2["mobile"].ToString() : table1["mobile"].ToString()
                              };

                foreach (var item in results)
                {
                    dtppl.Rows.Add(item.EMPLOYEE_ID, item.FULLNAMETH, item.POSNAME, item.unitname, item.mobile,200);
                }
            }




            ViewState["CurrentPTTPPL"] = dtppl;
            if (dtppl.Rows.Count > 0)
            {

                Grid_ppl.DataSource = dtppl;
                Grid_ppl.DataBind();
                PPL_PANEL.Visible = true;
            }
            else
            {
                Grid_ppl.DataSource = null;
                Grid_ppl.DataBind();
                PPL_PANEL.Visible = false;
            }
        }

        protected void Grid_ppl_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            Grid_ppl.PageIndex = e.NewPageIndex;
            Grid_ppl.DataBind();
            ViewState["CurrentPTTPPL"] = Grid_ppl.DataSource as DataTable;
        } 
        protected void Grid_ppl_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int pindex = Grid_ppl.PageIndex * Grid_ppl.PageSize;
            if (e.CommandName == "DelPPL")
            {

                DataTable dtgroup = ViewState["CurrentPTTGrp"] as DataTable;
                DataTable dtppl = ViewState["CurrentPTTPPL"] as DataTable;
                int index = Convert.ToInt32(e.CommandArgument.ToString()) + pindex;
                string group_id = groupLabel.Text;
                int level = Convert.ToInt32(LevelLabel.Text);
                // string text = TreeView1.SelectedNode.Text;
                if (group_id == "-1") return;
                string depart = group_id.Substring(0, 3);
                OracleQuery2 cc2 = new OracleQuery2();
                string d = dtppl.Rows[index]["GROUP_KEY"].ToString();
                string person_id = dtppl.Rows[index]["EMPLOYEE_ID"].ToString();
                cc2.DeletePTTPPL(group_id, d, person_id);

                //TreeView1.Nodes.Clear();
                //bindTree();
                BindPPLDetail(group_id, level);

            }

            if (e.CommandName == "EditPPL")
            {
                AddPPLPanel.Visible = true;
                grpPanel.Visible = true;
                DataTable dtppl = ViewState["CurrentPTTPPL"] as DataTable;
                int index = Convert.ToInt32(e.CommandArgument.ToString()) + pindex;
                string EmployeeID = dtppl.Rows[index]["EMPLOYEE_ID"].ToString();
                string Name = dtppl.Rows[index]["NAME"].ToString();
                string posname = dtppl.Rows[index]["POSITION"].ToString();
                string unitname = dtppl.Rows[index]["COMPANY"].ToString();
                string MOBILE = dtppl.Rows[index]["MOBILE"].ToString();
                EmployeeIDLabel.Text = EmployeeID;
                NameLabel.Text = Name;
                posnameLabel.Text = posname;
                unitnameLabel.Text = unitname;
                TelLabel.Text = MOBILE;
            }

        }
        protected void Grid_ppl_Sorting(object sender, GridViewSortEventArgs e)
        {

            DataTable sourceTable = Grid_ppl.DataSource as DataTable;

            SortGrid(Grid_ppl, sourceTable, e.SortExpression);
            DataView view = Grid_ppl.DataSource as DataView;
            ViewState["CurrentPTTPPL"] = view.ToTable();
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
        protected void ClosePanel_Click(object sender, EventArgs e)
        {
            grpPanel.Visible = false;
            AddPPLPanel.Visible = false;
        }
        protected void UpdatePPL_Click(object sender, EventArgs e)
        {
            string group_id = groupLabel.Text;
            int level = Convert.ToInt32(LevelLabel.Text);
            string EmployeeID = EmployeeIDLabel.Text;
            string MOBILE = TelLabel.Text;
            string longinName = Session["ID"].ToString();
            OracleQuery cc = new OracleQuery();
            OracleQuery2 cc2 = new OracleQuery2();
            string result = cc2.InsertPTTPPL(EmployeeID, MOBILE, longinName);
            if(result !="0")
                cc2.UpdatePTTPPL(EmployeeID, MOBILE, longinName);
            grpPanel.Visible = false;
            AddPPLPanel.Visible = false;
            BindPPLDetail(group_id, level);
        }

        public string CountPPL()
        {
            DataTable dt = new DataTable();
            if (ViewState["CurrentPTTPPL"] != null)
                dt = ViewState["CurrentPTTPPL"] as DataTable;

            string cnt = dt == null ? "-" : dt.Rows.Count.ToString();
            return cnt;
        }
    }
}