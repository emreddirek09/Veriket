using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VeriPaket
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeDataGridView();
            if (!IsRunAsAdmin())
            {
                RunAsAdmin(); 
            }
            else
            {
                InitInstaller();
                InitWindowsService();
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            if (!IsRunAsAdmin())
            { 
                Application.Exit();
            }
        }


        #region WindowsServiceMethods

        private void InitWindowsService()
        {
            try
            {
                ServiceController sc = new ServiceController("Veriket Application");
                if (sc.Status == ServiceControllerStatus.Stopped)
                {
                    sc.Start();
                }
                else
                {
                    MessageBox.Show("Hizmet başlatılamadı. Durum: " + sc.Status.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void InitInstaller()
        {
            try
            {
                using (AssemblyInstaller installer = new AssemblyInstaller(typeof(WindowsService.ProjectInstaller).Assembly, null))
                {
                    installer.UseNewContext = true;
                    installer.Install(null);
                    installer.Commit(null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }
        private bool IsRunAsAdmin()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        private void RunAsAdmin()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.UseShellExecute = true;
            startInfo.WorkingDirectory = Environment.CurrentDirectory;
            startInfo.FileName = Application.ExecutablePath;
            startInfo.Verb = "runas";

            try
            {

                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Yönetici olarak çalıştırma başarısız oldu: " + ex.Message);
            } 
            Application.Exit(); 
        }
        #endregion

        private void InitializeDataGridView()
        {
            dataGridView1.Columns.Add("Column1", "Tarih");
            dataGridView1.Columns.Add("Column2", "Bilgisayar Adı");
            dataGridView1.Columns.Add("Column3", "Kullanıcı Adı");
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            LoadLogFromCSV();
        }
        private void LoadLogFromCSV()
        {
            dataGridView1.Rows.Clear();

            try
            {
                string programDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
                string veriketAppFolder = Path.Combine(programDataFolder, "VeriketApp");
                string filePath = Path.Combine(veriketAppFolder, "VeriketAppTest.txt");
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length >= 3)
                    {
                        dataGridView1.Rows.Add(parts[0], parts[1], parts[2]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
