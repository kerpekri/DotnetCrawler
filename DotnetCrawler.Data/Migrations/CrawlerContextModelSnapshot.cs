﻿// <auto-generated />
using System;
using DotnetCrawler.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DotnetCrawler.Data.Migrations
{
    [DbContext(typeof(CrawlerContext))]
    partial class CrawlerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DotnetCrawler.Data.Models.Clarteys.ClarteysApartment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Area")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("Rooms")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("StartingPrice")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.ToTable("ClarteysApartments");
                });

            modelBuilder.Entity("DotnetCrawler.Data.Models.EIzsoles.EIzsolesThing", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("EIzsolesThings");
                });
#pragma warning restore 612, 618
        }
    }
}
