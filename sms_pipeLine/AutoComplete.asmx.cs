using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;



namespace sms_pipeLine
{
    /// <summary>
    /// Summary description for AutoComplete
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class AutoComplete : System.Web.Services.WebService
    {

        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public string[] GetList(string prefixText, int count)
        {

            string[] results = { "test", "wesdf", "fdsfs" };
            return results;



        }
     
    }
}
