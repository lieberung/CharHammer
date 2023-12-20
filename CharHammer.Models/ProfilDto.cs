namespace CharHammer.Models;

public record ProfilDto(int Cc, int Ct, int F, int E, int I, int Ag, int Dex, int Int, int Fm, int Soc, int M, int B)
{
    public int Bf => F / 10;
    public int Be => E / 10;
    public int BonusDInitiative => I / 10;
    public int BonusDAgilite => Ag / 10;
    public int Bfm => Fm / 10;

    public int GetStat(string caracteristique)
    {
        caracteristique = caracteristique.Replace(" ", "").Split("/").First();
        return caracteristique switch
        {
            "CC" => Cc,
            "CT" => Ct,
            "F" => F,
            "E" => E,
            "I" => I,
            "Ag" => Ag,
            "Dex" => Dex,
            "Int" => Int,
            "FM" => Fm,
            "Soc" => Soc,
            _ => 0
        };
    }
}
