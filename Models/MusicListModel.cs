using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iTunesProject.Models
{
    public class MusicListModel
    {
        public int ResultCount { get; set; }
        public List<MusicModel> Results { get; set; }
    }
}