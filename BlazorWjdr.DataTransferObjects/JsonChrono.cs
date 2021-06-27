using System;
using System.Collections.Generic;

namespace BlazorWjdr.JsonDto
{
    public class JsonChrono
    {
        public int chronoid { get; set; }
        public bool chronoalive { get; set; }
        public DateTime chronocreatedon { get; set; }
        public DateTime chronoupdatedon { get; set; }
        public int? fk_userid { get; set; }
        public int chronodebut { get; set; }
        public int? chronofin { get; set; }
        public string chronoresume { get; set; }
        public string? chronotitre { get; set; }
        public string chronocomment { get; set; }
        public List<int> chronosources { get; set; }
        public List<int> chronodomaines { get; set; }
    }

    public class RootChrono
    {
        public List<JsonChrono> items { get; set; }
    }
}
