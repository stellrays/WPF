using System.Windows;
using Microsoft.EntityFrameworkCore;
using WPFEntityFrameworkCore.Model;
namespace WPFEntityFrameworkCore
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            UserContext db;
            db = new UserContext();
            db.Users.Load();
            UsersGrid.ItemsSource = db.Users.Local.ToBindingList();
        }
    }
}
