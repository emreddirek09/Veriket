using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace WindowsService
{
    public partial class Service1 : ServiceBase
    {
        Timer timer = new Timer();
        public Service1()
        {
            this.ServiceName = "Veriket Application Test";
            this.CanStop = true;
            this.CanPauseAndContinue = true;
            this.AutoLog = true;
            InitializeComponent();
        }

        private void ElapsedTime(object source, ElapsedEventArgs e)
        {
            string logMessage = $"{DateTime.Now} [{Environment.MachineName}] [{Environment.UserName}]";
            WriteLog(logMessage);
        }

        protected override void OnStart(string[] args)
        {
            timer.Elapsed += new ElapsedEventHandler(ElapsedTime);
            timer.Interval = 60000; //1dk
            timer.Enabled = true;
        }

        protected override void OnStop()
        {
            WriteLog("Servis Durdu " + DateTime.Now);
            timer.Dispose();
        }

        public void WriteLog(string message)
        {
            try
            {
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "/VeriketApp";
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                string textPath = AppDomain.CurrentDomain.BaseDirectory + "/VeriketApp/VeriketAppTest.txt";
                if (!File.Exists(textPath))
                {
                    using (StreamWriter streamWriter = File.CreateText(textPath))
                    {
                        streamWriter.WriteLine(message);
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(textPath))
                    {
                        sw.WriteLine(message);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        
    }
}
