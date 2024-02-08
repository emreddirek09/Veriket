using System.Windows.Forms;

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
            dataGridView1.Columns.Add("Column1", "Date Time");
            dataGridView1.Columns.Add("Column2", "Computer Name");
            dataGridView1.Columns.Add("Column3", "User Name");
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        string _baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string filePath = AppDomain.CurrentDomain.BaseDirectory + "/VeriketApp";
        string textPath = AppDomain.CurrentDomain + "/VeriketApp/VeriketAppTest.text";
        string aa = "C:\\Users\\EMRE\\source\\repos\\Veriket\\WindowsService\\bin\\Debug\\VeriketApp\\VeriketAppTest.text"; 
        private void button1_Click(object sender, EventArgs e)
        {
            LoadLogFromCSV();
        }
        private void LoadLogFromCSV()
        {
            try
            {
                // CSV dosyasýný oku
                string[] lines = File.ReadAllLines(aa);

                // Her satýrý DataGridView'e ekle
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
