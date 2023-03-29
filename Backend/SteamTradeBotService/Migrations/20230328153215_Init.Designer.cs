﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SteamTradeBotService.BusinessLogicLayer.Database;

#nullable disable

namespace SteamTradeBotService.Migrations
{
    [DbContext(typeof(MarketDataContext))]
    [Migration("20230328153215_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SteamTradeBotService.BusinessLogicLayer.Database.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("AvgPrice")
                        .HasColumnType("double precision");

                    b.Property<int>("BuyOrderQuantity")
                        .HasColumnType("integer");

                    b.Property<double>("BuyPrice")
                        .HasColumnType("double precision");

                    b.Property<string>("EngItemName")
                        .HasColumnType("text");

                    b.Property<bool>("IsTherePurchaseOrder")
                        .HasColumnType("boolean");

                    b.Property<int>("ItemPriority")
                        .HasColumnType("integer");

                    b.Property<string>("RusItemName")
                        .HasColumnType("text");

                    b.Property<double>("Sales")
                        .HasColumnType("double precision");

                    b.Property<double>("SellPrice")
                        .HasColumnType("double precision");

                    b.Property<double>("Trend")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("Items");
                });
#pragma warning restore 612, 618
        }
    }
}