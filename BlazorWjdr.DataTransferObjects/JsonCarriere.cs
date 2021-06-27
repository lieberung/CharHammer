using System;
using System.Collections.Generic;

namespace BlazorWjdr.JsonDto
{
    public class JsonCarriere
    {
        public int carriereid { get; set; }
        public bool carrierealive { get; set; }
        public DateTime carrierecreatedon { get; set; }
        public DateTime carriereupdatedon { get; set; }
        public string carrierelibelle { get; set; }
        public string carrieredescription { get; set; }
        public string? carriereimage { get; set; }
        public bool carriereavancee { get; set; }
        public string carriererestriction { get; set; }
        public int fk_plandecarriereid { get; set; }
        public string carrieresource { get; set; }
        public List<int> fk_debouches { get; set; }
        public List<int> fk_competences { get; set; }
        public List<int> fk_talents { get; set; }
        public List<int> fk_choixcompetences { get; set; }
        public List<int> fk_choixtalents { get; set; }
        public int fk_sourceid { get; set; }
        public bool carrierecomplete { get; set; }
        public string? carrieredotations { get; set; }
        public int? fk_parentcarriereid { get; set; }
    }

    public class RootCarriere
    {
        public List<JsonCarriere> items { get; set; }
    }
}
