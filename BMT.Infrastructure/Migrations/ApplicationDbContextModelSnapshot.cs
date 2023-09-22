﻿// <auto-generated />
using System;
using BMT.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BMT.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BMT.Domain.Locations.Store", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<Guid>("ManagerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("stores", (string)null);
                });

            modelBuilder.Entity("BMT.Domain.Orders.Item", b =>
                {
                    b.Property<Guid>("ItemID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<Guid?>("StoreId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("StoreId1")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ItemID");

                    b.HasIndex("ProductId");

                    b.HasIndex("StoreId");

                    b.HasIndex("StoreId1");

                    b.ToTable("items", (string)null);
                });

            modelBuilder.Entity("BMT.Domain.Orders.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<DateTime>("DateOrderPlaced")
                        .HasColumnType("datetime2")
                        .HasColumnName("date_order_placed");

                    b.Property<Guid>("ManagerId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("manager_id");

                    b.Property<string>("OrderStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("order_status");

                    b.Property<string>("OrderType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("order_type");

                    b.Property<Guid?>("StoreId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("StoreOrWarehouseId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("store_or_warehouse_id");

                    b.Property<int>("TotalCost")
                        .HasColumnType("int")
                        .HasColumnName("total_cost");

                    b.HasKey("Id");

                    b.HasIndex("StoreId");

                    b.ToTable("orders", (string)null);
                });

            modelBuilder.Entity("BMT.Domain.Products.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("InStock")
                        .HasColumnType("bit");

                    b.Property<float>("Rating")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("products", (string)null);
                });

            modelBuilder.Entity("BMT.Domain.Shopping_Cart.ShoppingCart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<Guid>("ManagerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("OrderType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ShoppingCartMaxValue")
                        .HasColumnType("int");

                    b.Property<Guid>("StoreOrWarehouseId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("shoppingcarts", (string)null);
                });

            modelBuilder.Entity("BMT.Domain.Users.Manager", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("email");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<int>("ManagerScope")
                        .HasColumnType("int")
                        .HasColumnName("manager_scope");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("password");

                    b.Property<Guid>("StoreOrWarehouseId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("store_or_warehouse_id");

                    b.HasKey("Id");

                    b.ToTable("managers", (string)null);
                });

            modelBuilder.Entity("BMT.Domain.Locations.Store", b =>
                {
                    b.OwnsOne("BMT.Domain.Locations.Store.Address#BMT.Domain.Shared.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("StoreId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("State")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("StoreId");

                            b1.ToTable("stores", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("StoreId");
                        });

                    b.Navigation("Address")
                        .IsRequired();
                });

            modelBuilder.Entity("BMT.Domain.Orders.Item", b =>
                {
                    b.HasOne("BMT.Domain.Products.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BMT.Domain.Locations.Store", null)
                        .WithMany("InventoryOnHand")
                        .HasForeignKey("StoreId");

                    b.HasOne("BMT.Domain.Locations.Store", null)
                        .WithMany("InventoryOnOrder")
                        .HasForeignKey("StoreId1");

                    b.OwnsOne("BMT.Domain.Orders.Item.Price#BMT.Domain.Shared.Money", "Price", b1 =>
                        {
                            b1.Property<Guid>("ItemID")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Value")
                                .HasColumnType("int");

                            b1.HasKey("ItemID");

                            b1.ToTable("items", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("ItemID");
                        });

                    b.Navigation("Price")
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("BMT.Domain.Orders.Order", b =>
                {
                    b.HasOne("BMT.Domain.Locations.Store", null)
                        .WithMany("StoreOrders")
                        .HasForeignKey("StoreId");
                });

            modelBuilder.Entity("BMT.Domain.Products.Product", b =>
                {
                    b.OwnsOne("BMT.Domain.Products.Product.Name#BMT.Domain.Shared.Name", "Name", b1 =>
                        {
                            b1.Property<Guid>("ProductId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("ThisName")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ProductId");

                            b1.ToTable("products", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.OwnsOne("BMT.Domain.Products.Product.UnitPrice#BMT.Domain.Shared.Money", "UnitPrice", b1 =>
                        {
                            b1.Property<Guid>("ProductId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Value")
                                .HasColumnType("int");

                            b1.HasKey("ProductId");

                            b1.ToTable("products", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.Navigation("Name")
                        .IsRequired();

                    b.Navigation("UnitPrice")
                        .IsRequired();
                });

            modelBuilder.Entity("BMT.Domain.Shopping_Cart.ShoppingCart", b =>
                {
                    b.OwnsOne("BMT.Domain.Shopping_Cart.ShoppingCart.ShoppingCartTotal#BMT.Domain.Shared.Money", "ShoppingCartTotal", b1 =>
                        {
                            b1.Property<Guid>("ShoppingCartId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Value")
                                .HasColumnType("int");

                            b1.HasKey("ShoppingCartId");

                            b1.ToTable("shoppingcarts", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("ShoppingCartId");
                        });

                    b.OwnsOne("BMT.Domain.Shopping_Cart.ShoppingCart.TheShoppingCart#System.Collections.Generic.List<BMT.Domain.Orders.Item>", "TheShoppingCart", b1 =>
                        {
                            b1.Property<Guid>("ShoppingCartId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Capacity")
                                .HasColumnType("int");

                            b1.HasKey("ShoppingCartId");

                            b1.ToTable("shoppingcarts", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("ShoppingCartId");
                        });

                    b.Navigation("ShoppingCartTotal");

                    b.Navigation("TheShoppingCart");
                });

            modelBuilder.Entity("BMT.Domain.Locations.Store", b =>
                {
                    b.Navigation("InventoryOnHand");

                    b.Navigation("InventoryOnOrder");

                    b.Navigation("StoreOrders");
                });
#pragma warning restore 612, 618
        }
    }
}
