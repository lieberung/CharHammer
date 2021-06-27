namespace BlazorWjdr.DataSource.JsonDto
{
    using System.Collections.Generic;

    public class JsonTalent
    {
        public int talentid { get; set; }
        public string talentlibelle { get; set; } = null!;
        public string talentresume { get; set; } = null!;
        public string? talentdescription { get; set; }
        public bool talenttrait { get; set; }
        public string? talentspecialis { get; set; }
        public int? talentparentkey { get; set; }
        public bool talentignore { get; set; }
    }

    public class RootTalent
    {
        public List<JsonTalent> items { get; set; } = null!;
    }

}
