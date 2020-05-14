﻿// <auto-generated />
using System;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data.Migrations
{
    [DbContext(typeof(MeuDbContext))]
    partial class MeuDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Business.Models.Cidade", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("EstadoId");

                    b.HasKey("Id");

                    b.HasIndex("EstadoId");

                    b.ToTable("Cidades");
                });

            modelBuilder.Entity("Business.Models.Departamento", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("varchar(15)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Departamento");
                });

            modelBuilder.Entity("Business.Models.EmprDept", b =>
                {
                    b.Property<Guid>("EmpresaId");

                    b.Property<Guid>("DepartamentoId");

                    b.Property<Guid>("Id");

                    b.HasKey("EmpresaId", "DepartamentoId");

                    b.HasIndex("DepartamentoId");

                    b.ToTable("EmprDept");
                });

            modelBuilder.Entity("Business.Models.Empresa", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CidadeId");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("varchar(15)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("CidadeId");

                    b.ToTable("Empresa");
                });

            modelBuilder.Entity("Business.Models.Estado", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("PaisId");

                    b.Property<string>("UF")
                        .IsRequired()
                        .HasColumnType("varchar(2)");

                    b.HasKey("Id");

                    b.HasIndex("PaisId");

                    b.ToTable("Estados");
                });

            modelBuilder.Entity("Business.Models.Pais", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Paises");
                });

            modelBuilder.Entity("Business.Models.Cidade", b =>
                {
                    b.HasOne("Business.Models.Estado", "Estado")
                        .WithMany("Cidades")
                        .HasForeignKey("EstadoId");
                });

            modelBuilder.Entity("Business.Models.EmprDept", b =>
                {
                    b.HasOne("Business.Models.Departamento", "Departamento")
                        .WithMany("EmpresasDepartamento")
                        .HasForeignKey("DepartamentoId");

                    b.HasOne("Business.Models.Empresa", "Empresa")
                        .WithMany("EmpresaDepartamentos")
                        .HasForeignKey("EmpresaId");
                });

            modelBuilder.Entity("Business.Models.Empresa", b =>
                {
                    b.HasOne("Business.Models.Cidade", "Cidade")
                        .WithMany("Empresas")
                        .HasForeignKey("CidadeId");
                });

            modelBuilder.Entity("Business.Models.Estado", b =>
                {
                    b.HasOne("Business.Models.Pais", "Pais")
                        .WithMany("Estados")
                        .HasForeignKey("PaisId");
                });
#pragma warning restore 612, 618
        }
    }
}
