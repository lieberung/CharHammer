namespace BlazorWjdr.Models
{
    public class CombattantDto
    {
        public CombattantDto(BestioleDto combattant, int jetDInitiative, string detailDuJet)
        {
            Combattant = combattant;
            JetDInitiative = jetDInitiative;
            DetailDuJet = detailDuJet;
        }

        public BestioleDto Combattant { get; }
        public int JetDInitiative { get; }
        public string DetailDuJet { get; }
    }
}