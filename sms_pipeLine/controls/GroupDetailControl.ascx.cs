using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace sms_pipeLine.controls
{
    public partial class GroupDetailControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                bindTree();
                //AddgroupBtn.Visible = false;
                //AddpplBtn.Visible = false;
                //DelGroup.Visible = false;

            }
            else
            {
                //setEnableBtn();
            }
        }
        protected void RefreshLink_Click(object sender, EventArgs e) 
        {
            TreeView1.Nodes.Clear();
         
            bindTree(); 
            BindGridDetail("-1", 0);
        }
        private void bindTree()
        {
            OracleQuery2 cc2 = new OracleQuery2();
            DataTable dt = cc2.LoadAllGroups();
            ViewState["AllGroup"] = dt;
            try
            {

                foreach (DataRow r in dt.Rows)
                {
                    if (r["MASTER_GROUP_ID"].ToString() == r["GROUP_ID"].ToString())
                    {
                        TreeNode root = new TreeNode(r["master_name"].ToString(), r["MASTER_GROUP_ID"].ToString());
                        //   root.SelectAction = TreeNodeSelectAction.Expand;
                        CreateNode(root, dt, r["GROUP_ID"].ToString());
                        TreeView1.Nodes.Add(root);
                    }
                }


            }
            catch (Exception Ex) { throw Ex; }



        }

        private void CreateNode(TreeNode root, DataTable dt, string p)
        {

            DataRow[] Rows = dt.Select("MASTER_GROUP_ID =" + p);
            if (Rows.Length == 0) { return; }
            for (int i = 0; i < Rows.Length; i++)
            {
                if (Rows[i]["MASTER_GROUP_ID"].ToString() != Rows[i]["GROUP_ID"].ToString())
                {
                    TreeNode Childnode = new TreeNode(Rows[i]["GROUP_NAME"].ToString(), Rows[i]["GROUP_ID"].ToString());
                    // Childnode.SelectAction = TreeNodeSelectAction.Expand;
                    root.ChildNodes.Add(Childnode);
                    CreateNode(Childnode, dt, Rows[i]["GROUP_ID"].ToString());
                }
            }

        }

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {

            GROUPLabel.Text = TreeView1.SelectedNode.Text;
            PPLLabel.Text = TreeView1.SelectedNode.Text;
            string group_id = TreeView1.SelectedNode.Value;
            int level = TreeView1.SelectedNode.Depth;
            BindGridDetail(group_id, level);

        }

        private void BindGridDetail(string group_id, int level)
        {
            string group = group_id.Substring(0, 1);
            CountPPL.Text = "";
            if (group == "1")
            {
                PTTB.BindGroupDetail(group_id, level);
                PTTBPPL.BindPPLDetail(group_id, level);
                CountPPL.Text = PTTBPPL.CountPPL();
                PTTB.Visible = true;
                PTT.Visible = false;
                GC.Visible = false;
                CS.Visible = false;
                TCS.Visible = false;
                TEST.Visible = false;
                PTTPPL.Visible = false;
                PTTBPPL.Visible = true;
                GCPPL.Visible = false;
                CSPPL.Visible = false;
                TCSPPL.Visible = false;
                TESTPPL.Visible = false;

            }
            else if (group == "2")
            {
                PTT.BindGroupDetail(group_id, level);
                PTTPPL.BindPPLDetail(group_id, level);
                CountPPL.Text = PTTPPL.CountPPL();
                PTTB.Visible = false;
                PTT.Visible = true;
                GC.Visible = false;
                CS.Visible = false;
                TCS.Visible = false;
                TEST.Visible = false;
                PTTPPL.Visible = true;
                PTTBPPL.Visible = false;
                GCPPL.Visible = false;
                CSPPL.Visible = false;
                TCSPPL.Visible = false;
                TESTPPL.Visible = false;
            }
            else if (group == "3")
            {
                GC.BindGroupDetail(group_id, level);
                GCPPL.BindPPLDetail(group_id, level);
                CountPPL.Text = GCPPL.CountPPL();
                PTTB.Visible = false;
                PTT.Visible = false;
                GC.Visible = true;
                CS.Visible = false;
                TCS.Visible = false;
                TEST.Visible = false;
                PTTPPL.Visible = false;
                PTTBPPL.Visible = false;
                GCPPL.Visible = true;
                CSPPL.Visible = false;
                TCSPPL.Visible = false;
                TESTPPL.Visible = false;

            }
            else if (group == "4")
            {
                CS.BindGroupDetail(group_id, level);
                CSPPL.BindPPLDetail(group_id, level);
                CountPPL.Text = CSPPL.CountPPL();
                PTTB.Visible = false;
                PTT.Visible = false;
                GC.Visible = false;
                TCS.Visible = false;
                CS.Visible = true;
                TEST.Visible = false;
                PTTPPL.Visible = false;
                PTTBPPL.Visible = false;
                GCPPL.Visible = false;
                CSPPL.Visible = true;
                TCSPPL.Visible = false;
                TESTPPL.Visible = false;
            }
            else if (group == "5")
            {
                TCS.BindGroupDetail(group_id, level);
                TCSPPL.BindPPLDetail(group_id, level);
                CountPPL.Text = TCSPPL.CountPPL();
                PTTB.Visible = false;
                PTT.Visible = false;
                GC.Visible = false;
                CS.Visible = false;
                TCS.Visible = true;
                TEST.Visible = false;
                PTTPPL.Visible = false;
                PTTBPPL.Visible = false;
                GCPPL.Visible = false;
                CSPPL.Visible = false;
                TCSPPL.Visible = true;
                TESTPPL.Visible = false;
            }
            else if (group == "6")
            {
                TEST.BindGroupDetail(group_id, level);
                TESTPPL.BindPPLDetail(group_id, level);
                PTTB.Visible = false;
                PTT.Visible = false;
                GC.Visible = false;
                TCS.Visible = false;
                CS.Visible = false;
                TEST.Visible = true;
                PTTPPL.Visible = false;
                PTTBPPL.Visible = false;
                GCPPL.Visible = false;
                CSPPL.Visible = false;
                TCSPPL.Visible = false;
                TESTPPL.Visible = true;
            }
            else 
            {
                GROUPLabel.Text = "-";
                PPLLabel.Text = "-";
                PTTB.Visible = false;
                PTT.Visible = false;
                GC.Visible = false;
                CS.Visible = false;
                TCS.Visible = false;
                TEST.Visible = false;
                PTTPPL.Visible = false;
                PTTBPPL.Visible = false;
                GCPPL.Visible = false;
                CSPPL.Visible = false;
                TCSPPL.Visible = false;
                TESTPPL.Visible = false;
            } 
            //setEnableBtn();

        }

        //private void setEnableBtn()
        //{
        //    int level =  TreeView1.SelectedNode != null ? TreeView1.SelectedNode.Depth : 0;
        //    if (level == 0||level == -1)
        //    {
        //        AddgroupBtn.Visible = true;
        //        AddpplBtn.Visible = true;
        //        DelGroup.Visible = false;
        //    }
        //    if (level == 3)
        //    {
        //        AddgroupBtn.Visible = false;
        //        AddpplBtn.Visible = true;
        //        DelGroup.Visible = true;
        //    }
        //    else
        //    {
        //        AddgroupBtn.Visible = true;
        //        AddpplBtn.Visible = true;
        //        DelGroup.Visible = true;
        //    }
        //}

        //protected void DelGroup_Click(object sender, EventArgs e)
        //{

        //    string Group_id = TreeView1.SelectedNode.Value;
        //    OracleQuery2 cc2 = new OracleQuery2();
        //    cc2.DeleteGroup(Group_id);
        //    TreeView1.Nodes.Clear();
        //    bindTree();
        //    BindGridDetail("-1", 0);
        //}


        //protected void ClosePanel_Click(object sender, EventArgs e)
        //{
        //    grpPanel.Visible = false;
        //    AddGroupPanel.Visible = false;
        //    AddPPLPanel.Visible = false;
        //}

        //protected void AddgroupBtn_Click(object sender, EventArgs e)
        //{
        //    InputGroup.Text = "";
        //    grpPanel.Visible = true;
        //    AddGroupPanel.Visible = true;
        //}
        //protected void AddpplBtn_Click(object sender, EventArgs e)
        //{


        //    OracleQuery cc = new OracleQuery();
        //    DataTable dt = cc.LoadAllDepart();
        //    string group_id = TreeView1.SelectedNode.Value;
        //    int level = TreeView1.SelectedNode.Depth;
        //    string text = TreeView1.SelectedNode.Text;
        //    if (group_id == "-1") return;
        //    string depart = group_id.Substring(0, 3);
        //    dt = dt.Select("ID =1 OR  ID=" + depart).CopyToDataTable();
        //    SetPanelDetail(dt, group_id, text);
        //    AddPPLPanel.Visible = true;
        //    grpPanel.Visible = true;
        //}
        //protected void SAVEGrp_Click(object sender, EventArgs e)
        //{
        //    if (InputGroup.Text == "")
        //        return;
        //    int level = TreeView1.SelectedNode.Depth + 2;
        //    if (level > 4)
        //        return;
        //    DataTable dt = ViewState["AllGroup"] as DataTable;
        //    string master_Group_id = TreeView1.SelectedNode.Value;
        //    string department = master_Group_id.Substring(0, 3);
        //    DataTable dt2 = new DataTable();
        //    string maxId;
        //    DataRow[] row = dt.Select("DEPARTMENT_ID=" + department + " AND GROUP_LEVEL =" + level);
        //    if (row.Count() > 0)
        //    {
        //        dt2 = row.CopyToDataTable();
        //        var maxRow = dt2.Select("Group_id = MAX(Group_id)");
        //        maxId = (Convert.ToInt32(maxRow[0]["Group_id"]) + 1).ToString();
        //    }
        //    else
        //    {
        //        maxId = department + level + "001";
        //    }
        //    string Group_id = maxId;
        //    string Group_name = InputGroup.Text;
        //    OracleQuery cc = new OracleQuery();
        //    cc.InsertNewGroup(Group_id, Group_name, department, level, master_Group_id);

        //    grpPanel.Visible = false;
        //    AddGroupPanel.Visible = false;
        //    TreeView1.Nodes.Clear();
        //    bindTree();

        //    BindGridDetail(master_Group_id, 0);
        //}

        protected void SavePPL_Click(object sender, EventArgs e)
        {
            //grpPanel.Visible = false;
            //AddGroupPanel.Visible = false;
            AddPPLPanel.Visible = false;
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            string group_id = TreeView1.SelectedNode.Value;
            int level = TreeView1.SelectedNode.Depth;
            string text = TreeView1.SelectedNode.Text;
            if (group_id == "-1") return;
            string depart = group_id.Substring(0, 3);
            string group = PPLDepartList.SelectedValue;
            if (FinalListBox.Items.Count > 0)
            {

                OracleQuery cc = new OracleQuery();



                for (int i = 0; i < FinalListBox.Items.Count; i++)
                {
                    dt.Rows.Add(FinalListBox.Items[i].Value);

                }
                //if (depart == "200")
                //    cc.SaveCSPPL(dt, group_id, group);
                //else if (depart == "300")
                //    cc.SaveTCSPPL(dt, group_id, group);
                //else if (depart == "100")
                //    cc.SaveGCPPL(dt, group_id, group);
            }

            BindGridDetail(group_id, level);

        }

        public void SetPanelDetail(System.Data.DataTable dt, string group_id, string text)
        {
            SchPPLBox.Text = "";

            ResultListBox.Items.Clear();
            FinalListBox.Items.Clear();
            GroupPPLPanel.Text = text;
            PPLDepartList.DataSource = dt;
            PPLDepartList.DataTextField = "NAME";
            PPLDepartList.DataValueField = "ID";
            PPLDepartList.DataBind();
            PPLDepartList.Items.Insert(0, new ListItem("--Select--", "-1"));
            PPLDepartList.SelectedIndex = 0;
        }
        protected void SchPPLbtn_Click(object sender, EventArgs e)
        {

            string group_id = TreeView1.SelectedNode.Value;
            string depart = group_id.Substring(0, 3);

            if (depart == "200")
                CaseCs200();
            else if (depart == "300")
                CaseTCs300();
            else if (depart == "100")
                CaseGC100();
        }

        private void CaseGC100()
        {
            OracleQuery cc = new OracleQuery();
            DataTable dt = new DataTable();
            string schtext = SchPPLBox.Text.TrimEnd();
            string group = PPLDepartList.SelectedValue;
            string group_id = TreeView1.SelectedNode.Value;
            if (group == "1")
            {
              //  string pisincs = cc.LoadPISinCS(group_id);
            //    bindFromPIS(group_id, pisincs);

            }

            else if (group == "100")
            {
                //dt = cc.LoadGCPPL(group_id, schtext);
                //ResultListBox.DataSource = dt;
                //ResultListBox.DataTextField = "NAME_fk";
                //ResultListBox.DataValueField = "P_ID";
                //ResultListBox.DataBind();
            }
        }

        private void CaseTCs300()
        {
            OracleQuery cc = new OracleQuery();
            DataTable dt = new DataTable();
            string schtext = SchPPLBox.Text.TrimEnd();
            string group = PPLDepartList.SelectedValue;
            string group_id = TreeView1.SelectedNode.Value;
            if (group == "1")
            {
                //string pisincs = cc.LoadPISinTCS(group_id);
                //bindFromPIS(group_id, pisincs);

            }
            else if (group == "300")
            {
                //dt = cc.LoadTCSPPL(group_id, schtext);
                //ResultListBox.DataSource = dt;
                //ResultListBox.DataTextField = "NAME_fk";
                //ResultListBox.DataValueField = "P_ID";
                //ResultListBox.DataBind();
            }
        }
        private void CaseCs200()
        {
            OracleQuery cc = new OracleQuery();
            DataTable dt = new DataTable();
            string schtext = SchPPLBox.Text.TrimEnd();
            string group = PPLDepartList.SelectedValue;
            string group_id = TreeView1.SelectedNode.Value;
            if (group == "1")
            {
                //string pisincs = cc.LoadPISinCS(group_id);
                //indFromPIS(group_id, pisincs);

            }

            else if (group == "200")
            {
                //dt = cc.LoadCSPPL(group_id, schtext);
                //ResultListBox.DataSource = dt;
                //ResultListBox.DataTextField = "NAME_fk";
                //ResultListBox.DataValueField = "P_ID";
                //ResultListBox.DataBind();
            }
        }

        private void bindFromPIS(string group_id, string pisincs)
        {
            SQLServerQuery ss = new SQLServerQuery();
            DataTable dt = new DataTable();
            string schtext = SchPPLBox.Text.TrimEnd();
            dt = ss.LoadPIS(schtext, pisincs);
            ResultListBox.DataSource = dt;
            ResultListBox.DataTextField = "NAME_fk";
            ResultListBox.DataValueField = "P_ID";
            ResultListBox.DataBind();
        }
        protected void Add2Final_Click(object sender, EventArgs e)
        {
            if (ResultListBox.Items.Count > 0)
            {
                for (int i = 0; i < ResultListBox.Items.Count; i++)
                {
                    if (ResultListBox.Items[i].Selected)
                    {
                        string selectedText = ResultListBox.Items[i].Text;
                        string selectedValue = ResultListBox.Items[i].Value;
                        FinalListBox.Items.Add(new ListItem(selectedText, selectedValue));

                        //insert command
                    }
                }
                for (int i = 0; i < ResultListBox.Items.Count; i++)
                {
                    if (ResultListBox.Items[i].Selected)
                    {
                        string selectedText = ResultListBox.Items[i].Text;
                        string selectedValue = ResultListBox.Items[i].Value;

                        ResultListBox.Items.Remove(ResultListBox.Items.FindByValue(selectedValue));
                        //insert command
                    }
                }
            }

        }
        protected void RemoveFinal_Click(object sender, EventArgs e)
        {

            if (FinalListBox.Items.Count > 0)
            {
                for (int i = 0; i < FinalListBox.Items.Count; i++)
                {
                    if (FinalListBox.Items[i].Selected)
                    {
                        string selectedText = FinalListBox.Items[i].Text;
                        string selectedValue = FinalListBox.Items[i].Value;
                        ResultListBox.Items.Add(new ListItem(selectedText, selectedValue));

                        //insert command
                    }
                }
                for (int i = 0; i < FinalListBox.Items.Count; i++)
                {
                    if (FinalListBox.Items[i].Selected)
                    {
                        string selectedText = FinalListBox.Items[i].Text;
                        string selectedValue = FinalListBox.Items[i].Value;

                        FinalListBox.Items.Remove(FinalListBox.Items.FindByValue(selectedValue));
                        //insert command
                    }
                }
            }








        }
    }
}