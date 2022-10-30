using Microsoft.EntityFrameworkCore;
namespace WinFormsAppEntityFrameworkCore
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            UserContext db;
            db = new UserContext();
            db.Users.Load();
            dataGridView1.DataSource = db.Users.Local.ToBindingList();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}