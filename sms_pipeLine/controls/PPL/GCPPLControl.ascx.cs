using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace sms_pipeLine.controls
{
    public partial class GCPPLControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                Grid_ppl.DataSource = ViewState["CurrentGCPPL"] as DataTable;
                Grid_ppl.DataBind();
            }
        }
        public void BindPPLDetail(string group_id, int level)
        {
            groupLabel.Text = group_id;
            LevelLabel.Text = level.ToString();
            OracleQuery2 cc2 = new OracleQuery2();
            DataTable dtppl = cc2.LoadGCPPL(group_id);
            //dtppl = findPTTPerson(dtppl, group_id);
            ViewState["CurrentGCPPL"] = dtppl;
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
        private DataTable findPTTPerson(DataTable dtppl, string group_id)
        {
            OracleQuery cc = new OracleQuery();
            SQLServerQuery ss = new SQLServerQuery();
            string group = group_id.Substring(0, 1);
          //  string pisincs = cc.LoadPISinGC(group_id);
            //string head = cc.LoadHeadinGC(group_id);
         //   DataTable dt = ss.LoadINPIS("", pisincs);
            //DataTable dt2 = ss.LoadHeadINPIS("", head);
          //  dtppl = pushtodtppl(dtppl, dt);
          //  dtppl = pushtodtppl(dtppl, dt2);

            return dtppl;
        }
        private DataTable pushtodtppl(DataTable dtppl, DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {

                foreach (DataRow r in dt.Rows)
                {
                    dtppl.Rows.Add(r["P_ID"].ToString(), r["FULLNAMETH"], r["POSNAME"], "PTT", "1", r["mobile"], "1");
                }

            }
            return dtppl;
        }
        protected void Grid_ppl_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            Grid_ppl.PageIndex = e.NewPageIndex;
            Grid_ppl.DataBind();
            ViewState["CurrentGCPPL"] = Grid_ppl.DataSource as DataTable;
        } 
        protected void Grid_ppl_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int pindex = Grid_ppl.PageIndex * Grid_ppl.PageSize;
            if (e.CommandName == "DelPPL")
            {

                DataTable dtgroup = ViewState["CurrentGCGrp"] as DataTable;
                DataTable dtppl = ViewState["CurrentGCPPL"] as DataTable;
                int index = Convert.ToInt32(e.CommandArgument.ToString()) + pindex;
                string group_id = groupLabel.Text;
                int level = Convert.ToInt32(LevelLabel.Text);
                // string text = TreeView1.SelectedNode.Text;
                if (group_id == "-1") return;
                string depart = group_id.Substring(0, 3);
                OracleQuery2 cc2 = new OracleQuery2();
                string d = dtppl.Rows[index]["GROUP_KEY"].ToString();
                string person_id = dtppl.Rows[index]["EMPLOYEE_id"].ToString();
             
                string company_id = dtppl.Rows[index]["COMPANY_id"].ToString();
                cc2.DeleteGCPPL(group_id, d, person_id,  company_id);

                //TreeView1.Nodes.Clear();
                //bindTree();
                BindPPLDetail(group_id, level);

            }
            if (e.CommandName == "EditPPL")
            {
                AddPPLPanel.Visible = true;
                grpPanel.Visible = true;
                DataTable dtppl = ViewState["CurrentGCPPL"] as DataTable;
                int index = Convert.ToInt32(e.CommandArgument.ToString()) + pindex;
                string EmployeeID = dtppl.Rows[index]["EMPLOYEE_ID"].ToString();
                string Name = dtppl.Rows[index]["NAME"].ToString();
                string posname = dtppl.Rows[index]["POSITION"].ToString();
                string unitname = dtppl.Rows[index]["COMPANY"].ToString();
                string MOBILE = dtppl.Rows[index]["MOBILE"].ToString();
                string company_id = dtppl.Rows[index]["COMPANY_id"].ToString();
                EmployeeIDLabel.Text = EmployeeID;
                NameLabel.Text = Name;
                posnameLabel.Text = posname;
                unitnameLabel.Text = unitname;
                TelLabel.Text = MOBILE;
                CompanyIDLabel.Text = company_id;
            }

        }
        protected void Grid_ppl_Sorting(object sender, GridViewSortEventArgs e)
        {

            DataTable sourceTable = Grid_ppl.DataSource as DataTable;

            SortGrid(Grid_ppl, sourceTable, e.SortExpression);
            DataView view = Grid_ppl.DataSource as DataView;
            ViewState["CurrentGCPPL"] = view.ToTable();
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
            string company_id = CompanyIDLabel.Text;
            OracleQuery2 cc2 = new OracleQuery2();
            cc2.updateGCPPL(EmployeeID, NameLabel.Text, company_id, MOBILE, posnameLabel.Text, longinName);
            grpPanel.Visible = false;
            AddPPLPanel.Visible = false;
            BindPPLDetail(group_id, level);
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


        public string CountPPL()
        {
            DataTable dt = new DataTable();
            if (ViewState["CurrentGCPPL"] != null)
                dt = ViewState["CurrentGCPPL"] as DataTable;

            string cnt = dt == null ? "-" : dt.Rows.Count.ToString();
            return cnt;
        }
    }
}