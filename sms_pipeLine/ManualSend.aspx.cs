using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace sms_pipeLine
{
    public partial class ManualSend : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
              
                bindtempl();

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
        protected void TemplList_SelectedIndexChanged(object sender, EventArgs e)
        {
            MSGTEXT.Text = TemplList.SelectedItem.Text;
            TextCount.Text = MSGTEXT.Text.Length.ToString();
        }
        protected void ClosePanel_Click(object sender, EventArgs e)
        {
            grpPanel.Visible = false;
         
            PreviewPanel.Visible = false;
        }
        protected void CancelBtn_Click(object sender, EventArgs e)
        {
            MSGTEXT.Text = "";
            TemplList.SelectedIndex = 0;
            GroupTosend.Text = "";
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
                else
                {
                    Label sms1 = new Label();
                    sms1.Text = "<h5><u>ข้อความ " + sms_cnt + " </u>: </h5>" + sms.Substring(i, sms.Length - i) + "<br/>";
                    SMSHolder.Controls.Add(sms1);
                }
                i += sms_lng;
            }
            grpPanel.Visible = true;
            PreviewPanel.Visible = true;
        }
        protected void MSGTEXT_TextChanged(object sender, EventArgs e)
        {
            if (MSGTEXT.Text.Length > 320)
                MSGTEXT.Text = MSGTEXT.Text.Substring(0, 320);
            TextCount.Text = MSGTEXT.Text.Length.ToString();
        }

        protected void SendBtn_Click(object sender, EventArgs e)
        {
            string [] listrecipient = GroupTosend.Text.Trim().Split(',');
            SendSMS ss = new SendSMS();
            string resultsent = "";
            if (listrecipient.Length==0)
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "norecipient", "alert('No Recipient')", true);
            else if (MSGTEXT.Text.Trim() == "")
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "norecipient", "alert('No Message')", true);
            else {
                string loginName = Session["ID"].ToString();
                string smsText = MSGTEXT.Text;
                string sendername = "";
                DataTable output = new DataTable();
                output.Columns.Add("EMPLOYEE_ID");
                output.Columns.Add("MOBILE");
                output.Columns.Add("Department_ID");
                output.Columns.Add("GROUP_ID");
                output.Columns.Add("NAME");
                output.Columns.Add("COMPANY");
                if (SenderCheck.Checked)
                    sendername = "From GasControl";
                else
                    sendername = "";
                for (int i = 0; i < listrecipient.Length; i++) 
                {
                    output.Rows.Add(null, listrecipient[i],null,-1,null,null);
                }



               resultsent = ss.sendSMS_webPost(output, smsText, sendername, loginName);
                if (resultsent == "1")
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "notext", "alert('Success')", true);
                else
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "notext", "alert('" + resultsent + "')", true);
            }


        }
    }
}