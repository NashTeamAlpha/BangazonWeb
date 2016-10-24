using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using BangazonWeb.Data;

namespace BangazonWeb.Migrations
{
    [DbContext(typeof(BangazonWebContext))]
    partial class BangazonWebContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431");

            modelBuilder.Entity("BangazonWeb.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.HasKey("CustomerId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("BangazonWeb.Models.LineItem", b =>
                {
                    b.Property<int>("LineItemId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("OrderId");

                    b.Property<int>("ProductId");

                    b.HasKey("LineItemId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("LineItem");
                });

            modelBuilder.Entity("BangazonWeb.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CustomerId");

                    b.Property<DateTime>("DateCompleted")
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("strftime('%Y-%m-%d %H:%M:%S')");

                    b.Property<bool>("IsCompleted");

                    b.Property<int?>("PaymentTypeId");

                    b.HasKey("OrderId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("PaymentTypeId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("BangazonWeb.Models.PaymentType", b =>
                {
                    b.Property<int>("PaymentTypeId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CardNumber");

                    b.Property<string>("City")
                        .IsRequired();

                    b.Property<int>("CustomerId");

                    b.Property<DateTime>("ExpirationDate");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("Processor")
                        .IsRequired();

                    b.Property<string>("State")
                        .IsRequired();

                    b.Property<string>("StreetAddress")
                        .IsRequired();

                    b.Property<int>("ZipCode");

                    b.HasKey("PaymentTypeId");

                    b.HasIndex("CustomerId");

                    b.ToTable("PaymentType");
                });

            modelBuilder.Entity("BangazonWeb.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("strftime('%Y-%m-%d %H:%M:%S')");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 255);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<double>("Price");

                    b.Property<int>("ProductTypeId");

                    b.HasKey("ProductId");

                    b.HasIndex("ProductTypeId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("BangazonWeb.Models.ProductType", b =>
                {
                    b.Property<int>("ProductTypeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 255);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("ProductTypeId");

                    b.ToTable("ProductType");
                });

            modelBuilder.Entity("BangazonWeb.Models.LineItem", b =>
                {
                    b.HasOne("BangazonWeb.Models.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BangazonWeb.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BangazonWeb.Models.Order", b =>
                {
                    b.HasOne("BangazonWeb.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BangazonWeb.Models.PaymentType", "PaymentType")
                        .WithMany()
                        .HasForeignKey("PaymentTypeId");
                });

            modelBuilder.Entity("BangazonWeb.Models.PaymentType", b =>
                {
                    b.HasOne("BangazonWeb.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BangazonWeb.Models.Product", b =>
                {
                    b.HasOne("BangazonWeb.Models.ProductType", "ProductType")
                        .WithMany()
                        .HasForeignKey("ProductTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
