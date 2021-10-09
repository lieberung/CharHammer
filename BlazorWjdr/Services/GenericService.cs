using System.Collections.Generic;
using System.Linq;

namespace BlazorWjdr.Services
{
    public static class GenericService
    {
        public static IEnumerable<IEnumerable<T>> Split<T>(this T[] array, int size)
        {
            for (var i = 0; i < (float)array.Length / size; i++)
            {
                yield return array.Skip(i * size).Take(size);
            }
        }
        
        #region Supprimer les caractères indésirables

        private const string CaracteresARemplacer =     "àáâãäåòóôõöøèéêëìíîïùúûüÿñç-'";
        private const string CaracteresDeRemplacement = "aaaaaaooooooeeeeiiiiuuuuync  ";

        internal static string GetUrlChunck(string chaine) => ConvertirCaracteres(chaine).Replace(" ", "-");

        internal static string NettoyerPourRecherche(string chaine)
        {
            chaine = chaine.ToLower();
            chaine = chaine.Replace("armes", "arme");
            chaine = chaine.Replace("connaissances", "conn");
            chaine = chaine.Replace("connaissance", "conn");
            chaine = chaine.Replace("conn.", "conn");
            chaine = chaine.Replace("académiques", "acad");
            chaine = chaine.Replace("générales", "gene");
            return ConvertirCaracteres(chaine);
        }

        private static string ConvertirCaracteres(string chaine)
        {
            chaine = chaine.ToLower();
            
            char[] tableauFind = CaracteresDeRemplacement.ToCharArray();
            char[] tableauReplace = CaracteresARemplacer.ToCharArray();

            for (var i = 0; i < tableauReplace.Length; i++)
                chaine = chaine.Replace(tableauReplace[i], tableauFind[i]);

            while (chaine.Contains("  "))
                chaine = chaine.Replace("  ", " ");
                    
            return chaine;
        }

        internal static List<string> MotsClefsDeRecherche(string chaineADecouper)
        {
            return chaineADecouper
                .Split(' ')
                .Where(m => !string.IsNullOrWhiteSpace(m) && m != "de" && m != "des" && m != "du" && m != "la" && m != "a" && m != "l")
                .ToList();
        }

        #endregion
    }
}