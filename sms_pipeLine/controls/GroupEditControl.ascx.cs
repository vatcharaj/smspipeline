using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace sms_pipeLine.controls
{
    public partial class GroupEditControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                bindTree();
                DelGroup.Enabled = false;
                AddgroupBtn.Enabled = false;
            
            }
        }

        private void bindTree()
        {
            OracleQuery2 cc2 = new OracleQuery2();
            DataTable dt = cc2.LoadAllGroups2Edit();
            dt = dt.Select("DEPARTMENT_ID =300").CopyToDataTable();
            ViewState["AllGroup"] = dt;
            try
            {

                foreach (DataRow r in dt.Rows)
                {
                    if (r["MASTER_GROUP_ID"].ToString() == r["GROUP_ID"].ToString())
                    {
                        TreeNode root = new TreeNode("กลุ่ม", r["MASTER_GROUP_ID"].ToString());
                        //   root.SelectAction = TreeNodeSelectAction.Expand;
                        CreateNode(root, dt, r["GROUP_ID"].ToString());
                        TreeView1.Nodes.Add(root);
                    }
                }
                TreeView1.Nodes[0].Selected = true;

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
        public void BindGroupDetail(string group_id, int level)
        {

            OracleQuery2 cc2 = new OracleQuery2();
            DataTable dtgroup = cc2.LoadGroupAllCreate(group_id);


            ViewState["CurrentEditGrp"] = dtgroup;
            if (dtgroup.Rows.Count > 0)
            {
                Grid_group.DataSource = dtgroup;
                Grid_group.DataBind();
               
            }
            else
            {
                Grid_group.DataSource = null;
                Grid_group.DataBind();
               
            }



        }
        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {

            GROUPLabel.Text = TreeView1.SelectedNode.Text;
            GROUP_IDLabel.Text=TreeView1.SelectedNode.Value;
            string group_id = TreeView1.SelectedNode.Value;
            int level = TreeView1.SelectedNode.Depth;
            BindGroupDetail(group_id, level);
           SetGroup( group_id,level);
           DelGroup.Enabled = level > 1;
           AddgroupBtn.Enabled = level > 0;

        }
        protected void AddgroupBtn_Click(object sender, EventArgs e)
        {
            InputGroup.Text = "";
            grpPanel.Visible = true;
            AddGroupPanel.Visible = true;
        }


        protected void ClosePanel_Click(object sender, EventArgs e)
        {
            grpPanel.Visible = false;
            AddGroupPanel.Visible = false;
            EditGrpPanel.Visible = false;
           
        }
        protected void SAVEGrp_Click(object sender, EventArgs e)
        {
            if (InputGroup.Text == "")
                return;
            int level = TreeView1.SelectedNode.Depth + 2;
            if (level > 4)
                return;
            DataTable dt = ViewState["AllGroup"] as DataTable;
            string master_Group_id = GROUP_IDLabel.Text;// TreeView1.SelectedNode.Value;
            string department = master_Group_id.Substring(0, 3);
            DataTable dt2 = new DataTable();
            string maxId;
            DataRow[] row = dt.Select("DEPARTMENT_ID=" + department + " AND GROUP_LEVEL =" + level);
            if (row.Count() > 0)
            {
                dt2 = row.CopyToDataTable();
                var maxRow = dt2.Select("Group_id = MAX(Group_id)");
                maxId = (Convert.ToInt32(maxRow[0]["Group_id"]) + 1).ToString();
            }
            else
            {
                maxId = department + level + "001";
            }
            string Group_id = maxId;
            string Group_name = InputGroup.Text;
            string longinName = Session["ID"].ToString();
            OracleQuery2 cc2 = new OracleQuery2();
            cc2.InsertNewGroup(Group_id, Group_name, level, master_Group_id, longinName);

            grpPanel.Visible = false;
            AddGroupPanel.Visible = false;
            TreeView1.Nodes.Clear();
            bindTree();

            BindGroupDetail(master_Group_id, 0);
        
            
        }
        protected void DelGroup_Click(object sender, EventArgs e)
        {

            string Group_id = GROUP_IDLabel.Text;//TreeView1.SelectedNode.Value;
            OracleQuery2 cc2 = new OracleQuery2();
            if (TreeView1.SelectedNode.Depth == 0)
                return;
            cc2.DeleteGroup(Group_id);
            TreeView1.Nodes.Clear();
            bindTree();
            string group_id = TreeView1.Nodes[0].Value;
            GROUPLabel.Text = TreeView1.SelectedNode.Text;
            GROUP_IDLabel.Text = TreeView1.SelectedNode.Value;
            Grid_group.DataSource = null;
            Grid_group.DataBind();
            cl.DataSource = null;
            cl.DataBind();
            BindGroupDetail(group_id, 0);
          

        }
        private void SetGroup(string group_id, int level)
        {

            OracleQuery2 cc2 = new OracleQuery2();
            string st = group_id.Remove(0, 1);
            DataTable dt   = cc2.LoadDisplayGroup(st); //cc2.LoadCSGroup(group_id);
            cl.DataSource = dt;
            cl.DataTextField = "NAME";
            cl.DataValueField = "DEPARTMENT_ID";
            cl.DataBind();
            cl.Enabled = level > 1;
            //string select_group = "";
            foreach (DataRow r in dt.Rows)
            {
                string Group_id_in = r["isshow"].ToString();
                string depart = r["DEPARTMENT_ID"].ToString();
                if (!String.IsNullOrEmpty(Group_id_in))
                {
                    cl.Items.FindByValue(depart).Selected = Group_id_in == "1";
                }
            }
            //GroupIDINLabel.Text = select_group;
        }

        protected void cl_SelectedIndexChanged(object sender, EventArgs e)
        {
            string group_id = GROUP_IDLabel.Text;
            string st = group_id.Remove(0, 1);
           OracleQuery2 cc2 = new OracleQuery2();
           string loginName = Session["ID"].ToString();
            foreach (ListItem item in cl.Items)
            {
                if (item.Selected)
                {
                    cc2.updateDisplayGroup(st, item.Value, 1, loginName);
                }
                else 
                {
                    cc2.updateDisplayGroup(st, item.Value, 0, loginName);
                }
            }
         
          
            

        }
        protected void Grid_group_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditGrp")
            {
                DataTable dd =Grid_group.DataSource as DataTable;
                EditGrpPanel.Visible = true;
                grpPanel.Visible = true;
                string index = e.CommandArgument.ToString();
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                // row contains current Clicked Gridview Row
                string grpname = row.Cells[1].Text;
                EditGroupTextBox.Text = grpname;
                EditGroupIDTextBox.Text = index;
            }
        }

        protected void EditGrp_Click(object sender, EventArgs e)
        {
            string grpname = EditGroupTextBox.Text.Trim();
            string grpid = EditGroupIDTextBox.Text;
            string loginName = Session["ID"].ToString();
            grpid = grpid.Remove(0, 1);
            OracleQuery2 cc2 = new OracleQuery2();
            cc2.UpdateGroup(grpid, grpname, loginName);
             grpPanel.Visible = false;
             EditGrpPanel.Visible = false;
             string group_id = TreeView1.SelectedNode.Value;
             int level = TreeView1.SelectedNode.Depth;
             BindGroupDetail(group_id, level);
             SetGroup(group_id, level);
          
        }
       
    }
}