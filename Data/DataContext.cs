using CP.API.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CP.API.Data
{
    public class DataContext : IdentityDbContext<User,Role,int,IdentityUserClaim<int>,UserRole,IdentityUserLogin<int>,IdentityRoleClaim<int>,IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Shipper> Shippers { get; set; }
        // public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Vendor> Vendors { get; set; }

        public DbSet<PhotoForSupplier> PhotoForSuppliers { get; set; }
        public DbSet<PhotoForProduct> PhotoForProducts { get; set; }
        public DbSet<SubCategory> SubCategorys { get; set;}
        public DbSet<SocialCommunication> SocialCommunications {get; set;}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserRole>(
                supplierRole=>{
                    supplierRole.HasKey(sp=>new{sp.UserId,sp.RoleId});
                    supplierRole.HasOne(sp=>sp.Role)
                    .WithMany(r=>r.UserRoles)
                    .HasForeignKey(sr=>sr.RoleId)
                    .IsRequired();

                     supplierRole.HasOne(sp=>sp.User)
                    .WithMany(r=>r.UserRoles)
                    .HasForeignKey(sr=>sr.UserId)
                    .IsRequired();
                }
            );

            builder.Entity<Customer>()
            .HasMany<Order>(o => o.Orders)
            .WithOne(o => o.Customer)
            .HasForeignKey(c => c.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<PaymentType>()
            .HasMany<Order>(p => p.Orders)
            .WithOne(p => p.Payment)
            .HasForeignKey(p => p.PaymentId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Shipper>()
            .HasMany<Order>(s => s.Orders)
            .WithOne(s => s.Shipper)
            .HasForeignKey(s => s.ShipperId)
            .OnDelete(DeleteBehavior.Restrict);

            

            builder.Entity<Order>()
            .HasMany<OrderDetail>(d => d.OrderDetails)
            .WithOne(d => d.Order)
            .HasForeignKey(d => d.OrderId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Category>()
              .HasMany<SubCategory>(d => d.SubCategorys)
              .WithOne(d => d.Category)
              .HasForeignKey(d => d.CategoryId)
              .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<User>()
                          .HasMany<PhotoForSupplier>(d => d.PhotoForSuppliers)
                          .WithOne(d => d.Supplier)
                          .HasForeignKey(d => d.SupplierId)
                          .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<Product>()
            .HasMany<OrderDetail>(d => d.OrderDetails)
            .WithOne(d => d.Product)
            .HasForeignKey(d => d.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

         
            builder.Entity<SubCategory>()
            .HasMany<Product>(p => p.Products)
            .WithOne(p => p.SubCategory)
            .HasForeignKey(p => p.SubCategoryId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Coupon>()
            .HasMany<Product>(p => p.Products)
            .WithOne(s => s.Coupon)
            .HasForeignKey(s => s.CouponId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Vendor>()
           .HasMany<Product>(p => p.Products)
           .WithOne(s => s.Vendor)
           .HasForeignKey(s => s.VendorId)
           .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<User>()
            .HasMany<PhotoForSupplier>(p => p.PhotoForSuppliers)
            .WithOne(s => s.Supplier)
            .HasForeignKey(s => s.SupplierId)
            .OnDelete(DeleteBehavior.Cascade);


            builder.Entity<Product>()
            .HasMany<PaymentType>(p => p.Payments)
            .WithOne(s => s.Product)
            .HasForeignKey(s => s.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Product>()
            .HasMany<PhotoForProduct>(p => p.PhotoForProducts)
            .WithOne(s => s.Product)
            .HasForeignKey(s => s.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

         


        }


    }
}