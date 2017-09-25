using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace sms_pipeLine.controls
{
    public partial class TestControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void BindGroupDetail(string group_id, int level)
        {

            OracleQuery2 cc2 = new OracleQuery2();
            DataTable dtgroup = cc2.LoadGroup(group_id);


            ViewState["CurrentGrp"] = dtgroup;
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
    }
}