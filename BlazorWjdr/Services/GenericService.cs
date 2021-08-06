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

        internal static string ConvertirCaracteres(string chaineANettoyer)
        {
            chaineANettoyer = chaineANettoyer.ToLower();

            chaineANettoyer = chaineANettoyer.Replace("armes", "arme");
            chaineANettoyer = chaineANettoyer.Replace("connaissances", "conn");
            chaineANettoyer = chaineANettoyer.Replace("connaissance", "conn");
            chaineANettoyer = chaineANettoyer.Replace("conn.", "conn");
            chaineANettoyer = chaineANettoyer.Replace("académiques", "acad");
            chaineANettoyer = chaineANettoyer.Replace("générales", "gene");
            
            char[] tableauFind = CaracteresDeRemplacement.ToCharArray();
            char[] tableauReplace = CaracteresARemplacer.ToCharArray();

            for (var i = 0; i < tableauReplace.Length; i++)
                chaineANettoyer = chaineANettoyer.Replace(tableauReplace[i], tableauFind[i]);

            while (chaineANettoyer.Contains("  "))
                chaineANettoyer = chaineANettoyer.Replace("  ", " ");
                    
            return chaineANettoyer;
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