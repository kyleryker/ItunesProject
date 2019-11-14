using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iTunesProject.Models
{
    public class MusicWithClicksModel
    {
        public MusicModel Music { get; set; }

        public int Clicks { get; set; }
    }
}