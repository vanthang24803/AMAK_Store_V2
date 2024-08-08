using AMAK.Domain.Enums;
using AMAK.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AMAK.Infrastructure.Context {
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> {
        public ApplicationDbContext(DbContextOptions options) : base(options) {
        }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Billboard> Billboards { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Photo> Photos { get; set; }

        public DbSet<Option> Options { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<ReviewPhoto> ReviewPhotos { get; set; }

        public DbSet<Voucher> Vouchers { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        public DbSet<MessageUser> MessageUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>(entity => {
                entity.Property(x => x.FirstName).HasMaxLength(128);
                entity.Property(x => x.LastName).HasMaxLength(128);
                entity.Property(x => x.PhoneNumber).HasMaxLength(12);
                entity.Property(x => x.Email).HasMaxLength(128);
                entity.HasIndex(x => x.Email).IsUnique();
            });

            modelBuilder.Entity<Option>(entity => {
                entity.ToTable("Options");
                entity.Property(x => x.Name).HasMaxLength(256);
                entity.Property(x => x.IsActive).HasDefaultValue(true);
            });

            modelBuilder.Entity<Product>(e => {
                e.HasIndex(x => x.Name).IsUnique();
                e.Property(x => x.Name).HasMaxLength(256);
            });

            modelBuilder.Entity<Order>(e => {
                e.ToTable("Orders");
                e.Property(x => x.Email).HasMaxLength(128);
                e.Property(x => x.Customer).HasMaxLength(128);
                e.Property(x => x.Address).HasMaxLength(256);
                e.Property(x => x.Status).HasDefaultValue(EOrderStatus.PENDING);
                e.Property(x => x.Payment).HasDefaultValue(EPayment.COD);
                e.Property(x => x.Shipping).HasDefaultValue(true);
            });

            // TODO: May-to-Many
            modelBuilder.Entity<Product>()
                        .HasMany(e => e.Categories)
                        .WithMany(e => e.Products)
                        .UsingEntity<ProductCategory>();

            modelBuilder.Entity<Notification>()
                        .HasMany(e => e.Users)
                        .WithMany(e => e.Notifications)
                        .UsingEntity<MessageUser>(
                            j => j
                                .HasOne<ApplicationUser>()
                                .WithMany()
                                .HasForeignKey(mu => mu.UserId), 
                            j => j
                                .HasOne<Notification>()
                                .WithMany()
                                .HasForeignKey(mu => mu.NonfictionId),
                            j => {
                                j.HasKey(mu => new { mu.UserId, mu.NonfictionId });
                            }
                        );

            modelBuilder.Entity<Option>()
                        .HasMany(e => e.Orders)
                        .WithMany(e => e.Options)
                        .UsingEntity<OrderDetail>();

            // TODO: One-to-Many 
            modelBuilder.Entity<Address>()
                        .HasOne(u => u.User)
                        .WithMany(a => a.Addresses)
                        .HasForeignKey(u => u.UserId)
                        .IsRequired();

            modelBuilder.Entity<Review>()
                        .HasOne(u => u.User)
                        .WithMany(a => a.Reviews)
                        .HasForeignKey(u => u.UserId)
                        .IsRequired();

            modelBuilder.Entity<Order>()
                        .HasOne(u => u.User)
                        .WithMany(a => a.Orders)
                        .HasForeignKey(u => u.UserId)
                        .IsRequired();


            modelBuilder.Entity<Photo>()
                        .HasOne(p => p.Product)
                        .WithMany(p => p.Photos)
                        .HasForeignKey(p => p.ProductId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Option>()
                        .HasOne(p => p.Product)
                        .WithMany(p => p.Options)
                        .HasForeignKey(p => p.ProductId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Review>()
                        .HasOne(p => p.Product)
                        .WithMany(p => p.Reviews)
                        .HasForeignKey(p => p.ProductId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<ReviewPhoto>()
                        .HasOne(rp => rp.Review)
                        .WithMany(r => r.Photos)
                        .HasForeignKey(rp => rp.ReviewId)
                        .OnDelete(DeleteBehavior.Cascade);
        }
    }
}