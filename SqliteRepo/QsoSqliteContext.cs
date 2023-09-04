using HamDotNetToolkit;

using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace SqliteLib
{
    //Refactor: This and the sqlserver could be made into on class
    public class QsoSqliteContext : DbContext
    {
        private string connectionString;
        public DbSet<Qso> Qsos { get; set; }
        public DbSet<QsoDetail> QsoDetail { get; set; }

        public DbSet<AwardsQSO> AwardQSO { get; set; }
        public DbSet<ExternalLogbooks> ExternalLogbooksQso { get; set; }
        public DbSet<ContestQsoInfo> ContestQsoInfos { get; set; }

        public DbSet<ContestEnums> ContestEnums { get; set; }

        public QsoSqliteContext(string? connectionString)
        {
            if (connectionString is null) 
                throw new ArgumentNullException(nameof(connectionString));
            this.connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(connectionString);
        }
    }
}
