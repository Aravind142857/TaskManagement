using System;
using System.Collections.Generic;
using System.Linq;
using backend.Types;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using backend.Auth;
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
        public DbSet<backend.Types.UserTasks> UserTasks { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<backend.Types.UserTasks>()
                .HasKey(ut => new { ut.UserId, ut.TaskId }); // Composite Key

            modelBuilder.Entity<backend.Types.UserTasks>()
                .HasOne(ut => ut.User)
                .WithMany(u => u.UserTasks)
                .HasForeignKey(ut => ut.UserId); // 1 User -> Many UserTasks

            modelBuilder.Entity<backend.Types.UserTasks>()
                .HasOne(ut => ut.Task)
                .WithMany(t => t.UserTasks)
                .HasForeignKey(ut => ut.TaskId); // 1 Task -> Many UserTasks

            base.OnModelCreating(modelBuilder);
        }
    }
}