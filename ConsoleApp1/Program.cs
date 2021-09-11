using System;
using System.IO;
using System.Text;
using BlazorWjdr.DataSource.JsonDto;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
            var comp = LoadJson<RootCompetence>("json-data/competence.json");
            var carr = LoadJson<RootCarriere>("json-data/carriere.json");
            var talents = LoadJson<RootTalent>("json-data/talent.json");
            var traits = LoadJson<RootTrait>("json-data/trait.json");
            var regles = LoadJson<RootRegle>("json-data/regle.json");
            
            var bestioles = LoadJson<RootBestiole>("json-data/bestiole.json");
            var personnages = LoadJson<RootPersonnage>("json-data/personnage.json");
            var pjs = LoadJson<RootPj>("json-data/pj.json");

            Refonte.RefonteJson(comp.items, talents.items, traits.items, carr.items, regles.items
                , bestioles.items, personnages.items, pjs.items);
        }

        public static T LoadJson<T>(string path)
        {
            using (StreamReader r = new StreamReader(path, Encoding.UTF8))
            {
                string json = r.ReadToEnd();
                T items = JsonConvert.DeserializeObject<T>(json);
                return items;
            }
        }
    }
}