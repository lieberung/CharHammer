namespace BlazorWjdr.Models
{
    public class TraitDto
    {
        public int Id { get; init; }
        public int Severite { get; init; }
        public string Groupe { get; init; } = null!;
        public string Nom { get; init; } = null!;
        public string Spe { get; init; } = null!;
        public string Guerison { get; init; } = null!;
        public string Description { get; init; } = null!;
        public bool? Contagieux { get; init; }

        public string NomComplet => Nom + (string.IsNullOrWhiteSpace(Spe) ? "" : $" : {Spe}");
        public string GroupeSexy {
            get
            {
                return Groupe switch
                {
                    "trait" => "signe distinctif",
                    "nevrose" => "névrose",
                    "addiction" => "addiction",
                    "allergie" => "allergie",
                    "folie" => "folie",
                    "maladie" => "maladie",
                    _ => "non classé"
                };
            }
        }
    }
}