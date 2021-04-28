using System;
using System.Threading;
using System.Threading.Tasks;
using Label.API.Config;
using Label.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

// dotnet ef
// dotnet ef migrations add "naammigratie



namespace Label.API.DataContext
{
    public interface ILabelContext
    {
        DbSet<Album> Albums { get; set; }
        DbSet<AlbumSong> AlbumSongs { get; set; }
        DbSet<Artist> Artists { get; set; }
        DbSet<Song> Songs { get; set; }
        DbSet<SongArtist> SongArtists { get; set; }
        DbSet<Recordlabel> Recordlabels { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

    public class LabelContext : DbContext, ILabelContext
    {
        public DbSet<Album> Albums { get; set; }
        public DbSet<AlbumSong> AlbumSongs { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<SongArtist> SongArtists { get; set; }
        public DbSet<Recordlabel> Recordlabels { get; set; }
        private ConnectionStrings _connectionStrings;


        public LabelContext(DbContextOptions<LabelContext> options, IOptions<ConnectionStrings> connectionStrings)
            : base(options)
        {
            _connectionStrings = connectionStrings.Value;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {

            options.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddDebug()));
            options.UseSqlServer(_connectionStrings.SQL);
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AlbumSong>()
               .HasKey(cs => new { cs.AlbumId, cs.SongId });

            modelBuilder.Entity<SongArtist>()
               .HasKey(cs => new { cs.SongId, cs.ArtistId });

            // modelBuilder.Entity<Album>()
            // .HasMany(p => p.Songs)
            // .WithOne();


            modelBuilder.Entity<Recordlabel>().HasData(new Recordlabel()
            {
                RecordLabelId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                LabelName = "Loud Memory Records",
                Country = "Belgium",
            });
            modelBuilder.Entity<Recordlabel>().HasData(new Recordlabel()
            {
                RecordLabelId = Guid.NewGuid(),
                LabelName = "Deep Memory",
                Country = "Belgium",
            });
            modelBuilder.Entity<Artist>().HasData(new Artist()
            {
                ArtistId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                ArtistName = "Mave",
                FirstName = "Maxime",
                LastName = "Vermeeren",
                Birthdate = "08/08/2001",
                Country = "Belgium",
                StreetName = "Deinzestraat",
                HouseNumber = "175",
                PostalCode = "9700",
                PhoneNumber = "+32470053774",
                Email = "maxime6128@gmail.com"
            });
            modelBuilder.Entity<Artist>().HasData(new Artist()
            {
                ArtistId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa5"),
                ArtistName = "Max Kilian",
                FirstName = "Kilian",
                LastName = "Vermeeren",
                Birthdate = "08/08/2001",
                Country = "Belgium",
                StreetName = "Deinzestraat",
                HouseNumber = "175",
                PostalCode = "9700",
                PhoneNumber = "+32470053774",
                Email = "maxime6128@gmail.com"
            });
            modelBuilder.Entity<Song>().HasData(new Song()
            {
                SongId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                ReleaseDate = "22/08/2021",
                SongName = "Tunnelvision",
                CoverArt = "String",
                Description = "Joehooee",
                Lyrics = "You gave me tunnelvision",
                LabelId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
            });
            modelBuilder.Entity<Song>().HasData(new Song()
            {
                SongId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa5"),
                ReleaseDate = "22/08/2021",
                SongName = "Be alive",
                CoverArt = "String",
                Description = "make me feel alivee",
                Lyrics = "Take me to another room",
                LabelId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
            });
            modelBuilder.Entity<SongArtist>().HasData(new SongArtist()
            {
                SongArtistId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                SongId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                ArtistId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
            });
            modelBuilder.Entity<SongArtist>().HasData(new SongArtist()
            {
                SongArtistId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa5"),
                SongId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                ArtistId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa5"),
            });
            modelBuilder.Entity<Album>().HasData(new Album()
            {
                AlbumId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                AlbumName = "Insanium",
                ReleaseDate = "22/08/2021",
                ArtistId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
            });
            modelBuilder.Entity<AlbumSong>().HasData(new AlbumSong()
            {
                AlbumSongId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                AlbumId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                SongId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
            });
            modelBuilder.Entity<AlbumSong>().HasData(new AlbumSong()
            {
                AlbumSongId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                AlbumId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                SongId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa5"),
            });


        }

    }
}
