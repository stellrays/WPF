using Microsoft.Data.SqlClient;

using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Drawing.Text;
namespace WinFormsAppADONet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadData();
        }
        private SqlDataAdapter adapter = null;
        private DataTable dataTable = null;
        private void LoadData()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionLocalDb"].ConnectionString;
            // Создание подключения
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                //Подключение открыто
                adapter = new SqlDataAdapter("SELECT * FROM Users", connectionString);
                dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
            //Подключение закрыто
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}



