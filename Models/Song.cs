using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Label.API.Models
{
    public class Song
    {
        public Guid SongId { get; set; }
        public string SongName { get; set; }
        public string ReleaseDate { get; set; }
        public string CoverArt { get; set; }
        public string Description { get; set; }
        public string Lyrics { get; set; }
        [JsonIgnore]
        public Guid LabelId { get; set; }
        public Recordlabel Recordlabel { get; set; }
        public List<Artist> Artists { get; set; }
    }
}
