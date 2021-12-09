﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using testapp.Models;

namespace testapp.Migrations
{
    [DbContext(typeof(TestContext))]
    [Migration("20211207155917_img student")]
    partial class imgstudent
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.12");

            modelBuilder.Entity("testapp.Models.GroupModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("GroupName")
                        .HasColumnType("TEXT");

                    b.Property<int>("GroupYear")
                        .HasColumnType("INTEGER");

                    b.Property<string>("President")
                        .HasColumnType("TEXT");

                    b.Property<int>("Year")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("testapp.Models.ProfessorsModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Professors");
                });

            modelBuilder.Entity("testapp.Models.ProgramsModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("GroupId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("GroupId")
                        .IsUnique();

                    b.ToTable("Programs");
                });

            modelBuilder.Entity("testapp.Models.StudentModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("GroupForeignKey")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ImgFile")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("GroupForeignKey");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("testapp.Models.ProgramsModel", b =>
                {
                    b.HasOne("testapp.Models.GroupModel", "Group")
                        .WithOne("Program")
                        .HasForeignKey("testapp.Models.ProgramsModel", "GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");
                });

            modelBuilder.Entity("testapp.Models.StudentModel", b =>
                {
                    b.HasOne("testapp.Models.GroupModel", "Group")
                        .WithMany("Students")
                        .HasForeignKey("GroupForeignKey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");
                });

            modelBuilder.Entity("testapp.Models.GroupModel", b =>
                {
                    b.Navigation("Program");

                    b.Navigation("Students");
                });
#pragma warning restore 612, 618
        }
    }
}
