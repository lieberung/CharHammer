using System;
using System.Collections.Generic;

namespace BlazorWjdr.JsonDto
{
    public class JsonTalent
    {
        public int talentid { get; set; }
        public bool talentalive { get; set; }
        public DateTime talentcreatedon { get; set; }
        public DateTime talentupdatedon { get; set; }
        public string talentlibelle { get; set; }
        public string talentresume { get; set; }
        public string? talentdescription { get; set; }
        public bool talenttrait { get; set; }
        public string? talentspecialis { get; set; }
        public int? talentparentkey { get; set; }
        public bool talentignore { get; set; }
    }

    public class Root
    {
        public List<JsonTalent> items { get; set; }
    }

}
