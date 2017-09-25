using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.IO;


    public class OracleConnect
    {
        private OleDbConnection conn;
        string connectionString = ConfigurationSettings.AppSettings["connectionStringOracle"].ToString(); //ConfigurationManager.AppSettings["connectionStringOracle"].ToString();
        public OracleConnect()
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

        public DataTable GetDatas(string inputOracle)
        {
            DataTable dt = new DataTable();

            try
            {


                OleDbConnection conn = ConnectToDatabase(connectionString);
                OleDbCommand cmd = new OleDbCommand(inputOracle, conn);
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(cmd);

                dataAdapter.Fill(dt);

                conn.Close();
                conn.Dispose();
            }
            catch { }

            return dt;
        }

        public void SetDatas(string inputOracle)
        {
            try
            {


                OleDbConnection conn = ConnectToDatabase(connectionString);
                OleDbCommand cmd = new OleDbCommand(inputOracle, conn);
                cmd.ExecuteNonQuery();

                conn.Close();
                conn.Dispose();
            }
            catch { }

        }
    }
