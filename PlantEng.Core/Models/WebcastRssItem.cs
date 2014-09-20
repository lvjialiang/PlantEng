using System;

namespace PlantEng.Models
{
    public class WebcastRssItem
    {
        public string Title
        {
            get;
            set;
        }
        public string Url
        {
            get;
            set;
        }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
