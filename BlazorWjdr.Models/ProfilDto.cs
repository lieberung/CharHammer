using System.Linq;

namespace BlazorWjdr.Models;

public class ProfilDto
{
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
    public int B { get; init; }
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
