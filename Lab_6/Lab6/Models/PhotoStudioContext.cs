using Microsoft.EntityFrameworkCore;

namespace Lab6.Models
{
    public class PhotoStudioContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Order> Orders { get; set; }
        
        public PhotoStudioContext(DbContextOptions<PhotoStudioContext> options)
            :base(options)
        {
            Database.EnsureCreated();
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasOne(order => order.Client)
                .WithMany(o => o.Orders)
                .HasForeignKey(order => order.ClientId);

            modelBuilder.Entity<Order>()
                .HasOne(order => order.Option)
                .WithMany(o => o.Orders)
                .HasForeignKey(order => order.OptionId);
        }



    }
}
