using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace sms_pipeLine
{
    public class SQLServerQuery
    {
        public string conn = ConfigurationManager.ConnectionStrings["SQLDatabase"].ConnectionString;
        SqlConnection myConnection = new SqlConnection();
      
        public DataTable LoadPIS() {
            myConnection.ConnectionString = conn;
            DataSet dataset = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(@"SELECT  a.[code] as P_ID
      ,[tname]
      ,[mobile]
	  ,[email]
      ,b.POSNAME
      ,c.unitname,[unitabbr],tname +'('+[unitabbr]+')' as name_fk
  FROM [PIS].[dbo].[directory_info] a
join [PIS].[dbo].[personel_info] b  on a.code=b.CODE
join [PIS].[dbo].[unit] c on b.UNITCODE=c.unitcode
where tname is not null
and WSTCODE in ('A','B','I','J')
order by [tname]", myConnection);
            myConnection.Open();
            adapter.Fill(dataset);
            myConnection.Close();
            return dataset.Tables[0];
        }

        public DataTable LoadPIS(string schtext, string pisincs)
        {
            string condi = "";
            if (!string.IsNullOrEmpty(pisincs))
                condi = " and a.[code] not in (" + pisincs + ")";
            myConnection.ConnectionString = conn;
            DataSet dataset = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(@"select * from( SELECT  b.[code] as P_ID
      ,[FULLNAMETH]
      ,[mobile]
	  ,[email]
      ,b.POSNAME
      ,c.unitname,c.unitcode,[unitabbr],FULLNAMETH +'('+[unitabbr]+')' as name_fk
      , b.[code]+' '+FULLNAMETH +' ' + [unitabbr]+' '+unitname+' '+POSNAME+' '+b.CODE as keyword
  FROM [PIS].[dbo].[personel_info] b
 left join [PIS].[dbo].[unit] c on b.UNITCODE=c.unitcode
  left join [PIS].[dbo].[directory_info] a on a.code=b.CODE
  where FULLNAMETH is not null  and WSTCODE in ('A','B','I','J') " +

condi
 + ")a where a.keyword like '%" + schtext + "%' "
+ " order by [FULLNAMETH]", myConnection);
            myConnection.Open();
            adapter.Fill(dataset);
            myConnection.Close();
            return dataset.Tables[0];
        }

        public DataTable LoadINPIS(string schtext, string pisincs)
        {
            string condi = "";
            DataSet dataset = new DataSet();
            if (!string.IsNullOrEmpty(pisincs))
            {
                condi = " and b.[code]  in (" + pisincs + ")";
                myConnection.ConnectionString = conn;

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(@"select * from( SELECT  b.[code] as P_ID
      ,[FULLNAMETH]
      ,[mobile]
	  ,[email]
      ,b.POSNAME
      ,c.unitname,c.unitcode,[unitabbr],FULLNAMETH +'('+[unitabbr]+')' as name_fk
      ,FULLNAMETH +' ' + [unitabbr]+' '+unitname+' '+POSNAME+' '+b.CODE as keyword
  FROM [PIS].[dbo].[personel_info] b
 left join [PIS].[dbo].[unit] c on b.UNITCODE=c.unitcode
  left join [PIS].[dbo].[directory_info] a on a.code=b.CODE
  where FULLNAMETH is not null    " +

    condi
     + ")a where a.keyword like '%" + schtext + "%' "
    + " order by [FULLNAMETH]", myConnection);
                myConnection.Open();
                adapter.Fill(dataset);
                myConnection.Close();
                return dataset.Tables[0];
            }
            else {
                return null;
            }
            

        }

        public DataTable LoadHeadINPIS(string p, string head)
        {
            string condi = "";
            DataSet dataset = new DataSet();
            if (!string.IsNullOrEmpty(head))
            {
                condi = " and a.[poscode]  in (" + head + ")";
                myConnection.ConnectionString = conn;

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(@"select * from( SELECT  a.[code] as P_ID
      ,[tname]
      ,[mobile]
	  ,[email]
      ,b.POSNAME
        ,a.[poscode]
      ,c.unitname,[unitabbr],tname +'('+[unitabbr]+')' as name_fk
      ,tname +' ' + [unitabbr]+' '+unitname+' '+POSNAME as keyword
  FROM [PIS].[dbo].[directory_info] a
join [PIS].[dbo].[personel_info] b  on a.code=b.CODE
join [PIS].[dbo].[unit] c on b.UNITCODE=c.unitcode
where tname is not null " +

    condi
     + ")a where a.keyword like '%" + p + "%' "
    + " order by [tname]", myConnection);
                myConnection.Open();
                adapter.Fill(dataset);
                myConnection.Close();
                return dataset.Tables[0];
            }
            else
            {
                return null;
            }
        }

        public DataTable LoadPosecodeINPIS(string result)
        {

            string condi = "";
            DataSet dataset = new DataSet();
            if (!string.IsNullOrEmpty(result))
            {
                condi = " and b.[CODE]  in (" + result + ")";
                myConnection.ConnectionString = conn;

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(@"select * from( SELECT  b.[code] as P_ID
       ,[POSCODE]
      ,[FULLNAMETH]
      ,[mobile]
	  ,[email]
      ,b.POSNAME
      ,c.unitname,[unitabbr],FULLNAMETH +'('+[unitabbr]+')' as name_fk
      ,FULLNAMETH +' ' + [unitabbr]+' '+unitname+' '+POSNAME+' '+b.CODE as keyword
  FROM [PIS].[dbo].[personel_info] b
 left join [PIS].[dbo].[unit] c on b.UNITCODE=c.unitcode
  left join [PIS].[dbo].[directory_info] a on a.code=b.CODE
  where FULLNAMETH is not null and  WSTCODE in ('A','B','I','J')  " +

    condi
     + ")a "
    + " order by [FULLNAMETH]", myConnection);
                myConnection.Open();
                adapter.Fill(dataset);
                myConnection.Close();
                return dataset.Tables[0];
            }
            else
            {
                return null;
            }
        }
        public DataTable LoadPosecodeINPIS(string schtext, string result)
        {

            string condi = "";
            DataSet dataset = new DataSet();
            if (!string.IsNullOrEmpty(result))
                condi = " and b.[POSCODE] not in (" + result + ")";


                myConnection.ConnectionString = conn;

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(@"select * from( SELECT  b.[code] as P_ID
       ,[POSCODE]
      ,[FULLNAMETH]
      ,[mobile]
	  ,[email]
      ,b.POSNAME
,c.unitcode
      ,c.unitname,[unitabbr],FULLNAMETH +'('+[unitabbr]+')' as name_fk
      ,b.[code]+' '+' '+POSCODE+' '+FULLNAMETH +' ' + [unitabbr]+' '+unitname+' '+POSNAME+' '+b.CODE as keyword
  FROM [PIS].[dbo].[personel_info] b
 left join [PIS].[dbo].[unit] c on b.UNITCODE=c.unitcode
  left join [PIS].[dbo].[directory_info] a on a.code=b.CODE
  where FULLNAMETH is not null  and WSTCODE in ('A','B','I','J')  " +

    condi
     + ")a where a.keyword like '%" + schtext + "%' "
    + " order by [FULLNAMETH]", myConnection);
                myConnection.Open();
                adapter.Fill(dataset);
                myConnection.Close();
                return dataset.Tables[0];
            
        }
    }
}