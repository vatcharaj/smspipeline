using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace sms_pipeLine.controls.PPLEdit
{
    public partial class PTTBPPLEditControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                Grid_ppl.DataSource = ViewState["CurrentPTTBEditPPL"] as DataTable;
                Grid_ppl.DataBind();
            }
        }
        protected string GetImageUrl(string input,string updateby)
        {
            if (input.Equals("1"))
            {
                if (updateby.Equals("init"))
                return "~/Content/img/new_icon.gif";
                else
                    return "~/Content/img/edited.jpg";
            }
            else
            {
                return "~/Content/img/old_icon.gif";
            }
        }
        public void BindPPLDetail()
        {
            SchPPL.Text = "";
            OracleQuery2 cc2 = new OracleQuery2();
            SQLServerQuery ss = new SQLServerQuery();
            DataTable dt = cc2.LoadPTTBPPL();
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
            dtppl.Columns.Add("UPDATE_BY");
            dtppl.Columns.Add("LASTUPDATE",typeof(DateTime));
            dtppl.Columns.Add("UNITCODE");
            dtppl.Columns.Add("CODE");
            dtppl.Columns.Add("Keyword");
            dtppl.Columns.Add("GROUP_KEY"); 
            dtppl.Columns.Add("MOBILE_FM");
            dtppl.Columns.Add("STATUS");
           
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
                                  lastupdate = table1["LASTUPDATE"].ToString(),
                                  updateby = table1["UPDATE_BY"].ToString(),
                                  unitcode = table1["UNITCODE"].ToString(),
                                  code = table2["P_ID"].ToString(),
                                  keyword = table2["P_ID"].ToString() + " " + table2["FULLNAMETH"].ToString() + " " + table2["POSNAME"].ToString() + " " + table2["unitname"].ToString(),
                                  mobile = !string.IsNullOrEmpty(table2["mobile"].ToString()) ? table2["mobile"].ToString() : table1["mobile"].ToString(),
                                  MOBILE_FM=!string.IsNullOrEmpty(table2["mobile"].ToString())?1:0,
                                  STATUS = table1["STATUS"].ToString(),
                                 
                              };

                foreach (var item in results)
                {
                    dtppl.Rows.Add(item.POSCODE, item.FULLNAMETH, item.POSNAME, item.unitname, item.mobile,item.updateby, item.lastupdate,item.unitcode,item.code,item.keyword, 200, item.MOBILE_FM,item.STATUS);
                }
            }
            //  dtppl = findPTTPerson(dtppl, group_id);
            ViewState["CurrentPTTBEditPPL"] = dtppl;
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
            ViewState["CurrentPTTBEditPPL"] = Grid_ppl.DataSource as DataTable;
        } 



        protected void Grid_ppl_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int pindex = Grid_ppl.PageIndex * Grid_ppl.PageSize;

            if (e.CommandName == "DelPPL" )
            {
                
                DataTable dtppl = ViewState["CurrentPTTBEditPPL"] as DataTable;
                int index = Convert.ToInt32(e.CommandArgument.ToString()) + pindex;
                OracleQuery2 cc2 = new OracleQuery2();
                string d = dtppl.Rows[index]["GROUP_KEY"].ToString();
                string code = dtppl.Rows[index]["code"].ToString();

                // string company_id = dtppl.Rows[index]["COMPANY_id"].ToString();
                cc2.DeletePTTBPPLFromAllGroup(code);
                BindPPLDetail();

            }

            if (e.CommandName == "EditPPL")
            {
                AddPPLPanel.Visible = true;
                grpPanel.Visible = true;
                GroupIDINLabel.Text = "";
                DataTable dtppl = ViewState["CurrentPTTBEditPPL"] as DataTable;
                int index = Convert.ToInt32(e.CommandArgument.ToString()) + pindex;
                string poscode = dtppl.Rows[index]["poscode"].ToString();
                string code = dtppl.Rows[index]["code"].ToString();
                string Name = dtppl.Rows[index]["NAME"].ToString();
                string posname = dtppl.Rows[index]["POSITION"].ToString();
                string unitname = dtppl.Rows[index]["COMPANY"].ToString();
                string UNITCODE = dtppl.Rows[index]["UNITCODE"].ToString();
                string MOBILE = dtppl.Rows[index]["MOBILE"].ToString();
                string MOBILE_FM = dtppl.Rows[index]["MOBILE_FM"].ToString();
                //  string company_id = dtppl.Rows[index]["company_id"].ToString();
                SetGroup(code, UNITCODE);
                poscodeLabel.Text = poscode;
                NameLabel.Text = Name;
                posnameLabel.Text = posname;
                unitnameLabel.Text = unitname;
                codeLabel.Text = code;
                unitcodeLabel.Text = UNITCODE;
                //companyLabel.Text = company_id;
                TelLabel.Text = MOBILE;
                TelLabel.Enabled = MOBILE_FM == "0";
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
        private void SetGroup(string code,string unitcode)
        {

            OracleQuery2 cc2 = new OracleQuery2();
            DataTable dt = cc2.LoadPTTBGroup(code, unitcode);
            cl.DataSource = dt;

            cl.DataTextField = "GROUP_NAME";
            cl.DataValueField = "group_id";
            cl.DataBind();
            string select_group = "";
            string dfg_temp = dt.Rows[0]["GROUP_DEFAULT"].ToString();
            foreach (DataRow r in dt.Rows)
            {
                string Group_id_in = r["Group_id_in"].ToString();
                if (!String.IsNullOrEmpty(Group_id_in))
                {
                    cl.Items.FindByValue(Group_id_in).Selected = true;
                    select_group += Group_id_in + " ";
                }
            }
            if (!string.IsNullOrEmpty(dfg_temp))
            {
                string[] dfg = dfg_temp.Split(',');
                for (int i = 0; i < dfg.Count(); i++)
                {
                    if (cl.Items.FindByValue(dfg[i]) != null)
                        cl.Items.FindByValue(dfg[i]).Attributes.Add("Style", "color:Silver");

                }
            }
            GroupIDINLabel.Text = select_group;

        }
        protected void Grid_ppl_Sorting(object sender, GridViewSortEventArgs e)
        {

            DataTable sourceTable = Grid_ppl.DataSource as DataTable;

            SortGrid(Grid_ppl, sourceTable, e.SortExpression);
           DataView view= Grid_ppl.DataSource as  DataView;
           ViewState["CurrentPTTBEditPPL"] = view.ToTable();
        }
        protected void ClosePanel_Click(object sender, EventArgs e)
        {
            grpPanel.Visible = false;
            AddPPLPanel.Visible = false;
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
                ViewState["CurrentPTTBEditPPL"] = dtOutput;
                Grid_ppl.DataSource = dtOutput;
                Grid_ppl.DataBind();
            }
            catch
            {
                DataTable dtOutput = ViewState["ALLPPL"] as DataTable;
                ViewState["CurrentPTTBEditPPL"] = dtOutput;
                Grid_ppl.DataSource = dtOutput;
                Grid_ppl.DataBind();
            }
            
        }
        protected void UpdatePPL_Click(object sender, EventArgs e)
        {

            string poscode = poscodeLabel.Text;
            string Name = NameLabel.Text;
            string posname = posnameLabel.Text;
            string unitname = unitnameLabel.Text;
            string company_id = companyLabel.Text;
            string MOBILE = TelLabel.Text;
            string group_in = GroupIDINLabel.Text;
            string unicode = unitcodeLabel.Text;
            string code = codeLabel.Text;
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
            string loginName = Session["ID"].ToString();
            for (int i = 0; i < index; i++)
            {
                string id = cl.Items[i].Value;
                if (group_in.Contains(id) && !cl.Items[i].Selected)
                    cc2.DeletePTTBPPL(id, "100", code);
                else if (!group_in.Contains(id) && cl.Items[i].Selected)
                    cc2.InsertPTTBService(id, code);
            }
            string result = cc2.InsertPTTBPPL(poscode, MOBILE, loginName, unicode,code);
            if (result != "0")
                cc2.UpdatePTTBPPL(code, MOBILE,loginName);
            grpPanel.Visible = false;
            AddPPLPanel.Visible = false;
            BindPPLDetail();
        

        }
        protected void SchBox_Changed(object sender, EventArgs e)
        {
            string sch = SchBox.Text;
            try
            {
                SQLServerQuery ss = new SQLServerQuery();
                string result = "";
                DataTable dt = ss.LoadPosecodeINPIS(sch, result);
                if (dt.Rows.Count > 0)
                {
                    string POSCODE = dt.Rows[0]["POSCODE"].ToString();
                    string CODE = dt.Rows[0]["P_ID"].ToString();
                    string Name = dt.Rows[0]["FULLNAMETH"].ToString();
                    string posname = dt.Rows[0]["POSNAME"].ToString();
                    string unitname = dt.Rows[0]["unitname"].ToString();
                    string unitcode = dt.Rows[0]["unitcode"].ToString();
                    string MOBILE = dt.Rows[0]["mobile"].ToString();
                    SetGroup(CODE, unitcode);
                    poscodeLabel.Text = POSCODE;
                    NameLabel.Text = Name;
                    posnameLabel.Text = posname;
                    unitnameLabel.Text = unitname;
                    TelLabel.Text = MOBILE;
                    unitcodeLabel.Text = unitcode;
                    codeLabel.Text = CODE;
                    resultppl.Visible = true;
                    SavePPL.Enabled = true;
                    NoResult.Visible = false;

                }
                else
                {
                    string POSCODE = "";
                    string Name = "";
                    string posname = "";
                    string unitname = "";
                    string MOBILE = "";
                    string CODE = "";
                        string unitcode ="";
                    poscodeLabel.Text = POSCODE;
                    NameLabel.Text = Name;
                    posnameLabel.Text = posname;
                    unitnameLabel.Text = unitname;
                    TelLabel.Text = MOBILE;
                    unitcodeLabel.Text = unitcode;
                    codeLabel.Text = CODE;
                    SavePPL.Enabled = false;
                    NoResult.Visible = true;
                    errorlabel.Text = "ไม่พบข้อมูล";
                    resultppl.Visible = false;

                }
            }
            catch {
                string POSCODE = "";
                string Name = "";
                string posname = "";
                string unitname = "";
                string MOBILE = "";
                string CODE = "";
                string unitcode = "";
                TelLabel.Text = MOBILE;
                poscodeLabel.Text = POSCODE;
                NameLabel.Text = Name;
                posnameLabel.Text = posname;
                unitnameLabel.Text = unitname;
                unitcodeLabel.Text = unitcode;
                codeLabel.Text = CODE;
                SavePPL.Enabled = false;
                NoResult.Visible = true;
                errorlabel.Text = "ไม่พบข้อมูล";
                resultppl.Visible = false;
            }

            

        }
        protected void SavePPL_Click(object sender, EventArgs e)
        {
            string poscode = poscodeLabel.Text;
            string Name = NameLabel.Text;
            string posname = posnameLabel.Text;
            string unitname = unitnameLabel.Text;
            string company_id = companyLabel.Text;
            string MOBILE = TelLabel.Text;
            string group_in = GroupIDINLabel.Text;
           string unitcode = unitcodeLabel.Text;
            string code =codeLabel.Text ;
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
            dt_tempChk.DefaultView.RowFilter = "[code] = '" + code + "'";
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
            string loginName = Session["ID"].ToString();
            for (int i = 0; i < index; i++)
            {
                string id = cl.Items[i].Value;
                if (cl.Items[i].Selected)
                    cc2.InsertPTTBService(id, code);
            }
            string result = cc2.InsertPTTBPPL(poscode, MOBILE, loginName, unitcode, code);
            if (result != "0")
                cc2.UpdatePTTBPPL(code, MOBILE,loginName);
            grpPanel.Visible = false;
            AddPPLPanel.Visible = false;
            BindPPLDetail();
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
    }
}