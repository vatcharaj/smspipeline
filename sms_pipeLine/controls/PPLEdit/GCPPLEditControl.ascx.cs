using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace sms_pipeLine.controls
{
    public partial class GCPPLEditControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (IsPostBack)
            {
                Grid_ppl.DataSource = ViewState["CurrentGCEditPPL"] as DataTable;
                Grid_ppl.DataBind();
            }
        }
        public void BindPPLDetail()
        {
            SchPPL.Text = "";
            OracleQuery2 cc2 = new OracleQuery2();
            DataTable dtppl = cc2.LoadGCPPL();
            ViewState["CurrentGCEditPPL"] = dtppl;
            ViewState["ALLPPL"] = dtppl;
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
            ViewState["CurrentGCEditPPL"] = Grid_ppl.DataSource as DataTable;
        }

        protected void Grid_ppl_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int pindex = Grid_ppl.PageIndex * Grid_ppl.PageSize;

            if (e.CommandName == "DelPPL")
            {
                DataTable dtppl = ViewState["CurrentGCEditPPL"] as DataTable;
                int index = Convert.ToInt32(e.CommandArgument.ToString()) + pindex;
                OracleQuery2 cc2 = new OracleQuery2();
                string d = dtppl.Rows[index]["GROUP_KEY"].ToString();
                string person_id = dtppl.Rows[index]["EMPLOYEE_id"].ToString();

                string company_id = dtppl.Rows[index]["COMPANY_id"].ToString();
                cc2.DeleteGCPPLFromAllGroup(person_id, company_id);
                BindPPLDetail();


            }
            if (e.CommandName == "EditPPL")
            {
                AddPPLPanel.Visible = true;
                grpPanel.Visible = true;
                GroupIDINLabel.Text = "";
                DataTable dtppl = ViewState["CurrentGCEditPPL"] as DataTable;
                int index = Convert.ToInt32(e.CommandArgument.ToString()) + pindex;
                string EmployeeID = dtppl.Rows[index]["EMPLOYEE_ID"].ToString();
                string Name = dtppl.Rows[index]["NAME"].ToString();
                string posname = dtppl.Rows[index]["POSITION"].ToString();
                string unitname = dtppl.Rows[index]["COMPANY"].ToString();
                string MOBILE = dtppl.Rows[index]["MOBILE"].ToString();
                string company_id = dtppl.Rows[index]["company_id"].ToString();
                SetGroup(EmployeeID, company_id);
                EmployeeIDLabel.Text = EmployeeID;
                NameLabel.Text = Name;
                posnameLabel.Text = posname;
               // unitnameLabel.Text = unitname;
                companyLabel.Text = company_id;
                TelLabel.Text = MOBILE;
                OracleQuery2 cc2 = new OracleQuery2();
                DataTable dt = cc2.LoadAllGCUnit();
                unitnameList.DataSource = dt;
                unitnameList.DataTextField = "COMPANY_NAME";
                unitnameList.DataValueField = "COMPANY_ID";
                unitnameList.DataBind();
                unitnameList.Items.FindByValue(company_id).Selected = true;
                resultppl.Visible = true;
                SavePPL.Enabled = true;
                grpPanel.Visible = true;
                AddPPLPanel.Visible = true;
                HeadModal.Visible = false;
                UpdatePPL.Visible = true;
                SavePPL.Visible = false;
                NoResult.Visible = false;

            }

        }
        protected void SchBox_Changed(object sender, EventArgs e)
        {
            string sch = SchBox.Text;
            try
            {
                OracleQuery2 cc2 = new OracleQuery2();
                DataTable dt = cc2.LoadToAddGCPPL(sch);

                if (dt.Rows.Count > 0)
                {
                    string EmployeeID = dt.Rows[0]["EMPLOYEE_ID"].ToString();
                    string Name = dt.Rows[0]["NAME"].ToString();
                    string posname = dt.Rows[0]["POSITION"].ToString();
                    string unitname = dt.Rows[0]["COMPANY"].ToString();
                    string MOBILE = dt.Rows[0]["MOBILE"].ToString();
                    string company_id = dt.Rows[0]["company_id"].ToString();
                   
                    TelLabel.Text = MOBILE;
                    SetGroup(EmployeeID, company_id);
                    EmployeeIDLabel.Text = EmployeeID;
                    NameLabel.Text = Name;
                    posnameLabel.Text = posname;
                  //  unitnameLabel.Text = unitname;
                    companyLabel.Text = company_id;
                    resultppl.Visible = true;
                    SavePPL.Enabled = true;
                    NoResult.Visible = false;

                }
                else
                {
                    string EmployeeID = "";
                    string Name = "";
                    string posname = "";
                    string unitname = "";
                    string company_id = "";
                    string MOBILE = "";
                    TelLabel.Text = MOBILE;
                    EmployeeIDLabel.Text = EmployeeID;
                    NameLabel.Text = Name;
                    posnameLabel.Text = posname;
                   // unitnameLabel.Text = unitname;
                    companyLabel.Text = company_id;
                    SavePPL.Enabled = false;
                    NoResult.Visible = true;
                    resultppl.Visible = false;
                    errorlabel.Text = "ไม่พบข้อมูล";
                }
            }
            catch {
                string EmployeeID = "";
                string Name = "";
                string posname = "";
                string unitname = "";
                string company_id = "";
                string MOBILE = "";
                TelLabel.Text = MOBILE;
                EmployeeIDLabel.Text = EmployeeID;
                NameLabel.Text = Name;
                posnameLabel.Text = posname;
               // unitnameLabel.Text = unitname;
                companyLabel.Text = company_id;
                SavePPL.Enabled = false;
                NoResult.Visible = true;
                resultppl.Visible = false;
                errorlabel.Text = "ไม่พบข้อมูล";
            }
        }
        private void SetGroup(string EmployeeID, string company_id)
        {
            OracleQuery2 cc2 = new OracleQuery2();
            DataTable dt = cc2.LoadGCGroup(EmployeeID, company_id);
            cl.DataSource = dt;

            cl.DataTextField = "GROUP_NAME";
            cl.DataValueField = "group_id";
            cl.DataBind();
            string select_group = "";
            foreach (DataRow r in dt.Rows)
            {
                string Group_id_in = r["Group_id_in"].ToString();
                if (!String.IsNullOrEmpty(Group_id_in))
                {
                    cl.Items.FindByValue(Group_id_in).Selected = true;
                    select_group += Group_id_in + " ";
                }
            }

            GroupIDINLabel.Text = select_group;
        }
        protected void Grid_ppl_Sorting(object sender, GridViewSortEventArgs e)
        {

            DataTable sourceTable = Grid_ppl.DataSource as DataTable;

            SortGrid(Grid_ppl, sourceTable, e.SortExpression);
            DataView view = Grid_ppl.DataSource as DataView;
            ViewState["CurrentGCEditPPL"] = view.ToTable();
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
        protected void SchPPL_TextChanged(object sender, EventArgs e)
        {

            string schtext = SchPPL.Text.Trim();
            try
            {
                OracleQuery cc = new OracleQuery();
                DataTable dt = new DataTable();
                dt = ViewState["ALLPPL"] as DataTable;
                dt.DefaultView.RowFilter = "[keyword] LIKE '%" + schtext + "%'";
                DataTable dtOutput = dt.DefaultView.ToTable();
                ViewState["CurrentGCEditPPL"] = dtOutput;
                Grid_ppl.DataSource = dtOutput;
                Grid_ppl.DataBind();
            }
            catch { 
               DataTable dtOutput = ViewState["ALLPPL"] as DataTable;
                ViewState["CurrentGCEditPPL"] = dtOutput;
                Grid_ppl.DataSource = dtOutput;
                Grid_ppl.DataBind();
            }
            

        }
        protected void ClosePanel_Click(object sender, EventArgs e)
        {
            grpPanel.Visible = false;
            AddPPLPanel.Visible = false;
        }
        protected void UpdatePPL_Click(object sender, EventArgs e)
        {
            string EmployeeID = EmployeeIDLabel.Text;
            string Name = NameLabel.Text;
            string posname = posnameLabel.Text;
           // string unitname = unitnameLabel.Text;
            string company_id = unitnameList.SelectedValue;
            string MOBILE = TelLabel.Text;
            string group_in = GroupIDINLabel.Text;
            OracleQuery cc = new OracleQuery();
            OracleQuery2 cc2 = new OracleQuery2();
            var s = cl.Items.Cast<ListItem>()
           .Where(item => item.Selected)
           .Aggregate("", (current, item) => current + (item.Text + ", "));
            string hh = s.TrimEnd(new[] { ',', ' ' });
            if (hh == "")
            {

                NoResult.Visible = true;
                errorlabel.Text = "กรุณาเลือกกลุ่มอย่างน้อย 1 กลุ่ม";
                return;

            }
            string longinName = Session["ID"].ToString();
            int index = cl.Items.Count;
            for (int i = 0; i < index; i++)
            {
                string id = cl.Items[i].Value;
                if (group_in.Contains(id) && !cl.Items[i].Selected)
                    cc2.DeleteGCPPL(id, "300", EmployeeID, company_id);
                else if (!group_in.Contains(id) && cl.Items[i].Selected)
                    cc2.InsertGCService(id, EmployeeID, company_id);
            }
            cc2.updateGCPPL(EmployeeID, Name, company_id, MOBILE, posname, longinName);
            grpPanel.Visible = false;
            AddPPLPanel.Visible = false;
            BindPPLDetail();
        }
        protected void SavePPL_Click(object sender, EventArgs e)
        {

          // string EmployeeID = EmployeeIDLabel.Text;
            string Name = NameLabel.Text;
            string posname = posnameLabel.Text;
           // string unitname = unitnameLabel.Text;
            string company_id = unitnameList.SelectedValue;
            string MOBILE = TelLabel.Text;
            string group_in = GroupIDINLabel.Text;
            OracleQuery cc = new OracleQuery();
            OracleQuery2 cc2 = new OracleQuery2();
            var s = cl.Items.Cast<ListItem>()
                  .Where(item => item.Selected)
                  .Aggregate("", (current, item) => current + (item.Text + ", "));
            string hh = s.TrimEnd(new[] { ',', ' ' });

            if (hh == "")
            {


                NoResult.Visible = true;
                errorlabel.Text = "กรุณาเลือกกลุ่มอย่างน้อย 1 กลุ่ม";
                return;

            }
            DataTable dt_tempChk = ViewState["ALLPPL"] as DataTable;
            //dt_tempChk.DefaultView.RowFilter = "[EMPLOYEE_ID] = '" + EmployeeID + "'";
            //DataTable dtOutput = dt_tempChk.DefaultView.ToTable();
            //if (dtOutput.Rows.Count > 0)
            //{
            //    SavePPL.Enabled = false;
            //    NoResult.Visible = true;
            //    errorlabel.Text = "มีชื่อในระบบ";
            //    resultppl.Visible = false;
            //    return;
            //}
            DataView dv = dt_tempChk.DefaultView;
             dv.Sort = "EMPLOYEE_ID desc";
           DataTable sortedDT = dv.ToTable();
           string EmployeeID = (Convert.ToDouble(sortedDT.Rows[0]["EMPLOYEE_ID"].ToString())+1).ToString();
            int index = cl.Items.Count;
            string loginName = Session["ID"].ToString();
            cc2.InsertGCPPL(EmployeeID, Name, company_id, MOBILE, posname, loginName);
            for (int i = 0; i < index; i++)
            {
                string id = cl.Items[i].Value;
                if (cl.Items[i].Selected)
                    cc2.InsertGCService(id, EmployeeID, company_id);
            }
          
            grpPanel.Visible = false;
            AddPPLPanel.Visible = false;
            BindPPLDetail();
        }
        protected void AddpplBtn_Click(object sender, EventArgs e)
        {
            grpPanel.Visible = true;
            AddPPLPanel.Visible = true;
           // HeadModal.Visible = true;
            NameLabel.Text = "";
            posnameLabel.Text = "";
            OracleQuery2 cc2 = new OracleQuery2();
            DataTable dt = cc2.LoadAllGCUnit();
            unitnameList.DataSource = dt;
            unitnameList.DataTextField = "COMPANY_NAME";
            unitnameList.DataValueField = "COMPANY_ID";
            unitnameList.DataBind();
            EditpplLabel.Visible = false;
            SchBox.Text = "";
            TelLabel.Text = "";
            resultppl.Visible = true;
            // IsAdminChk.Checked = false;
            SetGroup("1", "1");
            UpdatePPL.Visible = false;
            SavePPL.Visible = true;
            NoResult.Visible = false;
            SavePPL.Enabled = true;
        }
       

    }
}