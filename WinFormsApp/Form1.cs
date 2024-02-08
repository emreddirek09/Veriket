using System.Diagnostics.Metrics;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

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
            // DataGridView özelliklerini ayarla
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
                string[] lines = File.ReadAllLines(subString(_baseDirectory) + "\\WindowsService\\bin\\Debug\\VeriketApp\\VeriketAppTest.text");

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
        private void button2_Click(object sender, EventArgs e)
        {
            FullList();
        }
        private void FullList()
        {
            dataGridView1.Rows.Clear();
            try
            {
                string[] lines = File.ReadAllLines(subString(_baseDirectory) + "\\WindowsService\\bin\\Debug\\VeriketApp\\VeriketAppTest.text");
                List<string> lastData = lines.Skip(Math.Max(0, lines.Count() - 5)).ToList();

                // Son on veriyi DataGridView'e ekle
                foreach (string data in lastData)
                {
                    string[] parts = data.Split(',');
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
