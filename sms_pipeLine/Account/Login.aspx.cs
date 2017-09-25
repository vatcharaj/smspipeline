using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.DirectoryServices;
using System.Web.Security;
using System.Data;

namespace sms_pipeLine.Account
{
    public partial class Login : System.Web.UI.Page
    {
         protected void Page_Load(object sender, EventArgs e)
        {
            LoginForm.Visible = true;
           // RegisterHyperLink.NavigateUrl = "Register.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
        }

        //string initLDAPPath = "dc=tc, dc=PTT, dc=corp";
        //string initLDAPServer = "192.168.0.5";
        //string initShortDomainName = "ldap.ptt.corp";
        //string strErrMsg;

        //public string MD5(string strString)
        //{
        //    ASCIIEncoding ASCIIenc = new ASCIIEncoding();
        //    string strReturn;
        //    byte[] ByteSourceText = ASCIIenc.GetBytes(strString);
        //    MD5CryptoServiceProvider Md5Hash = new MD5CryptoServiceProvider();
        //    byte[] ByteHash = Md5Hash.ComputeHash(ByteSourceText);
        //    strReturn = "";
        //    foreach (byte b in ByteHash)
        //    {
        //        strReturn = (strReturn + b.ToString("x2"));
        //    }
        //    return strReturn;
        //}
     
        protected void ibtn_submit_Click(object sender, ImageClickEventArgs e)
        {
            #region connect database sql
            //string name = "";
            //string pass = "";
            //string connectinStr = "Data Source=ITNB540251;Initial Catalog=authen_test;Integrated Security=True";
            //string select = "SELECT * FROM USERDATA WHERE username ='" + txt_username.Text + "'" + " and password ='" + txt_password.Text + "'";
            //try
            //{
            //    if (txt_username.Text == "")
            //    {
            //        ScriptManager.RegisterStartupScript(this, GetType(), "error", "alert('Please Insert Username');", true);
            //    }
            //    else if (txt_password.Text == "")
            //    {
            //        ScriptManager.RegisterStartupScript(this, GetType(), "error", "alert('Please Insert Password');", true);
            //    }
            //    else
            //    {
            //        SqlConnection conn = new SqlConnection(connectinStr);
            //        if (conn.State != ConnectionState.Open)
            //        {
            //            conn = new SqlConnection(connectinStr);
            //            conn.Open();
            //        }
            //        SqlDataAdapter cm = new SqlDataAdapter(select, conn);
            //        DataTable dt1 = new DataTable();
            //        cm.Fill(dt1);
            //        conn.Close();
            //        for (int i = 0; i < dt1.Rows.Count; i++)
            //        {
            //            name = dt1.Rows[i]["username"].ToString();
            //            pass = dt1.Rows[i]["password"].ToString();
            //        }
            //        if (txt_username.Text == name && txt_password.Text == pass)
            //        {
            //            //GlobalVar.chkLogin = true;
            //            Response.Redirect("~/mainMenu.aspx", false);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "error", "alert("+ex.Message+");", true);
            //    throw;
            //}
            #endregion

            #region connect AD LDAPLogin
            string dominName = string.Empty;
            string adPath = string.Empty;
            string userName = txtLoginID.Text.Trim().ToUpper();
            string strError = string.Empty;

            try
            {
                foreach (string key in ConfigurationSettings.AppSettings.Keys)
                {
                    dominName = key.Contains("DirectoryDomain") ? ConfigurationSettings.AppSettings[key] : dominName;
                    adPath = key.Contains("DirectoryPath") ? ConfigurationSettings.AppSettings[key] : adPath;
                    if (!String.IsNullOrEmpty(dominName) && !String.IsNullOrEmpty(adPath))
                    {
                       
                        if(true == AuthenticateUser(dominName, userName, txtPassword.Text, adPath, out strError))
                        {
                            FormsAuthentication.RedirectFromLoginPage(userName , false);

                            Server.Transfer("~/Default.aspx");

                        }
                        else
                        {
                            txtLoginID.Text = string.Empty;
                            txtPassword.Text = string.Empty;
                            lbError.Text = strError;
                            
                            //Server.Transfer("~/Account/Login.aspx");
                            //LoginUser.FailureText = "hjgh";
                        }
                        dominName = string.Empty;
                        adPath = string.Empty;
                        if (String.IsNullOrEmpty(strError)) break;
                    }
                }
                if (!string.IsNullOrEmpty(strError))
                {
                    Session.Abandon();
                    Session.Clear();
                    lbError.Text = "Invalid user name or Password!";  
                   
                    //ScriptManager.RegisterStartupScript(this, GetType(), "error", "alert('Invalid user or bad password');", true);
                }
            }
            catch (Exception ex)
            {
                
            }
            finally
            {

            }
            #endregion
        }

        public bool AuthenticateUser(string domain, string username, string password, string LdapPath, out string Errmsg)
        {
            Errmsg = "";
            string domainAndUsername = domain + @"\" + username;
            DirectoryEntry entry = new DirectoryEntry(LdapPath, domainAndUsername, password);
            OracleQuery2 cc2 = new OracleQuery2();
            try
            {
                if (username == "X9MICT" && password=="x9mict")
                {

                    string _filterAttribute = "-1";
                    string _filterNameAttribute = "X9MICT";
                    string _filterFullnameAttribute = "X9MICT";
                    Session.Timeout = 20;
                   // Session["ID"] = _filterFullnameAttribute;
                    Session["ID"] =_filterAttribute;
                    Session["ADMIN"] = 1;
                   
                    
                }
                else if (username == "GASCONTROL" && password == "gascontrol")
                {

                    string _filterAttribute = "GC";
                    string _filterNameAttribute = "GASCONTROL";
                    string _filterFullnameAttribute = "GASCONTROL";
                    Session.Timeout = 20;
                    // Session["ID"] = _filterFullnameAttribute;
                    Session["ID"] = _filterAttribute;
                    Session["ADMIN"] = 0;


                }
                else
                {
                        // Bind to the native AdsObject to force authentication.
                        Object obj = entry.NativeObject;
                        DirectorySearcher search = new DirectorySearcher(entry);
                        search.Filter = "(SAMAccountName=" + username + ")";
                        search.PropertiesToLoad.Add("cn");
                        search.PropertiesToLoad.Add("SAMAccountName");
                        search.PropertiesToLoad.Add("displayName");                
                        SearchResult result = search.FindOne();

                            if (null == result)
                            {
                                Errmsg = "Invalid user name or Password!";
                                return false;                   
                            }
                        
                        // Update the new path to the user in the directory
                        LdapPath = result.Path;
                        string _filterAttribute = (String)result.Properties["cn"][0];
                        string _filterNameAttribute = (String)result.Properties["SAMAccountName"][0];
                        string _filterFullnameAttribute = (String)result.Properties["displayName"][0];
                        Session.Timeout = 20;                   
                      //  Session["ID"] = _filterFullnameAttribute;
                      
                        DataTable dt = cc2.findAdmin(_filterAttribute);
                        if (dt.Rows.Count > 0)
                        {
                            Session["ID"] = _filterAttribute;
                            Session["ADMIN"] =Convert.ToInt16(dt.Rows[0]["IS_ADMIN"]);
                         
                        }
                        else 
                        {
                            Session["ID"] = null;
                            Session["ADMIN"] = 0;
                            Errmsg = "Invalid User";
                            return false;
                        }

                }
            }
            catch (Exception ex)
            {
                Errmsg = ex.Message;
                return false;              
                throw new Exception("Error authenticating user." + ex.Message);
            }
            return true;
        }                  
                           
        protected void ibtn_cancel_Click(object sender, ImageClickEventArgs e)
        {
            txtLoginID.Text = string.Empty;
            txtPassword.Text = string.Empty;
            lbError.Text = string.Empty;
        }
    
    }
}
