using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace sms_pipeLine.controls
{
    public partial class TESTPPLControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void BindPPLDetail(string group_id, int level)
        {
           
            groupLabel.Text = group_id;
            LevelLabel.Text = level.ToString();
            OracleQuery cc = new OracleQuery();
            DataTable dtppl = new DataTable();
          //  DataTable dtppl = cc.LoadPPL(group_id);
          //  dtppl = findPTTPerson(dtppl, group_id);
            ViewState["CurrentPPL"] = dtppl;
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
         //   string pisincs = cc.LoadPISinCS(group_id);
          //  DataTable dt = ss.LoadINPIS("", pisincs);
         //   dtppl = pushtodtppl(dtppl, dt);
            return dtppl;
        }
        private DataTable pushtodtppl(DataTable dtppl, DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {

                foreach (DataRow r in dt.Rows)
                {
                    dtppl.Rows.Add(r["P_ID"].ToString(), r["FULLNAMETH"], r["POSNAME"], "PTT", r["mobile"], "1");
                }

            }
            return dtppl;
        }
        protected void Grid_ppl_RowCommand(object sender, GridViewCommandEventArgs e)
        {


            if (e.CommandName == "DelPPL")
            {

                DataTable dtgroup = ViewState["CurrentGrp"] as DataTable;
                DataTable dtppl = ViewState["CurrentPPL"] as DataTable;
                int index = Convert.ToInt32(e.CommandArgument.ToString());
                string group_id = groupLabel.Text;
                int level = Convert.ToInt32(LevelLabel.Text);
                // string text = TreeView1.SelectedNode.Text;
                if (group_id == "-1") return;
                string depart = group_id.Substring(0, 3);
                OracleQuery2 cc2 = new OracleQuery2();
                string person_id = dtppl.Rows[index]["id"].ToString();
                cc2.DeleteTESTPPL(group_id, person_id);

                //TreeView1.Nodes.Clear();
                //bindTree();
                BindPPLDetail(group_id, level);

            }

        }
    }
}