using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace sms_pipeLine
{
    public partial class SMSTemplate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int is_admin = Convert.ToInt16(Session["ADMIN"]);
            if (is_admin == 0)
                Response.Redirect("~/Default.aspx");

            BindGrid();

        }

        private void BindGrid()
        {
            OracleQuery2 cc2 = new OracleQuery2();
            DataTable dt = cc2.LoadAllTempl();
            TemplateGridView.DataSource = dt;
            TemplateGridView.DataBind();
            

        }
        protected void TemplateGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DelTempl")
            {
                int index = Convert.ToInt32(e.CommandArgument.ToString());
                DataTable dtppl = TemplateGridView.DataSource as DataTable;
                string id = dtppl.Rows[index]["ID"].ToString();
                OracleQuery2 cc2 = new OracleQuery2();
                cc2.DeleteGTempl(id);
                BindGrid();
            
            }
            else if (e.CommandName == "EditTempl")
            {
                int index = Convert.ToInt32(e.CommandArgument.ToString());
                DataTable dtppl = TemplateGridView.DataSource as DataTable; 
                string id = dtppl.Rows[index]["ID"].ToString();
                string text = dtppl.Rows[index]["MESSAGE"].ToString();
                TemplID.Text = id;
                Update.Visible = true;
                Save.Visible = false;
                TextMessage.Text = text;
                grpPanel.Visible = true;
                AddPanel.Visible = true;
            
            }
        
        }
        protected void TemplateGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable sourceTable = TemplateGridView.DataSource as DataTable;

            SortGrid(TemplateGridView, sourceTable, e.SortExpression);
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
        protected void AddBtn_Click(object sender, EventArgs e)
        {
            Update.Visible = false;
            Save.Visible = true;
            TextMessage.Text = "";
            grpPanel.Visible = true;
            AddPanel.Visible = true;
        }
        protected void ClosePanel_Click(object sender, EventArgs e)
        {
            grpPanel.Visible = false;
            AddPanel.Visible = false;
        }
        protected void Update_Click(object sender, EventArgs e)
        {
            OracleQuery2 cc2 = new OracleQuery2();
            string text = TextMessage.Text;
            string id= TemplID.Text;
            string loginName = Session["ID"].ToString();
            cc2.UpdateTempl(text, id, loginName);
            BindGrid();
            Update.Visible = true;
            Save.Visible =false; 
            grpPanel.Visible = false;
            AddPanel.Visible = false;
        }
        protected void Save_Click(object sender, EventArgs e)
        {
            OracleQuery2 cc2 = new OracleQuery2();
            string text = TextMessage.Text;
           string loginName = Session["ID"].ToString();
            cc2.insertTempl(text, loginName);
            BindGrid();
            grpPanel.Visible = false;
            AddPanel.Visible = false;
        }

        protected void TextMessage_TextChanged(object sender, EventArgs e)
        {
            if (TextMessage.Text.Length == 0 || TextMessage.Text.Trim() == "")
                Save.Enabled = Update.Enabled = false;
            else
                Save.Enabled = Update.Enabled = true;

            if (TextMessage.Text.Length > 320)
                TextMessage.Text = TextMessage.Text.Substring(0, 320);
            TextCount.Text = TextMessage.Text.Length.ToString();

        }
    }
}