using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using Tunify_Platform.Models;
using Tunify_Platform.Models.DTOs;
using Tunify_Platform.Repositories.Interfaces;
using Tunify_Platform.Repositories.Services;

namespace Tunify_Platform.Data
{
    public class TunifyDbContext : IdentityDbContext<ApplicationUser>
    {
        public TunifyDbContext(DbContextOptions<TunifyDbContext> options) : base(options)
        {
        }
        public DbSet<Albums> albums { get; set; }
        public DbSet<Artists> artists { get; set; }
        public DbSet<Playlists> playlists { get; set; }
        public DbSet<PlaylistSongs> playlistSongs { get; set; }
        public DbSet<Songs> songs { get; set; }
        public DbSet<Subscriptions> subscriptions { get; set; }
        public DbSet<Users> users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Albums>()
                .HasOne(a => a.Artists)
                .WithMany(b => b.Albums)
                .HasForeignKey(a => a.ArtistsId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Playlists>()
                .HasOne(a => a.Users)
                .WithMany(b => b.Playlists)
                .HasForeignKey(a => a.UsersId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PlaylistSongs>()
                .HasOne(a => a.Songs)
                .WithMany(b => b.PlaylistSongs)
                .HasForeignKey(a => a.SongsId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PlaylistSongs>()
                .HasOne(a => a.Playlists)
                .WithMany(b => b.PlaylistSongs)
                .HasForeignKey(a => a.PlaylistsId)
               .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Songs>()
                .HasOne(a => a.Artists)
                .WithMany(b => b.Songs)
                .HasForeignKey(a => a.ArtistsId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Songs>()
              .HasOne(a => a.Albums)
              .WithMany(b => b.Songs)
              .HasForeignKey(a => a.AlbumsId)
             .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Users>()
                .HasOne(a => a.Subscriptions)
                .WithMany(b => b.Users)
                .HasForeignKey(a => a.SubscriptionsId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Albums>().HasData(
                  new Albums { AlbumsId = 1, AlbumName = "First Album", ArtistsId = 1, ReleaseDate = "2000" }
            );
            modelBuilder.Entity<Artists>().HasData(
                new Artists { ArtistsId = 1, ArtistsName = "First Artist", ArtistsBio = "no bio" },
                new Artists { ArtistsId = 2, ArtistsName = "Second Artist", ArtistsBio = "no bio" }
            );
            modelBuilder.Entity<Songs>().HasData(
                new Songs { SongsId = 1, AlbumsId = 1, ArtistsId = 1, Duration = 30, Title = "First Song", Genre = 1 }
            );
            modelBuilder.Entity<Users>().HasData(
                new Users { UsersId = 1, UserName = "Raghad", Email = "raghad@gmail.com", JoinDate = "2024", SubscriptionsId = 1 }
            );
            modelBuilder.Entity<Playlists>().HasData(
                new Playlists { PlaylistsId = 1, CreatedDate = "2024", PlaylistsName = "First Playlist", UsersId = 1 }
            );
            modelBuilder.Entity<Subscriptions>().HasData(
                new Subscriptions { SubscriptionsId = 1, SubscriptionsPrice = 1, SubscriptionsType = "Basic" }
            );


            ////////
            // Define role IDs
            var adminRoleId = "admin-role-id";
            var userRoleId = "user-role-id";

            // Define user IDs
            var adminUserId = "admin-user-id";

            // Seed Roles
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = adminRoleId, Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = userRoleId, Name = "User", NormalizedName = "USER" }
            );

            // Seed Admin User
            var hasher = new PasswordHasher<ApplicationUser>();

            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = adminUserId,
                    UserName = "admin@tunify.com",
                    NormalizedUserName = "ADMIN@TUNIFY.COM",
                    Email = "admin@tunify.com",
                    NormalizedEmail = "ADMIN@TUNIFY.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Admin@123"),
                    SecurityStamp = string.Empty,
                    FirstName = "Admin",
                    LastName = "User"
                }
            );

            // Assign Admin User to Admin Role
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { RoleId = adminRoleId, UserId = adminUserId }
            );





        }

    }
}


