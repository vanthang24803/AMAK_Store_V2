using AMAK.Domain.Enums;
using AMAK.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AMAK.Infrastructure.Context {
    public class ApplicationDbContext(DbContextOptions options)
        : IdentityDbContext<ApplicationUser>(options) {
        public required DbSet<Address> Addresses { get; init; }
        public required DbSet<AccountConfig> AccountConfigs { get; init; }
        public required DbSet<Billboard> Billboards { get; init; }
        public required DbSet<Blog> Blogs { get; init; }
        public required DbSet<Product> Products { get; init; }
        public required DbSet<Conversation> Conversations { get; init; }
        public required DbSet<Category> Categories { get; init; }
        public required DbSet<Chat> Chats { get; init; }
        public required DbSet<Cart> Carts { get; init; }
        public required DbSet<Configuration> Configurations { get; init; }
        public required DbSet<CartDetail> CartDetails { get; init; }
        public required DbSet<Option> Options { get; init; }
        public required DbSet<Order> Orders { get; init; }
        public required DbSet<OrderStatus> OrderStatus { get; init; }
        public required DbSet<MessageUser> MessageUsers { get; init; }
        public required DbSet<Notification> Notifications { get; init; }
        public required DbSet<Photo> Photos { get; init; }
        public required DbSet<Prompt> Prompts { get; init; }
        public required DbSet<EmailTemplate> Templates { get; init; }
        public required DbSet<ReviewPhoto> ReviewPhotos { get; init; }
        public required DbSet<Voucher> Vouchers { get; init; }
        public required DbSet<FlashSale> FlashSales { get; init; }
        public required DbSet<FlashSaleProduct> FlashSaleProducts { get; init; }
        public required DbSet<CancelOrder> CancelOrders { get; init; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>(entity => {
                entity.Property(x => x.FirstName).HasMaxLength(128);
                entity.Property(x => x.LastName).HasMaxLength(128);
                entity.Property(x => x.PhoneNumber).HasMaxLength(50);
                entity.Property(x => x.Email).HasMaxLength(128);
                entity.HasIndex(x => x.Email).IsUnique();
            });

            modelBuilder.Entity<Configuration>(e => {
                e.Property(x => x.Key).HasMaxLength(128);
                e.Property(x => x.Value).HasColumnType("jsonb");
            });

            modelBuilder.Entity<Conversation>(e => {
                e.Property(x => x.IsBotReply).HasDefaultValue(false);
            });

            modelBuilder.Entity<Option>(entity => {
                entity.ToTable("Options");
                entity.Property(x => x.Name).HasMaxLength(256);
                entity.Property(x => x.IsActive).HasDefaultValue(true);
                entity.Property(x => x.IsFlashSale).HasDefaultValue(false);
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
                e.Property(x => x.Payment).HasDefaultValue(EPayment.COD);
                e.Property(x => x.Shipping).HasDefaultValue(true);
            });

            modelBuilder.Entity<AccountConfig>(e => {
                e.Property(x => x.IsBan).HasDefaultValue(false);
                e.Property(x => x.IsActiveNotification).HasDefaultValue(false);
                e.Property(x => x.Language).HasDefaultValue(ELanguage.VI);
                e.Property(x => x.Timezone).HasDefaultValue(ETimezone.ICT);
            });

            modelBuilder.Entity<Prompt>()
                .HasIndex(p => p.Type)
                .IsUnique();

            modelBuilder.Entity<EmailTemplate>()
                .HasIndex(et => et.Name)
                .IsUnique();


            modelBuilder.Entity<Blog>(
                e => {
                    e.ToTable("Blogs");
                    e.Property(x => x.Title).HasMaxLength(255);
                }
            );


            // TODO: One-to-One

            modelBuilder.Entity<ApplicationUser>()
                .HasOne(u => u.Cart)
                .WithOne(c => c.User)
                .HasForeignKey<Cart>(c => c.UserId);

            modelBuilder.Entity<ApplicationUser>()
                .HasOne(u => u.Config)
                .WithOne(c => c.User)
                .HasForeignKey<AccountConfig>(c => c.UserId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.CancelOrder)
                .WithOne(c => c.Order)
                .HasForeignKey<CancelOrder>(c => c.OrderId);


            // TODO: May-to-Many
            modelBuilder.Entity<Product>()
                .HasMany(e => e.Categories)
                .WithMany(e => e.Products)
                .UsingEntity<ProductCategory>();

            modelBuilder.Entity<FlashSale>()
                .HasMany(e => e.Options)
                .WithMany(e => e.FlashSales)
                .UsingEntity<FlashSaleProduct>();

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
                    j => { j.HasKey(mu => new { mu.UserId, mu.NonfictionId }); }
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

            modelBuilder.Entity<Blog>()
                .HasOne(u => u.Author)
                .WithMany(b => b.Blogs)
                .HasForeignKey(u => u.AuthorId)
                .IsRequired();

            modelBuilder.Entity<Order>()
                .HasMany(o => o.Status)
                .WithOne(os => os.Order)
                .HasForeignKey(os => os.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CartDetail>()
                .HasOne(c => c.Cart)
                .WithMany(x => x.Details)
                .HasForeignKey(c => c.CartId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CartDetail>()
                .HasOne(cd => cd.Option)
                .WithMany(o => o.Carts)
                .HasForeignKey(cd => cd.OptionId)
                .OnDelete(DeleteBehavior.Cascade);


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

            // // TODO: Seed Data

            modelBuilder.Entity<Configuration>().HasData(
                new Configuration() {
                    Id = Guid.NewGuid(),
                    Key = Application.Constants.Configuration.Google
                },
                new Configuration() {
                    Id = Guid.NewGuid(),
                    Key = Application.Constants.Configuration.Cloudinary
                },
                new Configuration() {
                    Id = Guid.NewGuid(),
                    Key = Application.Constants.Configuration.Email
                },
                new Configuration() {
                    Id = Guid.NewGuid(),
                    Key = Application.Constants.Configuration.Momo
                },
                new Configuration() {
                    Id = Guid.NewGuid(),
                    Key = Application.Constants.Llm.Gemini
                },
                new Configuration() {
                    Id = Guid.NewGuid(),
                    Key = Application.Constants.Llm.Chatgpt4
                },
                new Configuration() {
                    Id = Guid.NewGuid(),
                    Key = Application.Constants.Llm.Chatgpt4O
                }
            );

            modelBuilder.Entity<Prompt>().HasData(
                new Prompt() {
                    Id = Guid.NewGuid(),
                    Type = EPrompt.ANALYTIC_REVENUE,
                    Context = Application.Constants.Prompt.AnalyticRevenue
                },
                new Prompt() {
                    Id = Guid.NewGuid(),
                    Type = EPrompt.ANALYTIC_REVIEW,
                    Context = Application.Constants.Prompt.AnalyticReview,
                },
                new Prompt() {
                    Id = Guid.NewGuid(),
                    Type = EPrompt.ANALYTIC_STATISTIC,
                    Context = Application.Constants.Prompt.AnalyticStatistic
                }
            );
        }
    }
}