﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tech.Challenge4.Data.Contexts;

#nullable disable

namespace Tech.Challenge4.Data.Migrations
{
    [DbContext(typeof(CoworkingContext))]
    [Migration("20240709195419_Tabela_Reserva")]
    partial class Tabela_Reserva
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Tech.Challenge4.Domain.Entities.Coworking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<TimeOnly>("HoraAbertura")
                        .HasColumnType("TIME");

                    b.Property<TimeOnly>("HoraFechamento")
                        .HasColumnType("TIME");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Coworking", (string)null);
                });

            modelBuilder.Entity("Tech.Challenge4.Domain.Entities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customer", (string)null);
                });

            modelBuilder.Entity("Tech.Challenge4.Domain.Entities.Reserva", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Comparecimento")
                        .HasColumnType("BIT");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CustomerID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DataPagamento")
                        .HasColumnType("DATETIME");

                    b.Property<DateOnly>("DataReserva")
                        .HasColumnType("DATE");

                    b.Property<TimeOnly>("HoraFinal")
                        .HasColumnType("TIME");

                    b.Property<TimeOnly>("HoraInicio")
                        .HasColumnType("TIME");

                    b.Property<int>("SalaID")
                        .HasColumnType("int");

                    b.Property<int>("StatusPagamento")
                        .HasColumnType("INT");

                    b.Property<int>("StatusReserva")
                        .HasColumnType("INT");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerID");

                    b.HasIndex("SalaID");

                    b.ToTable("Reserva", (string)null);
                });

            modelBuilder.Entity("Tech.Challenge4.Domain.Entities.Sala", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Capacidade")
                        .HasColumnType("INT");

                    b.Property<int>("CoworkingId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<decimal>("PrecoHora")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CoworkingId");

                    b.ToTable("Sala", (string)null);
                });

            modelBuilder.Entity("Tech.Challenge4.Domain.Entities.Reserva", b =>
                {
                    b.HasOne("Tech.Challenge4.Domain.Entities.Customer", "Customer")
                        .WithMany("Reservas")
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tech.Challenge4.Domain.Entities.Sala", "Sala")
                        .WithMany("Reservas")
                        .HasForeignKey("SalaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Sala");
                });

            modelBuilder.Entity("Tech.Challenge4.Domain.Entities.Sala", b =>
                {
                    b.HasOne("Tech.Challenge4.Domain.Entities.Coworking", "Coworking")
                        .WithMany("Salas")
                        .HasForeignKey("CoworkingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Coworking");
                });

            modelBuilder.Entity("Tech.Challenge4.Domain.Entities.Coworking", b =>
                {
                    b.Navigation("Salas");
                });

            modelBuilder.Entity("Tech.Challenge4.Domain.Entities.Customer", b =>
                {
                    b.Navigation("Reservas");
                });

            modelBuilder.Entity("Tech.Challenge4.Domain.Entities.Sala", b =>
                {
                    b.Navigation("Reservas");
                });
#pragma warning restore 612, 618
        }
    }
}
