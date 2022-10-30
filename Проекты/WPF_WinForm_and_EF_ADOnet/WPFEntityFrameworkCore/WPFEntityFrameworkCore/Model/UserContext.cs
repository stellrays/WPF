using Microsoft.EntityFrameworkCore;
using System.Configuration;
namespace WPFEntityFrameworkCore.Model
{
    public partial class UserContext : DbContext
    {
        public UserContext()
        {
        }
        public UserContext(DbContextOptions<UserContext> options)
            : base(options) { }        
        public DbSet<User> Users => Set<User>();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Подключение к SQL Server из App.config
                optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["ConnectionLocalDb"].ToString());
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}


	