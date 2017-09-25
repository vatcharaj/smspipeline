using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Text;
using System.Net;
using System.IO;
using System.Xml;

namespace sms_pipeLine
{
    public class SendSMS
    {
        public string sendSMS_webPost(DataTable ds, string content,string sendername, string loginName)
        {
            DateTime dtNow = System.DateTime.Now;
            string sender = ConfigurationSettings.AppSettings["ServiceMode"].ToString();
            string strHeader = dtNow.ToString("yyyy-MM-dd HH:mm:ss") + " " + sender;
            string SMSGatewaySever = ConfigurationSettings.AppSettings["SMSGatewaySever"].ToString();
            int SMSGatewaySeverPort = Int32.Parse(ConfigurationSettings.AppSettings["SMSGatewaySeverPort"]);
            string url = ConfigurationSettings.AppSettings["SMSGatewaySever_url"].ToString();
            //string SMSGatewaySever = "203.170.230.170";
            //int SMSGatewaySeverPort = 2257;
            byte[] data = new byte[1024];
            string str = string.Empty;
            string TRANSID = "BULK";
            string FROM = "GasControl";
            string TO = "66896213113";
            string CONTENT = "Test from automation" + strHeader; // MSG;
            string error = "";
            OracleQuery2 cc2 = new OracleQuery2();
            int counttotal=0, countsuc=0, counterr = 0;
            #region sendSMS
            if (ds != null && ds.Rows.Count > 0)
            {

                content = content.Trim() + " " + sendername;
                    CONTENT = ConvertToHex(content);
                  
                    for (int i = 0; i < ds.Rows.Count; i++)
                    {
                         string Employee_ID = ds.Rows[i]["EMPLOYEE_ID"].ToString();
                         string TO_Original = ds.Rows[i]["MOBILE"].ToString();
                         string Department_ID = ds.Rows[i]["Department_ID"].ToString();
                         string GROUP_ID = ds.Rows[i]["GROUP_ID"].ToString();
                         string name = ds.Rows[i]["NAME"].ToString();
                        string unit = ds.Rows[i]["COMPANY"].ToString();
                          TO = CleanNumber(TO_Original);
                        if (!string.IsNullOrEmpty(TO))
                        {
                            counttotal = counttotal + 1;

                          
                            try
                            {
                             

                                string _data = String.Format("TRANSID={0}&CMD=SENDMSG&FROM={1}&TO={2}&REPORT=Y&CHARGE=N&CODE=PTTNGV_BulkSMS&CTYPE=UNICODE&CONTENT={3}"
                                    , TRANSID, FROM, TO, CONTENT
                                    );
                             
                                string response = SendPost(url, _data);
                                  XmlDocument xDoc = new XmlDocument();
//                                            xDoc.LoadXml(@"<XML>
//                                                            <STATUS>OK</STATUS>
//                                                            <DETAIL></DETAIL>
//                                                            <SMID>1501019168651</SMID>
//                                                            </XML>");
                                          xDoc.LoadXml(response);
                                            string xpath = "XML";
                                            var nodes = xDoc.SelectNodes(xpath);
                                            foreach (XmlNode childrenNode in nodes)
                                            {
                                                string status = childrenNode.SelectSingleNode("//STATUS").InnerText;
                                                string DETAIL = childrenNode.SelectSingleNode("//DETAIL").InnerText;
                                                string SMID = childrenNode.SelectSingleNode("//SMID").InnerText;
                                                LogToFile("LOGDATE:" + DateTime.Now + ": " + status + " " + DETAIL + " " + SMID + "," + TO);
                                                if (status == "OK")
                                                {
                                                    countsuc = countsuc + 1;
                                                    cc2.insertLog(Employee_ID, Department_ID, TO_Original, content, loginName, 1, DETAIL, SMID, GROUP_ID, name, unit);
                                                }
                                                else if (SMID !="")
                                                {
                                                    countsuc = countsuc + 1;
                                                    cc2.insertLog(Employee_ID, Department_ID, TO_Original, content, loginName, 1, DETAIL, SMID, GROUP_ID, name, unit);
                                                }
                                                else
                                                {
                                                    counterr = counterr + 1;
                                                    cc2.insertLog(Employee_ID, Department_ID, TO_Original, content, loginName, 0, DETAIL, SMID, GROUP_ID, name, unit);
                                                }

                                            }

                                //LogToFile( response+"," + TO);

                              
                              

                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Unable to connect to server." + e.ToString());
                                str = String.Format("{0}|ERR" + "|Connect server error||", e.Message.ToString());
                                LogToFile("LOGDATE:" + DateTime.Now +": "+ " Error connect  " + str + " " + DateTime.Now);
                                //เขียน event log เพิ่มเติมเ
                                //   this.LOG(stationid, str, "");
                                // turn off event log

                                if (!System.Diagnostics.EventLog.SourceExists("SMS_Auto"))
                                {
                                    System.Diagnostics.EventLog.CreateEventSource("SMS_Auto", "MyLog");
                                }
                                // configure the event log instance to use this source name
                                error += "fail:" + TO + ";";
                                //LogToFile(e.ToString() + " " + DateTime.Now);
                            }
                        }
                    
                }
            }
            if (counterr == 0)
                return "1";
            else
                return error+" TOTAL : "+ counttotal +" SUCCESS : " + countsuc +" FAIL: "+ counterr;
            #endregion
          
        }

        private string ConvertToHex(string content)
        {
            var dfds = content.Select(c => ((int)c).ToString("X")).ToArray();
            //string hex = string.Join(string.Empty, CONTENT.Select(c => ((int)c).ToString("X")));
            StringBuilder temp_con = new StringBuilder();

            for (int k = 0; k < dfds.Count(); k++)
            {
                if (dfds[k].StartsWith("E"))
                {
                    temp_con.Append("%0E%" + dfds[k].TrimStart('E'));
                }
                else
                {
                    temp_con.Append("%00%" + dfds[k]);
                }

            }
           return temp_con.ToString();

        }

        private string CleanNumber(string TO)
        {
            
            string rs = "";
            if (!string.IsNullOrEmpty(TO)) {

                TO=  TO.Replace(@"\", string.Empty);
                TO= TO.Replace(@"-", string.Empty);
                TO= TO.Replace(@" ", string.Empty);
                TO=  TO.Replace(@"+", string.Empty);
                TO = TO.Replace(@",", string.Empty);
              
                 if (TO.StartsWith("0"))
                {
                    TO = TO.TrimStart('0');
                    TO = "66" + TO;
                }
                else if (TO.StartsWith("66"))
                {
                    //do nothing
                }
                else {
                    TO = "66" + TO;
                }

                 if (TO.Length != 11)
                     TO = "x";
                rs = TO;
            }
            return rs;

        }

        private static void LogToFile(string TextToWrite)
        {
             string logfile = ConfigurationSettings.AppSettings["SMSLOGFILE"].ToString();
             File.WriteAllText(logfile, String.Empty);
             File.AppendAllText(logfile, TextToWrite + Environment.NewLine);

        }

        private static void UpdateSMSstatus(string site_code, string tank_id, string alarm_type_id, DateTime startTime)
        {
            throw new NotImplementedException();
        }
        private static string SendPost(string url, string postData)
        {
            string webpageContent, webstring = string.Empty;

            try
            {

                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-urlencoded";
           
                using (var writer = new StreamWriter(webRequest.GetRequestStream()))
                {
                    writer.WriteLine(postData);
                }

                using (HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(webResponse.GetResponseStream()))
                    {
                        webpageContent = reader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                LogToFile("LOGDATE:" + DateTime.Now + ": " + ex.ToString() + " " + DateTime.Now);
                throw ex;
            }
        
            return webpageContent;
        }

        public void RetrySend(string mobile, string msg, string logid)
        {
            string sender = ConfigurationSettings.AppSettings["ServiceMode"].ToString();
            string SMSGatewaySever = ConfigurationSettings.AppSettings["SMSGatewaySever"].ToString();
            int SMSGatewaySeverPort = Int32.Parse(ConfigurationSettings.AppSettings["SMSGatewaySeverPort"]);
            string url = ConfigurationSettings.AppSettings["SMSGatewaySever_url"].ToString();
            //string SMSGatewaySever = "203.170.230.170";
            //int SMSGatewaySeverPort = 2257;
            byte[] data = new byte[1024];
            string str = string.Empty;
            string TRANSID = "BULK";
            string FROM = "GasControl";
            string TO = "66896213113";
            string CONTENT = "Test from automation" ; // MSG;
            string error = "";
            OracleQuery2 cc2 = new OracleQuery2();
             TO = CleanNumber(mobile);
             CONTENT = ConvertToHex(msg);
            if (!string.IsNullOrEmpty(TO))
            {



                try
                {


                    string _data = String.Format("TRANSID={0}&CMD=SENDMSG&FROM={1}&TO={2}&REPORT=Y&CHARGE=N&CODE=PTTNGV_BulkSMS&CTYPE=UNICODE&CONTENT={3}"
                        , TRANSID, FROM, TO, CONTENT
                        );

                    string response = SendPost(url, _data);
                    XmlDocument xDoc = new XmlDocument();
                    xDoc.LoadXml(response);
                    string xpath = "XML";
                    var nodes = xDoc.SelectNodes(xpath);
                    foreach (XmlNode childrenNode in nodes)
                    {
                        string status = childrenNode.SelectSingleNode("//STATUS").InnerText;
                        string DETAIL = childrenNode.SelectSingleNode("//DETAIL").InnerText;
                        string SMID = childrenNode.SelectSingleNode("//SMID").InnerText;
                        LogToFile("LOGDATE:" + DateTime.Now + ": " + status + " " + DETAIL + " " + SMID + "," + TO);
                        if (status == "OK")
                        {
                            cc2.UpdateLog(1, DETAIL, SMID, logid);
                        }
                        else
                        {
                            error += "fail:" + TO + ";";
                            cc2.UpdateLog(0, DETAIL, SMID, logid);
                        }

                    }

                    //LogToFile( response+"," + TO);




                }
                catch (Exception e)
                {
                    Console.WriteLine("Unable to connect to server." + e.ToString());
                    str = String.Format("{0}|ERR" + "|Connect server error||", e.Message.ToString());
                    LogToFile("LOGDATE:" + DateTime.Now + ": " + " Error connect  " + str + " " + DateTime.Now);
                    //เขียน event log เพิ่มเติมเ
                    //   this.LOG(stationid, str, "");
                    // turn off event log

                    if (!System.Diagnostics.EventLog.SourceExists("SMS_Auto"))
                    {
                        System.Diagnostics.EventLog.CreateEventSource("SMS_Auto", "MyLog");
                    }
                    // configure the event log instance to use this source name
                    error += "fail:" + TO + ";";
                    //LogToFile(e.ToString() + " " + DateTime.Now);
                }
            }
        }
    }
}