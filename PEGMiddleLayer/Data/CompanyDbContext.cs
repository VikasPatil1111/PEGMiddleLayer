using EntityFrameworkCore.Ase;
using Microsoft.EntityFrameworkCore;
using PEGMiddleLayer.Models.Sales.Masters;
using PEGMiddleLayer.Models.Sales.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Data
{
    public class CompanyDbContext : DbContext
    {

        
        public CompanyDbContext(){}
        public CompanyDbContext( DbContextOptions<CompanyDbContext> dbContextOptions ):base(dbContextOptions)
        {
            
        }
        public CompanyDbContext(string ConnectionString) 
            :base() { 
                
        }

        public static string ConnectionString { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder) {
            if (!string.IsNullOrEmpty(ConnectionString)) {
                dbContextOptionsBuilder.UseSqlServer(ConnectionString);
            } 
        }
        public DbSet<CustomerMaster> customerMasters { get; set; }

        public DbSet<Consignee_Type_Master> consignee_Type_Master { get; set; }

    }
}
