using HamDotNetToolkit;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SqliteLib
{
    public class QsoContext : DbContext
    {
        public DbSet<Qso> Qsos { get; set; }
        public DbSet<QsoDetail> QsoDetail { get; set; }
        private string? connectionString;
        public QsoContext(string? connectionString)
        {
            this.connectionString = connectionString;
            this.connectionString = "Data Source = (localDB)\\MSSQLLocalDB; Initial Catalog = AmateurRadio";
        }
        public QsoContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            //"Data Source = (localDB)\\MSSQLLocalDB; Initial Catalog = AmateurRadio");
            optionsBuilder.UseSqlServer(connectionString);



        }
    }
}
