using System;

namespace PlantEng.Models.Visitor
{
    public class VisitorInfo
    {
        public int FromUserId { get; set; }
        public string FromUserName { get; set; }
        public int ToUserId { get; set; }
        public string ToUserName { get; set; }
        public DateTime VisitDateTime { get; set; }
        public string VisitDateTimeToString { get; set; }
    }
}
