using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Label.API.Models
{
    public class Recordlabel
    {
        public Guid RecordLabelId { get; set; }
        public string LabelName { get; set; }
        public string Country { get; set; }
        [JsonIgnore]
        public List<Song> Songs { get; set; }
    }
}
