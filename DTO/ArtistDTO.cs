using System;
using System.ComponentModel.DataAnnotations;

namespace Label.API.DTO
{
    public class ArtistDTO
    {

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
    }
}