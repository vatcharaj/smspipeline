using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace sms_pipeLine
{
    public partial class admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int is_admin = Convert.ToInt16(Session["ADMIN"]);
            if (is_admin == 0)
                Response.Redirect("~/Default.aspx");
            BindGrid();
            resultppl.Visible = false;
            NoResult.Visible = false;
            SavePPL.Enabled = false;
           
        }

        private void BindGrid()
        {
             OracleQuery2 cc2 = new OracleQuery2();
         DataTable dt = cc2.LoadAdmin();
         SQLServerQuery ss = new SQLServerQuery();
         string result = "";
         foreach (DataRow r in dt.Rows)
         {

             result = result + r["EMPLOYEE_ID"].ToString() + ",";

         }
        result=result.TrimEnd(',');
        DataTable dt_pis = ss.LoadINPIS("", result);
        DataTable Final_result = new DataTable();
        Final_result.Columns.Add("EMPLOYEE_ID");
        Final_result.Columns.Add("FULLNAMETH");
        Final_result.Columns.Add("POSNAME");
        Final_result.Columns.Add("UNITNAME");
        Final_result.Columns.Add("IS_ADMIN");
        if (dt!=null && dt_pis!=null&&dt.Rows.Count>0 && dt_pis.Rows.Count>0)
        {
        var results = from table1 in dt.AsEnumerable()
                      join table2 in dt_pis.AsEnumerable() on table1["EMPLOYEE_ID"].ToString() equals table2["P_ID"].ToString()
                      select new
                      {
                          EMPLOYEE_ID = table1["EMPLOYEE_ID"].ToString(),
                          FULLNAMETH = table2["FULLNAMETH"].ToString(),
                          POSNAME = table2["POSNAME"].ToString(),
                          unitname = table2["unitname"].ToString(),
                          is_admin = table1["IS_ADMIN"].ToString()=="1"?"ADMIN":"USER" 
                      };
            
                foreach (var item in results)
                {
                    Final_result.Rows.Add(item.EMPLOYEE_ID, item.FULLNAMETH, item.POSNAME, item.unitname,item.is_admin);
                }
}
            AdminGridView.DataSource =Final_result;
            AdminGridView.DataBind();
        }

        protected void AddpplBtn_Click(object sender, EventArgs e)
        {
            grpPanel.Visible = true;
            AddPPLPanel.Visible = true;
            HeadModal.Visible = true;
            SchBox.Text = ""; 
            IsAdminChk.Checked = false;
            UpdatePPL.Visible = false;
            SavePPL.Visible = true;
           
        }
        protected void AdminGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {


            if (e.CommandName == "DelPPL")
            {
                int index = Convert.ToInt32(e.CommandArgument.ToString());
                DataTable dtppl = AdminGridView.DataSource as DataTable;
                string person_id = dtppl.Rows[index]["EMPLOYEE_id"].ToString();
                OracleQuery2 cc2 = new OracleQuery2();
                cc2.DeleteAdmin( person_id);
                BindGrid();
            }

            else if (e.CommandName == "EditPPL")
            {
                int index = Convert.ToInt32(e.CommandArgument.ToString());
                DataTable dtppl = AdminGridView.DataSource as DataTable;
                string EmployeeID = dtppl.Rows[index]["EMPLOYEE_id"].ToString();
                string isadmin = dtppl.Rows[index]["IS_ADMIN"].ToString();
                string Name = dtppl.Rows[index]["FULLNAMETH"].ToString();
                string posname = dtppl.Rows[index]["posname"].ToString();
                string unitname = dtppl.Rows[index]["unitname"].ToString();
                EmployeeIDLabel.Text = EmployeeID;
                NameLabel.Text = Name;
                posnameLabel.Text = posname;
                unitnameLabel.Text = unitname;
                resultppl.Visible = true;
                SavePPL.Enabled = true;
                grpPanel.Visible = true;
                AddPPLPanel.Visible = true;
                HeadModal.Visible = false;
                IsAdminChk.Checked = isadmin == "ADMIN";
                UpdatePPL.Visible = true;
                SavePPL.Visible = false;
            }
        }

        protected void ClosePanel_Click(object sender, EventArgs e)
        {
            grpPanel.Visible = false;
            AddPPLPanel.Visible = false;
        }
        protected void SavePPL_Click(object sender, EventArgs e)
        {
            OracleQuery2 cc2 = new OracleQuery2();

            string EmployeeID = EmployeeIDLabel.Text;
            string isadmin = IsAdminChk.Checked ? "1" : "0";
            string err = cc2.insertAuthor(EmployeeID, isadmin);
            if (err != "-1")
            {
               
                grpPanel.Visible = false;
                AddPPLPanel.Visible = false;
                BindGrid();
            }
            else
            {
                errorlabel.Text = "มีชื่ออยู่ในระบบแล้ว";
                SavePPL.Enabled = false;
                NoResult.Visible = true;
            }
        }
        protected void UpdatePPL_Click(object sender, EventArgs e)
        {
            OracleQuery2 cc2 = new OracleQuery2();

            string EmployeeID = EmployeeIDLabel.Text;
            string isadmin = IsAdminChk.Checked ? "1" : "0";
            string err = cc2.UpdateAuthor(EmployeeID, isadmin);
         

                grpPanel.Visible = false;
                AddPPLPanel.Visible = false;
                BindGrid();
            
        }

        protected void SchBox_Changed(object sender, EventArgs e) 
        {
          string sch=    SchBox.Text;

          try
          {
              SQLServerQuery ss = new SQLServerQuery();
              DataTable dt_pis = ss.LoadPIS(sch, "");

              if (dt_pis.Rows.Count > 0)
              {
                  string EmployeeID = dt_pis.Rows[0]["P_ID"].ToString();
                  string Name = dt_pis.Rows[0]["name_fk"].ToString();
                  string posname = dt_pis.Rows[0]["posname"].ToString();
                  string unitname = dt_pis.Rows[0]["unitname"].ToString();
                  EmployeeIDLabel.Text = EmployeeID;
                  NameLabel.Text = Name;
                  posnameLabel.Text = posname;
                  unitnameLabel.Text = unitname;
                  resultppl.Visible = true;
                  SavePPL.Enabled = true;
              }
              else
              {
                  string EmployeeID = "";
                  string Name = "";
                  string posname = "";
                  string unitname = "";
                  EmployeeIDLabel.Text = EmployeeID;
                  NameLabel.Text = Name;
                  posnameLabel.Text = posname;
                  unitnameLabel.Text = unitname;
                  SavePPL.Enabled = false;
                  NoResult.Visible = true;
                  errorlabel.Text = "ไม่พบข้อมูล";
              }
          }
          catch {
              string EmployeeID = "";
              string Name = "";
              string posname = "";
              string unitname = "";
              EmployeeIDLabel.Text = EmployeeID;
              NameLabel.Text = Name;
              posnameLabel.Text = posname;
              unitnameLabel.Text = unitname;
              SavePPL.Enabled = false;
              NoResult.Visible = true;
              errorlabel.Text = "ไม่พบข้อมูล";
          }

        }



        protected void AdminGridView_Sorting(object sender, GridViewSortEventArgs e)
        {

            DataTable sourceTable = AdminGridView.DataSource as DataTable;

            SortGrid(AdminGridView, sourceTable, e.SortExpression);
          
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

    }
}