using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sms_pipeLine
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string ID = Session["ID"] == null?"":(String)Session["ID"];
            int is_admin = Session["ADMIN"]==null?0:Convert.ToInt16(Session["ADMIN"]);
            if (ID == "")
            {
                Response.Redirect("~/Account/Login.aspx");
            }
            if (is_admin == 0)
            {
                NavigationMenu.Items.Remove(NavigationMenu.Items[NavigationMenu.Items.Count-1]);
                NavigationMenu.Items.Remove(NavigationMenu.Items[NavigationMenu.Items.Count - 1]);
                //NavigationMenu.Items.Remove(NavigationMenu.Items[NavigationMenu.Items.Count - 1]);
              
            }

            Session["ID"] = ID;
            Session["ADMIN"] = is_admin;

        }

       
    }
}
