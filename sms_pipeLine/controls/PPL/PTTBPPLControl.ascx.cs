using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace sms_pipeLine.controls.PPL
{
    public partial class PTTBPPLControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                Grid_ppl.DataSource = ViewState["CurrentPTTBPPL"] as DataTable;
                Grid_ppl.DataBind();
            }
        }
        public void BindPPLDetail(string group_id, int level)
        {
            groupLabel.Text = group_id;
            LevelLabel.Text = level.ToString();
            OracleQuery2 cc2 = new OracleQuery2();
            SQLServerQuery ss = new SQLServerQuery();
            DataTable dt = cc2.LoadPTTBPPL(group_id);
            string result = "";
            foreach (DataRow r in dt.Rows)
            {

                result = result + r["CODE"].ToString() + ",";

            }
            
            result = result.TrimEnd(',');
            result = result.TrimStart(',');
            DataTable dt_pis = ss.LoadPosecodeINPIS(result);
            DataTable dtppl = new DataTable();
            dtppl.Columns.Add("POSCODE");
            dtppl.Columns.Add("NAME");
            dtppl.Columns.Add("POSITION");
            dtppl.Columns.Add("COMPANY");
            dtppl.Columns.Add("MOBILE");
            dtppl.Columns.Add("UNITCODE");
            dtppl.Columns.Add("CODE");
            dtppl.Columns.Add("GROUP_KEY");
            if (dt != null && dt_pis != null && dt.Rows.Count > 0 && dt_pis.Rows.Count > 0)
            {
                var results = from table1 in dt.AsEnumerable()
                              join table2 in dt_pis.AsEnumerable() on table1["CODE"].ToString() equals table2["P_ID"].ToString()
                              select new
                              {
                                  POSCODE = table1["POSCODE"].ToString(),
                                  FULLNAMETH = table2["FULLNAMETH"].ToString(),
                                  POSNAME = table2["POSNAME"].ToString(),
                                  unitname = table2["unitname"].ToString(),
                                  unitcode = table1["UNITCODE"].ToString(),
                                  code = table1["CODE"].ToString(),
                                  mobile = !string.IsNullOrEmpty(table2["mobile"].ToString()) ? table2["mobile"].ToString() : table1["mobile"].ToString()
                              };

                foreach (var item in results)
                {
                    dtppl.Rows.Add(item.POSCODE, item.FULLNAMETH, item.POSNAME, item.unitname, item.mobile,item.unitcode,item.code, 100);
                }
            }




            ViewState["CurrentPTTBPPL"] = dtppl;
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
        protected void Grid_ppl_Sorting(object sender, GridViewSortEventArgs e)
        {

            DataTable sourceTable = Grid_ppl.DataSource as DataTable;

            SortGrid(Grid_ppl, sourceTable, e.SortExpression);
            DataView view = Grid_ppl.DataSource as DataView;
            ViewState["CurrentPTTBPPL"] = view.ToTable();
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
            string poscode = poscodeLabel.Text;
            string loginName = Session["ID"].ToString();
            string MOBILE = TelLabel.Text;
            string unitcode ="";
            string code ="";

            OracleQuery cc = new OracleQuery();
            OracleQuery2 cc2 = new OracleQuery2();
            string result = cc2.InsertPTTBPPL(poscode, MOBILE, loginName, unitcode, code);
            if (result != "0")
                cc2.UpdatePTTBPPL(code, MOBILE, loginName);
            grpPanel.Visible = false;
            AddPPLPanel.Visible = false;
            BindPPLDetail(group_id, level);
        }
        protected void Grid_ppl_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            Grid_ppl.PageIndex = e.NewPageIndex;
            Grid_ppl.DataBind();
            ViewState["CurrentPTTBPPL"] = Grid_ppl.DataSource as DataTable;
        } 
        protected void Grid_ppl_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int pindex = Grid_ppl.PageIndex * Grid_ppl.PageSize;

            if (e.CommandName == "DelPPL")
            {

                DataTable dtgroup = ViewState["CurrentPTTBGrp"] as DataTable;
                DataTable dtppl = ViewState["CurrentPTTBPPL"] as DataTable;
                int index = Convert.ToInt32(e.CommandArgument.ToString()) + pindex;
                string group_id = groupLabel.Text;
                int level = Convert.ToInt32(LevelLabel.Text);
                // string text = TreeView1.SelectedNode.Text;
                if (group_id == "-1") return;
                string depart = group_id.Substring(0, 3);
                OracleQuery cc = new OracleQuery();
                OracleQuery2 cc2 = new OracleQuery2();
                string d = dtppl.Rows[index]["GROUP_KEY"].ToString();
                string person_id = dtppl.Rows[index]["CODE"].ToString();
                cc2.DeletePTTBPPL(group_id, d, person_id);

                //TreeView1.Nodes.Clear();
                //bindTree();
                BindPPLDetail(group_id, level);

            }

            if (e.CommandName == "EditPPL")
            {
                AddPPLPanel.Visible = true;
                grpPanel.Visible = true;
                DataTable dtppl = ViewState["CurrentPTTBPPL"] as DataTable;
                int index = Convert.ToInt32(e.CommandArgument.ToString()) + +pindex;
                string EmployeeID = dtppl.Rows[index]["POSCODE"].ToString();
                string Name = dtppl.Rows[index]["NAME"].ToString();
                string posname = dtppl.Rows[index]["POSITION"].ToString();
                string unitname = dtppl.Rows[index]["COMPANY"].ToString();
                string MOBILE = dtppl.Rows[index]["MOBILE"].ToString();
                poscodeLabel.Text = EmployeeID;
                NameLabel.Text = Name;
                posnameLabel.Text = posname;
                unitnameLabel.Text = unitname;
                TelLabel.Text = MOBILE;
            }

        }

        public string CountPPL()
        {
             DataTable dt = new DataTable();
            if( ViewState["CurrentPTTBPPL"]!= null)
                dt =  ViewState["CurrentPTTBPPL"] as DataTable;
            
            string cnt = dt==null? "-":dt.Rows.Count.ToString();
            return cnt;
        }
    }
}