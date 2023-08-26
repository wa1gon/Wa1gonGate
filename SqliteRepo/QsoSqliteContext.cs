using HamDotNetToolkit;
using Microsoft.EntityFrameworkCore;


namespace SqliteLib
{
    //Refactor: This and the sqlserver could be made into on class
    public class QsoSqliteContext : DbContext
    {
        public DbSet<Qso> Qsos { get; set; }
        public DbSet<QsoDetail> QsoDetail { get; set; }
        private string? connectionString;

        public QsoSqliteContext(string? connectionString)
        {
            this.connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(connectionString);
        }
    }
}
