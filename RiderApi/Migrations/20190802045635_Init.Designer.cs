﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RiderApi.Models;

namespace RiderApi.Migrations
{
    [DbContext(typeof(RiderContext))]
    [Migration("20190802045635_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RiderApi.Models.Job", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("CompletedAt")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<DateTime>("JobDateTime");

                    b.Property<string>("ReviewComment")
                        .IsRequired();

                    b.Property<int>("ReviewScore");

                    b.Property<int>("RiderId");

                    b.HasKey("Id");

                    b.HasIndex("RiderId");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("RiderApi.Models.Rider", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime>("StartDate");

                    b.HasKey("Id");

                    b.ToTable("Riders");
                });

            modelBuilder.Entity("RiderApi.Models.Job", b =>
                {
                    b.HasOne("RiderApi.Models.Rider", "Rider")
                        .WithMany("Jobs")
                        .HasForeignKey("RiderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}