﻿// <auto-generated />
using System;
using E_commerce.Logic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace E_commerce.Logic.Migrations
{
    [DbContext(typeof(DBcontext))]
    partial class DBcontextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("E_commerce.Logic.Models.AdminUsers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AdminUsers");
                });

            modelBuilder.Entity("E_commerce.Logic.Models.Basket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("SessionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SessionId");

                    b.ToTable("Basket");
                });

            modelBuilder.Entity("E_commerce.Logic.Models.BasketDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BasketId")
                        .HasColumnType("int");

                    b.Property<int>("ProductsId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BasketId");

                    b.HasIndex("ProductsId");

                    b.ToTable("BasketDetails");
                });

            modelBuilder.Entity("E_commerce.Logic.Models.Categories", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("E_commerce.Logic.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("cvr")
                        .HasColumnType("int");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("E_commerce.Logic.Models.DiscountCodes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BasketDetailsId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("HasBeenUsed")
                        .HasColumnType("bit");

                    b.Property<string>("SalesCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("UsesLeft")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BasketDetailsId");

                    b.ToTable("DiscountCodes");
                });

            modelBuilder.Entity("E_commerce.Logic.Models.Favorites", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ProductIdId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductIdId");

                    b.ToTable("Favorites");
                });

            modelBuilder.Entity("E_commerce.Logic.Models.Images", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProductsId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductsId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("E_commerce.Logic.Models.OrderDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("OrdersId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<double>("price")
                        .HasColumnType("float");

                    b.Property<int>("quantity")
                        .HasColumnType("int");

                    b.Property<double>("total")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("OrdersId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("E_commerce.Logic.Models.Orders", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("SessionId")
                        .HasColumnType("int");

                    b.Property<int?>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SessionId");

                    b.HasIndex("UsersId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("E_commerce.Logic.Models.PriceList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ParentProductIdId")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParentProductIdId");

                    b.ToTable("PriceList");
                });

            modelBuilder.Entity("E_commerce.Logic.Models.ProductVariants", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ParentProductId")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<string>("VariantValue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ParentProductId");

                    b.ToTable("ProductVariants");
                });

            modelBuilder.Entity("E_commerce.Logic.Models.Products", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("ProductCategoriesId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProductCategoriesId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("E_commerce.Logic.Models.Reviews", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ReviewContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserIdId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserIdId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("E_commerce.Logic.Models.Session", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("SessId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("userId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("userId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("E_commerce.Logic.Models.SupportTickets", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("SupportContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserIdId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserIdId");

                    b.ToTable("SuportTickets");
                });

            modelBuilder.Entity("E_commerce.Logic.Models.Tags", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("E_commerce.Logic.Models.UserDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserIdId")
                        .HasColumnType("int");

                    b.Property<int>("ZipCode")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserIdId");

                    b.ToTable("UserDetails");
                });

            modelBuilder.Entity("E_commerce.Logic.Models.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Gender")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("E_commerce.Logic.Models.Basket", b =>
                {
                    b.HasOne("E_commerce.Logic.Models.Session", "Session")
                        .WithMany()
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Session");
                });

            modelBuilder.Entity("E_commerce.Logic.Models.BasketDetails", b =>
                {
                    b.HasOne("E_commerce.Logic.Models.Basket", null)
                        .WithMany("BasketDetails")
                        .HasForeignKey("BasketId");

                    b.HasOne("E_commerce.Logic.Models.Products", "Products")
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Products");
                });

            modelBuilder.Entity("E_commerce.Logic.Models.Company", b =>
                {
                    b.HasOne("E_commerce.Logic.Models.Users", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("E_commerce.Logic.Models.DiscountCodes", b =>
                {
                    b.HasOne("E_commerce.Logic.Models.BasketDetails", "BasketDetails")
                        .WithMany()
                        .HasForeignKey("BasketDetailsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BasketDetails");
                });

            modelBuilder.Entity("E_commerce.Logic.Models.Favorites", b =>
                {
                    b.HasOne("E_commerce.Logic.Models.Products", "ProductId")
                        .WithMany()
                        .HasForeignKey("ProductIdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductId");
                });

            modelBuilder.Entity("E_commerce.Logic.Models.Images", b =>
                {
                    b.HasOne("E_commerce.Logic.Models.Products", null)
                        .WithMany("Images")
                        .HasForeignKey("ProductsId");
                });

            modelBuilder.Entity("E_commerce.Logic.Models.OrderDetails", b =>
                {
                    b.HasOne("E_commerce.Logic.Models.Orders", null)
                        .WithMany("OrderLines")
                        .HasForeignKey("OrdersId");

                    b.HasOne("E_commerce.Logic.Models.Products", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("E_commerce.Logic.Models.Orders", b =>
                {
                    b.HasOne("E_commerce.Logic.Models.Session", "Session")
                        .WithMany()
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("E_commerce.Logic.Models.Users", "Users")
                        .WithMany()
                        .HasForeignKey("UsersId");

                    b.Navigation("Session");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("E_commerce.Logic.Models.PriceList", b =>
                {
                    b.HasOne("E_commerce.Logic.Models.Products", "ParentProductId")
                        .WithMany()
                        .HasForeignKey("ParentProductIdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParentProductId");
                });

            modelBuilder.Entity("E_commerce.Logic.Models.ProductVariants", b =>
                {
                    b.HasOne("E_commerce.Logic.Models.Products", "ParentProduct")
                        .WithMany("ProductVariants")
                        .HasForeignKey("ParentProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParentProduct");
                });

            modelBuilder.Entity("E_commerce.Logic.Models.Products", b =>
                {
                    b.HasOne("E_commerce.Logic.Models.Categories", "ProductCategories")
                        .WithMany()
                        .HasForeignKey("ProductCategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductCategories");
                });

            modelBuilder.Entity("E_commerce.Logic.Models.Reviews", b =>
                {
                    b.HasOne("E_commerce.Logic.Models.Users", "UserId")
                        .WithMany()
                        .HasForeignKey("UserIdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserId");
                });

            modelBuilder.Entity("E_commerce.Logic.Models.Session", b =>
                {
                    b.HasOne("E_commerce.Logic.Models.Users", "user")
                        .WithMany()
                        .HasForeignKey("userId");

                    b.Navigation("user");
                });

            modelBuilder.Entity("E_commerce.Logic.Models.SupportTickets", b =>
                {
                    b.HasOne("E_commerce.Logic.Models.Users", "UserId")
                        .WithMany()
                        .HasForeignKey("UserIdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserId");
                });

            modelBuilder.Entity("E_commerce.Logic.Models.UserDetails", b =>
                {
                    b.HasOne("E_commerce.Logic.Models.Users", "UserId")
                        .WithMany()
                        .HasForeignKey("UserIdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserId");
                });

            modelBuilder.Entity("E_commerce.Logic.Models.Basket", b =>
                {
                    b.Navigation("BasketDetails");
                });

            modelBuilder.Entity("E_commerce.Logic.Models.Orders", b =>
                {
                    b.Navigation("OrderLines");
                });

            modelBuilder.Entity("E_commerce.Logic.Models.Products", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("ProductVariants");
                });
#pragma warning restore 612, 618
        }
    }
}
