using Microsoft.EntityFrameworkCore;
using Todo_List_WebApp.Models;
using TodoList_MVC.Models;

namespace TodoList_MVC.Data
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
        : base(options) { }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<TaskModel> Tasks { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskModel>()
                .Property(t => t.Priority)
                .HasConversion<int>();

            modelBuilder.Entity<TaskModel>()
               .HasOne(sc => sc.User)
               .WithMany(s => s.Tasks)
               .HasForeignKey(sc => sc.UserID);
        }

    }
}
