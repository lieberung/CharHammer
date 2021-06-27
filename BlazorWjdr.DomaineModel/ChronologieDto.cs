using System;

namespace BlazorWjdr.DomainModel
{
    public class ChronologieDto
    {
        public ChronologieDto(int debut, int? fin, string resume, string? titre, string commentaire)
        {
            Debut = debut;
            Fin = fin;
            Resume = resume;
            Titre = titre;
            Commentaire = commentaire;
        }

        public int Debut { get; protected set; } 
        public int? Fin { get; protected set; }
        public string Periode => $"{Debut}{(Fin.HasValue ? $" ~ {Fin}" : "")}";

        public string Resume { get; protected set; }
        public string? Titre { get; protected set; }
        public string Commentaire { get; protected set; }
    }
}
