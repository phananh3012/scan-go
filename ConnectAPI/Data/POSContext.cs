using System;
using System.Collections.Generic;
using ConnectAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ConnectAPI.Data
{
    public partial class POSContext : DbContext
    {
        public POSContext()
        {
        }

        public POSContext(DbContextOptions<POSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<LogProduct> LogProducts { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Purchase> Purchases { get; set; } = null!;
        public virtual DbSet<PurchaseDetail> PurchaseDetails { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Vendor> Vendors { get; set; } = null!;
        public virtual DbSet<VendorProduct> VendorProducts { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("categories");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("category_name");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customers");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("customer_name");

                entity.Property(e => e.CustomerPhone)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("customer_phone");

                entity.Property(e => e.CustomerPoint).HasColumnName("customer_point");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("FK__customers__useri__3E723F9C");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employees");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.EmployeeEmail)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("employee_email");

                entity.Property(e => e.EmployeeName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("employee_name");

                entity.Property(e => e.EmployeePhone)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("employee_phone");

                entity.Property(e => e.EmployeePosition)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("employee_position");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("FK__employees__useri__442B18F2");
            });

            modelBuilder.Entity<LogProduct>(entity =>
            {
                entity.HasKey(e => e.LogId)
                    .HasName("PK__log_prod__9E2397E0FFDBB6FB");

                entity.ToTable("log_product");

                entity.Property(e => e.LogId).HasColumnName("log_id");

                entity.Property(e => e.ActorId).HasColumnName("actor_id");

                entity.Property(e => e.Delivered).HasColumnName("delivered");

                entity.Property(e => e.EndQty).HasColumnName("end_qty");

                entity.Property(e => e.LogDate)
                    .HasColumnType("date")
                    .HasColumnName("log_date");

                entity.Property(e => e.LogTime)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("log_time");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("product_name");

                entity.Property(e => e.Received).HasColumnName("received");

                entity.Property(e => e.StartQty).HasColumnName("start_qty");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.LogProducts)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__log_produ__produ__0D99FE17");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.Change)
                    .HasColumnType("money")
                    .HasColumnName("change");

                entity.Property(e => e.Discount)
                    .HasColumnType("money")
                    .HasColumnName("discount");

                entity.Property(e => e.OrderCustomer).HasColumnName("order_customer");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("date")
                    .HasColumnName("order_date");

                entity.Property(e => e.OrderEmployee).HasColumnName("order_employee");

                entity.Property(e => e.OrderStatus)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("order_status");

                entity.Property(e => e.OrderTime)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("order_time");

                entity.Property(e => e.PaymentMethod)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("payment_method");

                entity.Property(e => e.Received)
                    .HasColumnType("money")
                    .HasColumnName("received");

                entity.Property(e => e.Subtotal)
                    .HasColumnType("money")
                    .HasColumnName("subtotal");

                entity.Property(e => e.Total)
                    .HasColumnType("money")
                    .HasColumnName("total");

                entity.HasOne(d => d.OrderCustomerNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.OrderCustomer)
                    .HasConstraintName("FK__orders__order_cu__116A8EFB");

                entity.HasOne(d => d.OrderEmployeeNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.OrderEmployee)
                    .HasConstraintName("FK__orders__order_em__10766AC2");

                entity.HasOne(d => d.PaymentMethodNavigation)
                    .WithMany(p => p.Orders)
                    .HasPrincipalKey(p => p.MethodName)
                    .HasForeignKey(d => d.PaymentMethod)
                    .HasConstraintName("FK__orders__payment___125EB334");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => e.DetailId)
                    .HasName("PK__order_de__38E9A2249014F535");

                entity.ToTable("order_detail");

                entity.Property(e => e.DetailId).HasColumnName("detail_id");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Qty).HasColumnName("qty");

                entity.Property(e => e.Sum)
                    .HasColumnType("money")
                    .HasColumnName("_sum");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__order_det__order__153B1FDF");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__order_det__produ__162F4418");
            });

            modelBuilder.Entity<PaymentMethod>(entity =>
            {
                entity.HasKey(e => e.MethodId)
                    .HasName("PK__payment___747727B663505252");

                entity.ToTable("payment_method");

                entity.HasIndex(e => e.MethodName, "UQ__payment___2DA2FAEEF1A18536")
                    .IsUnique();

                entity.Property(e => e.MethodId).HasColumnName("method_id");

                entity.Property(e => e.MethodName)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("method_name");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("products");

                entity.Property(e => e.ProductId)
                    .ValueGeneratedNever()
                    .HasColumnName("product_id");

                entity.Property(e => e.ProductBarcode)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("product_barcode");

                entity.Property(e => e.ProductBrand)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("product_brand");

                entity.Property(e => e.ProductCategory).HasColumnName("product_category");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("product_name");

                entity.Property(e => e.ProductPrice)
                    .HasColumnType("money")
                    .HasColumnName("product_price");

                entity.Property(e => e.ProductQuantity).HasColumnName("product_quantity");

                entity.Property(e => e.ProductUnit)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("product_unit");

                entity.HasOne(d => d.ProductCategoryNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductCategory)
                    .HasConstraintName("FK__products__produc__7C6F7215");

                entity.HasOne(d => d.ProductNavigation)
                    .WithOne(p => p.Product)
                    .HasForeignKey<Product>(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__products__produc__7B7B4DDC");
            });

            modelBuilder.Entity<Purchase>(entity =>
            {
                entity.ToTable("purchases");

                entity.Property(e => e.PurchaseId).HasColumnName("purchase_id");

                entity.Property(e => e.PurchaseDate)
                    .HasColumnType("date")
                    .HasColumnName("purchase_date");

                entity.Property(e => e.PurchaseEmployee).HasColumnName("purchase_employee");

                entity.Property(e => e.PurchaseStatus)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("purchase_status");

                entity.Property(e => e.PurchaseTime)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("purchase_time");

                entity.Property(e => e.Total)
                    .HasColumnType("money")
                    .HasColumnName("total");

                entity.Property(e => e.VendorId).HasColumnName("vendor_id");

                entity.HasOne(d => d.Vendor)
                    .WithMany(p => p.Purchases)
                    .HasForeignKey(d => d.VendorId)
                    .HasConstraintName("FK__purchases__vendo__07E124C1");
            });

            modelBuilder.Entity<PurchaseDetail>(entity =>
            {
                entity.HasKey(e => e.DetailId)
                    .HasName("PK__purchase__38E9A224DB84071E");

                entity.ToTable("purchase_detail");

                entity.Property(e => e.DetailId).HasColumnName("detail_id");

                entity.Property(e => e.Cost)
                    .HasColumnType("money")
                    .HasColumnName("cost");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("product_name");

                entity.Property(e => e.PurchaseId).HasColumnName("purchase_id");

                entity.Property(e => e.Qty).HasColumnName("qty");

                entity.Property(e => e.Sum)
                    .HasColumnType("money")
                    .HasColumnName("_sum");

                entity.Property(e => e.Unit)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("unit");

                entity.HasOne(d => d.Purchase)
                    .WithMany(p => p.PurchaseDetails)
                    .HasForeignKey(d => d.PurchaseId)
                    .HasConstraintName("FK__purchase___purch__0ABD916C");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.Username, "UQ__users__F3DBC57266D87631")
                    .IsUnique();

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.Property(e => e.UserPassword)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("user_password");

                entity.Property(e => e.UserType)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("user_type");

                entity.Property(e => e.Username)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.ToTable("vendor");

                entity.Property(e => e.VendorId).HasColumnName("vendor_id");

                entity.Property(e => e.VendorAddress)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("vendor_address");

                entity.Property(e => e.VendorEmail)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("vendor_email");

                entity.Property(e => e.VendorName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("vendor_name");

                entity.Property(e => e.VendorPhone)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("vendor_phone");
            });

            modelBuilder.Entity<VendorProduct>(entity =>
            {
                entity.HasKey(e => e.VpId)
                    .HasName("PK__vendor_p__06B3C403537FD9BB");

                entity.ToTable("vendor_products");

                entity.Property(e => e.VpId).HasColumnName("vp_id");

                entity.Property(e => e.Cost)
                    .HasColumnType("money")
                    .HasColumnName("cost");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("product_name");

                entity.Property(e => e.Unit)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("unit");

                entity.Property(e => e.VendorId).HasColumnName("vendor_id");

                entity.HasOne(d => d.Vendor)
                    .WithMany(p => p.VendorProducts)
                    .HasForeignKey(d => d.VendorId)
                    .HasConstraintName("FK__vendor_pr__vendo__1B5E0D89");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
