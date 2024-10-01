﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using model.db;

#nullable disable

namespace main.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240927025832_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("model.KV", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Bl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long>("Ct")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("Int")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("KeyDesc")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double?>("Real")
                        .HasColumnType("REAL");

                    b.Property<string>("Str")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long>("Ut")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ValueDesc")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("VauleType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Bl");

                    b.HasIndex("Ct");

                    b.HasIndex("Key");

                    b.HasIndex("KeyDesc");

                    b.HasIndex("Ut");

                    b.ToTable("KV");
                });
#pragma warning restore 612, 618
        }
    }
}