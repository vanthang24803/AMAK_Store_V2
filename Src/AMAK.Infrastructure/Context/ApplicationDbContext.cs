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
        public DbSet<ReviewPhoto> ReviewPhotos { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<MessageUser> MessageUsers { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<AccountConfig> AccountConfigs { get; set; }
        public DbSet<Configuration> Configurations { get; set; }
        public DbSet<EmailTemplate> Templates { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }
        public DbSet<Prompt> Prompts { get; set; }
        public DbSet<AIConfig> AIConfigs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>(entity => {
                entity.Property(x => x.FirstName).HasMaxLength(128);
                entity.Property(x => x.LastName).HasMaxLength(128);
                entity.Property(x => x.PhoneNumber).HasMaxLength(12);
                entity.Property(x => x.Email).HasMaxLength(128);
                entity.HasIndex(x => x.Email).IsUnique();
            });

            modelBuilder.Entity<AIConfig>(e => {
                e.Property(x => x.Name).HasMaxLength(128);
                e.Property(x => x.Config).HasColumnType("jsonb");
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

            // TODO: One-to-One

            modelBuilder.Entity<ApplicationUser>()
                       .HasOne(u => u.Cart)
                       .WithOne(c => c.User)
                       .HasForeignKey<Cart>(c => c.UserId);

            modelBuilder.Entity<ApplicationUser>()
                       .HasOne(u => u.Config)
                       .WithOne(c => c.User)
                       .HasForeignKey<AccountConfig>(c => c.UserId);


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

            // TODO: Seed Data

            modelBuilder.Entity<AIConfig>().HasData(
                new AIConfig() {
                    Id = Guid.NewGuid(),
                    Name = ILMM.Gemini.ToString()
                },
                new AIConfig() {
                    Id = Guid.NewGuid(),
                    Name = ILMM.ChatGPT4.ToString()
                },
                 new AIConfig() {
                     Id = Guid.NewGuid(),
                     Name = ILMM.ChatGPT3_5.ToString()
                 }
            );

            modelBuilder.Entity<Prompt>().HasData(
                new Prompt() {
                    Id = Guid.NewGuid(),
                    Type = EPrompt.ANALYTIC_REVENUE,
                    Context = @"
                        Bạn là một AI Phân tích dữ liệu 
                        - Chỉ trả lời bằng tiếng Việt.
                        - Dựa vào các dữ liệu đã gửi hãy phân tích chi tiết dữ liệu tăng giảm
                        - Đây là dữ liệu tổng doanh thu trong các thời điểm trong năm với year month week đều là tháng hiện tại hãy đưa ra các phân tích theo từng
                        mục thời gian rồi tóm lại tổng quát 
                        - Có thể viết thành 1 bài phân tích ngắn không được nói chuyện dở dang khoảng 1000 ký tự
                        Vui lòng phân tích dữ liệu sau: {DATA}.
                    "
                },
                  new Prompt() {
                      Id = Guid.NewGuid(),
                      Type = EPrompt.ANALYTIC_REVIEW,
                      Context = @"
                        Bạn là một AI Phân tích dữ liệu 
                        - Chỉ trả lời bằng tiếng Việt.
                        - Dựa vào các dữ liệu đã gửi hãy phân tích và đưa ra đánh giá về các review đơn hàng hãy đưa ra phân tích chung về các review tốt và xấu của sản phẩm và đưa ra số liệu trung bình đây là 1 sản phẩm tốt hay xấu đáng mua hay không ?
                        - Có thể viết thành 1 bài phân tích ngắn không được nói chuyện dở dang khoảng 500 ký tự
                        Vui lòng phân tích dữ liệu sau: {DATA}.
                        ",
                  },
                  new Prompt() {
                      Id = Guid.NewGuid(),
                      Type = EPrompt.ANALYTIC_STATISTIC,
                      Context = @"
                        Bạn là một AI Phân tích dữ liệu 
                        - Chỉ trả lời bằng tiếng Việt.
                        - Dựa vào các dữ liệu đã gửi hãy phân tích chi tiết dữ liệu tăng giảm
                        - Nếu isStock là false thì đó là % giảm so với tháng trước tương ứng với stock ví dụ isStock là false và stock là 20% là giảm 20% so với tháng trước , total là tổng số dữ liệu trong tháng hiện tại hãy đưa ra các nhận xét về việc kinh doanh trong tháng hiện tại 
                        - Trả lời không trả về ## đầu dòng
                        - Mỗi ý trả lời xong phải . xuống dòng là có dấu cách đầu dòng và viết hoa chữ cái đầu mỗi dòng
                        - Chỉ trả các phân tích chứ k cần thiết nói ra các trường data bằng tiếng anh
                        - Không nói xen lẫn tiếng anh tiếng việt
                        - Có thể viết thành 1 bài phân tích ngắn không được nói chuyện dở dang khoảng 1000 ký tự
                        Vui lòng phân tích dữ liệu sau: {DATA}."
                  }
            );
        }
    }
}