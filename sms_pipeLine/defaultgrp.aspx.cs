using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sms_pipeLine
{
    public partial class defaultgrp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int is_admin = Convert.ToInt16(Session["ADMIN"]);
            if (is_admin == 0)
                Response.Redirect("~/Default.aspx");

            BindGrid();

        }

        private void BindGrid()
        {
            throw new NotImplementedException();
        }
    }
}