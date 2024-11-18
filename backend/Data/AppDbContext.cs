using System;
using System.Collections.Generic;
using System.Linq;
using backend.Types;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace backend.Data {
    public class AppDbContext : DbContext {
        // public AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base (options) { }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Initial Catalog=DBName;Integrated Security=True");
        // }

        public DbSet<backend.Types.Task> Tasks { get; set; }
        public DbSet<backend.Auth.User> Users { get; set; }
    }
}