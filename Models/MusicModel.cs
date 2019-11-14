using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iTunesProject.Models
{
    public class MusicModel
    {
        public string WrapperType { get; set; }
        public bool? explicitness { get; set; }
        public string kind { get; set; }
        public string TrackName { get; set; }
        public string ArtistName { get; set; }
        public string CollectionName { get; set; }
        public string CensoredName { get; set; }
        public string ArtworkUrl100 { get; set; }
        public string ArtworkUrl60 { get; set; }
        public string ViewUrl { get; set; }
        public string PreviewUrl { get; set; }
        public string TrackTimeMillis { get; set; }
    }
}