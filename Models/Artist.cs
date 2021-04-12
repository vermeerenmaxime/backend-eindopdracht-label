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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Birthdate { get; set; }
        public string Country { get; set; }
        public string StreetName { get; set; }
        public string HouseNumber { get; set; }
        public string PostalCode { get; set; }
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
