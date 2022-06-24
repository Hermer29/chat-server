
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace ChatServer.UserSystem.Data
{
    internal class ChatDataContext : DbContext
    {
        public DbSet<MessageModel> Messages { get; set; }
        public DbSet<UserModel> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "ChatServer.db" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            optionsBuilder.UseSqlite(connection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MessageModel>().HasOne(m => m.User).WithMany(x => x.Messages).HasForeignKey(x => x.UserId).HasPrincipalKey(x => x.Id);
        }
    }
}
