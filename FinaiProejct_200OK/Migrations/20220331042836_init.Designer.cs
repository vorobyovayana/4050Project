﻿// <auto-generated />
using System;
using FinaiProejct_200OK.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FinaiProejct_200OK.Migrations
{
    [DbContext(typeof(MovieContext))]
    [Migration("20220331042836_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.23")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FinaiProejct_200OK.Entities.Detail", b =>
                {
                    b.Property<int>("MovieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("MovieId");

                    b.ToTable("Detail");
                });

            modelBuilder.Entity("FinaiProejct_200OK.Entities.Director", b =>
                {
                    b.Property<int>("DirectorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("DirectorId");

                    b.ToTable("Director");
                });

            modelBuilder.Entity("FinaiProejct_200OK.Entities.Favorite", b =>
                {
                    b.Property<int>("MemberId")
                        .HasColumnType("int");

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("MemberId", "MovieId");

                    b.HasIndex("UserId");

                    b.ToTable("Favorite");
                });

            modelBuilder.Entity("FinaiProejct_200OK.Entities.Genre", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("MovieId")
                        .HasColumnType("int");

                    b.HasKey("GenreId");

                    b.HasIndex("MovieId");

                    b.ToTable("Genre");
                });

            modelBuilder.Entity("FinaiProejct_200OK.Entities.Movie", b =>
                {
                    b.Property<int>("MovieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("MovieDetailMovieId")
                        .HasColumnType("int");

                    b.Property<int?>("MovieDirectorDirectorId")
                        .HasColumnType("int");

                    b.Property<string>("MovieTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.HasKey("MovieId");

                    b.HasIndex("MovieDetailMovieId");

                    b.HasIndex("MovieDirectorDirectorId");

                    b.ToTable("Movie");
                });

            modelBuilder.Entity("FinaiProejct_200OK.Entities.Review", b =>
                {
                    b.Property<int>("ReviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<string>("ReviewDesc")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ReviewId");

                    b.HasIndex("MovieId");

                    b.ToTable("Review");
                });

            modelBuilder.Entity("FinaiProejct_200OK.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("FinaiProejct_200OK.Entities.Favorite", b =>
                {
                    b.HasOne("FinaiProejct_200OK.Entities.User", null)
                        .WithMany("Favorites")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("FinaiProejct_200OK.Entities.Genre", b =>
                {
                    b.HasOne("FinaiProejct_200OK.Entities.Movie", null)
                        .WithMany("Genres")
                        .HasForeignKey("MovieId");
                });

            modelBuilder.Entity("FinaiProejct_200OK.Entities.Movie", b =>
                {
                    b.HasOne("FinaiProejct_200OK.Entities.Detail", "MovieDetail")
                        .WithMany()
                        .HasForeignKey("MovieDetailMovieId");

                    b.HasOne("FinaiProejct_200OK.Entities.Director", "MovieDirector")
                        .WithMany()
                        .HasForeignKey("MovieDirectorDirectorId");
                });

            modelBuilder.Entity("FinaiProejct_200OK.Entities.Review", b =>
                {
                    b.HasOne("FinaiProejct_200OK.Entities.Movie", null)
                        .WithMany("Reviews")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
