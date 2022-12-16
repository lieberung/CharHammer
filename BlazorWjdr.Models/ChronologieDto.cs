namespace BlazorWjdr.Models;

public class ChronologieDto
{
    public ChronologieDto(int debut, int? fin, string resume, string titre, string commentaire, ReferenceDto[] sources)
    {
        Debut = debut;
        Fin = fin;
        Resume = resume;
        Titre = titre;
        Commentaire = commentaire;
        Sources = sources;
    }

    public int Debut { get; } 
    public int? Fin { get; }
    public string Periode => $"{Debut}{(Fin.HasValue ? $" ~ {Fin}" : "")}";

    public string Resume { get; }
    public string Titre { get; }
    public string Commentaire { get; }
    public ReferenceDto[] Sources { get; }
}
