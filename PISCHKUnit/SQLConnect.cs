using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.IO;


    public class SQLConnect 
    {
        private OleDbConnection conn;
        string connectionString = ConfigurationSettings.AppSettings["connectionStringPIS"].ToString(); //ConfigurationManager.ConnectionStrings["connectionStringPIS"].ToString();
        public SQLConnect()
        {
            conn = new OleDbConnection();
        }

        private OleDbConnection ConnectToDatabase(string connString)
        {
            try
            {
                conn.ConnectionString = connString;
            }
            catch { }

            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
                conn.Dispose();
            }
            conn.Open();

            return (conn);
        }

        public  DataTable GetDatas(string inputSQL)
        {
            DataTable dt = new DataTable();

            try
            {
                

                OleDbConnection conn = ConnectToDatabase(connectionString);
                OleDbCommand cmd = new OleDbCommand(inputSQL, conn);
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(cmd);

                dataAdapter.Fill(dt);

                conn.Close();
                conn.Dispose();
            }
            catch { }

            return dt;
        }

        public  void SetDatas(string inputSQL)
        {
            try
            {
              

                OleDbConnection conn = ConnectToDatabase(connectionString);
                OleDbCommand cmd = new OleDbCommand(inputSQL, conn);
                cmd.ExecuteNonQuery();

                conn.Close();
                conn.Dispose();
            }
            catch { }

        }
    }
