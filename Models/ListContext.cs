using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace To_Do_List.Models
{
    public class ListContext : DbContext
    {
        public ListContext(){}
        public ListContext(DbContextOptions<ListContext> options) : base(options){}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.ConfigureWarnings(waring => waring.Ignore(RelationalEventId.PendingModelChangesWarning));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

       public virtual DbSet<Task> Tasks { get; set; }
       public virtual DbSet<User> Users { get; set; }
    }
}
