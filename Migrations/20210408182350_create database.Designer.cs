﻿// <auto-generated />
using System;
using Label.API.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Label.API.Migrations
{
    [DbContext(typeof(LabelContext))]
    [Migration("20210408182350_create database")]
    partial class createdatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3");

            modelBuilder.Entity("ArtistSong", b =>
                {
                    b.Property<Guid>("ArtistsArtistId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SongsSongId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ArtistsArtistId", "SongsSongId");

                    b.HasIndex("SongsSongId");

                    b.ToTable("ArtistSong");
                });

            modelBuilder.Entity("Label.API.Models.Album", b =>
                {
                    b.Property<Guid>("AlbumId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AlbumName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ArtistId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ReleaseDate")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AlbumId");

                    b.HasIndex("ArtistId");

                    b.ToTable("Albums");
                });

            modelBuilder.Entity("Label.API.Models.Artist", b =>
                {
                    b.Property<Guid>("ArtistId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ArtistName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Birthdate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HouseNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ArtistId");

                    b.ToTable("Artists");

                    b.HasData(
                        new
                        {
                            ArtistId = new Guid("b955bcaf-80a3-4f1c-8409-28877784a594"),
                            ArtistName = "Mave",
                            Birthdate = "08/08/2001",
                            Country = "Belgium",
                            Email = "maxime6128@gmail.com",
                            FirstName = "Maxime",
                            HouseNumber = "175",
                            LastName = "Vermeeren",
                            PhoneNumber = "+32470053774",
                            PostalCode = "9700",
                            StreetName = "Deinzestraat"
                        });
                });

            modelBuilder.Entity("Label.API.Models.Recordlabel", b =>
                {
                    b.Property<Guid>("RecordLabelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LabelName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RecordLabelId");

                    b.ToTable("Recordlabels");

                    b.HasData(
                        new
                        {
                            RecordLabelId = new Guid("ab95e584-aec7-4bbb-b5d2-f3eae94101fd"),
                            Country = "Belgium",
                            LabelName = "Loud Memory Records"
                        });
                });

            modelBuilder.Entity("Label.API.Models.Song", b =>
                {
                    b.Property<Guid>("SongId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AlbumId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CoverArt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("LabelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Lyrics")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("RecordLabelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ReleaseDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SongName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SongId");

                    b.HasIndex("AlbumId");

                    b.HasIndex("RecordLabelId");

                    b.ToTable("Songs");
                });

            modelBuilder.Entity("Label.API.Models.SongArtist", b =>
                {
                    b.Property<Guid>("SongArtistId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ArtistId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SongId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("SongArtistId");

                    b.HasIndex("ArtistId");

                    b.ToTable("SongArtists");
                });

            modelBuilder.Entity("ArtistSong", b =>
                {
                    b.HasOne("Label.API.Models.Artist", null)
                        .WithMany()
                        .HasForeignKey("ArtistsArtistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Label.API.Models.Song", null)
                        .WithMany()
                        .HasForeignKey("SongsSongId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Label.API.Models.Album", b =>
                {
                    b.HasOne("Label.API.Models.Artist", null)
                        .WithMany("Albums")
                        .HasForeignKey("ArtistId");
                });

            modelBuilder.Entity("Label.API.Models.Song", b =>
                {
                    b.HasOne("Label.API.Models.Album", null)
                        .WithMany("Songs")
                        .HasForeignKey("AlbumId");

                    b.HasOne("Label.API.Models.Recordlabel", "Recordlabel")
                        .WithMany("Songs")
                        .HasForeignKey("RecordLabelId");

                    b.Navigation("Recordlabel");
                });

            modelBuilder.Entity("Label.API.Models.SongArtist", b =>
                {
                    b.HasOne("Label.API.Models.Artist", "Artist")
                        .WithMany("SongArtist")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artist");
                });

            modelBuilder.Entity("Label.API.Models.Album", b =>
                {
                    b.Navigation("Songs");
                });

            modelBuilder.Entity("Label.API.Models.Artist", b =>
                {
                    b.Navigation("Albums");

                    b.Navigation("SongArtist");
                });

            modelBuilder.Entity("Label.API.Models.Recordlabel", b =>
                {
                    b.Navigation("Songs");
                });
#pragma warning restore 612, 618
        }
    }
}
