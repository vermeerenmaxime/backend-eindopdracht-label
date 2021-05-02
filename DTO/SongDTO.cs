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
        [Required(ErrorMessage="Song name is required")]
        public string SongName { get; set; }
        [Required]
        public string ReleaseDate { get; set; }
        [Required]
        public string CoverArt { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Lyrics { get; set; }
        [Required]
        public Guid RecordLabelId { get; set; }
        [Required]
        public List<Guid> ArtistIds { get; set; }
    }
    public class SongUpdateDTO
    {
        [Required]
        public Guid SongId { get; set; }
        [Required]
        public string SongName { get; set; }
        [Required]
        public string ReleaseDate { get; set; }
        [Required]
        public string CoverArt { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Lyrics { get; set; }
        [Required]
        public Guid LabelId { get; set; }
    }
}