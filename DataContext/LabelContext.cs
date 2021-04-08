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
        DbSet<Artist> Artists { get; set; }
        DbSet<Song> Songs { get; set; }
        DbSet<Recordlabel> Recordlabels { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

    public class LabelContext : DbContext, ILabelContext
    {
        public DbSet<Album> Albums { get; set; }
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
            // modelBuilder.Entity<SneakerOccasion>()
            //    .HasKey(cs => new { cs.SneakerId, cs.OccasionId });
            modelBuilder.Entity<Recordlabel>().HasData(new Recordlabel()
            {
                RecordLabelId = Guid.NewGuid(),
                LabelName = "Loud Memory Records",
                Country = "Belgium",
            });
            modelBuilder.Entity<Artist>().HasData(new Artist()
            {
                ArtistId = Guid.NewGuid(),
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


            // modelBuilder.Entity<Brand>().HasData(new Brand()
            // {
            //     BrandId = 2,
            //     Name = "CONVERSE"
            // });

            // modelBuilder.Entity<Brand>().HasData(new Brand()
            // {
            //     BrandId = 3,
            //     Name = "JORDAN"
            // });

            // modelBuilder.Entity<Occasion>().HasData(new Occasion()
            // {
            //     OccasionId = 1,
            //     Description = "Sports"
            // });


            // modelBuilder.Entity<Occasion>().HasData(new Occasion()
            // {
            //     OccasionId = 2,
            //     Description = "Casual"
            // });

            // modelBuilder.Entity<Occasion>().HasData(new Occasion()
            // {
            //     OccasionId = 3,
            //     Description = "Skate"
            // });
        }

    }
}
