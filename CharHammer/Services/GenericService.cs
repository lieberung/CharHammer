using System.Globalization;
using System.Text;

namespace CharHammer.Services;

public static class GenericService
{
    public static int RollIndex(int max) => Random.Shared.Next(0, max);
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

    internal static string GetUrlChunk(string chaine) => ConvertirCaracteres(chaine).Replace(" ", "-");

    internal static string NettoyerPourRecherche(string chaine)
    {
        chaine = chaine.ToLowerInvariant();
        chaine = chaine.Replace("armes", "arme");
        return ConvertirCaracteres(chaine);
    }

    /// <summary>
    /// Passe en minuscules, retire les diacritiques (é → e, ç → c…), développe les ligatures (œ → oe, æ → ae),
    /// remplace tirets, apostrophes (droites ou typographiques) et espaces spéciales par une espace simple,
    /// et fusionne les espaces consécutives.
    /// </summary>
    private static string ConvertirCaracteres(string chaine)
    {
        var decomposee = chaine.ToLowerInvariant().Normalize(NormalizationForm.FormD);
        var resultat = new StringBuilder(decomposee.Length);

        foreach (var c in decomposee)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.NonSpacingMark)
                continue;

            switch (c)
            {
                case 'œ': resultat.Append("oe"); break;
                case 'æ': resultat.Append("ae"); break;
                case 'ø': resultat.Append('o'); break;
                case 'ß': resultat.Append("ss"); break;
                case '\'' or '’' or '‘' or 'ʼ' or '-' or '‐' or '‑' or '–' or '—':
                    AjouterEspace(resultat);
                    break;
                default:
                    if (char.IsWhiteSpace(c))
                        AjouterEspace(resultat);
                    else
                        resultat.Append(c);
                    break;
            }
        }

        return resultat.ToString();
    }

    private static void AjouterEspace(StringBuilder resultat)
    {
        if (resultat.Length == 0 || resultat[resultat.Length - 1] != ' ')
            resultat.Append(' ');
    }

    internal static string[] MotsClefsDeRecherche(string chaineADecouper)
    {
        return chaineADecouper
            .Split(' ')
            .Where(m => !string.IsNullOrWhiteSpace(m) && m != "de" && m != "des" && m != "du" && m != "la" && m != "a" && m != "l")
            .ToArray();
    }

    #endregion

    public static bool DieuEstDAccord(string? password) => password == "6800";
}