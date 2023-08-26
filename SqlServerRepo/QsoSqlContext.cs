using HamDotNetToolkit;
using Microsoft.EntityFrameworkCore;


namespace SqliteLib
{
    //Refactor: This and the sqlite could be made into on class
    public class QsoSqlContext : DbContext
    {
        public DbSet<Qso> Qsos { get; set; }
        public DbSet<QsoDetail> QsoDetail { get; set; }
        private string? connectionString;

        public QsoSqlContext(string? connectionString)
        {
            this.connectionString = connectionString;
            //this.connectionString = "Data Source = (localDB)\\MSSQLLocalDB; Initial Catalog = AmateurRadio";
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
