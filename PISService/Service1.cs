using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using System.Configuration;
using System.Data.SqlClient;

namespace PISService
{
    public partial class Service1 : ServiceBase
    {
        public static EventLog g_myLog;
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // สร้าง timer
            System.Timers.Timer timer = new System.Timers.Timer();

            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime); // สร้าง event สำหรับ timer ดู function OnElapsedTime ด้านล่าง
            timer.Interval = 60000*60*24; // ตั้งเวลา ให้ timer ทำทุก 1 วัน
            timer.Enabled = true;
            timer.Start();


            // สร้าง event log 
            this.AutoLog = false; // turn off event log

            if (!System.Diagnostics.EventLog.SourceExists("PIS_zyc"))
            {
                System.Diagnostics.EventLog.CreateEventSource("PIS_zyc", "MyLog");
            }
            // configure the event log instance to use this source name
            g_myLog = new EventLog();
            g_myLog.Source = "PIS_zyc";
            g_myLog.WriteEntry("onStart.");
        }
        public static void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            DataTable dt = findallpis();
            
        }

        private static DataTable findallpis()
        {
           
            string conn = ConfigurationSettings.AppSettings["SQLDatabase"].ToString();
            SqlConnection myConnection = new SqlConnection(); 
            myConnection.ConnectionString = conn;
            DataSet dataset = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(@"SELECT  a.[code] 
      ,[tname]
      ,[mobile]
	  ,[email]
      ,b.POSNAME
      ,c.unitname,[unitabbr]
  FROM [PIS].[dbo].[directory_info] a
join [PIS].[dbo].[personel_info] b  on a.code=b.CODE
join [PIS].[dbo].[unit] c on b.UNITCODE=c.unitcode
where tname is not null
order by [tname]", myConnection);
            myConnection.Open();
            adapter.Fill(dataset);
            myConnection.Close();
            return dataset.Tables[0];
        }






        protected override void OnStop()
        {
        }
    }
}
