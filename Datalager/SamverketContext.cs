using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Entitetslager;

namespace Datalager
{
    public class SamverketContext : DbContext
    {

        public DbSet<Utrustning> Utrustning { get; set; } //Skapar Utrustning table i sql databasen

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(
                "Server=sqlutb4-db.hb.se,56077;" +
                "Database=oosu2604;" +
                "User Id=oosu2604;" + //Inloggningsuppfifter
                "Password=RKW148;" +
                "Encrypt=True;" +
                "TrustServerCertificate=True;");
        }

    }
}
