using System.Collections.Generic;
using System.Linq;

namespace BlazorWjdr.Services
{
    public static class GenericService
    {
        #region Supprimer les caractères indésirables

        private const string CaracteresARemplacer =     "ÀÁÂÃÄÅàáâãäåÒÓÔÕÖØòóôõöøÈÉÊËèéêëÌÍÎÏìíîïÙÚÛÜùúûüÿÑñÇç-'";
        private const string CaracteresDeRemplacement = "aaaaaaaaaaaaooooooooooooeeeeeeeeiiiiiiiiuuuuuuuuynncc  ";

        internal static string ConvertirCaracteres(string chaineANettoyer)
        {
            chaineANettoyer = chaineANettoyer.ToLower();

            char[] tableauFind = CaracteresDeRemplacement.ToCharArray();
            char[] tableauReplace = CaracteresARemplacer.ToCharArray();

            for (var i = 0; i < tableauReplace.Length; i++)
                chaineANettoyer = chaineANettoyer.Replace(tableauReplace[i], tableauFind[i]);

        }

        internal static List<string> MotsClefsDeRecherche(string chaineADecouper)
        {
            return chaineADecouper
                .Split(' ')
                .Where(m => m != "de" && m != "des" && m != "du" && m != "la" && m != "a" && m != "l")
                .ToList();
        }

        #endregion
    }
}