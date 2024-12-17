using Microsoft.EntityFrameworkCore;
using ShoppingCart.API.Models;

namespace ShoppingCart.API.Data
{
    public class CartDbContext : DbContext
    {
        public CartDbContext(DbContextOptions<CartDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<ItemCart> ItemCart {  get; set; }
        public DbSet<CustomerCart> CustomerCart { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e =>
                e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("VARCHAR(160)");

            modelBuilder.Ignore<Voucher>();

            modelBuilder.Entity<CustomerCart>()
                .HasIndex(x => x.CustomerId)
                .HasDatabaseName("IDX_Customer");

            modelBuilder.Entity<CustomerCart>()
                .Ignore(c => c.Voucher)
                .OwnsOne(x => x.Voucher, v =>
                {
                    v.Property(vc => vc.Code)
                    .HasColumnName("VoucherCode")
                    .HasColumnType("VARCHAR(50)");

                    v.Property(vc => vc.DiscountType).HasColumnName("DiscountType");

                    v.Property(x => x.Percentual).HasColumnName("Percentual");

                    v.Property(x => x.DiscountValue).HasColumnName("DiscountValue");
                });

            modelBuilder.Entity<CustomerCart>().HasKey(x => x.Id);
            modelBuilder.Entity<ItemCart>().HasKey(x => x.Id);

            modelBuilder.Entity<CustomerCart>()
                .HasMany(x => x.Itens)
                .WithOne(x => x.CustomerCart)
                .HasForeignKey(x => x.CartId);

            foreach(var rel in modelBuilder.Model.GetEntityTypes().SelectMany(x => x.GetForeignKeys()))
                rel.DeleteBehavior = DeleteBehavior.Cascade;
        }
    }
}
