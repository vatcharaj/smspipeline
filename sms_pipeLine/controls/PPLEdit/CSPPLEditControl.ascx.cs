using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace sms_pipeLine.controls
{
    public partial class CSPPLEditControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                Grid_ppl.DataSource = ViewState["CurrentCSEditPPL"] as DataTable;
                Grid_ppl.DataBind();
            }
        }
        public void BindPPLDetail()
        {
            SchPPL.Text = "";
            OracleQuery2 cc2 = new OracleQuery2();
            DataTable dtppl = cc2.LoadCSPPL();
            //  dtppl = findPTTPerson(dtppl, group_id);
            ViewState["CurrentCSEditPPL"] = dtppl;
            ViewState["ALLPPL"] = dtppl;
            if (dtppl.Rows.Count > 0)
            {
                DataView dv = dtppl.DefaultView;
                dv.Sort = "LASTUPDATE desc";
                DataTable sortedDT = dv.ToTable();
                updateLabel.Text = ((DateTime)sortedDT.Rows[0]["LASTUPDATE"]).ToString("dd/MM/yyyy HH:mm:ss");//+ " by " + sortedDT.Rows[0]["UPDATE_BY"].ToString();
                Grid_ppl.DataSource = dtppl;
                Grid_ppl.DataBind();
                PPL_PANEL.Visible = true;
            }
            else
            {
                updateLabel.Text = "-";
                Grid_ppl.DataSource = null;
                Grid_ppl.DataBind();
                PPL_PANEL.Visible = false;
            }
        }
        protected void Grid_ppl_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
          
            Grid_ppl.PageIndex = e.NewPageIndex;
            Grid_ppl.DataBind();
            ViewState["CurrentCSEditPPL"] = Grid_ppl.DataSource as DataTable;
        } 
        protected void Grid_ppl_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int pindex = Grid_ppl.PageIndex*Grid_ppl.PageSize;

            if (e.CommandName == "DelPPL")
            {
                DataTable dtppl = ViewState["CurrentCSEditPPL"] as DataTable;
                int index = Convert.ToInt32(e.CommandArgument.ToString()) + pindex;
                OracleQuery2 cc2 = new OracleQuery2();
                string d = dtppl.Rows[index]["GROUP_KEY"].ToString();
                string person_id = dtppl.Rows[index]["Person_ID"].ToString();

               // string company_id = dtppl.Rows[index]["COMPANY_id"].ToString();
                cc2.DeleteCSPPLFromAllGroup(person_id);
                BindPPLDetail();

            }

            if (e.CommandName == "EditPPL")
            {
                AddPPLPanel.Visible = true;
                grpPanel.Visible = true;
                GroupIDINLabel.Text = "";
                DataTable dtppl = ViewState["CurrentCSEditPPL"] as DataTable;
                int index = Convert.ToInt32(e.CommandArgument.ToString())+pindex;
                string EmployeeID = dtppl.Rows[index]["Person_id"].ToString();
                string Name = dtppl.Rows[index]["NAME"].ToString();
                string posname = dtppl.Rows[index]["POSITION"].ToString();
                string unitname = dtppl.Rows[index]["COMPANY"].ToString();
                string MOBILE = dtppl.Rows[index]["MOBILE"].ToString();
              //  string company_id = dtppl.Rows[index]["company_id"].ToString();
                SetGroup(EmployeeID);
                EmployeeIDLabel.Text = EmployeeID;
                NameLabel.Text = Name;
                posnameLabel.Text = posname;
                unitnameLabel.Text = unitname;
                //companyLabel.Text = company_id;
                TelLabel.Text = MOBILE;
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

        private void SetGroup(string EmployeeID)
        {
     
            OracleQuery2 cc2 = new OracleQuery2();
            DataTable dt = cc2.LoadCSGroup(EmployeeID);
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
            ViewState["CurrentCSEditPPL"] = view.ToTable();
        }
        protected void ClosePanel_Click(object sender, EventArgs e)
        {
            grpPanel.Visible = false;
            AddPPLPanel.Visible = false;
        }

        protected void AddpplBtn_Click(object sender, EventArgs e)
        {
            grpPanel.Visible = true;
            AddPPLPanel.Visible = true;
            HeadModal.Visible = true;
            EditpplLabel.Visible = false;
            SchBox.Text = "";
            TelLabel.Text = "";
            resultppl.Visible = false;
           // IsAdminChk.Checked = false;
            UpdatePPL.Visible = false;
            SavePPL.Visible = true;
            NoResult.Visible = false;
            SavePPL.Enabled = false;
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
                ViewState["CurrentCSEditPPL"] = dtOutput;
                Grid_ppl.DataSource = dtOutput;
                Grid_ppl.DataBind();
            }
            catch
            {
                DataTable dtOutput = ViewState["ALLPPL"] as DataTable;
                ViewState["CurrentCSEditPPL"] = dtOutput;
                Grid_ppl.DataSource = dtOutput;
                Grid_ppl.DataBind();
            }
            
        }
        protected void UpdatePPL_Click(object sender, EventArgs e)
        {

            string EmployeeID = EmployeeIDLabel.Text;
            string Name = NameLabel.Text;
            string posname = posnameLabel.Text;
            string unitname = unitnameLabel.Text;
            string company_id = companyLabel.Text;
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
            int index = cl.Items.Count;
            for (int i = 0; i < index; i++)
            {
                string id = cl.Items[i].Value;
                if (group_in.Contains(id) && !cl.Items[i].Selected)
                    cc2.DeleteCSPPL(id, "400", EmployeeID);
                else if (!group_in.Contains(id) && cl.Items[i].Selected)
                    cc2.InsertCSService(id, EmployeeID, company_id);
            }
            cc2.updateCSPPL(EmployeeID, MOBILE);
            grpPanel.Visible = false;
            AddPPLPanel.Visible = false;
            BindPPLDetail();
        }
        protected void SchBox_Changed(object sender, EventArgs e)
        {
            string sch = SchBox.Text;
            try
            {
                OracleQuery2 cc2 = new OracleQuery2();
                DataTable dt = cc2.LoadToAddCSPPL(sch);

                if (dt.Rows.Count > 0)
                {
                    string EmployeeID = dt.Rows[0]["Person_id"].ToString();
                    string Name = dt.Rows[0]["NAME"].ToString();
                    string posname = dt.Rows[0]["POSITION"].ToString();
                    string unitname = dt.Rows[0]["COMPANY"].ToString();
                    string MOBILE = dt.Rows[0]["MOBILE"].ToString();

                    SetGroup(EmployeeID); 
                    TelLabel.Text = MOBILE;
                    EmployeeIDLabel.Text = EmployeeID;
                    NameLabel.Text = Name;
                    posnameLabel.Text = posname;
                    unitnameLabel.Text = unitname;
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
                    string MOBILE = "";
                    TelLabel.Text = MOBILE;
                    EmployeeIDLabel.Text = EmployeeID;
                    NameLabel.Text = Name;
                    posnameLabel.Text = posname;
                    unitnameLabel.Text = unitname;
                    SavePPL.Enabled = false;
                    NoResult.Visible = true;
                    errorlabel.Text = "ไม่พบข้อมูล";
                    resultppl.Visible = false;
                }
            }
            catch {
                string EmployeeID = "";
                string Name = "";
                string posname = "";
                string unitname = "";
                string MOBILE = "";
                TelLabel.Text = MOBILE;
                EmployeeIDLabel.Text = EmployeeID;
                NameLabel.Text = Name;
                posnameLabel.Text = posname;
                unitnameLabel.Text = unitname;
                SavePPL.Enabled = false;
                NoResult.Visible = true;
                errorlabel.Text = "ไม่พบข้อมูล";
                resultppl.Visible = false;
            }

        }
        protected void SavePPL_Click(object sender, EventArgs e)
        {

            string EmployeeID = EmployeeIDLabel.Text;
            string Name = NameLabel.Text;
            string posname = posnameLabel.Text;
            string unitname = unitnameLabel.Text;
            string company_id = companyLabel.Text;
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
           dt_tempChk.DefaultView.RowFilter = "[Person_id] = '" + EmployeeID + "'";
           DataTable dtOutput = dt_tempChk.DefaultView.ToTable();
           if (dtOutput.Rows.Count > 0)
           {
               SavePPL.Enabled = false;
               NoResult.Visible = true;
               errorlabel.Text = "มีชื่อในระบบ";
               resultppl.Visible = false;
               return;
           }
           int index = cl.Items.Count;
           for (int i = 0; i < index; i++)
           {
               string id = cl.Items[i].Value;
               if ( cl.Items[i].Selected)
                   cc2.InsertCSService(id, EmployeeID, company_id);
           }
           cc2.updateCSPPL(EmployeeID, MOBILE);

           grpPanel.Visible = false;
           AddPPLPanel.Visible = false;
           BindPPLDetail();
        }
      
    }
}