using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lab7.Entities;
using Microsoft.EntityFrameworkCore;

namespace lab7.EF
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Bus> Busses { get; set; }
        public DbSet<User> Users { get; set; }
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }
    }
}
