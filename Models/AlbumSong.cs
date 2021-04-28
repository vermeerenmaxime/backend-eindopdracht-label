using System;

namespace Label.API.Models
{
    public class AlbumSong
    {
        public Guid AlbumSongId { get; set; }
        public Guid AlbumId { get; set; }
        public Guid SongId { get; set; }
        public Song Song { get; set; }
    }
}
