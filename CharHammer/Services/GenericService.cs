namespace CharHammer.Services;

public static class GenericService
{
    public static int RollIndex(int max) => new Random().Next(0, max);
    public static int RollDice(int nombreDeFaces, int nombreDeDes = 1)
    {
        var total = 0;
        for (var i = 0; i < nombreDeDes; i++)
            total += 1 + RollIndex(nombreDeFaces);
        return total;
    }

    public static string GetLocalisation(int d100) => d100 switch
        {
            < 10 => $"Tête ({d100})",
            < 25 => $"Bras gauche ({d100})",
            < 45 => $"Bras droit ({d100})",
            < 80 => $"Torse ({d100})",
            < 90 => $"Jambe droite ({d100})",
            _ => $"Jambe gauche ({d100})"
        };
    
    #region Supprimer les caractères indésirables

    private const string CaracteresARemplacer =     "àáâãäåòóôõöøèéêëìíîïùúûüÿñç-'";
    private const string CaracteresDeRemplacement = "aaaaaaooooooeeeeiiiiuuuuync  ";

    internal static string GetUrlChunck(string chaine) => ConvertirCaracteres(chaine).Replace(" ", "-");

    internal static string NettoyerPourRecherche(string chaine)
    {
        chaine = chaine.ToLower();
        chaine = chaine.Replace("armes", "arme");
        return ConvertirCaracteres(chaine);
    }

    private static string ConvertirCaracteres(string chaine)
    {
        chaine = chaine.ToLower();
        
        var tableauFind = CaracteresDeRemplacement.ToCharArray();
        var tableauReplace = CaracteresARemplacer.ToCharArray();

        for (var i = 0; i < tableauReplace.Length; i++)
            chaine = chaine.Replace(tableauReplace[i], tableauFind[i]);

        while (chaine.Contains("  "))
            chaine = chaine.Replace("  ", " ");
                
        return chaine;
    }

    internal static IEnumerable<string> MotsClefsDeRecherche(string chaineADecouper)
    {
        return chaineADecouper
            .Split(' ')
            .Where(m => !string.IsNullOrWhiteSpace(m) && m != "de" && m != "des" && m != "du" && m != "la" && m != "a" && m != "l");
    }

    #endregion

    public static bool DieuEstDAccord(string? password) => password == "6800";
}