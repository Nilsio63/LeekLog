﻿// <auto-generated />
using System;
using LeekLog.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LeekLog.Data.Migrations
{
    [DbContext(typeof(LeekLogDbContext))]
    partial class LeekLogDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("LeekLog.Abstractions.Entites.ExerciseEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Exercises", (string)null);
                });

            modelBuilder.Entity("LeekLog.Abstractions.Entites.GymSessionEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("GymSessions", (string)null);
                });

            modelBuilder.Entity("LeekLog.Abstractions.Entites.SessionExerciseEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ExerciseId")
                        .HasColumnType("char(36)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<Guid>("SessionId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseId");

                    b.HasIndex("SessionId");

                    b.ToTable("SessionExercises", (string)null);
                });

            modelBuilder.Entity("LeekLog.Abstractions.Entites.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<byte[]>("Password")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("varbinary(32)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("LeekLog.Abstractions.Entites.WorkoutSetEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("NumberOfRepetitions")
                        .HasColumnType("int");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<Guid>("SessionExerciseId")
                        .HasColumnType("char(36)");

                    b.Property<double>("UsedWeight")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("SessionExerciseId");

                    b.ToTable("WorkoutSetEntity", (string)null);
                });

            modelBuilder.Entity("LeekLog.Abstractions.Entites.GymSessionEntity", b =>
                {
                    b.HasOne("LeekLog.Abstractions.Entites.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LeekLog.Abstractions.Entites.SessionExerciseEntity", b =>
                {
                    b.HasOne("LeekLog.Abstractions.Entites.ExerciseEntity", null)
                        .WithMany()
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LeekLog.Abstractions.Entites.GymSessionEntity", null)
                        .WithMany("Exercises")
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LeekLog.Abstractions.Entites.WorkoutSetEntity", b =>
                {
                    b.HasOne("LeekLog.Abstractions.Entites.SessionExerciseEntity", null)
                        .WithMany("Sets")
                        .HasForeignKey("SessionExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LeekLog.Abstractions.Entites.GymSessionEntity", b =>
                {
                    b.Navigation("Exercises");
                });

            modelBuilder.Entity("LeekLog.Abstractions.Entites.SessionExerciseEntity", b =>
                {
                    b.Navigation("Sets");
                });
#pragma warning restore 612, 618
        }
    }
}
