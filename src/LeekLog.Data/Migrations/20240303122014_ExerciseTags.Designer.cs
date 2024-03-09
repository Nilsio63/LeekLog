﻿// <auto-generated />
using System;
using LeekLog.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LeekLog.Data.Migrations
{
    [DbContext(typeof(LeekLogDbContext))]
    [Migration("20240303122014_ExerciseTags")]
    partial class ExerciseTags
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("Exercises", (string)null);
                });

            modelBuilder.Entity("LeekLog.Abstractions.Entites.ExerciseTagEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ExerciseId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("TagId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("TagId");

                    b.HasIndex("ExerciseId", "TagId")
                        .IsUnique();

                    b.ToTable("ExerciseTags", (string)null);
                });

            modelBuilder.Entity("LeekLog.Abstractions.Entites.FeedbackEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<double?>("BodyWeight")
                        .HasColumnType("double");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("UserId", "Date")
                        .IsUnique();

                    b.ToTable("Feedbacks", (string)null);
                });

            modelBuilder.Entity("LeekLog.Abstractions.Entites.GymSessionEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<double?>("AverageHeartRate")
                        .HasColumnType("double");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<TimeSpan?>("Duration")
                        .HasColumnType("time(6)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("UserId", "Date")
                        .IsUnique();

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

                    b.HasIndex("SessionId", "Order")
                        .IsUnique();

                    b.ToTable("SessionExercises", (string)null);
                });

            modelBuilder.Entity("LeekLog.Abstractions.Entites.TagEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("Tags", (string)null);
                });

            modelBuilder.Entity("LeekLog.Abstractions.Entites.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("tinyint(1)");

                    b.Property<byte[]>("Password")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("varbinary(32)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("LeekLog.Abstractions.Entites.WorkoutSetEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("AssistedRepetitions")
                        .HasColumnType("int");

                    b.Property<int>("CleanRepetitions")
                        .HasColumnType("int");

                    b.Property<int>("FailedRepetitions")
                        .HasColumnType("int");

                    b.Property<int>("NegativeRepetitions")
                        .HasColumnType("int");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<int>("PartialRepetitions")
                        .HasColumnType("int");

                    b.Property<Guid>("SessionExerciseId")
                        .HasColumnType("char(36)");

                    b.Property<int>("UncleanRepetitions")
                        .HasColumnType("int");

                    b.Property<double>("UsedWeight")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("SessionExerciseId");

                    b.HasIndex("SessionExerciseId", "Order")
                        .IsUnique();

                    b.ToTable("WorkoutSetEntity", (string)null);
                });

            modelBuilder.Entity("LeekLog.Abstractions.Entites.ExerciseTagEntity", b =>
                {
                    b.HasOne("LeekLog.Abstractions.Entites.ExerciseEntity", "Exercise")
                        .WithMany("ExerciseTags")
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LeekLog.Abstractions.Entites.TagEntity", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exercise");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("LeekLog.Abstractions.Entites.FeedbackEntity", b =>
                {
                    b.HasOne("LeekLog.Abstractions.Entites.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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
                    b.HasOne("LeekLog.Abstractions.Entites.ExerciseEntity", "Exercise")
                        .WithMany()
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LeekLog.Abstractions.Entites.GymSessionEntity", "Session")
                        .WithMany("Exercises")
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exercise");

                    b.Navigation("Session");
                });

            modelBuilder.Entity("LeekLog.Abstractions.Entites.WorkoutSetEntity", b =>
                {
                    b.HasOne("LeekLog.Abstractions.Entites.SessionExerciseEntity", null)
                        .WithMany("Sets")
                        .HasForeignKey("SessionExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LeekLog.Abstractions.Entites.ExerciseEntity", b =>
                {
                    b.Navigation("ExerciseTags");
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