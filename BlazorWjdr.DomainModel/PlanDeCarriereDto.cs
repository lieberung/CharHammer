namespace BlazorWjdr.Models
{
    public class PlanDeCarriereDto
    {
        public PlanDeCarriereDto(ProfilDto profil)
        {
            ProfilSource = profil;
            CC = ToBonus(profil.Cc);
            CT = ToBonus(profil.Ct);
            F = ToBonus(profil.F);
            E = ToBonus(profil.E);
            Ag = ToBonus(profil.Ag);
            Int = ToBonus(profil.Int);
            FM = ToBonus(profil.Fm);
            Soc = ToBonus(profil.Soc);
            A = ToBonus(profil.A);
            B = ToBonus(profil.B);
            M = ToBonus(profil.M);
            Mag = ToBonus(profil.Mag);
        }

        public ProfilDto ProfilSource { get; }

        public string CC { get; set; }
        public string CT { get; set; }
        public string F { get; set; }
        public string E { get; set; }
        public string Ag { get; set; }
        public string Int { get; set; }
        public string FM { get; set; }
        public string Soc { get; set; }
        public string A { get; set; }
        public string B { get; set; }
        public string M { get; set; }
        public string Mag { get; set; }

        private string ToBonus(int value) => value == 0 ? "" : $"+{value}";
    }
}
