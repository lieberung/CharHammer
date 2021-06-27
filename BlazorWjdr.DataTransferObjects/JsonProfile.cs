using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorWjdr.JsonDto
{
    public class JsonProfil
    {
        public int profilid { get; set; }
        public int profilcc { get; set; }
        public int profilct { get; set; }
        public int profilf { get; set; }
        public int profile { get; set; }
        public int profilag { get; set; }
        public int profilint { get; set; }
        public int profilfm { get; set; }
        public int profilsoc { get; set; }
        public int profila { get; set; }
        public int profilb { get; set; }
        public int profilbf { get; set; }
        public int profilbe { get; set; }
        public int profilm { get; set; }
        public int profilmag { get; set; }
        public int profilpf { get; set; }
        public int profilpd { get; set; }
    }

    public class RootProfil
    {
        public List<JsonProfil> items { get; set; }
    }
}
