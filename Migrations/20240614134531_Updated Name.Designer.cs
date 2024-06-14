﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WordNET_Server_2._0.DBRelations;

#nullable disable

namespace WordNET_Server_2._0.Migrations
{
    [DbContext(typeof(DBContext))]
    [Migration("20240614134531_Updated Name")]
    partial class UpdatedName
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WordNET_Server_2._0.Models.AssociatedWord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WordId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WordId");

                    b.ToTable("AssociatedWord");
                });

            modelBuilder.Entity("WordNET_Server_2._0.Models.AssociatedWordQuestionee", b =>
                {
                    b.Property<int>("AssociatedWordId")
                        .HasColumnType("int");

                    b.Property<int>("QuestioneeId")
                        .HasColumnType("int");

                    b.HasKey("AssociatedWordId", "QuestioneeId");

                    b.HasIndex("QuestioneeId");

                    b.ToTable("AssociatedWordQuestionees");
                });

            modelBuilder.Entity("WordNET_Server_2._0.Models.Questionee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<bool>("IsMan")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Questionee");
                });

            modelBuilder.Entity("WordNET_Server_2._0.Models.Word", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Word");
                });

            modelBuilder.Entity("WordNET_Server_2._0.Models.AssociatedWord", b =>
                {
                    b.HasOne("WordNET_Server_2._0.Models.Word", "Word")
                        .WithMany("AssociatedWords")
                        .HasForeignKey("WordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Word");
                });

            modelBuilder.Entity("WordNET_Server_2._0.Models.AssociatedWordQuestionee", b =>
                {
                    b.HasOne("WordNET_Server_2._0.Models.AssociatedWord", "AssociatedWord")
                        .WithMany("AssociatedWordQuestionees")
                        .HasForeignKey("AssociatedWordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WordNET_Server_2._0.Models.Questionee", "Questionee")
                        .WithMany("AssociatedWordQuestionees")
                        .HasForeignKey("QuestioneeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AssociatedWord");

                    b.Navigation("Questionee");
                });

            modelBuilder.Entity("WordNET_Server_2._0.Models.AssociatedWord", b =>
                {
                    b.Navigation("AssociatedWordQuestionees");
                });

            modelBuilder.Entity("WordNET_Server_2._0.Models.Questionee", b =>
                {
                    b.Navigation("AssociatedWordQuestionees");
                });

            modelBuilder.Entity("WordNET_Server_2._0.Models.Word", b =>
                {
                    b.Navigation("AssociatedWords");
                });
#pragma warning restore 612, 618
        }
    }
}
