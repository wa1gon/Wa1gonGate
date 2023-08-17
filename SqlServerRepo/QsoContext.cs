using HamDotNetToolkit;
using Microsoft.EntityFrameworkCore;
namespace SqlServerRepo
{
    public class QsoContext : DbContext
    {
        public DbSet<Qso> Qsos { get; set; }
        public DbSet<QsoDetail> QsoDetail { get; set; }
        private string? connectionString;
        public QsoContext(string? connectionString)
        {
            this.connectionString = connectionString;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(connectionString);


            //"Data Source = (localDB)\\MSSQLLocalDB; Initial Catalog = AmateurRadio");
        }
    }
}
