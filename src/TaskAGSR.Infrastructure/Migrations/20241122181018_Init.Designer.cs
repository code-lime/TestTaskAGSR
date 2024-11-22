﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskAGSR.Infrastructure.DataBase;

#nullable disable

namespace TaskAGSR.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241122181018_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("TaskAGSR.Domain.Entities.Patient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasColumnName("id");

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("active");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("birth_date");

                    b.Property<string>("Family")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("family");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("gender");

                    b.Property<string>("Given")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("given");

                    b.Property<string>("Use")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("use");

                    b.HasKey("Id")
                        .HasName("pk_patients");

                    b.ToTable("patients", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}