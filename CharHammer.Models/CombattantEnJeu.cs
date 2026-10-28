namespace CharHammer.Models;

/// <summary>
/// Un combattant pendant une bataille de l'assistant du MJ : son jet d'initiative et l'adversaire qu'il engage.
/// C'est un état de jeu, recréé à chaque jet d'initiative ; <see cref="CombattantDto"/> reste une donnée figée.
/// </summary>
public class CombattantEnJeu(CombattantDto participant, int jetDInitiative, string detailDuJet)
{
    public CombattantDto Participant { get; } = participant;
    public int JetDInitiative { get; } = jetDInitiative;
    public string DetailDuJet { get; } = detailDuJet;
    public CombattantEnJeu? EngageContre { get; set; }

    // Raccourcis vers le participant, pour l'affichage
    public BestioleDto Combattant => Participant.Combattant;
    public string Nom => Participant.Nom;
    public bool Ennemi => Participant.Ennemi;
    public string Code => Participant.Code;
    public IReadOnlyList<AptitudeAcquiseDto> CompetencesMartiales => Participant.CompetencesMartiales;
    public IReadOnlyList<AptitudeAcquiseDto> AutresTraitsMartiaux => Participant.AutresTraitsMartiaux;
}
