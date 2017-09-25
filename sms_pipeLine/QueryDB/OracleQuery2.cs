using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Oracle.DataAccess.Client;
using System.Configuration;

namespace sms_pipeLine
{
    public class OracleQuery2
    {
        public DataTable LoadAllGroups()
        {
            DataTable dt = null;
            OracleQuery2 ora = new OracleQuery2();
            dt = ora.ExecuteProcedureToDataTable("SMS_LoadAllGroups",
               new object[] { });

            return dt;

        }
        public DataTable LoadAllGroups2Edit()
        {
            DataTable dt = null;
            OracleQuery2 ora = new OracleQuery2();
            dt = ora.ExecuteProcedureToDataTable("SMS_LoadAllGroups2Edit",
               new object[] { });

            return dt;

        }
        public DataTable LoadCSPPL(string group_id)
        {
            string group = group_id.Substring(0, 1);
            DataTable dt = null;
            OracleQuery2 ora = new OracleQuery2();
            dt = ora.ExecuteProcedureToDataTable("SMS_LoadCSPPL",
               new object[] { group_id });
            return dt;
        }
        public DataTable LoadTCSPPL(string group_id)
        {
            string group = group_id.Substring(0, 1);
            DataTable dt = null;
            OracleQuery2 ora = new OracleQuery2();
            dt = ora.ExecuteProcedureToDataTable("SMS_LoadTCSPPL",
               new object[] { group_id });
            return dt;
           
        }
        public DataTable LoadGCPPL(string group_id)
        {
            string group = group_id.Substring(0, 1);
            DataTable dt = null;
            OracleQuery2 ora = new OracleQuery2();
            dt = ora.ExecuteProcedureToDataTable("SMS_LoadGCPPL",
               new object[] { group_id });
            return dt;
           
        }
        public DataTable LoadPTTPPL(string group_id)
        {
        
            string group = group_id.Substring(0, 1);
            DataTable dt = null;
            OracleQuery2 ora = new OracleQuery2();
            dt = ora.ExecuteProcedureToDataTable("SMS_LoadPTTPPL",
               new object[] { group_id });
            return dt;
        }
        public DataTable LoadPTTBPPL(string group_id)
        {
            
            string group = group_id.Substring(0, 1);
            DataTable dt = null;
            OracleQuery2 ora = new OracleQuery2();
            dt = ora.ExecuteProcedureToDataTable("SMS_LoadPTTBPPL",
               new object[] { group_id });
            return dt;
          
        }
        public DataTable LoadGroup(string group_id)
        {
            
            DataTable dt = null;
            OracleQuery2 ora = new OracleQuery2();
            dt = ora.ExecuteProcedureToDataTable("SMS_LoadGroupByID",
               new object[] { group_id });
            return dt;
        }
        internal DataTable LoadGroupAllCreate(string group_id)
        {
            DataTable dt = null;
            OracleQuery2 ora = new OracleQuery2();
            dt = ora.ExecuteProcedureToDataTable("SMS_LoadGroupAllCreateByID",
               new object[] { group_id });
            return dt;
        }

        public DataTable LoadMonthlyReport()
        {
            DataTable dt = null;
            OracleQuery2 ora = new OracleQuery2();
            dt = ora.ExecuteProcedureToDataTable("SMS_LoadMonthlyReport",
               new object[] { });
            return dt;
        }
        public DataTable LoadReport()
        {
            DataTable dt = null;
            OracleQuery2 ora = new OracleQuery2();
            dt = ora.ExecuteProcedureToDataTable("SMS_LoadReport",
               new object[] {  });
            return dt;

        }
        public DataTable LoadSumReport()
        {
            DataTable dt = null;
            OracleQuery2 ora = new OracleQuery2();
            dt = ora.ExecuteProcedureToDataTable("SMS_LoadSumReport",
               new object[] { });
            return dt;
        }
        public DataTable LoadAllTempl()
        {
            DataTable dt = null;
            OracleQuery2 ora = new OracleQuery2();
            dt = ora.ExecuteProcedureToDataTable("SMS_LoadAllTempl",
               new object[] { });
            return dt;
        }
        public void InsertNewGroup(string Group_id, string Group_name, int level, string master_Group_id,string logname)
        {

            Group_id = Group_id.Substring(3, Group_id.Length - 3);
            master_Group_id = master_Group_id.Substring(3, master_Group_id.Length - 3);
            OracleQuery2 ora = new OracleQuery2();
            ora.ExecuteProcedureNonquery("SMS_InsertNewGroup",
            new object[] { Group_id, Group_name, level, master_Group_id, logname });
        }
        public void DeleteGroup(string Group_id)
        {
            Group_id = Group_id.Substring(3, Group_id.Length - 3);
            OracleQuery2 ora = new OracleQuery2();
            ora.ExecuteProcedureNonquery("SMS_DeleteGroup",
            new object[] { Group_id });
            DeletePPL(Group_id);
        }
        private void DeletePPL(string group_id)
        {
            string group = group_id.Substring(0, 1); 
            OracleQuery2 ora = new OracleQuery2();
            ora.ExecuteProcedureNonquery("SMS_DeletePPL",
            new object[] { group, group_id });
        }
        public DataTable LoadAllDepart()
        {
         
            DataTable dt = null;
            OracleQuery2 ora = new OracleQuery2();
            dt = ora.ExecuteProcedureToDataTable("SMS_LoadAllDepart",
               new object[] { });
            return dt;
        }
        public void DeleteCSPPL(string group_id, string d, string person_id)
        {
            OracleQuery2 ora = new OracleQuery2();
            ora.ExecuteProcedureNonquery("SMS_DeleteCSPPL",
            new object[] { group_id, person_id });
        }
        public void DeleteTESTPPL(string group_id, string person_id)
        {
            OracleQuery2 ora = new OracleQuery2();
            ora.ExecuteProcedureNonquery("SMS_DeleteTESTPPL",
            new object[] { group_id, person_id });
        }
        public void DeleteTCSPPL(string group_id, string d, string person_id)
        {
            string group = group_id.Substring(0, 1);
            OracleQuery2 ora = new OracleQuery2();
            ora.ExecuteProcedureNonquery("SMS_DeleteTCSPPL",
           new object[] { group_id, person_id });

           
        }
        public void DeleteGCPPL(string group_id, string d, string person_id, string company_id)
        {
            string group = group_id.Substring(0, 1);
            OracleQuery2 ora = new OracleQuery2();
            ora.ExecuteProcedureNonquery("SMS_DeleteGCPPL",
           new object[] { group_id, person_id, company_id });
        }
        public DataTable findAdmin(string _filterAttribute)
        {
           
            DataTable dt = null;
            OracleQuery2 ora = new OracleQuery2();
            dt = ora.ExecuteProcedureToDataTable("SMS_findAdmin",
               new object[] { _filterAttribute });
            return dt;
        }
        public DataTable LoadAdmin()
        {
            DataTable dt = null;
            OracleQuery2 ora = new OracleQuery2();
            dt = ora.ExecuteProcedureToDataTable("SMS_LoadAdmin",
               new object[] {  });
            return dt;
        }
        public void DeleteAdmin(string person_id)
        {
            OracleQuery2 ora = new OracleQuery2();
            ora.ExecuteProcedureNonquery("SMS_DeleteAdmin",
           new object[] { person_id }); 
        }
        public string insertAuthor(string EmployeeID, string isadmin)
        {
           
            try
            {
                OracleQuery2 ora = new OracleQuery2();
               string res = ora.ExecuteProcedureNonqueryToString("SMS_insertAuthor",
               new object[] { EmployeeID,  isadmin });
               return res;

            }
            catch (Exception ex)
            {
                return "-1";
            }
       
            
        }
        public string UpdateAuthor(string EmployeeID, string isadmin)
        {

            try
            {
                OracleQuery2 ora = new OracleQuery2();
               string res = ora.ExecuteProcedureNonqueryToString("SMS_UpdateAuthor",
               new object[] { EmployeeID, isadmin }); ;
               return res;

            }
            catch (Exception ex)
            {
                return "-1";
            }
          
            
        }
        public void updateCSPPL(string EmployeeID, string MOBILE)
        {
            OracleQuery2 ora = new OracleQuery2();
            ora.ExecuteProcedureNonquery("SMS_updateCSPPL",
           new object[] { EmployeeID, MOBILE }); ;
        }
        public void updateGCPPL(string EmployeeID,string Name,string company_id,string MOBILE,string posname,string longinName)
        {
            OracleQuery2 ora = new OracleQuery2();
            ora.ExecuteProcedureNonquery("SMS_updateGCPPL",
           new object[] { EmployeeID, Name, company_id, MOBILE, posname, longinName }); ;
          
        }
        public void UpdateTCSPPL(string EmployeeID, string MOBILE,string logname)
        {
            OracleQuery2 ora = new OracleQuery2();
            ora.ExecuteProcedureNonquery("SMS_UpdateTCSPPL",
           new object[] { EmployeeID, MOBILE, logname }); ;
        }
        public void UpdatePTTPPL(string EmployeeID, string MOBILE, string loginname)
        {
            OracleQuery2 ora = new OracleQuery2();
            ora.ExecuteProcedureNonquery("SMS_UpdatePTTPPL",
           new object[] { EmployeeID, MOBILE, loginname }); 
        }
        public string InsertPTTPPL(string EmployeeID, string MOBILE,string loginname)
        {
          
            try
            {
                    OracleQuery2 ora = new OracleQuery2();
                    string resp= ora.ExecuteProcedureNonqueryToString("SMS_InsertPTTPPL",
                    new object[] { EmployeeID, MOBILE, loginname }); 
                    return resp;
            }
            catch (Exception ex)
            {
                return "-1";
            }

           
        }
        public void DeleteTCSPPLFromAllGroup(string person_id)
        {
            OracleQuery2 ora = new OracleQuery2();
            ora.ExecuteProcedureNonquery("SMS_DeleteTCSPPLFromAllGroup",
           new object[] { person_id}); 
           
        }
        public void InsertTCSService(string id, string EmployeeID)
        {
            OracleQuery2 ora = new OracleQuery2();
            ora.ExecuteProcedureNonquery("SMS_InsertTCSService",
           new object[] {id, EmployeeID}); 
 
        }
        public void DeleteCSPPLFromAllGroup(string person_id)
        {
            OracleQuery2 ora = new OracleQuery2();
            ora.ExecuteProcedureNonquery("SMS_DeleteCSPPLFromAllGroup",
           new object[] { person_id }); 
        }
        public void InsertCSService(string id, string EmployeeID, string company_id)
        {
            OracleQuery2 ora = new OracleQuery2();
            ora.ExecuteProcedureNonquery("SMS_InsertCSService",
           new object[] { id,  EmployeeID,  company_id }); 
        }
        public void DeleteGCPPLFromAllGroup(string person_id, string company_id)
        {

            OracleQuery2 ora = new OracleQuery2();
            ora.ExecuteProcedureNonquery("SMS_DeleteGCPPLFromAllGroup",
           new object[] { person_id,  company_id }); 
 
        }
        public void InsertGCService(string id, string EmployeeID, string company_id)
        {
            OracleQuery2 ora = new OracleQuery2();
            ora.ExecuteProcedureNonquery("SMS_InsertGCService",
           new object[] { id, EmployeeID,  company_id }); 

        }
        public string InsertPTTBPPL(string poscode, string MOBILE,string logname,string unitcode ,string vcode)
        {
            try
            {
                OracleQuery2 ora = new OracleQuery2();
               string res= ora.ExecuteProcedureNonqueryToString("SMS_InsertPTTBPPL",
               new object[] { poscode, MOBILE, logname,unitcode ,vcode });
               return res;
            }
            catch (Exception ex)
            {
                return "-1";
            }
        
            
        }
        public void UpdatePTTBPPL(string EmployeeID, string MOBILE,string logname)
        {
            OracleQuery2 ora = new OracleQuery2();
            ora.ExecuteProcedureNonquery("SMS_UpdatePTTBPPL",
           new object[] { EmployeeID, MOBILE, logname }); 
        }
        public void DeletePTTBPPL(string group_id, string d, string code)
        {

            OracleQuery2 ora = new OracleQuery2();
            ora.ExecuteProcedureNonquery("SMS_DeletePTTBPPL",
           new object[] { group_id,   code });    
        }
        public void DeletePTTBPPLFromAllGroup(string code)
        {
            OracleQuery2 ora = new OracleQuery2();
            ora.ExecuteProcedureNonquery("SMS_DeletePTTBPPLFromAllGroup",
           new object[] { code });    

        }
        public void InsertPTTService(string id, string EmployeeID)
        {
            OracleQuery2 ora = new OracleQuery2();
            ora.ExecuteProcedureNonquery("SMS_InsertPTTService",
           new object[] { id,  EmployeeID });    
           
        }
        public void InsertPTTBService(string id, string code)
        {
  
            OracleQuery2 ora = new OracleQuery2();
            ora.ExecuteProcedureNonquery("SMS_InsertPTTBService",
           new object[] { id, code });    

          
        }
        public void DeletePTTPPL(string group_id, string d, string person_id)
        {
            OracleQuery2 ora = new OracleQuery2();
            ora.ExecuteProcedureNonquery("SMS_DeletePTTPPL",
           new object[] {group_id, d, person_id });    
        }
        public void DeletePTTPPLFromAllGroup(string person_id)
        {
            OracleQuery2 ora = new OracleQuery2();
            ora.ExecuteProcedureNonquery("SMS_DeletePTTPPLFromAllGroup",
           new object[] {  person_id }); 
        }
        public DataTable LoadCSPPL()
        {
            DataTable dt = null;
            OracleQuery2 ora = new OracleQuery2();
            dt = ora.ExecuteProcedureToDataTable("SMS_LoadAllCSPPL",
               new object[] {  });
            return dt;
        }
        public DataTable LoadTCSPPL()
        {  
            DataTable dt = null;
            OracleQuery2 ora = new OracleQuery2();
            dt = ora.ExecuteProcedureToDataTable("SMS_LoadAllTCSPPL",
               new object[] { });
            return dt;
           
        }
        public DataTable LoadGCPPL()
        {
            DataTable dt = null;
            OracleQuery2 ora = new OracleQuery2();
            dt = ora.ExecuteProcedureToDataTable("SMS_LoadAllGCPPL",
               new object[] { });
            return dt; 
        }
        public DataTable LoadPTTPPL()
        {          
            DataTable dt = null;
            OracleQuery2 ora = new OracleQuery2();
            dt = ora.ExecuteProcedureToDataTable("SMS_LoadAllPTTPPL",
               new object[] { });
            return dt; 
          
        }
        public DataTable LoadPTTBPPL()
        {

            DataTable dt = null;
            OracleQuery2 ora = new OracleQuery2();
            dt = ora.ExecuteProcedureToDataTable("SMS_LoadAllPTTBPPL",
               new object[] { });
            return dt; 
          
        }
        public DataTable LoadTCSGroup(string EmployeeID)
        {

            DataTable dt = null;
            OracleQuery2 ora = new OracleQuery2();
            dt = ora.ExecuteProcedureToDataTable("SMS_LoadTCSGroup",
               new object[] {EmployeeID });
            return dt; 
           
        }
        public DataTable LoadCSGroup(string EmployeeID)
        {
            DataTable dt = null;
            OracleQuery2 ora = new OracleQuery2();
            dt = ora.ExecuteProcedureToDataTable("SMS_LoadCSGroup",
               new object[] { EmployeeID });
            return dt; 
        }
        public DataTable LoadGCGroup(string EmployeeID, string company_id)
        {
            DataTable dt = null;
            OracleQuery2 ora = new OracleQuery2();
            dt = ora.ExecuteProcedureToDataTable("SMS_LoadGCGroup",
               new object[] { EmployeeID, company_id });
            return dt; 
        }
        public DataTable LoadToAddCSPPL(string sch)
        {
            DataTable dt = null;
            OracleQuery2 ora = new OracleQuery2();
            dt = ora.ExecuteProcedureToDataTable("SMS_LoadToAddCSPPL",
               new object[] { sch });
            return dt; 
        }
        public DataTable LoadToAddGCPPL(string sch)
        {
            DataTable dt = null;
            OracleQuery2 ora = new OracleQuery2();
            dt = ora.ExecuteProcedureToDataTable("SMS_LoadToAddGCPPL",
               new object[] { sch });
            return dt; 
        }
        public DataTable LoadToAddTCSPPL(string sch)
        {
            DataTable dt = null;
            OracleQuery2 ora = new OracleQuery2();
            dt = ora.ExecuteProcedureToDataTable("SMS_LoadToAddTCSPPL",
               new object[] { sch });
            return dt; 
        }
        public DataTable LoadPTTGroup(string EmployeeID,string unitcode)
        {
            DataTable dt = null;
            OracleQuery2 ora = new OracleQuery2();
            dt = ora.ExecuteProcedureToDataTable("SMS_LoadPTTGroup2",
               new object[] { EmployeeID, unitcode });
            return dt; 
        }
        public DataTable LoadPTTBGroup(string code,string unitcode)
        {

            DataTable dt = null;
            OracleQuery2 ora = new OracleQuery2();
            dt = ora.ExecuteProcedureToDataTable("SMS_LoadPTTBGroup2",
               new object[] { code, unitcode });
            return dt; 
        }
        public DataTable LoadMobileInGC(string GC_str)
        {
            DataTable dt = null;
            OracleQuery2 ora = new OracleQuery2();
            dt = ora.ExecuteProcedureToDataTable("SMS_LoadMobileInGC",
               new object[] { GC_str });
            return dt; 
        }
        public DataTable LoadMobileInCS(string CS_str)
        {
            DataTable dt = null;
            OracleQuery2 ora = new OracleQuery2();
            dt = ora.ExecuteProcedureToDataTable("SMS_LoadMobileInCS",
               new object[] { CS_str });
            return dt; 
        }
        public DataTable LoadMobileInTCS(string TCS_str)
        {

            DataTable dt = null;
            OracleQuery2 ora = new OracleQuery2();
            dt = ora.ExecuteProcedureToDataTable("SMS_LoadMobileInTCS",
               new object[] { TCS_str });
            return dt; 
         
        }
        public DataTable LoadMobileInTest(string str)
        {
            DataTable dt = null;
            OracleQuery2 ora = new OracleQuery2();
            dt = ora.ExecuteProcedureToDataTable("SMS_LoadMobileInTest",
               new object[] { str });
            return dt; 
          
        }
        public DataTable LoadMobileInPTT(string PTT_str)
        {

            DataTable dt = null;
            OracleQuery2 ora = new OracleQuery2();
            dt = ora.ExecuteProcedureToDataTable("SMS_LoadMobileInPTT",
               new object[] { PTT_str });
            return dt; 
        }
        public DataTable LoadMobileInPTTB(string PTTB_str)
        {
 
            DataTable dt = null;
            OracleQuery2 ora = new OracleQuery2();
            dt = ora.ExecuteProcedureToDataTable("SMS_LoadMobileInPTTB",
               new object[] { PTTB_str });
            return dt; 
           
        }
        public DataTable LoadAllGCUnit()
        {
            DataTable dt = null;
            OracleQuery2 ora = new OracleQuery2();
            dt = ora.ExecuteProcedureToDataTable("SMS_LoadALLGCUnit",
               new object[] {  });
            return dt; 
        }
        public DataTable LoadDisplayGroup(string st)
        {
            DataTable dt = null;
            OracleQuery2 ora = new OracleQuery2();
            dt = ora.ExecuteProcedureToDataTable("SMS_LoadDisplayGroup",
               new object[] { st });
            return dt; 
        }
        public void InsertGCPPL(string EmployeeID, string Name, string company_id, string MOBILE, string posname,string logname)
        {
           
            OracleQuery2 ora = new OracleQuery2();
           ora.ExecuteProcedureNonquery("SMS_InsertGCPPL",
               new object[] { EmployeeID, Name, company_id, MOBILE, posname, logname });
           
        }
        public void updateDisplayGroup(string st, string chk, int vsh,string logname)
        {
            OracleQuery2 ora = new OracleQuery2();
            ora.ExecuteProcedureNonquery("SMS_updateDisplayGroup",
           new object[] { st, chk, vsh, logname }); 
        }
        public void UpdateGroup(string grpid, string grpname, string logname)
        {
            OracleQuery2 ora = new OracleQuery2();
            ora.ExecuteProcedureNonquery("SMS_UpdateGroup",
           new object[] { grpid, grpname,  logname }); 
        }
        public void insertLog(string Employee_ID, string Department_ID, string TO_Original, string content, string loginName, int status, string DETAIL, string SMID,string groupid,string name,string unit)
        {
            OracleQuery2 ora = new OracleQuery2();
            ora.ExecuteProcedureNonquery("SMS_INSERTLOG",
           new object[] { Employee_ID, Department_ID, TO_Original, content, loginName, status, DETAIL, SMID, groupid, name, unit }); 
        }
        public DataTable LoadAllSMSReport(DateTime st_date, DateTime end_date, string groupid)
        {
            DataTable dt = null;
            OracleQuery2 ora = new OracleQuery2();
            dt = ora.ExecuteProcedureToDataTable("SMS_LoadAllSMSReport",
               new object[] {st_date,  end_date,  groupid });
            return dt; 
        }
        public DataTable LoadSMSReport(DateTime st_date, DateTime end_date, int p, string groupid)
        {
            DataTable dt = null;
            OracleQuery2 ora = new OracleQuery2();
            dt = ora.ExecuteProcedureToDataTable("SMS_LoadSMSReport",
               new object[] { st_date,  end_date,  p,  groupid });
            return dt; 
        }
        public DataTable LoadManualSMSReport(DateTime st_date, DateTime end_date)
        {
            DataTable dt = null;
            OracleQuery2 ora = new OracleQuery2();
            dt = ora.ExecuteProcedureToDataTable("SMS_LoadManualSMSReport",
               new object[] { st_date, end_date });
            return dt;
        }

        public void UpdateLog(int sta, string DETAIL, string SMID, string logid)
        {
            OracleQuery2 ora = new OracleQuery2();
            ora.ExecuteProcedureNonquery("SMS_updatelog",
           new object[] { sta,  DETAIL,  SMID,  logid }); 
        }
        public void UpdateLogMobile(string mobile, string logid)
        {
            OracleQuery2 ora = new OracleQuery2();
            ora.ExecuteProcedureNonquery("SMS_UpdateLogMobile",
           new object[] {mobile,  logid }); 
        }
        public void insertTempl(string text, string loginName)
        {
            OracleQuery2 ora = new OracleQuery2();
            ora.ExecuteProcedureNonquery("SMS_INSERTTempl",
           new object[] { text, loginName });
        }
        public void DeleteGTempl(string id)
        {
            OracleQuery2 ora = new OracleQuery2();
            ora.ExecuteProcedureNonquery("SMS_DeleteTempl",
           new object[] { id });
        }

        public void UpdateTempl(string text, string id, string loginName)
        {
            OracleQuery2 ora = new OracleQuery2();
            ora.ExecuteProcedureNonquery("SMS_UpdateTempl",
           new object[] { text, id, loginName });
        }

        public DataTable LoadAllTempAIS()
        {
            DataTable dt = null;
            OracleQuery2 ora = new OracleQuery2();
            dt = ora.ExecuteProcedureToDataTable("smstemplateall",
               new object[] { });
            DataTable dtt = new DataTable();
            DataColumn colString = new DataColumn("StringCol");
            colString.DataType = System.Type.GetType("System.String");
            dtt.Columns.Add(colString);
            DataColumn colString2 = new DataColumn("StringCol2");
            colString2.DataType = System.Type.GetType("System.String");
            dtt.Columns.Add(colString2);
            dtt.Rows.Add("081".ToString(),"081".ToString());
            return dt;

        }
        
        #region classforstore

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
        public string ExecuteProcedureNonqueryToString(string CommandText, params object[] parameterValues)
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



                resp = "0";
            }

            catch (Exception ex)
            {
                resp = "-1";
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
            
            return resp;
        }
        public DataTable ExecuteProcedureToDataTable(string CommandText, params object[] parameterValues)
        {
            OracleConnection connection = null;
            OracleCommand command = null;
            OracleDataAdapter adapter = null;
            DataTable dt = null;
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
                //command.ExecuteNonQuery();

                adapter = new OracleDataAdapter(command);
                dt = new DataTable();
                adapter.Fill(dt);

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

                if (adapter != null)
                {
                    adapter.Dispose();
                    adapter = null;
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
            return dt;
        }
        public DataSet ExecuteProcedureToDataSet(string CommandText, params object[] parameterValues)
        {
            OracleConnection connection = null;
            OracleCommand command = null;
            OracleDataAdapter adapter = null;
            DataSet ds = null;
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
                //command.ExecuteNonQuery();

                adapter = new OracleDataAdapter(command);
                ds = new DataSet();
                adapter.Fill(ds);

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

                if (adapter != null)
                {
                    adapter.Dispose();
                    adapter = null;
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
            return ds;
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

        public DataSet ExecuteProcedureToDataSet2(string CommandText, params object[] parameterValues)
        {
            OracleConnection connection = null;
            OracleCommand command = null;
            OracleDataAdapter adapter = null;
            DataSet ds = null;
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
                //command.ExecuteNonQuery();

                adapter = new OracleDataAdapter(command);
                ds = new DataSet();
                adapter.Fill(ds);

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

                if (adapter != null)
                {
                    adapter.Dispose();
                    adapter = null;
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
            return ds;
        }
        #endregion



    }
}