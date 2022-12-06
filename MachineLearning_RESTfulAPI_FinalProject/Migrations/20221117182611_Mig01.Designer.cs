﻿// <auto-generated />
using MachineLearning_RESTfulAPI_FinalProject.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MachineLearning_RESTfulAPI_FinalProject.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221117182611_Mig01")]
    partial class Mig01
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MachineLearning_RESTfulAPI_FinalProject.Models.Entities.Command", b =>
                {
                    b.Property<string>("Action")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CommandType")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Information")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.HasKey("Action");

                    b.ToTable("Commands");
                });

            modelBuilder.Entity("MachineLearning_RESTfulAPI_FinalProject.Models.Entities.CommandSentence", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CommandAction")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("SentenceSpelling")
                        .IsRequired()
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("CommandAction");

                    b.HasIndex("SentenceSpelling");

                    b.ToTable("CommandSentences");
                });

            modelBuilder.Entity("MachineLearning_RESTfulAPI_FinalProject.Models.Entities.Sentence", b =>
                {
                    b.Property<string>("Spelling")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Meaning")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Spelling");

                    b.ToTable("Sentences");
                });

            modelBuilder.Entity("MachineLearning_RESTfulAPI_FinalProject.Models.Entities.CommandSentence", b =>
                {
                    b.HasOne("MachineLearning_RESTfulAPI_FinalProject.Models.Entities.Command", "Command")
                        .WithMany("CommandSentences")
                        .HasForeignKey("CommandAction")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MachineLearning_RESTfulAPI_FinalProject.Models.Entities.Sentence", "Sentence")
                        .WithMany("CommandSentences")
                        .HasForeignKey("SentenceSpelling")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Command");

                    b.Navigation("Sentence");
                });

            modelBuilder.Entity("MachineLearning_RESTfulAPI_FinalProject.Models.Entities.Command", b =>
                {
                    b.Navigation("CommandSentences");
                });

            modelBuilder.Entity("MachineLearning_RESTfulAPI_FinalProject.Models.Entities.Sentence", b =>
                {
                    b.Navigation("CommandSentences");
                });
#pragma warning restore 612, 618
        }
    }
}
