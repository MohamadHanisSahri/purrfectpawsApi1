using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PurrfectpawsApi.Models;

namespace PurrfectpawsApi.DatabaseDbContext;

public partial class PurrfectpawsContext : DbContext
{
    public PurrfectpawsContext()
    {
    }

    public PurrfectpawsContext(DbContextOptions<PurrfectpawsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MCategory> MCategories { get; set; }

    public virtual DbSet<MOrderMaster> MOrderMasters { get; set; }

    public virtual DbSet<MOrderStatus> MOrderStatuses { get; set; }

    public virtual DbSet<MPaymentStatus> MPaymentStatuses { get; set; }

    public virtual DbSet<MRole> MRoles { get; set; }

    public virtual DbSet<MSize> MSizes { get; set; }

    public virtual DbSet<TBillingAddress> TBillingAddresses { get; set; }

    public virtual DbSet<TCart> TCarts { get; set; }

    public virtual DbSet<TLeadLength> TLeadLengths { get; set; }

    public virtual DbSet<TOrder> TOrders { get; set; }

    public virtual DbSet<TProduct> TProducts { get; set; }

    public virtual DbSet<TProductDetail> TProductDetails { get; set; }

    public virtual DbSet<TShippingAddress> TShippingAddresses { get; set; }

    public virtual DbSet<TTransaction> TTransactions { get; set; }

    public virtual DbSet<TUser> TUsers { get; set; }

    public virtual DbSet<TVariation> TVariations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId);

            entity.ToTable("M_Category");

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Category)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("category");
        });

        modelBuilder.Entity<MOrderMaster>(entity =>
        {
            entity.HasKey(e => e.OrderMasterId);

            entity.ToTable("M_Order_Master");

            entity.Property(e => e.OrderMasterId).HasColumnName("order_master_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<MOrderStatus>(entity =>
        {
            entity.HasKey(e => e.OrderStatusId);

            entity.ToTable("M_Order_Status");

            entity.Property(e => e.OrderStatusId).HasColumnName("order_status_id");
            entity.Property(e => e.Status)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("status");
        });

        modelBuilder.Entity<MPaymentStatus>(entity =>
        {
            entity.HasKey(e => e.PaymentStatusId);

            entity.ToTable("M_Payment_Status");

            entity.Property(e => e.PaymentStatusId).HasColumnName("payment_status_id");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("payment_status");
        });

        modelBuilder.Entity<MRole>(entity =>
        {
            entity.HasKey(e => e.RoleId);

            entity.ToTable("M_Role");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<MSize>(entity =>
        {
            entity.HasKey(e => e.SizeId);

            entity.ToTable("M_Size");

            entity.Property(e => e.SizeId).HasColumnName("size_id");
            entity.Property(e => e.SizeLabel)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("size_label");
        });

        modelBuilder.Entity<TBillingAddress>(entity =>
        {
            entity.HasKey(e => e.BillingAddressId);

            entity.ToTable("T_Billing_Address");

            entity.Property(e => e.BillingAddressId).HasColumnName("billing_address_id");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.Postcode).HasColumnName("postcode");
            entity.Property(e => e.State)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("state");
            entity.Property(e => e.Street1)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("street_1");
            entity.Property(e => e.Street2)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("street_2");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<TCart>(entity =>
        {
            entity.HasKey(e => e.CartId);

            entity.ToTable("T_Cart");

            entity.Property(e => e.CartId).HasColumnName("cart_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<TLeadLength>(entity =>
        {
            entity.HasKey(e => e.LeadLengthId).HasName("PK_T_Lead");

            entity.ToTable("T_Lead_Length");

            entity.Property(e => e.LeadLengthId).HasColumnName("lead_length_id");
            entity.Property(e => e.LeadLength)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("lead_length");
        });

        modelBuilder.Entity<TOrder>(entity =>
        {
            entity.HasKey(e => e.OrderId);

            entity.ToTable("T_Order");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.BillingAddressId).HasColumnName("billing_address_id");
            entity.Property(e => e.OrderMasterId).HasColumnName("order_master_id");
            entity.Property(e => e.OrderStatusId).HasColumnName("order_status_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.ShippingAddressId).HasColumnName("shipping_address_id");
            entity.Property(e => e.TotalPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total_price");
        });

        modelBuilder.Entity<TProduct>(entity =>
        {
            entity.HasKey(e => e.ProductId);

            entity.ToTable("T_Product");

            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.LeadLengthId).HasColumnName("lead_length_id");
            entity.Property(e => e.ProductDetailsId).HasColumnName("product_details_id");
            entity.Property(e => e.ProductQuantity).HasColumnName("product_quantity");
            entity.Property(e => e.SizeId).HasColumnName("size_id");
            entity.Property(e => e.VariationId).HasColumnName("variation_id");
        });

        modelBuilder.Entity<TProductDetail>(entity =>
        {
            entity.HasKey(e => e.ProductDetailsId);

            entity.ToTable("T_Product_Details");

            entity.Property(e => e.ProductDetailsId).HasColumnName("product_details_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.ProductCost)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("product_cost");
            entity.Property(e => e.ProductDescription)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("product_description");
            entity.Property(e => e.ProductName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("product_name");
            entity.Property(e => e.ProductPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("product_price");
            entity.Property(e => e.ProductProfit)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("product_profit");
            entity.Property(e => e.ProductRevenue)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("product_revenue");
        });

        modelBuilder.Entity<TShippingAddress>(entity =>
        {
            entity.HasKey(e => e.ShippingAddressId);

            entity.ToTable("T_Shipping_Address");

            entity.Property(e => e.ShippingAddressId).HasColumnName("shipping_address_id");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.Postcode).HasColumnName("postcode");
            entity.Property(e => e.State)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("state");
            entity.Property(e => e.Street1)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("street_1");
            entity.Property(e => e.Street2)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("street_2");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<TTransaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId);

            entity.ToTable("T_Transaction");

            entity.Property(e => e.TransactionId).HasColumnName("transaction_id");
            entity.Property(e => e.CartId).HasColumnName("cart_id");
            entity.Property(e => e.OrderMasterId).HasColumnName("order_master_id");
            entity.Property(e => e.PaymentStatusId).HasColumnName("payment_status_id");
            entity.Property(e => e.TransactionAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("transaction_amount");
            entity.Property(e => e.TransactionDate)
                .HasColumnType("datetime")
                .HasColumnName("transaction_date");
        });

        modelBuilder.Entity<TUser>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("T_User");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.HasMany(u => u.ShippingAddresses).WithOne(a => a.User).HasForeignKey(a => a.UserId);
            entity.HasMany(u => u.BillingAddresses).WithOne(a => a.User).HasForeignKey(a => a.UserId);
        });

        modelBuilder.Entity<TVariation>(entity =>
        {
            entity.HasKey(e => e.VariationId);

            entity.ToTable("T_Variation");

            entity.Property(e => e.VariationId).HasColumnName("variation_id");
            entity.Property(e => e.VariationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("variation_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
