using System;
using System.Collections.Generic;

namespace Label.API.Models
{
    public class Album
    {
        public Guid AlbumId { get; set; }
        public string AlbumName { get; set; }
        public string ReleaseDate { get; set; }
        public List<Song> Songs { get; set; }
    }
}
