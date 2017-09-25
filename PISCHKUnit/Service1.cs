using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;

namespace PISCHKUnit
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            System.Timers.Timer timer = new System.Timers.Timer();

            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime); // สร้าง event สำหรับ timer ดู function OnElapsedTime ด้านล่าง
            timer.Interval = 60000*5; // ตั้งเวลา ให้ timer ทำทุก 1 นาที
            timer.Enabled = true;
            timer.Start();
        }
        public static void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            DateTime dtNow = System.DateTime.Now;
            string strHeader = dtNow.ToString("yyyy-MM-dd HH:mm:ss");
            if (dtNow > DateTime.Today && dtNow < DateTime.Today.AddMinutes(10)) 
            {
                chkunit();
            }
         
        }

        private static void chkunit()
        {
            
           
        }

        protected override void OnStop()
        {
        }

    }
}
