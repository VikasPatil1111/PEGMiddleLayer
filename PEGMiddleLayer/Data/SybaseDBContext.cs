using EntityFrameworkCore.Ase;
using Microsoft.EntityFrameworkCore;
using PEGMiddleLayer.Models.Sales.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Data
{
    public class SybaseDBContext : DbContext
    {
        public SybaseDBContext(DbContextOptions<SybaseDBContext> options): base(options)
        {

        }

        public SybaseDBContext() { }
       
        public SybaseDBContext(string ConnectionString)
            : base()
        {

        }

        public static string ConnectionString { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            if (!string.IsNullOrEmpty(ConnectionString))
            {
                dbContextOptionsBuilder.UseAse(ConnectionString);
            }
        }


        public DbSet<CustomerMaster> CustomerMaster { get; set; }
    }
}
