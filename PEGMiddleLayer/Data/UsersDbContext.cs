using Microsoft.EntityFrameworkCore;
using PEGMiddleLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEGMiddleLayer.Data
{
    public class UsersDbContext : DbContext
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options) { }

        public DbSet<AspNetRoleClaims> aspNetRoleClaims { get; set; }

    }
}
