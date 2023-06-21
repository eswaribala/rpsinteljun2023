﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProductAPI.Contexts;

#nullable disable

namespace ProductAPI.Migrations
{
    [DbContext(typeof(ProductContext))]
    partial class ProductContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ProductAPI.Models.Product", b =>
                {
                    b.Property<long>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("ProductId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ProductId"), 1L, 1);

                    b.Property<long>("CategoryId")
                        .HasColumnType("bigint");

                    b.Property<string>("ProductName")
                        .HasColumnType("varchar(200)")
                        .HasColumnName("ProductName");

                    b.Property<string>("SKU")
                        .HasColumnType("varchar(200)")
                        .HasColumnName("SKU");

                    b.HasKey("ProductId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("ProductAPI.Models.Product", b =>
                {
                    b.OwnsOne("ProductAPI.Models.ProductDescription", "productDescription", b1 =>
                        {
                            b1.Property<long>("ProductId")
                                .HasColumnType("bigint");

                            b1.Property<long>("BufferLevel")
                                .HasColumnType("bigint")
                                .HasColumnName("BufferLevel");

                            b1.Property<long>("Cost")
                                .HasColumnType("bigint")
                                .HasColumnName("Cost");

                            b1.Property<DateTime>("ExpiryDate")
                                .HasColumnType("datetime2")
                                .HasColumnName("ExpiryDate");

                            b1.Property<DateTime>("PurchasedDate")
                                .HasColumnType("datetime2")
                                .HasColumnName("PurchasedDate");

                            b1.HasKey("ProductId");

                            b1.ToTable("Product");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.Navigation("productDescription");
                });
#pragma warning restore 612, 618
        }
    }
}
