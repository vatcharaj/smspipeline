using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Script.Serialization;



namespace sms_pipeLine
{
    public partial class _Default : System.Web.UI.Page
    {
        public string[] names;
        public JavaScriptSerializer javaSerial = new JavaScriptSerializer();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindAllgroup();
                bindtempl();
              
            }
            int itemCount = GroupList.Items.Count;
            names = new string[itemCount - 1];
            for (int i = 0; i < itemCount - 1; i++)
            {
                names[i] = GroupList.Items[i].ToString();
            }
        }

        private void bindtempl()
        {
            OracleQuery2 cc2 = new OracleQuery2();
            DataTable dt = cc2.LoadAllTempl();

            TemplList.DataSource = dt;
            TemplList.DataTextField = "MESSAGE";
            TemplList.DataValueField = "ID";
            TemplList.DataBind();
            TemplList.Items.Insert(0, new ListItem(" ", "-1"));
            TemplList.SelectedIndex = 0;
        }

        private void bindAllgroup()
        {
            OracleQuery2 cc2 = new OracleQuery2();
            DataTable _dtgroup = cc2.LoadAllGroups();
            DataTable dtgroup = _dtgroup.Select("GROUP_LEVEL >2").Length > 0 ? _dtgroup.Select("GROUP_LEVEL >2").CopyToDataTable() : _dtgroup.Clone();

            dtgroup.Columns.Add("text");
            dtgroup.Columns.Add("text_grp");
            foreach (DataRow r in dtgroup.Rows) {
                r["text"] = r["DEPARTMENT"] + " - " + r["GROUP_NAME"];
                r["text_grp"] =  r["GROUP_NAME"];
            }
            DataView view = new DataView(dtgroup.Select("MASTER_GROUP_ID <> GROUP_ID ").CopyToDataTable());
            DataTable distinctValues = view.ToTable(true, "text_grp");
            GroupList.DataSource = distinctValues;
            GroupList.DataTextField = "text_grp";
            GroupList.DataValueField = "text_grp";
            GroupList.DataBind();



            GroupList.Items.Insert(0, new ListItem("--ทั้งหมด--", "-1"));
            GroupList.SelectedIndex = 0;
            GroupList_selectall.DataSource = dtgroup;
            GroupList_selectall.DataTextField = "text";
            GroupList_selectall.DataValueField = "GROUP_ID";
            GroupList_selectall.DataBind();
            GroupList_selectall.Items.Insert(0, new ListItem("--กรุณาเลือกกลุ่ม--", "-11"));
            //GroupList_selectall.Items.Insert(1, new ListItem("--ทั้งหมด--", "0"));
            GroupList_selectall.SelectedIndex = 0;

        }
      


        protected void TemplList_SelectedIndexChanged(object sender, EventArgs e)
        {
            MSGTEXT.Text = TemplList.SelectedItem.Text;
            TextCount.Text = MSGTEXT.Text.Length.ToString();
        }
        protected void GroupList_selectall_SelectedIndexChanged(object sender, EventArgs e)
        {
            string GROUP_ID=GroupList_selectall.SelectedValue;
            string GROUP_NAME=GroupList_selectall.SelectedItem.Text;
            string DEPARTMENT = GROUP_NAME.Substring(0, GROUP_NAME.IndexOf('-'));
            string DEPARTMENT_ID = GROUP_ID.Substring(0, 3);
            if (GROUP_ID == "0")
            {
                DataTable dt = new DataTable();
             
                    dt.Columns.Add("GROUP_ID");
                    dt.Columns.Add("GROUP_NAME");
                    dt.Columns.Add("DEPARTMENT");
                    dt.Columns.Add("DEPARTMENT_ID");
                    dt.Columns.Add("NAME");
                    dt.Columns.Add("PHONE");
                    dt.Rows.Add();
                    dt.Rows[dt.Rows.Count - 1]["GROUP_ID"] = GROUP_ID;
                    dt.Rows[dt.Rows.Count - 1]["GROUP_NAME"] = GROUP_NAME;
                    dt.Rows[dt.Rows.Count - 1]["DEPARTMENT"] = DEPARTMENT;
                    dt.Rows[dt.Rows.Count - 1]["DEPARTMENT_ID"] = DEPARTMENT_ID;
                    dt.Rows[dt.Rows.Count - 1]["NAME"] = "";
                    dt.Rows[dt.Rows.Count - 1]["PHONE"] = "";
                    listReciGrid.DataSource = dt;
                    listReciGrid.DataBind();
                    listReci.Visible = true;
            }
            else if (GROUP_ID != "-11" && GROUP_ID != "0")
            {
                DataTable dt = new DataTable();
                if ((DataTable)ViewState["GroupList"] == null)
                {
                    dt.Columns.Add("GROUP_ID");
                    dt.Columns.Add("GROUP_NAME");
                    dt.Columns.Add("DEPARTMENT");
                    dt.Columns.Add("DEPARTMENT_ID");
                    dt.Columns.Add("NAME");
                    dt.Columns.Add("PHONE");
                }
                else {
                    dt = (DataTable)ViewState["GroupList"];
                    DataRow[] r = dt.Select("GROUP_ID=" + GROUP_ID);
                    if (r.Count() > 0)
                        return;
                     r = dt.Select("GROUP_ID=0" );
                        if (r.Count() > 0)
                         return;
                }
                
                dt.Rows.Add();
                dt.Rows[dt.Rows.Count - 1]["GROUP_ID"] = GROUP_ID;
                dt.Rows[dt.Rows.Count - 1]["GROUP_NAME"] = GROUP_NAME;
                dt.Rows[dt.Rows.Count - 1]["DEPARTMENT"] = DEPARTMENT;
                dt.Rows[dt.Rows.Count - 1]["DEPARTMENT_ID"] = DEPARTMENT_ID;
                //dt.Rows[dt.Rows.Count - 1]["NAME"] = "";
                //dt.Rows[dt.Rows.Count - 1]["PHONE"] = "";
                listReciGrid.DataSource = dt;
                listReciGrid.DataBind();
                listReci.Visible = true;
            }
            else {

                listReciGrid.DataSource = null;
                listReciGrid.DataBind();
                listReci.Visible = false;
            }
            ViewState["GroupList"] = (DataTable)listReciGrid.DataSource;
        }

        protected void Tobtn_Click(object sender, EventArgs e)
        {
            group2sendSch();
            
        }

        private void group2sendSch()
        {
            updateProgress.Visible = false;
            try
            {
                string shtext = GroupTosend.Text.TrimEnd().ToUpper();
                string group_id = GroupList.SelectedValue;

                if (GroupList.Items.FindByText(shtext) != null)
                    group_id = GroupList.Items.FindByText(shtext).Value;
                else
                    group_id = GroupList.Items[0].Value;


                DataTable dt = new DataTable();
                int ismain = 0;
                if (shtext == "" && group_id == "-1")
                    return;

                OracleQuery2 cc2 = new OracleQuery2();
                DataTable dttemp = cc2.LoadAllGroups();
                if (shtext == "" && group_id != "-1")
                    dt = dttemp.Select("GROUP_NAME = '" + group_id + "'").CopyToDataTable();
                else if (shtext != "" && group_id != "-1")
                    dt = dttemp.Select("GROUP_NAME = '" + group_id + "' and " + "Keyword like '%" + shtext + "%'").CopyToDataTable();
                else if (shtext != "" && group_id == "-1")
                    dt = dttemp.Select("Keyword like '%" + shtext + "%'").CopyToDataTable();






                if (dt != null && dt.Rows.Count > 0)
                {
                    dt.Columns.Add("ISselect");
                    foreach (DataRow r in dt.Rows)
                    {
                        r["ISselect"] = 0;
                    }
                    #region
                    //if (ismain == 0)
                    //{
                    //    string group_name = "";
                    //    if (GroupList.Items.FindByText(GroupTosend.Text) != null)
                    //        group_name = GroupTosend.Text;
                    //    else
                    //        group_name = "";
                    //    DataRow newRow = dt.NewRow();
                    //    newRow["GROUP_ID"] = GroupList.SelectedValue == "-1" ? dt.Rows[dt.Rows.Count - 1]["GROUP_ID"] : GroupList.SelectedValue;
                    //    newRow["GROUP_NAME"] = GroupList.SelectedValue == "-1" ? group_name : GroupList.SelectedItem.Text;
                    //    newRow["DEPARTMENT"] = dt.Rows[dt.Rows.Count - 1]["DEPARTMENT"];
                    //    //newRow["DESCR"] = "";
                    //    //newRow["NAME"] = "";
                    //    newRow["ISselect"] = 0;
                    //    dt.Rows.InsertAt(newRow, 0);
                    //}
                    #endregion
                }
                else
                {
                    return;
                }
                DataView dv = dt.DefaultView;
                dv.Sort = "GROUP_NAME,GROUP_ID asc";
                dt = dv.ToTable();
                ViewState["SCHTable"] = dt;
                GroupResultGrid.DataSource = dt;
                GroupResultGrid.DataBind();
                grpPanel.Visible = true;
                DetailPanel.Visible = true;
            }
            catch
            {
                return;
            }
        }

        protected void ClosePanel_Click(object sender, EventArgs e)
        {
            grpPanel.Visible = false;
            DetailPanel.Visible = false;
            PreviewPanel.Visible = false;
            PreviewSendPanel.Visible = false;

        }

        protected void CancelBtn_Click(object sender, EventArgs e)
        {
            MSGTEXT.Text = "";
            GroupList.SelectedIndex = 0;
            TemplList.SelectedIndex = 0;
            GroupList_selectall.SelectedIndex = 0;
            GroupTosend.Text = "";
            listReciGrid.DataSource = null;
            listReciGrid.DataBind();
            listReci.Visible = false;
            ViewState["GroupList"] = null;
            TextCount.Text = "0";
        }

        protected void PreviewBtn_Click(object sender, EventArgs e)
        {
            string sms = MSGTEXT.Text;
            int sms_cnt = 0;
            int sms_lng = 70;
            for (int i = 0; i < sms.Length; i++)
            {
                sms_cnt += 1;

                if (sms.Length - i > sms_lng)
                {
                    Label sms1 = new Label();
                    sms1.Text = "<h5><u>ข้อความ " + sms_cnt + "</u> : </h5>" + sms.Substring(i, sms_lng) + "<br/>";
                    SMSHolder.Controls.Add(sms1);
                }
                else {
                    Label sms1 = new Label();
                    sms1.Text = "<h5><u>ข้อความ " + sms_cnt + " </u>: </h5>" + sms.Substring(i, sms.Length - i) + "<br/>";
                    SMSHolder.Controls.Add(sms1);
                }
                i += sms_lng;
            }
            grpPanel.Visible = true;
            PreviewPanel.Visible = true;
        }
   

        protected void SELECTGrp_Click(object sender, EventArgs e)
        {

            DataTable dt_sch = (DataTable)ViewState["SCHTable"];
             DataTable   dt_grp = new DataTable();
             if (ViewState["GroupList"] == null)
             {

                 dt_grp.Columns.Add("GROUP_ID");
                 dt_grp.Columns.Add("GROUP_NAME");
                 dt_grp.Columns.Add("DEPARTMENT");
                 dt_grp.Columns.Add("DEPARTMENT_ID");
                 //dt_grp.Columns.Add("NAME");
                 //dt_grp.Columns.Add("PHONE");
             }
             else
             {

                 dt_grp = ViewState["GroupList"] as DataTable;
             }

             if (dt_sch!=null&&dt_sch.Rows.Count > 0)
            {
                foreach (DataRow row in dt_sch.Rows)
                {
                    int isselect = Convert.ToInt32(row["ISselect"]);
                    string GROUP_ID = row["GROUP_ID"].ToString();
                    //string PHONE = row["PHONE"].ToString() == "-1" ? "" : row["PHONE"].ToString();
                    if (isselect == 0)
                        continue;

                    if (dt_grp.Rows.Count>0)
                    {
                      

                            //  DataRow[] r = dt_grp.Select("GROUP_ID=" + GROUP_ID + " AND PHONE='" + PHONE + "'");
                        DataRow[] r = dt_grp.Select("GROUP_ID=" + GROUP_ID);
                            if (r.Count() > 0)
                                continue;
                            r = dt_grp.Select("GROUP_ID=0");
                            if (r.Count() > 0)
                                continue;
                       }

                    dt_grp.Rows.Add();
                    dt_grp.Rows[dt_grp.Rows.Count - 1]["GROUP_ID"] = row["GROUP_ID"];
                    dt_grp.Rows[dt_grp.Rows.Count - 1]["GROUP_NAME"] = row["GROUP_NAME"];
                    dt_grp.Rows[dt_grp.Rows.Count - 1]["DEPARTMENT"] =row["DEPARTMENT"];
                    dt_grp.Rows[dt_grp.Rows.Count - 1]["DEPARTMENT_ID"] = row["DEPARTMENT_ID"];
                    //dt_grp.Rows[dt_grp.Rows.Count - 1]["NAME"]  =row["NAME"];
                    //dt_grp.Rows[dt_grp.Rows.Count - 1]["PHONE"] = PHONE;

                }
                ViewState["GroupList"] = dt_grp;
                listReciGrid.DataSource = dt_grp;
                listReciGrid.DataBind();
                listReci.Visible = true;
            }
            grpPanel.Visible = false;
            DetailPanel.Visible = false;
            PreviewPanel.Visible = false;
        }

        protected void chkSelected_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dataTable = ViewState["SCHTable"] as DataTable;

            GridViewRow Row = ((GridViewRow)((Control)sender).Parent.Parent);
            string groupid = GroupResultGrid.DataKeys[Row.RowIndex].Value.ToString();
            CheckBox chk = GroupResultGrid.Rows[Row.RowIndex].FindControl("chkSelected") as CheckBox;
            if(chk.Checked)
                dataTable.Rows[Row.RowIndex]["ISselect"] = 1;
            else
                dataTable.Rows[Row.RowIndex]["ISselect"] = 0;
            
            ViewState["SCHTable"] = dataTable;
          

        }


        protected void chkSelectedAll_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dataTable = ViewState["SCHTable"] as DataTable;
            GridViewRow grid = ((GridViewRow)((Control)sender).Parent.Parent);
            CheckBox chkAll = GroupResultGrid.HeaderRow.FindControl("chkSelectedAll") as CheckBox;

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                 CheckBox chk = GroupResultGrid.Rows[i].FindControl("chkSelected") as CheckBox;
                 if (chkAll.Checked)
                 {
                     dataTable.Rows[i]["ISselect"] = 1;
                     chk.Checked = true;
                 }
                 else {
                     dataTable.Rows[i]["ISselect"] = 1;
                     chk.Checked = false;
                 }
            }
            ViewState["SCHTable"] = dataTable;

        }

        protected void listReciGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DelPPL")
            {
                DataTable dt = (DataTable)ViewState["GroupList"];
                int index = Convert.ToInt32(e.CommandArgument);
                dt.Rows.RemoveAt(index);
                ViewState["GroupList"] = dt;
                ViewState["GroupList"] = dt;
                listReciGrid.DataSource = dt;
                listReciGrid.DataBind();
                listReci.Visible = true;
            }
        }

        protected void listReciGrid_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dataTable = ViewState["GroupList"] as DataTable;

            if (dataTable != null)
            {
                DataView dataView = new DataView(dataTable);
                dataView.Sort = e.SortExpression + " " + ConvertSortDirection(e.SortDirection);

                listReciGrid.DataSource = dataView;
                listReciGrid.DataBind();
            }
        }
    

        private string ConvertSortDirection(SortDirection sortDirection)
        {
            string newSortDirection = String.Empty;

            switch (sortDirection)
            {
                case SortDirection.Ascending:
                    newSortDirection ="DESC"; 
                    break;

                case SortDirection.Descending:
                    newSortDirection = "ASC";
                    break;
            }

            return newSortDirection;
        }

        protected void SendBtn_Click(object sender, EventArgs e)
        {
            MSGLabel.Text = SenderCheck.Checked ? MSGTEXT.Text + " From GasControl" : MSGTEXT.Text;
            ListGrpChk.DataSource =ViewState["GroupList"]==null?null: ViewState["GroupList"] as DataTable;
            ListGrpChk.DataBind();
            PreviewSendPanel.Visible = true;
            grpPanel.Visible = true;
         
        }

        private string[] SetSMSText()
        {
            string sms = MSGTEXT.Text;
            int sms_cnt = 0;
            int sms_lng = 70;
            string temp = "";
            string[] ss ;
            for (int i = 0; i < sms.Length; i++)
            {
                sms_cnt += 1;

                if (sms.Length - i > sms_lng)
                {

                    temp+=sms.Substring(i, sms_lng)+";";
                 
                }
                else
                {
                    temp += sms.Substring(i, sms.Length - i)+";";
                   
                }
                i += sms_lng;
            }
            temp = temp.TrimEnd(';');
            ss = temp==""?null:temp.Split(';');
            return ss;
        }

        private DataTable FindPttBInPis(string PTTB_str)
        {
            OracleQuery2 cc2 = new OracleQuery2();
            DataTable dt = cc2.LoadMobileInPTTB(PTTB_str);
            SQLServerQuery ss = new SQLServerQuery();
            string result = "";
            foreach (DataRow r in dt.Rows)
            {

                result = result + r["CODE"].ToString() + ",";

            }
            result = result.TrimEnd(',');
            result = result.TrimStart(',');

            DataTable dt_pis = ss.LoadPosecodeINPIS(result);
            DataTable dtppl = new DataTable(); 
            dtppl.Columns.Add("MOBILE");
            dtppl.Columns.Add("DEPARTMENT_ID");
            dtppl.Columns.Add("EMPLOYEE_ID");
            dtppl.Columns.Add("NAME");
            dtppl.Columns.Add("COMPANY");
            dtppl.Columns.Add("GROUP_ID");
            if (dt != null && dt_pis != null && dt.Rows.Count > 0 && dt_pis.Rows.Count > 0)
            {
                var results = from table1 in dt.AsEnumerable()
                              join table2 in dt_pis.AsEnumerable() on table1["CODE"].ToString() equals table2["P_ID"].ToString()
                              select new
                              {
                                   code =  table1["CODE"].ToString() ,
                                   DEPART=table1["DEPARTMENT_ID"].ToString(),
                                
                                  mobile = !string.IsNullOrEmpty(table1["mobile"].ToString()) ? table1["mobile"].ToString() : table2["mobile"].ToString(),
                                   name = table2["FULLNAMETH"].ToString(),
                                    unit= table2["unitname"].ToString(),
                                   GROUP_ID = table1["GROUP_ID"].ToString(),
                              };

                foreach (var item in results)
                {
                    dtppl.Rows.Add( item.mobile,item.DEPART,item.code,item.name,item.unit,item.GROUP_ID);
                }
            }
            return dtppl;
        }

        private DataTable FindPttInPis(string PTT_str)
        {
            OracleQuery2 cc2 = new OracleQuery2();
            DataTable dt = cc2.LoadMobileInPTT(PTT_str);
            SQLServerQuery ss = new SQLServerQuery();
            string result = "";
            foreach (DataRow r in dt.Rows)
            {

                result = result + r["EMPLOYEE_ID"].ToString() + ",";

            }
            result = result.TrimEnd(',');
            DataTable dt_pis = ss.LoadINPIS("", result);
            DataTable dtppl = new DataTable();
            dtppl.Columns.Add("MOBILE");
            dtppl.Columns.Add("DEPARTMENT_ID");
            dtppl.Columns.Add("EMPLOYEE_ID");
            dtppl.Columns.Add("NAME");
            dtppl.Columns.Add("COMPANY");
            dtppl.Columns.Add("GROUP_ID");
            if (dt != null && dt_pis != null && dt.Rows.Count > 0 && dt_pis.Rows.Count > 0)
            {
                var results = from table1 in dt.AsEnumerable()
                              join table2 in dt_pis.AsEnumerable() on table1["EMPLOYEE_ID"].ToString() equals table2["P_ID"].ToString()
                              select new
                              {
                                  DEPART = table1["DEPARTMENT_ID"].ToString(),
                                 
                                  EMPLOYEE_ID=table1["EMPLOYEE_ID"].ToString(),
                                  mobile = !string.IsNullOrEmpty(table1["mobile"].ToString()) ? table1["mobile"].ToString() : table2["mobile"].ToString(),
                                    name=table2["FULLNAMETH"].ToString(),
                                    unit= table2["unitname"].ToString(),
                                  GROUP_ID = table1["GROUP_ID"].ToString(),
                              };

                foreach (var item in results)
                {
                    dtppl.Rows.Add(item.mobile, item.DEPART, item.EMPLOYEE_ID, item.name, item.unit, item.GROUP_ID);
                }
            }
            return dtppl;
        }

        private string getListStringGroup(DataTable datatable)
        {
            string[] ids = datatable.AsEnumerable()
                              .Select(row => row["GROUP_ID"].ToString())
                              .ToArray();
            string result = "";
            foreach (string ss in ids) {
                result += ss + ",";
            }
            return result.TrimEnd(',');
        }

        protected void GroupTosend_TextChanged(object sender, EventArgs e)
        {
            
            group2sendSch();
        }

        protected void MSGTEXT_TextChanged(object sender, EventArgs e)
        {
            if (MSGTEXT.Text.Length > 320)
                MSGTEXT.Text = MSGTEXT.Text.Substring(0, 320);
            TextCount.Text = MSGTEXT.Text.Length.ToString();
        }

        protected void SndFinal_Click(object sender, EventArgs e)
        {
            updateProgress.Visible = true;
            grpPanel.Visible = false;
            DetailPanel.Visible = false;
            PreviewPanel.Visible = false;
            PreviewSendPanel.Visible = false;

            DataTable dataTable = ViewState["GroupList"] as DataTable;
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                DataTable PTTB = dataTable.Select("Department_ID =100").Count() > 0 ? dataTable.Select("Department_ID =100").CopyToDataTable() : null;
                DataTable PTT = dataTable.Select("Department_ID =200").Count() > 0 ? dataTable.Select("Department_ID =200").CopyToDataTable() : null;
                DataTable GC = dataTable.Select("Department_ID =300").Count() > 0 ? dataTable.Select("Department_ID =300").CopyToDataTable() : null;
                DataTable CS = dataTable.Select("Department_ID =400").Count() > 0 ? dataTable.Select("Department_ID =400").CopyToDataTable() : null;
                DataTable TCS = dataTable.Select("Department_ID =500").Count() > 0 ? dataTable.Select("Department_ID =500").CopyToDataTable() : null;
                DataTable TEST = dataTable.Select("Department_ID =600").Count() > 0 ? dataTable.Select("Department_ID =600").CopyToDataTable() : null;
                string PTTB_str, PTT_str, GC_str, CS_str, TCS_str, TEST_str = "";
                PTTB_str = PTTB == null ? "" : getListStringGroup(PTTB);
                PTT_str = PTT == null ? "" : getListStringGroup(PTT);
                GC_str = GC == null ? "" : getListStringGroup(GC);
                CS_str = CS == null ? "" : getListStringGroup(CS);
                TCS_str = TCS == null ? "" : getListStringGroup(TCS);
                TEST_str = TEST == null ? "" : getListStringGroup(TEST);
                DataTable Result_Dt = new DataTable();
                OracleQuery cc = new OracleQuery();
                OracleQuery2 cc2 = new OracleQuery2();
                Result_Dt.Columns.Add("MOBILE", typeof(string));
                Result_Dt.Columns.Add("DEPARTMENT_ID", typeof(string));
                Result_Dt.Columns.Add("EMPLOYEE_ID", typeof(string));
                Result_Dt.Columns.Add("NAME", typeof(string));
                Result_Dt.Columns.Add("COMPANY", typeof(string));
                Result_Dt.Columns.Add("GROUP_ID", typeof(string));
                //Result_Dt.Rows.Add();
                if (PTTB_str != "") { Result_Dt.Merge(FindPttBInPis(PTTB_str)); }
                if (PTT_str != "") { Result_Dt.Merge(FindPttInPis(PTT_str)); }
                if (GC_str != "") { Result_Dt.Merge(cc2.LoadMobileInGC(GC_str)); }
                if (CS_str != "") { Result_Dt.Merge(cc2.LoadMobileInCS(CS_str)); }
                if (TCS_str != "") { Result_Dt.Merge(cc2.LoadMobileInTCS(TCS_str)); }
                if (TEST_str != "") { Result_Dt.Merge(cc2.LoadMobileInTest(TEST_str)); }

                DataTable output = Result_Dt.Rows.Count > 0 ? Result_Dt.Copy() : null;

                DataView dv = new DataView(output);
                dv.Sort = "MOBILE";
                DataTable output2 = dv.ToTable();

                var dis = output.AsEnumerable().Select(row => new
                {
                    MOBILE = row.Field<string>("MOBILE"),
                    DEPARTMENT_ID = row.Field<string>("DEPARTMENT_ID"),
                    EMPLOYEE_ID = row.Field<string>("EMPLOYEE_ID"),
                    NAME = row.Field<string>("NAME"),
                    COMPANY = row.Field<string>("COMPANY"),
                    GROUP_ID = row.Field<string>("GROUP_ID")
                }).GroupBy(x => x.MOBILE).Select(x => x.FirstOrDefault());

                //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "notext", "alert('"+ output.Rows.Count +","+ dis.Count() + "')", true);

                SendSMS ss = new SendSMS();
                string resultsent = "";
                string smsText = MSGTEXT.Text;//SetSMSText();
                if (smsText == null)
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "notext", "alert('No Text Message')", true);
                else if (output == null)
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "notext", "alert('No Recipient')", true);
                else
                {
                    DataView view = new DataView(output);
                    output = view.ToTable(true);
                    string loginName = Session["ID"].ToString();
                    string sendername = "";
                    if (SenderCheck.Checked)
                        sendername = "From GasControl";
                    else
                        sendername = "";

                    resultsent = ss.sendSMS_webPost(output, smsText, sendername, loginName);
                    if (resultsent == "1")
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "notext", "alert('Success')", true);
                    else
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "notext", "alert('" + resultsent + "')", true);
                }



                return;
            }


            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "notext", "alert('No Recipient')", true);

        }

    
        //[System.Web.Script.Services.ScriptMethod()]

        //[System.Web.Services.WebMethod]

        //public static List<string> GetListofgroup(string prefixText)
        //{


        //    OracleQuery2 cc2 = new OracleQuery2();
        //    DataTable _dtgroup = cc2.LoadAllGroups();
        //    DataTable dtgroup = _dtgroup.Select("GROUP_LEVEL >2").Length > 0 ? _dtgroup.Select("GROUP_LEVEL >2").CopyToDataTable() : _dtgroup.Clone();

        //    dtgroup.Columns.Add("text");
        //    dtgroup.Columns.Add("text_grp");
        //    foreach (DataRow r in dtgroup.Rows)
        //    {
        //        r["text"] = r["DEPARTMENT"] + " - " + r["GROUP_NAME"];
        //        r["text_grp"] = r["GROUP_NAME"];
        //    }
        //    DataView view = new DataView(dtgroup.Select("MASTER_GROUP_ID <> GROUP_ID ").CopyToDataTable());
        //    DataTable distinctValues = view.ToTable(true, "text_grp");
        //    List<string> grps = new List<string>();
        //    int i = 0;
        //    foreach (DataRow dr in distinctValues.Rows)
        //    {
        //        grps.Add(dr["text_grp"].ToString());
        //    }
        //    return grps;


        //}
       

    }
}
