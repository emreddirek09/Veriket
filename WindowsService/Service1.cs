using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace WindowsService
{
    public partial class Service1 : ServiceBase
    {
        Timer timer = new Timer();
        string filePath = string.Empty;
        public Service1()
        {
            this.ServiceName = "Veriket Application Test";
            this.CanStop = true;
            this.CanPauseAndContinue = true;
            this.AutoLog = true;
            CreateFile();
            InitializeComponent();
        }

        private void ElapsedTime(object source, ElapsedEventArgs e)
        {
            string logMessage = $"{DateTime.Now},[{Environment.MachineName}],[{Environment.UserName}]";
            WriteLog(logMessage);
        }
        private void CreateFile()
        {
            try
            {
                string programDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);

                string veriketAppFolder = Path.Combine(programDataFolder, "VeriketApp");

                if (!Directory.Exists(veriketAppFolder))
                {
                    Directory.CreateDirectory(veriketAppFolder);
                }

                filePath = Path.Combine(veriketAppFolder, "VeriketAppTest.txt");
            }
            catch (Exception ex)
            {
                filePath = string.Empty;
            }
        }

        protected override void OnStart(string[] args)
        {
            timer.Elapsed += new ElapsedEventHandler(ElapsedTime);
            timer.Interval = 60000;
            timer.Enabled = true;
        }

        protected override void OnStop()
        { 
            timer.Dispose();
        }

        public void WriteLog(string message)
        {
            var asd = this;
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(filePath, true))
                { 
                    streamWriter.WriteLine(message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        public Service1 GetService()
        {
            return this;
        }
    }
}
