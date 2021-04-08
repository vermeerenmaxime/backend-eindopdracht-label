using System;

namespace Label.API.Models
{
    public class SongArtist
    {
        public Guid SongArtistId { get; set; }
        public Guid SongId { get; set; }
        public Guid ArtistId { get; set; }
        public Artist Artist { get; set; }
    }
}
