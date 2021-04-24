using System;

namespace Label.API.Models
{
    public class songAlbum
    {
        public Guid SongArtistId { get; set; }
        public Guid AlbumId { get; set; }
        public Guid ArtistId { get; set; }
    }
}
