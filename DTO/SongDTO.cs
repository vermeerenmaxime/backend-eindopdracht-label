using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Label.API.Models;

namespace Label.API.DTO
{
    public class SongDTO
    {
        public Guid SongId { get; set; }
        public string SongName { get; set; }
        public string ReleaseDate { get; set; }
        public string CoverArt { get; set; }
        public string Description { get; set; }
        public string Lyrics { get; set; }
        public Guid LabelId { get; set; }
        public Recordlabel Recordlabel { get; set; }
        public List<Guid> Artists { get; set; }
    }
    public class SongAddDTO
    {
        public string SongName { get; set; }
        public string ReleaseDate { get; set; }
        public string CoverArt { get; set; }
        public string Description { get; set; }
        public string Lyrics { get; set; }
        [Required]
        public Guid RecordLabelId { get; set; }

        [Required]
        public List<Guid> ArtistIds { get; set; }
    }
    public class SongUpdateDTO
    {
        public Guid SongId { get; set; }
        public string SongName { get; set; }
        public string ReleaseDate { get; set; }
        public string CoverArt { get; set; }
        public string Description { get; set; }
        public string Lyrics { get; set; }
        public Guid LabelId { get; set; }
    }
}