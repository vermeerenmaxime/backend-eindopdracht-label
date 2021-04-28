using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Label.API.Models
{
    public class Album
    {
        public Guid AlbumId { get; set; }
        public string AlbumName { get; set; }
        public string ReleaseDate { get; set; }
        [JsonIgnore]
        public Guid ArtistId { get; set; }
        public Artist Artist { get; set; }
        public List<Song> Songs { get; set; }
    }
}
