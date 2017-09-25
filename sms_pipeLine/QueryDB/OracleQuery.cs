using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.OleDb;
using System.Data;

namespace sms_pipeLine
{
    public class OracleQuery
    {
        public string conn = ConfigurationManager.ConnectionStrings["OracleDatabase"].ConnectionString;
        public OleDbConnection OraConn = new OleDbConnection();


//        public DataTable LoadAllGroups()
//        {
//            OleDbCommand OleComm = new OleDbCommand();
//            OraConn.ConnectionString = conn;
//            OleComm.Connection = OraConn;
//            string query = @"SELECT 
//   a.GROUP_ID, a.GROUP_NAME, c.NAME as DEPARTMENT,
//   a.DEPARTMENT_ID, a.GROUP_LEVEL ,a.MASTER_GROUP_ID,b.GROUP_NAME as master_name
//   , c.NAME || ' '||b.GROUP_NAME|| ' '||a.GROUP_NAME as keyword
//FROM SMSPIPE.SMS_PIPELINE_GROUP a
//join SMS_PIPELINE_DEPART c on a.DEPARTMENT_ID=c.ID
//left join SMSPIPE.SMS_PIPELINE_GROUP b on a.MASTER_GROUP_ID = b.group_id
//order by  a.MASTER_GROUP_ID,a.GROUP_ID ";
//            DataTable dt = new DataTable();
//            OraConn.Open();
//            OleDbDataAdapter da = new OleDbDataAdapter(query, OraConn);
//            if (da != null)
//                da.Fill(dt);
//            else
//                dt = null;

//            OraConn.Close();
//            return dt;

//        }
//        public DataTable LoadCSPPL(string group_id)
//        {
//            OleDbCommand OleComm = new OleDbCommand();
//            OraConn.ConnectionString = conn;
//            OleComm.Connection = OraConn;
//            string query = "";
//            string group = group_id.Substring(0, 1);

//            query = @"select b.PERSON_ID,TITLE||FNAME||' '||LNAME as NAME ,POSITION , CUST_NAME as COMPANY ,MOBILE ,'400' as group_key
//FROM SMSPIPELINE_CSSERVICE a
//join SMSPIPELINE_CSPPL b on a.PERSON_ID=b.PERSON_ID
//where GROUP_ID=" + group_id + " order by PERSON_ID";

//            DataTable dt = new DataTable();
//            OraConn.Open();
//            OleDbDataAdapter da = new OleDbDataAdapter(query, OraConn);
//            if (da != null)
//                da.Fill(dt);
//            else
//                dt = null;

//            OraConn.Close();
//            return dt;
//        }
//        public DataTable LoadTCSPPL(string group_id)
//        {
//            OleDbCommand OleComm = new OleDbCommand();
//            OraConn.ConnectionString = conn;
//            OleComm.Connection = OraConn;
//            string query = "";
//            string group = group_id.Substring(0, 1);

//            query = @"select b.EMPLOYEE_ID,TITLE||FIRSTNAME||' '||LASTNAME as NAME ,POSITION , COMPANY_NAME as COMPANY,COMPANY_ID ,MOBILE,'500' as group_key
//FROM SMSPIPELINE_TCSSERVICE a
//join SMSPIPELINE_TCSPPL b on a.EMPLOYEE_ID=b.EMPLOYEE_ID  and a.COMPANY_ID=b.CUST_ID
//join  SMS_PIPELINE_TCSCOM com on b.cust_id = com.company_id
//where GROUP_ID= " + group_id + " order by b.EMPLOYEE_ID";

//            DataTable dt = new DataTable();
//            OraConn.Open();
//            OleDbDataAdapter da = new OleDbDataAdapter(query, OraConn);
//            if (da != null)
//                da.Fill(dt);
//            else
//                dt = null;

//            OraConn.Close();
//            return dt;
//        }
//        public DataTable LoadGCPPL(string group_id)
//        {
//            OleDbCommand OleComm = new OleDbCommand();
//            OraConn.ConnectionString = conn;
//            OleComm.Connection = OraConn;
//            string query = "";
//            string group = group_id.Substring(0, 1);

//            query = @" select b.EMPLOYEE_ID,NAME as NAME ,POSITION , COMPANY_NAME as COMPANY,COMPANY_ID ,MOBILE,'300' as group_key
//FROM SMSPIPELINE_GCSERVICE a
//join SMSPIPELINE_GCPPL b on a.EMPLOYEE_ID=b.EMPLOYEE_ID and b.company_id = a.company_id
//join  SMSPIPE.SMSPIPELINE_GCCOM com on b.company_id = com.company_id
//where GROUP_ID= '" + group_id + "' order by b.EMPLOYEE_ID";

//            DataTable dt = new DataTable();
//            OraConn.Open();
//            OleDbDataAdapter da = new OleDbDataAdapter(query, OraConn);
//            if (da != null)
//                da.Fill(dt);
//            else
//                dt = null;

//            OraConn.Close();
//            return dt;
//        }
//        public DataTable LoadPTTPPL(string group_id)
//        {
//            OleDbCommand OleComm = new OleDbCommand();
//            OraConn.ConnectionString = conn;
//            OleComm.Connection = OraConn;
//            string query = "";
//            string group = group_id.Substring(0, 1);

//            query = @"select a.EMPLOYEE_ID,a.group_id,b.mobile
//from SMSPIPE.SMSPIPELINE_PTTSERVICE a
//left join SMSPIPE.SMSPIPELINE_PTTPPL b on a.EMPLOYEE_ID=b.EMPLOYEE_ID 
//where group_id ='" + group_id + "'";

//            DataTable dt = new DataTable();
//            OraConn.Open();
//            OleDbDataAdapter da = new OleDbDataAdapter(query, OraConn);
//            if (da != null)
//                da.Fill(dt);
//            else
//                dt = null;

//            OraConn.Close();
//            return dt;
//        }
//        public DataTable LoadPTTBPPL(string group_id)
//        {
//            OleDbCommand OleComm = new OleDbCommand();
//            OraConn.ConnectionString = conn;
//            OleComm.Connection = OraConn;
//            string query = "";
//            string group = group_id.Substring(0, 1);

//            query = @"select a.POSCODE,a.group_id,b.mobile
//from   SMSPIPE.SMSPIPELINE_PTTBSERVICE a
//left join SMSPIPE.SMSPIPELINE_PTTBPPL b on  a.POSCODE =b.POSCODE
//where group_id ='" + group_id + "'";

//            DataTable dt = new DataTable();
//            OraConn.Open();
//            OleDbDataAdapter da = new OleDbDataAdapter(query, OraConn);
//            if (da != null)
//                da.Fill(dt);
//            else
//                dt = null;

//            OraConn.Close();
//            return dt;
//        }
//        public DataTable LoadGroup(string group_id)
//        {
//            OleDbCommand OleComm = new OleDbCommand();
//            OraConn.ConnectionString = conn;
//            OleComm.Connection = OraConn;
//            string query = @" SELECT 
//   a.GROUP_ID, a.GROUP_NAME,b.name as DEPARTMENT, 
//   a.DEPARTMENT_ID, a.GROUP_LEVEL, a.MASTER_GROUP_ID
//FROM SMSPIPE.SMS_PIPELINE_GROUP a
//join SMS_PIPELINE_DEPART b on b.id=a.DEPARTMENT_ID
//where  a.MASTER_GROUP_ID =" + group_id +
//" order by a.GROUP_ID";
//            DataTable dt = new DataTable();
//            OraConn.Open();
//            OleDbDataAdapter da = new OleDbDataAdapter(query, OraConn);
//            if (da != null)
//                da.Fill(dt);
//            else
//                dt = null;

//            OraConn.Close();
//            return dt;
//        }
//        public DataTable LoadReport()
//        {
//            OleDbCommand OleComm = new OleDbCommand();
//            OraConn.ConnectionString = conn;
//            OleComm.Connection = OraConn;
//            string query = @"SELECT 
//    REPORT_ID, REPORT_NAME
//FROM SMSPIPE.SMS_PIPELINE_REPORT Tbl order by  REPORT_ID  ";
//            DataTable dt = new DataTable();
//            OraConn.Open();
//            OleDbDataAdapter da = new OleDbDataAdapter(query, OraConn);
//            if (da != null)
//                da.Fill(dt);
//            else
//                dt = null;

//            OraConn.Close();
//            return dt;

//        }
//        public DataTable LoadAllTempl()
//        {
//            OleDbCommand OleComm = new OleDbCommand();
//            OraConn.ConnectionString = conn;
//            OleComm.Connection = OraConn;
//            string query = @"SELECT ID, MESSAGE
//FROM SMSPIPE.SMSPIPELINE_TEMPL Tbl ";
//            DataTable dt = new DataTable();
//            OraConn.Open();
//            OleDbDataAdapter da = new OleDbDataAdapter(query, OraConn);
//            if (da != null)
//                da.Fill(dt);
//            else
//                dt = null;

//            OraConn.Close();
//            return dt;
//        }
//        public DataTable LoadSelectGroup(string shtext, string group_id)
//        {
//            string condition;
//            if (shtext == "")
//                condition = "where upper(gg.DESCR) like '%" + group_id + "%'" + " or  upper(gg.Keyword) like '%" + group_id + "%' or gg.name like '%" + group_id + "%'";
//            else if (group_id == "-1")
//                condition = "where upper(gg.DESCR) like '%" + shtext + "%'" + " or  upper(gg.Keyword) like '%" + shtext + "%' or gg.name like '%" + shtext + "%'";
//            else
//                condition = "where (upper(gg.DESCR) like '%" + shtext + "%'"
//                            + " or  upper(gg.Keyword) like '%" + shtext
//                            + "%' or gg.name like '%" + shtext + "%')"
//                            + " and (GROUP_ID = " + group_id + " or MASTER_GROUP_ID = " + group_id + ")";
//            OleDbCommand OleComm = new OleDbCommand();
//            OraConn.ConnectionString = conn;
//            OleComm.Connection = OraConn;
//            string query = @"select * from (
//select a.group_id,a.GROUP_NAME,a.MASTER_GROUP_ID,e.name as DEPARTMENT,b.NAME||d.NAME|| c.NAME as NAME
//,case 
//when a.DEPARTMENT_ID=100 then '-'
//when  a.DEPARTMENT_ID=200 then b.POSITION ||' , '|| b.COMPANY ||' TEL. :'||b.PHONE
//when a.DEPARTMENT_ID=300 then c.POSITION ||' , '|| c.COMPANY ||' TEL. :'||c.PHONE
//when a.DEPARTMENT_ID=400 then  ' NAME : ' ||d.NAME || ', TEL. : ' ||d.PHONE
//end DESCR,
//case 
//when a.DEPARTMENT_ID=100 then ''||-1
//when  a.DEPARTMENT_ID=200 then  ''||b.PHONE
//when a.DEPARTMENT_ID=300 then   ''||c.PHONE
//when a.DEPARTMENT_ID=400 then  ''||d.PHONE
//end PHONE,
//e.name  ||' - '||a.GROUP_NAME  as Keyword
//from SMS_PIPELINE_GROUP a
//join SMS_PIPELINE_DEPART e on e.ID= a.DEPARTMENT_ID
//left join (select aa.GROUP_ID,bb.TITLE||bb.FNAME||' '||bb.LNAME as NAME ,bb.POSITION,bb.Mobile as PHONE,bb.cust_name as COMPANY 
//from SMSPIPELINE_CSSERVICE aa
//join SMSPIPELINE_CSPPL bb on  aa.PERSON_ID=bb.person_id)  b  on a.GROUP_ID=b.GROUP_ID
//left join (select aa.GROUP_ID,bb.TITLE||bb.FIRSTNAME||' '||bb.LASTNAME as NAME ,bb.POSITION,bb.Mobile as PHONE,CC.COMPANY_name as COMPANY 
//from SMSPIPELINE_TCSSERVICE aa
//join SMSPIPELINE_TCSPPL bb on  aa.EMPLOYEE_ID=bb.EMPLOYEE_ID
//join SMS_PIPELINE_TCSCOM cc on bb.cust_id =cc.company_id) c on a.GROUP_ID=c.GROUP_ID
//left join SMSPIPLINE_TEST d on a.GROUP_ID=d.GROUP_ID
//) gg " + condition + " order by GROUP_ID";

//            DataTable dt = new DataTable();
//            OraConn.Open();
//            OleDbDataAdapter da = new OleDbDataAdapter(query, OraConn);
//            if (da != null)
//                da.Fill(dt);
//            else
//                dt = null;

//            OraConn.Close();
//            return dt;
//        }
//        public void InsertNewGroup(string Group_id, string Group_name, int level, string master_Group_id)
//        {

//            Group_id = Group_id.Substring(3, Group_id.Length - 3);
//            master_Group_id = master_Group_id.Substring(3, master_Group_id.Length - 3);
//            OleDbCommand OleComm = new OleDbCommand();
//            OraConn.ConnectionString = conn;
//            OleComm.Connection = OraConn;
//            string query;
//            query = @" INSERT INTO SMS_PIPELINE_GROUP 
//                        select ID||'" + Group_id + "' ,'" + Group_name + "',ID," + level + ",ID||'" + master_Group_id + "'" +
//" from SMSPIPE.SMS_PIPELINE_DEPART";
//            OleComm.CommandText = query;
//            try
//            {
//                OraConn.Open();
//                OleComm.ExecuteNonQuery();


//            }
//            catch (Exception ex)
//            {

//            }
//            finally
//            {

//                OraConn.Close();

//            }
//        }
//        public void DeleteGroup(string Group_id)
//        {
//            Group_id = Group_id.Substring(3, Group_id.Length-3);
//            OleDbCommand OleComm = new OleDbCommand();
//            OraConn.ConnectionString = conn;
//            OleComm.Connection = OraConn;
//            string query;
//            query = @"delete  SMS_PIPELINE_GROUP a
//where a.GROUP_ID like '%" + Group_id + "'" +
//" or master_group_id  like '%" + Group_id + "'";
//           OleComm.CommandText = query;
//            try
//            {
//                OraConn.Open();
//                OleComm.ExecuteNonQuery();


//            }
//            catch (Exception ex)
//            {

//            }
//            finally
//            {

//                OraConn.Close();

//            }
//            DeletePPL(Group_id);
//        }
//        private void DeletePPL(string group_id)
//        {
//            string group = group_id.Substring(0, 1);
//            OleDbCommand OleComm = new OleDbCommand();
//            OraConn.ConnectionString = conn;
//            OleComm.Connection = OraConn;
//            string query = "";
//            if (group == "2")
//            {
//                query = @"delete SMSPIPE.SMSPIPELINE_CSSERVICE where GROUP_ID =" + group_id;
//            }
//            else if (group == "3")
//            {

//                query = @"delete SMSPIPE.SMS_PIPENINE_TCSERVICES where group_id = " + group_id;
//            }
//            else if (group == "1")
//            {
//                query = @"delete SMSPIPE.SMS_PIPELINE_CSERVICES where GROUP_ID =" + group_id;

//            }
//            else
//            {
//                query = @"delete SMSPIPE.SMSPIPLINE_TEST Tbl where GROUP_ID =" + group_id;

//            }
//            OleComm.CommandText = query;
//            try
//            {
//                OraConn.Open();
//                OleComm.ExecuteNonQuery();


//            }
//            catch (Exception ex)
//            {

//            }
//            finally
//            {

//                OraConn.Close();

//            }



//       }
//        public DataTable LoadAllDepart()
//        {
//            OleDbCommand OleComm = new OleDbCommand();
//            OraConn.ConnectionString = conn;
//            OleComm.Connection = OraConn;
//            string query = @" select ID,NAME from SMS_PIPELINE_DEPART
//order by id";
//            DataTable dt = new DataTable();
//            OraConn.Open();
//            OleDbDataAdapter da = new OleDbDataAdapter(query, OraConn);
//            if (da != null)
//                da.Fill(dt);
//            else
//                dt = null;

//            OraConn.Close();
//            return dt;
//        }
        //public void DeleteCSPPL(string group_id, string d, string person_id)
        //{
        //    string group = group_id.Substring(0, 1);
        //    OleDbCommand OleComm = new OleDbCommand();
        //    OraConn.ConnectionString = conn;
        //    OleComm.Connection = OraConn;
        //    string query = "";

        //    query = @"delete SMSPIPELINE_CSSERVICE Tbl where GROUP_ID =" + group_id + " AND PERSON_ID = " + person_id;


        //    OleComm.CommandText = query;
        //    try
        //    {
        //        OraConn.Open();
        //        OleComm.ExecuteNonQuery();


        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {

        //        OraConn.Close();

        //    }
        //}

        //public void DeleteTESTPPL(string group_id, string person_id)
        //{
        //    string group = group_id.Substring(0, 1);
        //    OleDbCommand OleComm = new OleDbCommand();
        //    OraConn.ConnectionString = conn;
        //    OleComm.Connection = OraConn;
        //    string query = "";

        //    query = @"delete SMSPIPLINE_TEST Tbl where GROUP_ID =" + group_id + " AND ID = " + person_id;


        //    OleComm.CommandText = query;
        //    try
        //    {
        //        OraConn.Open();
        //        OleComm.ExecuteNonQuery();


        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {

        //        OraConn.Close();

        //    }
        //}

        //public void DeleteTCSPPL(string group_id, string d, string person_id, string company_id)
        //{
        //    string group = group_id.Substring(0, 1);
        //    OleDbCommand OleComm = new OleDbCommand();
        //    OraConn.ConnectionString = conn;
        //    OleComm.Connection = OraConn;
        //    string query = "";

        //    query = @"delete SMSPIPELINE_TCSSERVICE Tbl where GROUP_ID =" + group_id + " AND EMPLOYEE_ID = " + person_id + " AND COMPANY_ID = " + company_id;


        //    OleComm.CommandText = query;
        //    try
        //    {
        //        OraConn.Open();
        //        OleComm.ExecuteNonQuery();


        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {

        //        OraConn.Close();

        //    }
        //}



        //public void DeleteGCPPL(string group_id, string d, string person_id, string company_id)
        //{
        //    string group = group_id.Substring(0, 1);
        //    OleDbCommand OleComm = new OleDbCommand();
        //    OraConn.ConnectionString = conn;
        //    OleComm.Connection = OraConn;
        //    string query = "";

        //    query = @"delete SMSPIPELINE_GCSERVICE Tbl where GROUP_ID =" + group_id + " AND EMPLOYEE_ID = " + person_id + " AND COMPANY_ID = " + company_id;


        //    OleComm.CommandText = query;
        //    try
        //    {
        //        OraConn.Open();
        //        OleComm.ExecuteNonQuery();


        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {

        //        OraConn.Close();

        //    }
        //}
        //public void DeletePTTPPL(string group_id, string d, string person_id)
        //{
        //    OleDbCommand OleComm = new OleDbCommand();
        //    OraConn.ConnectionString = conn;
        //    OleComm.Connection = OraConn;
        //    string query = "";
        //    query = @"delete SMSPIPELINE_PTTSERVICE Tbl where GROUP_ID =" + group_id + " AND EMPLOYEE_ID = " + person_id;



        //    OleComm.CommandText = query;
        //    try
        //    {
        //        OraConn.Open();
        //        OleComm.ExecuteNonQuery();


        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {

        //        OraConn.Close();

        //    }
        //}
//        public DataTable findAdmin(string _filterAttribute)
//        {
//            OleDbCommand OleComm = new OleDbCommand();
//            OraConn.ConnectionString = conn;
//            OleComm.Connection = OraConn;
//            string query = @"SELECT 
//    EMPLOYEE_ID, IS_ADMIN
//FROM SMSPIPE.SMSPIPELINE_AUTHOR Tbl where  EMPLOYEE_ID =" + _filterAttribute;
//            DataTable dt = new DataTable();
//            OraConn.Open();
//            OleDbDataAdapter da = new OleDbDataAdapter(query, OraConn);
//            if (da != null)
//                da.Fill(dt);
//            else
//                dt = null;

//            OraConn.Close();
//            return dt;
//        }

//        public DataTable LoadAdmin()
//        {
//            OleDbCommand OleComm = new OleDbCommand();
//            OraConn.ConnectionString = conn;
//            OleComm.Connection = OraConn;
//            string query = @"SELECT 
//    EMPLOYEE_ID, IS_ADMIN
//FROM SMSPIPE.SMSPIPELINE_AUTHOR Tbl";
//            DataTable dt = new DataTable();
//            OraConn.Open();
//            OleDbDataAdapter da = new OleDbDataAdapter(query, OraConn);
//            if (da != null)
//                da.Fill(dt);
//            else
//                dt = null;

//            OraConn.Close();
//            return dt;
//        }

        //public void DeleteAdmin(string person_id)
        //{

        //    OleDbCommand OleComm = new OleDbCommand();
        //    OraConn.ConnectionString = conn;
        //    OleComm.Connection = OraConn;
        //    string query = "";

        //    query = @"delete SMSPIPELINE_AUTHOR Tbl where EMPLOYEE_ID =" + person_id;


        //    OleComm.CommandText = query;
        //    try
        //    {
        //        OraConn.Open();
        //        OleComm.ExecuteNonQuery();


        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {

        //        OraConn.Close();

        //    }
        //}

        //public string insertAuthor(string EmployeeID, string isadmin)
        //{
        //    OleDbCommand OleComm = new OleDbCommand();
        //    OraConn.ConnectionString = conn;
        //    OleComm.Connection = OraConn;
        //    string query = "";

        //    query = @"insert into SMSPIPELINE_AUTHOR values(" + EmployeeID + "," + isadmin + ")";


        //    OleComm.CommandText = query;
        //    try
        //    {
        //        OraConn.Open();
        //        OleComm.ExecuteNonQuery();


        //    }
        //    catch (Exception ex)
        //    {
        //        return "-1";
        //    }
        //    finally
        //    {

        //        OraConn.Close();

        //    }
        //    return "0";
        //}

        //public string UpdateAuthor(string EmployeeID, string isadmin)
        //{
        //    OleDbCommand OleComm = new OleDbCommand();
        //    OraConn.ConnectionString = conn;
        //    OleComm.Connection = OraConn;
        //    string query = "";

        //    query = @"UPDATE SMSPIPELINE_AUTHOR set IS_ADMIN= " + isadmin + " where Employee_ID = " + EmployeeID;


        //    OleComm.CommandText = query;
        //    try
        //    {
        //        OraConn.Open();
        //        OleComm.ExecuteNonQuery();


        //    }
        //    catch (Exception ex)
        //    {
        //        return "-1";
        //    }
        //    finally
        //    {

        //        OraConn.Close();

        //    }
        //    return "0";
        //}

         //public void updateCSPPL(string EmployeeID, string MOBILE)
         //{
         //    OleDbCommand OleComm = new OleDbCommand();
         //    OraConn.ConnectionString = conn;
         //    OleComm.Connection = OraConn;
         //    string query = "";

         //    query = @"UPDATE SMSPIPELINE_CSPPL set MOBILE= '" + MOBILE + "' where PERSON_ID = " + EmployeeID;


         //    OleComm.CommandText = query;
         //    try
         //    {
         //        OraConn.Open();
         //        OleComm.ExecuteNonQuery();


         //    }
         //    catch (Exception ex)
         //    {
                
         //    }
         //    finally
         //    {

         //        OraConn.Close();

         //    }
           
         //}

         //public void updateGCPPL(string EmployeeID, string MOBILE)
         //{
         //    OleDbCommand OleComm = new OleDbCommand();
         //    OraConn.ConnectionString = conn;
         //    OleComm.Connection = OraConn;
         //    string query = "";

         //    query = @"UPDATE SMSPIPELINE_GCPPL  set MOBILE= '" + MOBILE + "' where Employee_ID = " + EmployeeID;


         //    OleComm.CommandText = query;
         //    try
         //    {
         //        OraConn.Open();
         //        OleComm.ExecuteNonQuery();


         //    }
         //    catch (Exception ex)
         //    {

         //    }
         //    finally
         //    {

         //        OraConn.Close();

         //    }
         //}

         //public void UpdateTCSPPL(string EmployeeID, string MOBILE)
         //{
         //    OleDbCommand OleComm = new OleDbCommand();
         //    OraConn.ConnectionString = conn;
         //    OleComm.Connection = OraConn;
         //    string query = "";

         //    query = @"UPDATE SMSPIPELINE_TCSPPL  set MOBILE= '" + MOBILE + "' where Employee_ID = " + EmployeeID;


         //    OleComm.CommandText = query;
         //    try
         //    {
         //        OraConn.Open();
         //        OleComm.ExecuteNonQuery();


         //    }
         //    catch (Exception ex)
         //    {

         //    }
         //    finally
         //    {

         //        OraConn.Close();

         //    }
            
         //}
         //public void UpdatePTTPPL(string EmployeeID, string MOBILE)
         //{
            
         //    OleDbCommand OleComm = new OleDbCommand();
         //    OraConn.ConnectionString = conn;
         //    OleComm.Connection = OraConn;
         //    string query = "";
         //    query = @"UPDATE SMSPIPELINE_PTTPPL  set MOBILE= '" + MOBILE + "' where Employee_ID = " + EmployeeID;


         //    OleComm.CommandText = query;
         //    try
         //    {
         //        OraConn.Open();
         //        OleComm.ExecuteNonQuery();


         //    }
         //    catch (Exception ex)
         //    {

         //    }
         //    finally
         //    {

         //        OraConn.Close();

         //    }
         //}
         //public string InsertPTTPPL(string EmployeeID, string MOBILE)
         //{
         //    OleDbCommand OleComm = new OleDbCommand();
         //    OraConn.ConnectionString = conn;
         //    OleComm.Connection = OraConn;
         //    string query = "";

         //    query = @"insert into  SMSPIPELINE_PTTPPL values(" + EmployeeID+ ", " +MOBILE  +")";


         //    OleComm.CommandText = query;
         //    try
         //    {
         //        OraConn.Open();
         //        OleComm.ExecuteNonQuery();


         //    }
         //    catch (Exception ex)
         //    {
         //        return "-1";
         //    }
         //    finally
         //    {

         //        OraConn.Close();

         //    }
         //    return "0";
         //}

//         public DataTable LoadCSPPL()
//         {
//             OleDbCommand OleComm = new OleDbCommand();
//             OraConn.ConnectionString = conn;
//             OleComm.Connection = OraConn;
//             string query = "";


//             query = @"select distinct * from (
//select  b.PERSON_ID,TITLE||FNAME||' '||LNAME as NAME 
//,POSITION 
//, CUST_NAME as COMPANY 
//,MOBILE ,'400' as group_key
//,TITLE||FNAME||' '||LNAME||' '||CUST_NAME||' '||POSITION||' '||CUST_ABBR as Keyword
//FROM SMSPIPELINE_CSSERVICE a
//join SMSPIPELINE_CSPPL b on a.PERSON_ID=b.PERSON_ID
// order by a.PERSON_ID
//
//) c";

//             DataTable dt = new DataTable();
//             OraConn.Open();
//             OleDbDataAdapter da = new OleDbDataAdapter(query, OraConn);
//             if (da != null)
//                 da.Fill(dt);
//             else
//                 dt = null;

//             OraConn.Close();
//             return dt;
//         }

//         public DataTable LoadTCSPPL()
//         {
//             OleDbCommand OleComm = new OleDbCommand();
//             OraConn.ConnectionString = conn;
//             OleComm.Connection = OraConn;
//             string query = "";

//             query = @"select distinct * from (
//select b.EMPLOYEE_ID,TITLE||FIRSTNAME||' '||LASTNAME as NAME 
//,POSITION 
//, COMPANY_NAME as COMPANY,COMPANY_ID 
//,MOBILE,'500' as group_key
//,b.EMPLOYEE_ID||' '||TITLE||FIRSTNAME||' '||LASTNAME||' '||COMPANY_NAME||' '||POSITION  as keyword
//FROM SMSPIPELINE_TCSSERVICE a
//join SMSPIPELINE_TCSPPL b on a.EMPLOYEE_ID=b.EMPLOYEE_ID and a.COMPANY_ID=b.CUST_ID
//join  SMS_PIPELINE_TCSCOM com on b.cust_id = com.company_id
//  order by b.EMPLOYEE_ID
//  )";

//             DataTable dt = new DataTable();
//             OraConn.Open();
//             OleDbDataAdapter da = new OleDbDataAdapter(query, OraConn);
//             if (da != null)
//                 da.Fill(dt);
//             else
//                 dt = null;

//             OraConn.Close();
//             return dt;
//         }

//         public DataTable LoadGCPPL()
//         {
//             OleDbCommand OleComm = new OleDbCommand();
//             OraConn.ConnectionString = conn;
//             OleComm.Connection = OraConn;
//             string query = "";


//             query = @"select distinct * from (
//select b.EMPLOYEE_ID,NAME as NAME 
//,POSITION 
//, COMPANY_NAME as COMPANY,COMPANY_ID 
//,MOBILE,'300' as group_key
//,b.EMPLOYEE_ID||' '||NAME||' '||COMPANY_NAME||' '||POSITION  as keyword
//FROM SMSPIPELINE_GCSERVICE a
//join SMSPIPELINE_GCPPL b on a.EMPLOYEE_ID=b.EMPLOYEE_ID and b.company_id = a.company_id
//join SMSPIPELINE_GCCOM  com on b.company_id = com.company_id
//  order by b.EMPLOYEE_ID
//  )";

//             DataTable dt = new DataTable();
//             OraConn.Open();
//             OleDbDataAdapter da = new OleDbDataAdapter(query, OraConn);
//             if (da != null)
//                 da.Fill(dt);
//             else
//                 dt = null;

//             OraConn.Close();
//             return dt;
//         }
//         public DataTable LoadPTTPPL()
//         {
//             OleDbCommand OleComm = new OleDbCommand();
//             OraConn.ConnectionString = conn;
//             OleComm.Connection = OraConn;
//             string query = "";


//             query = @"select distinct * from (
//select a.EMPLOYEE_ID,b.MOBILE
//FROM SMSPIPELINE_PTTSERVICE a
//left join SMSPIPELINE_PTTPPL b on a.EMPLOYEE_ID=b.EMPLOYEE_ID
// order by a.EMPLOYEE_ID
//) c";

//             DataTable dt = new DataTable();
//             OraConn.Open();
//             OleDbDataAdapter da = new OleDbDataAdapter(query, OraConn);
//             if (da != null)
//                 da.Fill(dt);
//             else
//                 dt = null;

//             OraConn.Close();
//             return dt; 
//         }
//         public DataTable LoadPTTBPPL()
//         {
//             OleDbCommand OleComm = new OleDbCommand();
//             OraConn.ConnectionString = conn;
//             OleComm.Connection = OraConn;
//             string query = "";


//             query = @"select distinct * from (
//select a.POSCODE,b.MOBILE
//FROM SMSPIPELINE_PTTBSERVICE a
//left join SMSPIPELINE_PTTBPPL b on a.POSCODE=b.POSCODE
// order by a.POSCODE
//) c";

//             DataTable dt = new DataTable();
//             OraConn.Open();
//             OleDbDataAdapter da = new OleDbDataAdapter(query, OraConn);
//             if (da != null)
//                 da.Fill(dt);
//             else
//                 dt = null;

//             OraConn.Close();
//             return dt; 
//         }

         //public void DeleteTCSPPL(string person_id, string company_id)
         //{
         //    OleDbCommand OleComm = new OleDbCommand();
         //    OraConn.ConnectionString = conn;
         //    OleComm.Connection = OraConn;
         //    string query = "";

         //    query = @"delete SMSPIPELINE_TCSSERVICE Tbl where EMPLOYEE_ID =" + person_id + " and company_id =" + company_id;


         //    OleComm.CommandText = query;
         //    try
         //    {
         //        OraConn.Open();
         //        OleComm.ExecuteNonQuery();


         //    }
         //    catch (Exception ex)
         //    {

         //    }
         //    finally
         //    {

         //        OraConn.Close();

         //    }
         //}

//        public DataTable LoadTCSGroup(string EmployeeID, string company_id)
//        {
//            OleDbCommand OleComm = new OleDbCommand();
//            OraConn.ConnectionString = conn;
//            OleComm.Connection = OraConn;
//            string query = "";


//            query = @" select employee_id,company_id,a.group_id,b.group_id as Group_id_in ,a.GROUP_NAME from SMSPIPE.SMS_PIPELINE_GROUP a 
//left join (select group_id ,  employee_id,company_id
// from  SMSPIPE.SMSPIPELINE_TCSSERVICE
// where employee_id = '" + EmployeeID + "' and company_id='" + company_id + "'" +
//@")b
// on a.GROUP_ID=b.GROUP_ID 
//where a.DEPARTMENT_ID=500
//and a.group_id != a.MASTER_GROUP_ID
//order by a.MASTER_GROUP_ID ,a.group_id";

//            DataTable dt = new DataTable();
//            OraConn.Open();
//            OleDbDataAdapter da = new OleDbDataAdapter(query, OraConn);
//            if (da != null)
//                da.Fill(dt);
//            else
//                dt = null;

//            OraConn.Close();
//            return dt;
//        }
        //public void InsertTCSService(string id, string EmployeeID, string company_id)
        //{
        //    OleDbCommand OleComm = new OleDbCommand();
        //    OraConn.ConnectionString = conn;
        //    OleComm.Connection = OraConn;
        //    string query = "";

        //    query = @"insert into SMSPIPELINE_TCSSERVICE Tbl values(" + id + "," + EmployeeID + "," + company_id + ")";


        //    OleComm.CommandText = query;
        //    try
        //    {
        //        OraConn.Open();
        //        OleComm.ExecuteNonQuery();


        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {

        //        OraConn.Close();

        //    }
        //}

        //public void DeleteCSPPL(string person_id)
        //{
        //    OleDbCommand OleComm = new OleDbCommand();
        //    OraConn.ConnectionString = conn;
        //    OleComm.Connection = OraConn;
        //    string query = "";

        //    query = @"delete SMSPIPE.SMSPIPELINE_CSSERVICE Tbl where person_id =" + person_id;


        //    OleComm.CommandText = query;
        //    try
        //    {
        //        OraConn.Open();
        //        OleComm.ExecuteNonQuery();


        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {

        //        OraConn.Close();

        //    }
        //}
      
//        public DataTable LoadCSGroup(string EmployeeID)
//        {
//            OleDbCommand OleComm = new OleDbCommand();
//            OraConn.ConnectionString = conn;
//            OleComm.Connection = OraConn;
//            string query = "";


//            query = @" select person_id,a.group_id,b.group_id as Group_id_in ,a.GROUP_NAME from SMSPIPE.SMS_PIPELINE_GROUP a 
//left join (select group_id ,  person_id
// from  SMSPIPE.SMSPIPELINE_CSSERVICE
// where person_id ='" + EmployeeID + "'" +
//@")b
// on a.GROUP_ID=b.GROUP_ID 
//where a.DEPARTMENT_ID=400
//and a.group_id != a.MASTER_GROUP_ID
//order by a.MASTER_GROUP_ID ,a.group_id";

//            DataTable dt = new DataTable();
//            OraConn.Open();
//            OleDbDataAdapter da = new OleDbDataAdapter(query, OraConn);
//            if (da != null)
//                da.Fill(dt);
//            else
//                dt = null;

//            OraConn.Close();
//            return dt;
//        }
        //public void InsertCSService(string id, string EmployeeID, string company_id)
        //{
        //    OleDbCommand OleComm = new OleDbCommand();
        //    OraConn.ConnectionString = conn;
        //    OleComm.Connection = OraConn;
        //    string query = "";

        //    query = @"insert into SMSPIPELINE_CSSERVICE Tbl values(" + id + "," + EmployeeID + ")";


        //    OleComm.CommandText = query;
        //    try
        //    {
        //        OraConn.Open();
        //        OleComm.ExecuteNonQuery();


        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {

        //        OraConn.Close();

        //    }
        //}

        //public void DeleteGCPPL(string person_id, string company_id)
        //{
        //    OleDbCommand OleComm = new OleDbCommand();
        //    OraConn.ConnectionString = conn;
        //    OleComm.Connection = OraConn;
        //    string query = "";

        //    query = @"delete SMSPIPELINE_GCSERVICE Tbl where EMPLOYEE_ID =" + person_id + " and company_id =" + company_id;


        //    OleComm.CommandText = query;
        //    try
        //    {
        //        OraConn.Open();
        //        OleComm.ExecuteNonQuery();


        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {

        //        OraConn.Close();

        //    }
        //}
       
//        public DataTable LoadGCGroup(string EmployeeID, string company_id)
//        {
//            OleDbCommand OleComm = new OleDbCommand();
//            OraConn.ConnectionString = conn;
//            OleComm.Connection = OraConn;
//            string query = "";


//            query = @" select employee_id,company_id,a.group_id,b.group_id as Group_id_in ,a.GROUP_NAME from SMSPIPE.SMS_PIPELINE_GROUP a 
//left join (select group_id ,  employee_id,company_id
// from  SMSPIPE.SMSPIPELINE_GCSERVICE
// where employee_id = '" + EmployeeID + "' and company_id='" + company_id + "'" +
//@" )b
// on a.GROUP_ID=b.GROUP_ID 
//where a.DEPARTMENT_ID=300
//and a.group_id != a.MASTER_GROUP_ID
//order by a.MASTER_GROUP_ID ,a.group_id";

//            DataTable dt = new DataTable();
//            OraConn.Open();
//            OleDbDataAdapter da = new OleDbDataAdapter(query, OraConn);
//            if (da != null)
//                da.Fill(dt);
//            else
//                dt = null;

//            OraConn.Close();
//            return dt;
//        }

        //public void InsertGCService(string id, string EmployeeID, string company_id)
        //{
        //    OleDbCommand OleComm = new OleDbCommand();
        //    OraConn.ConnectionString = conn;
        //    OleComm.Connection = OraConn;
        //    string query = "";

        //    query = @"insert into SMSPIPELINE_GCSERVICE Tbl values(" + id + "," + EmployeeID + "," + company_id + ")";


        //    OleComm.CommandText = query;
        //    try
        //    {
        //        OraConn.Open();
        //        OleComm.ExecuteNonQuery();


        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {

        //        OraConn.Close();

        //    }
        //}

      

//        public DataTable LoadToAddCSPPL(string sch)
//        {
//            OleDbCommand OleComm = new OleDbCommand();
//            OraConn.ConnectionString = conn;
//            OleComm.Connection = OraConn;
//            string query = @"select distinct * from (
//select  b.PERSON_ID,TITLE||FNAME||' '||LNAME as NAME 
//,POSITION 
//, CUST_NAME as COMPANY 
//,MOBILE ,'400' as group_key
//,TITLE||FNAME||' '||LNAME||' '||CUST_NAME||' '||POSITION||' '||CUST_ABBR as Keyword
//From SMSPIPELINE_CSPPL b "+
////where b.PERSON_ID not in (select person_id from SMSPIPELINE_CSSERVICE)
//@" order by b.PERSON_ID
//) c where Keyword like '%" + sch + "%'";
//            DataTable dt = new DataTable();
//            OraConn.Open();
//            OleDbDataAdapter da = new OleDbDataAdapter(query, OraConn);
//            if (da != null)
//                da.Fill(dt);
//            else
//                dt = null;

//            OraConn.Close();
//            return dt;
//        }

//        public DataTable LoadToAddGCPPL(string sch)
//        {
//            OleDbCommand OleComm = new OleDbCommand();
//            OraConn.ConnectionString = conn;
//            OleComm.Connection = OraConn;
//            string query = @"select distinct * from (
//select b.EMPLOYEE_ID,NAME as NAME 
//,POSITION 
//, COMPANY_NAME as COMPANY,b.COMPANY_ID 
//,MOBILE,'300' as group_key
//,b.EMPLOYEE_ID||' '||NAME||' '||com.COMPANY_NAME||' '||b.POSITION  as keyword
//FROM 
// SMSPIPELINE_GCPPL b 
//join SMSPIPELINE_GCCOM  com on b.company_id = com.company_id "+
////where b.EMPLOYEE_ID not in (select EMPLOYEE_ID from SMSPIPELINE_GCSERVICE)
//@"  order by b.EMPLOYEE_ID
//) c where Keyword like '%" + sch + "%'";
//            DataTable dt = new DataTable();
//            OraConn.Open();
//            OleDbDataAdapter da = new OleDbDataAdapter(query, OraConn);
//            if (da != null)
//                da.Fill(dt);
//            else
//                dt = null;

//            OraConn.Close();
//            return dt;
//        }

//        public DataTable LoadToAddTCSPPL(string sch)
//        {
//            OleDbCommand OleComm = new OleDbCommand();
//            OraConn.ConnectionString = conn;
//            OleComm.Connection = OraConn;
//            string query = @"select distinct * from (
//select b.EMPLOYEE_ID,TITLE||FIRSTNAME||' '||LASTNAME as NAME 
//,POSITION 
//, COMPANY_NAME as COMPANY,COMPANY_ID 
//,MOBILE,'500' as group_key
//,b.EMPLOYEE_ID||' '||TITLE||FIRSTNAME||' '||LASTNAME||' '||COMPANY_NAME||' '||POSITION  as keyword
//FROM 
//SMSPIPELINE_TCSPPL b 
//join  SMS_PIPELINE_TCSCOM com on b.cust_id = com.company_id "+
////where EMPLOYEE_ID not in (select EMPLOYEE_ID from SMSPIPELINE_TCSSERVICE)
// @" order by b.EMPLOYEE_ID
//) c where Keyword like '%" + sch + "%'";
//            DataTable dt = new DataTable();
//            OraConn.Open();
//            OleDbDataAdapter da = new OleDbDataAdapter(query, OraConn);
//            if (da != null)
//                da.Fill(dt);
//            else
//                dt = null;

//            OraConn.Close();
//            return dt;
//        }


        //public string InsertPTTBPPL(string EmployeeID, string MOBILE)
        //{
        //    OleDbCommand OleComm = new OleDbCommand();
        //    OraConn.ConnectionString = conn;
        //    OleComm.Connection = OraConn;
        //    string query = "";

        //    query = @"insert into  SMSPIPELINE_PTTBPPL values(" + EmployeeID + ", " + MOBILE + ")";


        //    OleComm.CommandText = query;
        //    try
        //    {
        //        OraConn.Open();
        //        OleComm.ExecuteNonQuery();


        //    }
        //    catch (Exception ex)
        //    {
        //        return "-1";
        //    }
        //    finally
        //    {

        //        OraConn.Close();

        //    }
        //    return "0";
        //}

        //public void UpdatePTTBPPL(string EmployeeID, string MOBILE)
        //{
        //    OleDbCommand OleComm = new OleDbCommand();
        //    OraConn.ConnectionString = conn;
        //    OleComm.Connection = OraConn;
        //    string query = "";

        //    query = @"UPDATE SMSPIPELINE_PTTBPPL  set MOBILE= '" + MOBILE + "' where POSCODE = " + EmployeeID;


        //    OleComm.CommandText = query;
        //    try
        //    {
        //        OraConn.Open();
        //        OleComm.ExecuteNonQuery();


        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {

        //        OraConn.Close();

        //    }
        //}

        //public void DeletePTTBPPL(string group_id, string d, string poscode)
        //{
        //    OleDbCommand OleComm = new OleDbCommand();
        //    OraConn.ConnectionString = conn;
        //    OleComm.Connection = OraConn;
        //    string query = "";
        //    query = @"delete SMSPIPELINE_PTTBSERVICE Tbl where GROUP_ID =" + group_id + " AND POSCODE = " + poscode;



        //    OleComm.CommandText = query;
        //    try
        //    {
        //        OraConn.Open();
        //        OleComm.ExecuteNonQuery();


        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {

        //        OraConn.Close();

        //    }
        //}



        //public void DeletePTTPPL(string person_id)
        //{
        //    OleDbCommand OleComm = new OleDbCommand();
        //    OraConn.ConnectionString = conn;
        //    OleComm.Connection = OraConn;
        //    string query = "";

        //    query = @"delete SMSPIPE.SMSPIPELINE_PTTSERVICE Tbl where Employee_id =" + person_id;


        //    OleComm.CommandText = query;
        //    try
        //    {
        //        OraConn.Open();
        //        OleComm.ExecuteNonQuery();


        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {

        //        OraConn.Close();

        //    }
        //}
        //public void DeletePTTBPPL(string poscode)
        //{
        //    OleDbCommand OleComm = new OleDbCommand();
        //    OraConn.ConnectionString = conn;
        //    OleComm.Connection = OraConn;
        //    string query = "";

        //    query = @"delete SMSPIPE.SMSPIPELINE_PTTBSERVICE Tbl where poscode =" + poscode;


        //    OleComm.CommandText = query;
        //    try
        //    {
        //        OraConn.Open();
        //        OleComm.ExecuteNonQuery();


        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {

        //        OraConn.Close();

        //    }
        //}

//        public DataTable LoadPTTGroup(string EmployeeID)
//        {
//            OleDbCommand OleComm = new OleDbCommand();
//            OraConn.ConnectionString = conn;
//            OleComm.Connection = OraConn;
//            string query = "";


//            query = @" select Employee_id,a.group_id,b.group_id as Group_id_in ,a.GROUP_NAME 
//from SMSPIPE.SMS_PIPELINE_GROUP a 
//left join (select group_id ,  Employee_id
// from  SMSPIPE.SMSPIPELINE_PTTSERVICE
// where Employee_id ='" + EmployeeID + "'" +
//@")b
// on a.GROUP_ID=b.GROUP_ID 
//where a.DEPARTMENT_ID=200
//and a.group_id != a.MASTER_GROUP_ID
//order by a.MASTER_GROUP_ID ,a.group_id";

//            DataTable dt = new DataTable();
//            OraConn.Open();
//            OleDbDataAdapter da = new OleDbDataAdapter(query, OraConn);
//            if (da != null)
//                da.Fill(dt);
//            else
//                dt = null;

//            OraConn.Close();
//            return dt;
//        }

        //public void InsertPTTService(string id, string EmployeeID)
        //{
        //    OleDbCommand OleComm = new OleDbCommand();
        //    OraConn.ConnectionString = conn;
        //    OleComm.Connection = OraConn;
        //    string query = "";

        //    query = @"insert into SMSPIPELINE_PTTSERVICE Tbl values(" + EmployeeID + "," + id + ")";


        //    OleComm.CommandText = query;
        //    try
        //    {
        //        OraConn.Open();
        //        OleComm.ExecuteNonQuery();


        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {

        //        OraConn.Close();

        //    }
        //}







//        public DataTable LoadPTTBGroup(string poscode)
//        {
//            OleDbCommand OleComm = new OleDbCommand();
//            OraConn.ConnectionString = conn;
//            OleComm.Connection = OraConn;
//            string query = "";


//            query = @" select POSCODE,a.group_id,b.group_id as Group_id_in ,a.GROUP_NAME 
//from SMSPIPE.SMS_PIPELINE_GROUP a 
//left join (select group_id ,  POSCODE
// from  SMSPIPE.SMSPIPELINE_PTTBSERVICE
// where POSCODE ='" + poscode + "'" +
//@")b
// on a.GROUP_ID=b.GROUP_ID 
//where a.DEPARTMENT_ID=100
//and a.group_id != a.MASTER_GROUP_ID
//order by a.MASTER_GROUP_ID ,a.group_id";

//            DataTable dt = new DataTable();
//            OraConn.Open();
//            OleDbDataAdapter da = new OleDbDataAdapter(query, OraConn);
//            if (da != null)
//                da.Fill(dt);
//            else
//                dt = null;

//            OraConn.Close();
//            return dt;
//        }

        //public void InsertPTTBService(string id, string poscode)
        //{
        //    OleDbCommand OleComm = new OleDbCommand();
        //    OraConn.ConnectionString = conn;
        //    OleComm.Connection = OraConn;
        //    string query = "";

        //    query = @"insert into SMSPIPELINE_PTTBSERVICE Tbl values(" + poscode + "," + id + ")";


        //    OleComm.CommandText = query;
        //    try
        //    {
        //        OraConn.Open();
        //        OleComm.ExecuteNonQuery();


        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {

        //        OraConn.Close();

        //    }
        //}

//        public DataTable LoadMobileInGC(string GC_str)
//        {
//            OleDbCommand OleComm = new OleDbCommand();
//            OraConn.ConnectionString = conn;
//            OleComm.Connection = OraConn;
//            string query = "";


//            query = @" select p.MOBILE,g.DEPARTMENT_ID, s.EMPLOYEE_ID||'' as Employee_ID from SMSPIPE.SMS_PIPELINE_GROUP g
//join SMSPIPE.SMSPIPELINE_GCSERVICE s on g.GROUP_ID=s.GROUP_ID
//join SMSPIPE.SMSPIPELINE_GCPPL p on s.EMPLOYEE_ID=p.EMPLOYEE_ID
//where g.group_id in (select a.group_id from
//SMSPIPE.SMS_PIPELINE_GROUP a
//left join SMSPIPE.SMSPIPELINE_GCSERVICE b on a.GROUP_ID=b.GROUP_ID
//where a.master_group_id in (" +GC_str+") "
//+@" or a.group_id in  (" +GC_str+")) "
//+@" or g.master_group_id in (select a.group_id from "
//+@" SMSPIPE.SMS_PIPELINE_GROUP a "
//+@" left join SMSPIPE.SMSPIPELINE_GCSERVICE b on a.GROUP_ID=b.GROUP_ID 
//where a.master_group_id in ("+GC_str+") "
//+ @" or a.group_id in  (" + GC_str + "))";

//            DataTable dt = new DataTable();
//            OraConn.Open();
//            OleDbDataAdapter da = new OleDbDataAdapter(query, OraConn);
//            if (da != null)
//                da.Fill(dt);
//            else
//                dt = null;

//            OraConn.Close();
//            return dt;
//        }

//        public DataTable LoadMobileInCS(string CS_str)
//        {
//            OleDbCommand OleComm = new OleDbCommand();
//            OraConn.ConnectionString = conn;
//            OleComm.Connection = OraConn;
//            string query = "";


//            query = @"select p.MOBILE,g.DEPARTMENT_ID, s.PERSON_ID||'' as EMPLOYEE_ID from SMSPIPE.SMS_PIPELINE_GROUP g
//join SMSPIPE.SMSPIPELINE_CSSERVICE s on g.GROUP_ID=s.GROUP_ID
//join SMSPIPE.SMSPIPELINE_CSPPL p on s.PERSON_ID=p.PERSON_ID
//where g.group_id in (select a.group_id from
//SMSPIPE.SMS_PIPELINE_GROUP a
//left join SMSPIPE.SMSPIPELINE_CSSERVICE b on a.GROUP_ID=b.GROUP_ID
//where a.master_group_id in (" +CS_str+")"+
//@" or a.group_id in  ("+CS_str+")) "+
//@" or g.master_group_id in (select a.group_id from
// SMSPIPE.SMS_PIPELINE_GROUP a 
// left join SMSPIPE.SMSPIPELINE_CSSERVICE b on a.GROUP_ID=b.GROUP_ID
// where a.master_group_id in ("+CS_str+")"+
// @" or a.group_id in  (" + CS_str + ")) ";

//            DataTable dt = new DataTable();
//            OraConn.Open();
//            OleDbDataAdapter da = new OleDbDataAdapter(query, OraConn);
//            if (da != null)
//                da.Fill(dt);
//            else
//                dt = null;

//            OraConn.Close();
//            return dt;
//        }

//        public DataTable LoadMobileInTCS(string TCS_str)
//        {
//            OleDbCommand OleComm = new OleDbCommand();
//            OraConn.ConnectionString = conn;
//            OleComm.Connection = OraConn;
//            string query = "";


//            query = @"
//select p.MOBILE,g.DEPARTMENT_ID, s.EMPLOYEE_ID||'' as Employee_ID  from SMSPIPE.SMS_PIPELINE_GROUP g
//join SMSPIPE.SMSPIPELINE_TCSSERVICE s on g.GROUP_ID=s.GROUP_ID
//join SMSPIPE.SMSPIPELINE_TCSPPL p on s.EMPLOYEE_ID=p.EMPLOYEE_ID
//where g.group_id in (select a.group_id from
//SMSPIPE.SMS_PIPELINE_GROUP a
//left join SMSPIPE.SMSPIPELINE_TCSSERVICE b on a.GROUP_ID=b.GROUP_ID
//where a.master_group_id in (" + TCS_str + " ) " +
//@" or a.group_id in  ("+TCS_str+")) "+
//@" or g.master_group_id in (select a.group_id from
//SMSPIPE.SMS_PIPELINE_GROUP a
//left join SMSPIPE.SMSPIPELINE_TCSSERVICE b on a.GROUP_ID=b.GROUP_ID
//where a.master_group_id in ("+TCS_str+") "
//+ @" or a.group_id in  (" + TCS_str + "))";

//            DataTable dt = new DataTable();
//            OraConn.Open();
//            OleDbDataAdapter da = new OleDbDataAdapter(query, OraConn);
//            if (da != null)
//                da.Fill(dt);
//            else
//                dt = null;

//            OraConn.Close();
//            return dt;
//        }

//        public DataTable LoadMobileInTest(string str)
//        {
//            OleDbCommand OleComm = new OleDbCommand();
//            OraConn.ConnectionString = conn;
//            OleComm.Connection = OraConn;
//            string query = "";


//            query = @"select b.MOBILE,a.DEPARTMENT_ID,b.id||'' as Employee_ID  from SMSPIPE.SMS_PIPELINE_GROUP a
//left join SMSPIPE.SMSPIPLINE_TEST b on a.GROUP_ID=b.GROUP_ID
//where a.GROUP_ID in(" + str + ") "
//+ " or a.MASTER_GROUP_ID in (" + str + ")";

//            DataTable dt = new DataTable();
//            OraConn.Open();
//            OleDbDataAdapter da = new OleDbDataAdapter(query, OraConn);
//            if (da != null)
//                da.Fill(dt);
//            else
//                dt = null;

//            OraConn.Close();
//            return dt;
//        }

//        public DataTable LoadMobileInPTT(string PTT_str)
//        {
//            OleDbCommand OleComm = new OleDbCommand();
//            OraConn.ConnectionString = conn;
//            OleComm.Connection = OraConn;
//            string query = "";


//            query = @"select distinct p.MOBILE,main.Employee_ID||'' as Employee_ID ,main.DEPARTMENT_ID  from(
//select g.GROUP_ID,g.MASTER_GROUP_ID,s.EMPLOYEE_ID,g.DEPARTMENT_ID  from SMSPIPE.SMS_PIPELINE_GROUP g
//join SMSPIPE.SMSPIPELINE_PTTSERVICE s on g.GROUP_ID=s.GROUP_ID
//where g.group_id in (select a.group_id from
//SMSPIPE.SMS_PIPELINE_GROUP a
//left join SMSPIPE.SMSPIPELINE_PTTSERVICE b on a.GROUP_ID=b.GROUP_ID
//where a.master_group_id in (" + PTT_str+") "
//+ " or a.group_id in  (" + PTT_str + ")) "
//+" or g.master_group_id in (select a.group_id from "
//+" SMSPIPE.SMS_PIPELINE_GROUP a"
//+" left join SMSPIPE.SMSPIPELINE_PTTSERVICE b on a.GROUP_ID=b.GROUP_ID"
//+ " where a.master_group_id in (" + PTT_str + ")"
//+ " or a.group_id in  (" + PTT_str + "))"
//+" ) main"
//+" left join SMSPIPE.SMSPIPELINE_PTTPPL p on main.EMPLOYEE_ID=p.EMPLOYEE_ID";

//            DataTable dt = new DataTable();
//            OraConn.Open();
//            OleDbDataAdapter da = new OleDbDataAdapter(query, OraConn);
//            if (da != null)
//                da.Fill(dt);
//            else
//                dt = null;

//            OraConn.Close();
//            return dt;
//        }

//        public DataTable LoadMobileInPTTB(string PTTB_str)
//        {
//            OleDbCommand OleComm = new OleDbCommand();
//            OraConn.ConnectionString = conn;
//            OleComm.Connection = OraConn;
//            string query = "";


//            query = @"
//select distinct p.MOBILE,main.POSCODE||'' as POSCODE,main.DEPARTMENT_ID  from(
//select g.GROUP_ID,g.MASTER_GROUP_ID,s.POSCODE,g.DEPARTMENT_ID  from SMSPIPE.SMS_PIPELINE_GROUP g
//join SMSPIPE.SMSPIPELINE_PTTBSERVICE s on g.GROUP_ID=s.GROUP_ID
//where g.group_id in (select a.group_id from
//SMSPIPE.SMS_PIPELINE_GROUP a
//left join SMSPIPE.SMSPIPELINE_PTTBSERVICE b on a.GROUP_ID=b.GROUP_ID
//where a.master_group_id in (" + PTTB_str+") "
//+" or a.group_id in  (" +PTTB_str+")) "
//+" or g.master_group_id in (select a.group_id from"
//+" SMSPIPE.SMS_PIPELINE_GROUP a"
//+" left join SMSPIPE.SMSPIPELINE_PTTBSERVICE b on a.GROUP_ID=b.GROUP_ID"
//+" where a.master_group_id in (" +PTTB_str+")"
//+" or a.group_id in  ("+ PTTB_str+")) "
//+" ) main"
//+" left join SMSPIPE.SMSPIPELINE_PTTBPPL p on main.POSCODE=p.POSCODE";

//            DataTable dt = new DataTable();
//            OraConn.Open();
//            OleDbDataAdapter da = new OleDbDataAdapter(query, OraConn);
//            if (da != null)
//                da.Fill(dt);
//            else
//                dt = null;

//            OraConn.Close();
//            return dt;
//        }
    }
}