﻿// <auto-generated />
using System;
using MarketBackend.DataLayer.DatabaseObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MarketBackend.Migrations
{
    [DbContext(typeof(Database))]
    [Migration("20220608163041_CompletedProduct")]
    partial class CompletedProduct
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MarketBackend.DataLayer.DataDTOs.Buyers.Carts.DataCart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.HasKey("Id");

                    b.ToTable("DataCart");
                });

            modelBuilder.Entity("MarketBackend.DataLayer.DataDTOs.Buyers.Carts.DataProductInBag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int?>("DataShoppingBagId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DataShoppingBagId");

                    b.ToTable("DataProductInBag");
                });

            modelBuilder.Entity("MarketBackend.DataLayer.DataDTOs.Buyers.Carts.DataShoppingBag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("DataCartId")
                        .HasColumnType("int");

                    b.Property<int>("StoreId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DataCartId");

                    b.HasIndex("StoreId");

                    b.ToTable("DataShoppingBag");
                });

            modelBuilder.Entity("MarketBackend.DataLayer.DataDTOs.Buyers.DataMember", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CartId")
                        .HasColumnType("int");

                    b.Property<int>("Password")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CartId");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("MarketBackend.DataLayer.DataDTOs.Buyers.DataNotification", b =>
                {
                    b.Property<string>("Notification")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("DataMemberId")
                        .HasColumnType("int");

                    b.HasKey("Notification");

                    b.HasIndex("DataMemberId");

                    b.ToTable("DataNotification");
                });

            modelBuilder.Entity("MarketBackend.DataLayer.DataDTOs.DataProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AmountInInventory")
                        .HasColumnType("int");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DataStoreId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PricePerUnit")
                        .HasColumnType("float");

                    b.Property<double>("Productdicount")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("DataStoreId");

                    b.ToTable("DataProduct");
                });

            modelBuilder.Entity("MarketBackend.DataLayer.DataDTOs.DataProductReview", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("MemberId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("Review")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MemberId");

                    b.HasIndex("ProductId");

                    b.ToTable("DataProductReview");
                });

            modelBuilder.Entity("MarketBackend.DataLayer.DataDTOs.DataPurchaseOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("DataProductId")
                        .HasColumnType("int");

                    b.Property<int>("PurchaseOption")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DataProductId");

                    b.ToTable("DataPurchaseOption");
                });

            modelBuilder.Entity("MarketBackend.DataLayer.DataDTOs.Market.DataPurchase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("DataMember")
                        .HasColumnType("int");

                    b.Property<int>("DataStore")
                        .HasColumnType("int");

                    b.Property<DateTime>("PurchaseDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PurchaseDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PurchasePrice")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("DataMember");

                    b.HasIndex("DataStore");

                    b.ToTable("DataPurchase");
                });

            modelBuilder.Entity("MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DataAppointmentsNode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("DataAppointmentsNodeId")
                        .HasColumnType("int");

                    b.Property<int>("MemberId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DataAppointmentsNodeId");

                    b.ToTable("DataAppointmentsNode");
                });

            modelBuilder.Entity("MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DataManagerPermission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("DataStoreMemberRolesMemberId")
                        .HasColumnType("int");

                    b.Property<int?>("DataStoreMemberRolesStoreId")
                        .HasColumnType("int");

                    b.Property<int>("Permission")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DataStoreMemberRolesMemberId", "DataStoreMemberRolesStoreId");

                    b.ToTable("DataManagerPermission");
                });

            modelBuilder.Entity("MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DataStore", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AppointmentsId")
                        .HasColumnType("int");

                    b.Property<int>("FounderMemberId")
                        .HasColumnType("int");

                    b.Property<bool>("IsOpen")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AppointmentsId");

                    b.ToTable("Stores");
                });

            modelBuilder.Entity("MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DataStoreMemberRoles", b =>
                {
                    b.Property<int>("MemberId")
                        .HasColumnType("int");

                    b.Property<int>("StoreId")
                        .HasColumnType("int");

                    b.Property<int?>("DataStoreId")
                        .HasColumnType("int");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("MemberId", "StoreId");

                    b.HasIndex("DataStoreId");

                    b.ToTable("DataStoreMemberRoles");
                });

            modelBuilder.Entity("MarketBackend.DataLayer.DataDTOs.Buyers.Carts.DataProductInBag", b =>
                {
                    b.HasOne("MarketBackend.DataLayer.DataDTOs.Buyers.Carts.DataShoppingBag", null)
                        .WithMany("ProductsAmounts")
                        .HasForeignKey("DataShoppingBagId");
                });

            modelBuilder.Entity("MarketBackend.DataLayer.DataDTOs.Buyers.Carts.DataShoppingBag", b =>
                {
                    b.HasOne("MarketBackend.DataLayer.DataDTOs.Buyers.Carts.DataCart", null)
                        .WithMany("ShoppingBags")
                        .HasForeignKey("DataCartId");

                    b.HasOne("MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DataStore", "Store")
                        .WithMany()
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Store");
                });

            modelBuilder.Entity("MarketBackend.DataLayer.DataDTOs.Buyers.DataMember", b =>
                {
                    b.HasOne("MarketBackend.DataLayer.DataDTOs.Buyers.Carts.DataCart", "Cart")
                        .WithMany()
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");
                });

            modelBuilder.Entity("MarketBackend.DataLayer.DataDTOs.Buyers.DataNotification", b =>
                {
                    b.HasOne("MarketBackend.DataLayer.DataDTOs.Buyers.DataMember", null)
                        .WithMany("PendingNotifications")
                        .HasForeignKey("DataMemberId");
                });

            modelBuilder.Entity("MarketBackend.DataLayer.DataDTOs.DataProduct", b =>
                {
                    b.HasOne("MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DataStore", null)
                        .WithMany("Products")
                        .HasForeignKey("DataStoreId");
                });

            modelBuilder.Entity("MarketBackend.DataLayer.DataDTOs.DataProductReview", b =>
                {
                    b.HasOne("MarketBackend.DataLayer.DataDTOs.Buyers.DataMember", "Member")
                        .WithMany()
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MarketBackend.DataLayer.DataDTOs.DataProduct", "Product")
                        .WithMany("Reviews")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Member");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("MarketBackend.DataLayer.DataDTOs.DataPurchaseOption", b =>
                {
                    b.HasOne("MarketBackend.DataLayer.DataDTOs.DataProduct", null)
                        .WithMany("PurchaseOptions")
                        .HasForeignKey("DataProductId");
                });

            modelBuilder.Entity("MarketBackend.DataLayer.DataDTOs.Market.DataPurchase", b =>
                {
                    b.HasOne("MarketBackend.DataLayer.DataDTOs.Buyers.DataMember", "Member")
                        .WithMany("PurchaseHistory")
                        .HasForeignKey("DataMember")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DataStore", "Store")
                        .WithMany("PurchaseHistory")
                        .HasForeignKey("DataStore")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Member");

                    b.Navigation("Store");
                });

            modelBuilder.Entity("MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DataAppointmentsNode", b =>
                {
                    b.HasOne("MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DataAppointmentsNode", null)
                        .WithMany("Children")
                        .HasForeignKey("DataAppointmentsNodeId");
                });

            modelBuilder.Entity("MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DataManagerPermission", b =>
                {
                    b.HasOne("MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DataStoreMemberRoles", null)
                        .WithMany("ManagerPermissions")
                        .HasForeignKey("DataStoreMemberRolesMemberId", "DataStoreMemberRolesStoreId");
                });

            modelBuilder.Entity("MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DataStore", b =>
                {
                    b.HasOne("MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DataAppointmentsNode", "Appointments")
                        .WithMany()
                        .HasForeignKey("AppointmentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Appointments");
                });

            modelBuilder.Entity("MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DataStoreMemberRoles", b =>
                {
                    b.HasOne("MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DataStore", null)
                        .WithMany("MembersPermissions")
                        .HasForeignKey("DataStoreId");
                });

            modelBuilder.Entity("MarketBackend.DataLayer.DataDTOs.Buyers.Carts.DataCart", b =>
                {
                    b.Navigation("ShoppingBags");
                });

            modelBuilder.Entity("MarketBackend.DataLayer.DataDTOs.Buyers.Carts.DataShoppingBag", b =>
                {
                    b.Navigation("ProductsAmounts");
                });

            modelBuilder.Entity("MarketBackend.DataLayer.DataDTOs.Buyers.DataMember", b =>
                {
                    b.Navigation("PendingNotifications");

                    b.Navigation("PurchaseHistory");
                });

            modelBuilder.Entity("MarketBackend.DataLayer.DataDTOs.DataProduct", b =>
                {
                    b.Navigation("PurchaseOptions");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DataAppointmentsNode", b =>
                {
                    b.Navigation("Children");
                });

            modelBuilder.Entity("MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DataStore", b =>
                {
                    b.Navigation("MembersPermissions");

                    b.Navigation("Products");

                    b.Navigation("PurchaseHistory");
                });

            modelBuilder.Entity("MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DataStoreMemberRoles", b =>
                {
                    b.Navigation("ManagerPermissions");
                });
#pragma warning restore 612, 618
        }
    }
}
