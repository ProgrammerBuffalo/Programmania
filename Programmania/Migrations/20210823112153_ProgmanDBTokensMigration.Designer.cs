﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Programmania.DAL;

namespace Programmania.Migrations
{
    [DbContext(typeof(ProgrammaniaDBContext))]
    [Migration("20210823112153_ProgmanDBTokensMigration")]
    partial class ProgmanDBTokensMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AchievementUser", b =>
                {
                    b.Property<int>("AchievementsId")
                        .HasColumnType("int");

                    b.Property<int>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("AchievementsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("AchievementUser");
                });

            modelBuilder.Entity("LessonUser", b =>
                {
                    b.Property<int>("LessonsId")
                        .HasColumnType("int");

                    b.Property<int>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("LessonsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("LessonUser");
                });

            modelBuilder.Entity("Programmania.Models.Achievement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Desc")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Description");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Name");

                    b.Property<int>("Points")
                        .HasColumnType("int")
                        .HasColumnName("Points");

                    b.Property<Guid>("StreamId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("stream_id");

                    b.HasKey("Id");

                    b.ToTable("Achievements");
                });

            modelBuilder.Entity("Programmania.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Name");

                    b.Property<int>("Points")
                        .HasColumnType("int")
                        .HasColumnName("Points");

                    b.Property<Guid>("StreamId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("stream_id");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("Programmania.Models.Discipline", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Name");

                    b.Property<int>("Points")
                        .HasColumnType("int")
                        .HasColumnName("Points");

                    b.Property<Guid>("StreamId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("stream_id");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Disciplines");
                });

            modelBuilder.Entity("Programmania.Models.Document", b =>
                {
                    b.Property<string>("Extension")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Size")
                        .HasColumnType("bigint");

                    b.Property<Guid>("StreamId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("stream_id");

                    b.ToView("DocumentsView");
                });

            modelBuilder.Entity("Programmania.Models.Lesson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DisciplineId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Name");

                    b.Property<int>("Order")
                        .HasColumnType("int")
                        .HasColumnName("Order");

                    b.Property<Guid>("StreamId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("stream_id");

                    b.HasKey("Id");

                    b.HasIndex("DisciplineId");

                    b.ToTable("Lessons");
                });

            modelBuilder.Entity("Programmania.Models.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Expires")
                        .HasColumnType("datetime2");

                    b.Property<string>("IpCreator")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IpRevoker")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LocationCreator")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LocationRevorker")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Revoked")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Tokens");
                });

            modelBuilder.Entity("Programmania.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Exp")
                        .HasColumnType("int")
                        .HasColumnName("Experience");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("Login");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("Nickname");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("Password");

                    b.Property<Guid>("StreamId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("stream_id");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AchievementUser", b =>
                {
                    b.HasOne("Programmania.Models.Achievement", null)
                        .WithMany()
                        .HasForeignKey("AchievementsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Programmania.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LessonUser", b =>
                {
                    b.HasOne("Programmania.Models.Lesson", null)
                        .WithMany()
                        .HasForeignKey("LessonsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Programmania.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Programmania.Models.Discipline", b =>
                {
                    b.HasOne("Programmania.Models.Course", "Course")
                        .WithMany("Disciplines")
                        .HasForeignKey("CourseId");

                    b.Navigation("Course");
                });

            modelBuilder.Entity("Programmania.Models.Lesson", b =>
                {
                    b.HasOne("Programmania.Models.Discipline", "Discipline")
                        .WithMany("Lessons")
                        .HasForeignKey("DisciplineId");

                    b.Navigation("Discipline");
                });

            modelBuilder.Entity("Programmania.Models.RefreshToken", b =>
                {
                    b.HasOne("Programmania.Models.User", "User")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Programmania.Models.Course", b =>
                {
                    b.Navigation("Disciplines");
                });

            modelBuilder.Entity("Programmania.Models.Discipline", b =>
                {
                    b.Navigation("Lessons");
                });

            modelBuilder.Entity("Programmania.Models.User", b =>
                {
                    b.Navigation("RefreshTokens");
                });
#pragma warning restore 612, 618
        }
    }
}