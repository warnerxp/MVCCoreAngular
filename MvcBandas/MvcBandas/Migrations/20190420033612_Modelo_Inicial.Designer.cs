﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MvcBandas.Models;

namespace MvcBandas.Migrations
{
    [DbContext(typeof(MvcBandasContext))]
    [Migration("20190420033612_Modelo_Inicial")]
    partial class Modelo_Inicial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MvcBandas.Models.Banda", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("FechaRegistro");

                    b.Property<string>("Genero");

                    b.Property<string>("Nombre");

                    b.Property<int>("NumeroIntegrantes");

                    b.Property<string>("Vocalista");

                    b.Property<int>("anioFormacion");

                    b.HasKey("Id");

                    b.ToTable("Banda");
                });
#pragma warning restore 612, 618
        }
    }
}
