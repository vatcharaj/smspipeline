using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace sms_pipeLine.controls
{
    public partial class GCControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                Grid_group.DataSource = ViewState["CurrentGCGrp"] as DataTable;
                Grid_group.DataBind();
            }
        }
        public void BindGroupDetail(string group_id, int level)
        {

            OracleQuery2 cc2 = new OracleQuery2();
            DataTable dtgroup = cc2.LoadGroup(group_id);


            ViewState["CurrentGCGrp"] = dtgroup;
            if (dtgroup.Rows.Count > 0)
            {
                Grid_group.DataSource = dtgroup;
                Grid_group.DataBind();
                GROUP_PANEL.Visible = true;
            }
            else
            {
                Grid_group.DataSource = null;
                Grid_group.DataBind();
                GROUP_PANEL.Visible = false;
            }



        }
        protected void Grid_group_Sorting(object sender, GridViewSortEventArgs e)
        {

            DataTable sourceTable = Grid_group.DataSource as DataTable;

            SortGrid(Grid_group, sourceTable, e.SortExpression);

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
    }
}