using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace Label.API.Models
{
    public class Artist
    {
        [Required]
        public Guid ArtistId { get; set; }
        [Required]
        public string ArtistName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Birthdate { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string StreetName { get; set; }
        [Required]
        public string HouseNumber { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Phone]
        [Required]
        public string PhoneNumber { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [JsonIgnore]
        public List<Song> Songs { get; set; }
        [JsonIgnore]
        public List<Album> Albums { get; set; }
        [JsonIgnore]
        public List<SongArtist> SongArtist { get; set; }
    }
}
