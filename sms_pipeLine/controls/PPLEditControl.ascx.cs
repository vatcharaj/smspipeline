using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace sms_pipeLine.controls
{
    public partial class PPLEditControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          
                bindLink();
           
        }

        private void bindLink()
        {
            OracleQuery2 cc2 = new OracleQuery2();
            DataTable dt = cc2.LoadAllDepart();
            string name = "";
            string value = "";
            if (dt != null && dt.Rows.Count > 0)
            {

                foreach (DataRow r in dt.Rows)
                { 
                    name = r["NAME"].ToString();
                    value=r["ID"].ToString();
                    LinkButton lb = new LinkButton();
                    lb.Text = "" + name + "" + "<br/>";
                    lb.Command += new CommandEventHandler(bindGrid);
                    lb.CommandArgument = value;
                    LinkHolder.Controls.Add(lb);
                }
            }
        }
        private void bindGrid(Object sender, CommandEventArgs e)
        {
         string dept=   e.CommandArgument.ToString();
            if (dept == "100")
            {
                PTTB.Visible = true;
                PTT.Visible = false;
                GC.Visible = false; 
                CS.Visible = false;
                TCS.Visible = false;
                PTTB.BindPPLDetail();
            } 
            else if (dept == "200")
            {
                PTTB.Visible = false;
                PTT.Visible = true;
                GC.Visible = false;
                CS.Visible = false;
                TCS.Visible = false;
                PTT.BindPPLDetail();
            }
            else if (dept == "300")
            {
                PTTB.Visible = false;
                PTT.Visible = false;
                GC.Visible = true;
                CS.Visible = false;
                TCS.Visible = false;
                GC.BindPPLDetail();
            }
            else if (dept == "400")
            {
                PTTB.Visible = false;
                PTT.Visible = false;
                GC.Visible = false;
                CS.Visible = true;
                TCS.Visible = false;
                CS.BindPPLDetail();
            }
            else if (dept == "500")
            {
                PTTB.Visible = false;
                PTT.Visible = false;
                GC.Visible = false;
                CS.Visible =false;
                TCS.Visible =  true;
                TCS.BindPPLDetail();
            }
            else if (dept == "600")
            {
                PTTB.Visible = false;
                PTT.Visible = false;
                GC.Visible = false;
                CS.Visible = false;
                TCS.Visible = false;
            }

        }
        protected void RefreshLink_Click(object sender, EventArgs e)
        {
        }
    }
}