using System.Linq;

namespace BlazorWjdr.Models
{
    public class ProfilDto
    {
        public int Id { get; init; }
        public int Cc { get; init; }
        public int Ct { get; init; }
        public int F { get; init; }
        public int E { get; init; }
        public int I { get; init; }
        public int Ag { get; init; }
        public int Dex { get; init; }
        public int Int { get; init; }
        public int Fm { get; init; }
        public int Soc { get; init; }
        public int M { get; init; }
        public int Bf => F / 10;
        public int Be => E / 10;
        public int BonusDInitiative => I / 10;
        public int BonusDAgilite => Ag / 10;
        public int Bfm => Fm / 10;
        
        public int GetStat(string caracteristique)
        {
            caracteristique = caracteristique.Replace(" ", "").Split("/").First();
            switch (caracteristique)
            {
                case "CC": return Cc;
                case "CT": return Ct;
                case "F": return F;
                case "E": return E;
                case "I": return I;
                case "Ag": return Ag;
                case "Dex": return Dex;
                case "Int": return Int;
                case "FM": return Fm;
                case "Soc": return Soc;
                default: return 0;
            }
        }
    }
}
