﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tunify_Platform.Data;

#nullable disable

namespace Tunify_Platform.Migrations
{
    [DbContext(typeof(TunifyDbContext))]
    [Migration("20240805161912_CreateMusicTables")]
    partial class CreateMusicTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Tunify_Platform.Models.Albums", b =>
                {
                    b.Property<int>("AlbumsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AlbumsId"));

                    b.Property<string>("AlbumName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ArtistsId")
                        .HasColumnType("int");

                    b.Property<string>("ReleaseDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AlbumsId");

                    b.ToTable("albums");
                });

            modelBuilder.Entity("Tunify_Platform.Models.Artists", b =>
                {
                    b.Property<int>("ArtistsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ArtistsId"));

                    b.Property<string>("ArtistsBio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ArtistsName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ArtistsId");

                    b.ToTable("artists");
                });

            modelBuilder.Entity("Tunify_Platform.Models.PlaylistSongs", b =>
                {
                    b.Property<int>("PlaylistSongsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PlaylistSongsId"));

                    b.Property<int>("PlaylistsId")
                        .HasColumnType("int");

                    b.Property<int>("SongsId")
                        .HasColumnType("int");

                    b.HasKey("PlaylistSongsId");

                    b.HasIndex("PlaylistsId");

                    b.HasIndex("SongsId");

                    b.ToTable("playlistSongs");
                });

            modelBuilder.Entity("Tunify_Platform.Models.Playlists", b =>
                {
                    b.Property<int>("PlaylistsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PlaylistsId"));

                    b.Property<string>("CreatedDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlaylistsName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("PlaylistsId");

                    b.ToTable("playlists");
                });

            modelBuilder.Entity("Tunify_Platform.Models.Songs", b =>
                {
                    b.Property<int>("SongsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SongsId"));

                    b.Property<int>("AlbumsId")
                        .HasColumnType("int");

                    b.Property<int>("ArtistsId")
                        .HasColumnType("int");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<int>("Genre")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SongsId");

                    b.ToTable("songs");
                });

            modelBuilder.Entity("Tunify_Platform.Models.Subscriptions", b =>
                {
                    b.Property<string>("SubscriptionsId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("SubscriptionsPrice")
                        .HasColumnType("int");

                    b.Property<string>("SubscriptionsType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SubscriptionsId");

                    b.ToTable("subscriptions");
                });

            modelBuilder.Entity("Tunify_Platform.Models.Users", b =>
                {
                    b.Property<int>("UsersId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UsersId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JoinDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SubscriptionId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UsersId");

                    b.ToTable("users");
                });

            modelBuilder.Entity("Tunify_Platform.Models.PlaylistSongs", b =>
                {
                    b.HasOne("Tunify_Platform.Models.Playlists", null)
                        .WithMany("playlists")
                        .HasForeignKey("PlaylistsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tunify_Platform.Models.Songs", null)
                        .WithMany("songs")
                        .HasForeignKey("SongsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tunify_Platform.Models.Playlists", b =>
                {
                    b.Navigation("playlists");
                });

            modelBuilder.Entity("Tunify_Platform.Models.Songs", b =>
                {
                    b.Navigation("songs");
                });
#pragma warning restore 612, 618
        }
    }
}
