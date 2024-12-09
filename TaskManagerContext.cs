using System.Data.Entity;
using SmartTaskManager.Models;

namespace SmartTaskManager.Data
{
    public class TaskManagerContext : DbContext
    {
        public TaskManagerContext() : base("name=TaskManagerDBConnectionString")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Tasks)
                .WithRequired(t => t.User)
                .HasForeignKey(t => t.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Log>()
                .HasRequired(l => l.User)
                .WithMany()
                .HasForeignKey(l => l.UserID)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}