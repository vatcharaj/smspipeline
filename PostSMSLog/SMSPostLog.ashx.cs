using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;
using System.Data;
using System.Configuration;
using System.IO;
using System.Xml;

namespace WebApplication1
{
    /// <summary>
    /// Summary description for SMSPostLog
    /// </summary>
    /// 


    public class SMSPostLog : IHttpHandler
    {

  //      private long[][] _privateIps = new long[][] {
  //new long[] {ConvertIPToLong("0.0.0.0"), ConvertIPToLong("2.255.255.255")},
  //new long[] {ConvertIPToLong("10.0.0.0"), ConvertIPToLong("10.255.255.255")},
  //new long[] {ConvertIPToLong("127.0.0.0"), ConvertIPToLong("127.255.255.255")},
  //new long[] {ConvertIPToLong("169.254.0.0"), ConvertIPToLong("169.254.255.255")},
  //new long[] {ConvertIPToLong("172.16.0.0"), ConvertIPToLong("172.31.255.255")},
  //new long[] {ConvertIPToLong("192.0.2.0"), ConvertIPToLong("192.0.2.255")},
  //new long[] {ConvertIPToLong("192.168.0.0"), ConvertIPToLong("192.168.255.255")},
  //new long[] {ConvertIPToLong("255.255.255.0"), ConvertIPToLong("255.255.255.255")}
//};
         public void ProcessRequest(HttpContext context)  {
             
            context.Response.ContentType = "text/plain";
            var cmd = context.Request.Params["CMD"];

            if (cmd == "DLVRREP")
            {
                string SMID = context.Request.Params["SMID"];
                string STA = context.Request.Params["STATUS"];
                string DETAIL = context.Request.Params["DETAIL"];
                //string ip = DetermineIP(context);
                if (STA == "OK")
                {
                    UpdateSentLog(SMID, 1, DETAIL);
                }
                else if (STA == "ERR")
                {
                    UpdateSentLog(SMID, 0, DETAIL);
                }
                string log = "ReqIP:" + 1;
             string resp = @"<XML>
                        <STATUS>OK</STATUS>
                        <DETAIL></DETAIL>
                        </XML>";
                            context.Response.Write(resp);
                LogFile(log);
               
            }
            else {
               // string ip = DetermineIP(context);

                string log = "ReqIP:error "+0 ;
                string resp = @"<XML>
            <STATUS>ERR</STATUS>
            <DETAIL></DETAIL>
            </XML>";
                context.Response.Write(resp);
                LogFile(log);
            }



        }

              private void LogFile(string log)
              {
                  UpdatePstLog(log);
              }

              private void UpdatePstLog(string log)
              {

                  ExecuteProcedureNonquery("SMS_UPDATE_IP_POST",
                   new object[] { log });


              }

          



              //public string DetermineIP(HttpContext context)
              //{
              //    if (context.Request.ServerVariables.AllKeys.Contains("HTTP_CLIENT_IP") && CheckIP(context.Request.ServerVariables["HTTP_CLIENT_IP"]))
              //        return context.Request.ServerVariables["HTTP_CLIENT_IP"];

              //    if (context.Request.ServerVariables.AllKeys.Contains("HTTP_X_FORWARDED_FOR"))
              //        foreach (string ip in context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Split(','))
              //            if (CheckIP(ip.Trim()))
              //                return ip.Trim();

              //    if (context.Request.ServerVariables.AllKeys.Contains("HTTP_X_FORWARDED") && CheckIP(context.Request.ServerVariables["HTTP_X_FORWARDED"]))
              //        return context.Request.ServerVariables["HTTP_X_FORWARDED"];

              //    if (context.Request.ServerVariables.AllKeys.Contains("HTTP_X_CLUSTER_CLIENT_IP") && CheckIP(context.Request.ServerVariables["HTTP_X_CLUSTER_CLIENT_IP"]))
              //        return context.Request.ServerVariables["HTTP_X_CLUSTER_CLIENT_IP"];

              //    if (context.Request.ServerVariables.AllKeys.Contains("HTTP_FORWARDED_FOR") && CheckIP(context.Request.ServerVariables["HTTP_FORWARDED_FOR"]))
              //        return context.Request.ServerVariables["HTTP_FORWARDED_FOR"];

              //    if (context.Request.ServerVariables.AllKeys.Contains("HTTP_FORWARDED") && CheckIP(context.Request.ServerVariables["HTTP_FORWARDED"]))
              //        return context.Request.ServerVariables["HTTP_FORWARDED"];

              //    return context.Request.ServerVariables["REMOTE_ADDR"];
              //}

              //private bool CheckIP(string ip)
              //{
              //    if (!String.IsNullOrEmpty(ip))
              //    {
              //        long ipToLong = -1;
              //        //Is it valid IP address
              //        if (TryConvertIPToLong(ip, out ipToLong))
              //        {
              //            //Does it fall within a private network range
              //            foreach (long[] privateIp in _privateIps)
              //                if ((ipToLong >= privateIp[0]) && (ipToLong <= privateIp[1]))
              //                    return false;
              //            return true;
              //        }
              //        else
              //            return false;
              //    }
              //    else
              //        return false;
              //}
              //private bool TryConvertIPToLong(string ip, out long ipToLong)
              //{
              //    try
              //    {
              //        ipToLong = ConvertIPToLong(ip);
              //        return true;
              //    }
              //    catch
              //    {
              //        ipToLong = -1;
              //        return false;
              //    }
              //}

              //private static long ConvertIPToLong(string ip)
              //{
              //    string[] ipSplit = ip.Split('.');
              //    return (16777216 * Convert.ToInt32(ipSplit[0]) + 65536 * Convert.ToInt32(ipSplit[1]) + 256 * Convert.ToInt32(ipSplit[2]) + Convert.ToInt32(ipSplit[3]));
              //}



            
          

        private void UpdateSentLog(string SMID, int sta, string DETAIL)
        {

          ExecuteProcedureNonquery("SMS_UPDATEPOSTLOG",
           new object[] { SMID,  sta,  DETAIL }); 
       

        }

        public void ExecuteProcedureNonquery(string CommandText, params object[] parameterValues)
        {

            OracleConnection connection = null;
            OracleCommand command = null;
            string resp = "";

            try
            {
                connection = CreateConnection();
                command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = CommandText;
                connection.Open();
                OracleCommandBuilder.DeriveParameters(command);
                int index = 0;
                foreach (OracleParameter parameter in command.Parameters)
                {
                    if (parameter.Direction == ParameterDirection.Input || parameter.Direction == ParameterDirection.InputOutput)
                    {
                        parameter.Value = parameterValues[index];
                        index++;
                    }
                    else
                    {
                        parameter.Value = new object();
                        index++;
                    }
                }
                command.ExecuteNonQuery();



                resp = "success";
            }

            catch (Exception ex)
            {
                resp = "error";
                //throw;
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                    command = null;
                }


                if (connection != null)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    connection.Dispose();
                    connection = null;
                }
            }

        }
        public OracleConnection CreateConnection()
        {
            OracleConnection conn;
            try
            {
                string conns = ConfigurationManager.ConnectionStrings["OracleDatabase2"].ConnectionString;
                conn = new OracleConnection();
                conn.ConnectionString = conns;
            }
            catch
            {
                conn = null;
            }
            return conn;
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
    }
