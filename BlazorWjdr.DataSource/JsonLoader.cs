namespace BlazorWjdr.DataSource
{
    using JsonDto;
    using Newtonsoft.Json;
    using System.IO;

    public static class JsonLoader
    {
        private static T LoadRootFromJson<T>(string path) => JsonConvert.DeserializeObject<T>(File.ReadAllText(path))
            ?? throw new System.Exception($"Impossible de désérialiser '{path}' avec la class '{typeof(T)}'");

        public static RootBestiole GetRootBestiole() => LoadRootFromJson<RootBestiole>("./json-data/bestiole.json");
        public static RootCarriere GetRootCarriere() => LoadRootFromJson<RootCarriere>("./json-data/carriere.json");
        public static RootChoixCompetence GetRootChoixCompetence() => LoadRootFromJson<RootChoixCompetence>("./json-data/choixcompetence.json");
        public static RootChoixTalent GetRootChoixTalent() => LoadRootFromJson<RootChoixTalent>("./json-data/choixtalent.json");
        public static RootChrono GetRootChrono() => LoadRootFromJson<RootChrono>("./json-data/chrono.json");
        public static RootCompetence GetRootCompetence() => LoadRootFromJson<RootCompetence>("./json-data/competence.json");
        public static RootDieu GetRootDieu() => LoadRootFromJson<RootDieu>("./json-data/dieu.json");
        public static RootDomaine GetRootDomaine() => LoadRootFromJson<RootDomaine>("./json-data/domaine.json");
        public static RootLieu GetRootLieu() => LoadRootFromJson<RootLieu>("./json-data/lieu.json");
        public static RootLieuType GetRootLieuType() => LoadRootFromJson<RootLieuType>("./json-data/lieutype.json");
        public static RootArme GetRootArme() => LoadRootFromJson<RootArme>("./json-data/arme.json");
        public static RootArmeAttribut GetRootArmeAttribut() => LoadRootFromJson<RootArmeAttribut>("./json-data/armeattribut.json");
        public static RootPersonnage GetRootPersonnage() => LoadRootFromJson<RootPersonnage>("./json-data/personnage.json");
        public static RootPj GetRootPj() => LoadRootFromJson<RootPj>("./json-data/pj.json");
        public static RootProfil GetRootProfil() => LoadRootFromJson<RootProfil>("./json-data/profil.json");
        public static RootRace GetRootRace() => LoadRootFromJson<RootRace>("./json-data/race.json");
        public static RootReference GetRootReference() => LoadRootFromJson<RootReference>("./json-data/reference.json");
        public static RootTableCalcPoints GetRootTableCalcPoints() => LoadRootFromJson<RootTableCalcPoints>("./json-data/tablecalcpoints.json");
        public static RootTableCarriereInitiale GetRootTableCarriereInitiale() => LoadRootFromJson<RootTableCarriereInitiale>("./json-data/tablecarriereinitiale.json");
        public static RootTalent GetRootTalent() => LoadRootFromJson<RootTalent>("./json-data/talent.json");
    }
}
