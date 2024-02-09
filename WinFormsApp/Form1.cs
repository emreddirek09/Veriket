using System.Diagnostics.Metrics;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using System.ServiceProcess;

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {   
            InitializeComponent();
            InitializeDataGridView();
        }

        private void InitializeDataGridView()
        { 
            dataGridView1.Columns.Add("Column1", "Tarih");
            dataGridView1.Columns.Add("Column2", "Bilgisayar Adý");
            dataGridView1.Columns.Add("Column3", "Kullanýcý Adý");
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        string _baseDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory);

        private string subString(string path)
        {
            int index = path.IndexOf("Veriket");
            string text = path.Substring(0, index + "Veriket".Length);

            return text;
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
                string[] lines = File.ReadAllLines(veriketAppFolder);

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
