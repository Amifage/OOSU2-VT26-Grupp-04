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

        public DbSet<Utrustning> utrustning { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(
                "Server=localhost;Database=oosu2604;Trusted_Connection=True;");
        }

        

        // här behöver det stå något som typ: public DbSet<Utrustning> utrustning {get; set;} för att skapa ett table för utrsutning.
    }
}
