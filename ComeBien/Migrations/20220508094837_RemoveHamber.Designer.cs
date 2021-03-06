// <auto-generated />
using System;
using ComeBien.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ComeBien.Migrations
{
    [DbContext(typeof(ComeBienContext))]
    [Migration("20220508094837_RemoveHamber")]
    partial class RemoveHamber
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("ComeBien.Models.Database.Administrator", b =>
                {
                    b.Property<string>("UserName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserName");

                    b.ToTable("Administrators");

                    b.HasData(
                        new
                        {
                            UserName = "admin",
                            Password = "admin1234*"
                        });
                });

            modelBuilder.Entity("ComeBien.Models.Database.Ingredients", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("EnName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("EsName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("FrName")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.ToTable("Ingredients");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            EnName = "Cheese",
                            EsName = "Queso",
                            FrName = "Fromage",
                            Price = 1m
                        },
                        new
                        {
                            Id = 2,
                            EnName = "Jam",
                            EsName = "Jamón",
                            FrName = "Jambon",
                            Price = 1m
                        },
                        new
                        {
                            Id = 3,
                            EnName = "Bacon",
                            EsName = "Bacon",
                            FrName = "Bacon",
                            Price = 1m
                        },
                        new
                        {
                            Id = 4,
                            EnName = "Chorizo",
                            EsName = "Chorizo",
                            FrName = "Saucisse",
                            Price = 1m
                        },
                        new
                        {
                            Id = 5,
                            EnName = "Pineapple",
                            EsName = "Piña",
                            FrName = "Ananas",
                            Price = 1m
                        },
                        new
                        {
                            Id = 6,
                            EnName = "Onion",
                            EsName = "Cebolla",
                            FrName = "Oignon",
                            Price = 1m
                        });
                });

            modelBuilder.Entity("ComeBien.Models.Database.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ComeBien.Models.Database.OrderProductIngredients", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("IngredientId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderId", "ProductId", "IngredientId");

                    b.ToTable("OrderProductIngredients");
                });

            modelBuilder.Entity("ComeBien.Models.Database.OrderProducts", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("OrderProducts");
                });

            modelBuilder.Entity("ComeBien.Models.Database.Products", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("EnDescription")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("EnName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("EsDescription")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("EsName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("FrDescription")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("FrName")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            EnDescription = "Build your own pizza",
                            EnName = "Pizza",
                            EsDescription = "Construye tu propia pizza",
                            EsName = "Pizza",
                            FrDescription = "Construisez votre propre pizza",
                            FrName = "Pizza",
                            Price = 6m
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
