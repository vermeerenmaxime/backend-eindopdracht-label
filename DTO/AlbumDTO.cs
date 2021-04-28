using System;
using System.Collections.Generic;
using Label.API.Models;

namespace Label.API.DTO
{
    public class AlbumDTO
    {
        public string AlbumName { get; set; }
        public string ReleaseDate { get; set; }
        public Guid ArtistId { get; set; }
        public List<Guid> SongIds { get; set; }
    }
    public class AlbumAddSongDTO
    {
        public Guid AlbumId { get; set; }
        public Guid SongId { get; set; }
    }
}