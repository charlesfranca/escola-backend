using System;
using System.Collections.Generic;

namespace EscolaDeVoce.Backend.ViewModel
{
    public class VideoSambatech
    {
        public string id { get; set; }
        public string title { get; set; }
        public string status { get; set; }
        public List<ThumbSambatech> thumbs { get; set; }
        public List<FileSambatech> files { get; set; }
    }
}