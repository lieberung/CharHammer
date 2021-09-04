/*
namespace BlazorWjdr.DataSource
{
    using JsonDto;
    using Newtonsoft.Json;
    using System.IO;

    public static class JsonLoader
    {
        const string jsonDataPath = "../json-data";

        private static T LoadRootFromJson<T>(string path) => JsonConvert.DeserializeObject<T>(File.ReadAllText(path))
            ?? throw new System.Exception($"Impossible de désérialiser '{path}' avec la class '{typeof(T)}'");

        public static RootBestiole GetRootBestiole() => LoadRootFromJson<RootBestiole>($"{jsonDataPath}/bestiole.json");
        public static RootCarriere GetRootCarriere() => LoadRootFromJson<RootCarriere>($"{jsonDataPath}/carriere.json");
        public static RootChrono GetRootChrono() => LoadRootFromJson<RootChrono>($"{jsonDataPath}/chrono.json");
        public static RootCompetence GetRootCompetence() => LoadRootFromJson<RootCompetence>($"{jsonDataPath}/competence.json");
        public static RootDieu GetRootDieu() => LoadRootFromJson<RootDieu>($"{jsonDataPath}/dieu.json");
        public static RootDomaine GetRootDomaine() => LoadRootFromJson<RootDomaine>($"{jsonDataPath}/domaine.json");
        public static RootLieu GetRootLieu() => LoadRootFromJson<RootLieu>($"{jsonDataPath}/lieu.json");
        public static RootLieuType GetRootLieuType() => LoadRootFromJson<RootLieuType>($"{jsonDataPath}/lieutype.json");
        public static RootArme GetRootArme() => LoadRootFromJson<RootArme>($"{jsonDataPath}/arme.json");
        public static RootArmeAttribut GetRootArmeAttribut() => LoadRootFromJson<RootArmeAttribut>($"{jsonDataPath}/armeattribut.json");
        public static RootPersonnage GetRootPersonnage() => LoadRootFromJson<RootPersonnage>($"{jsonDataPath}/personnage.json");
        public static RootPj GetRootPj() => LoadRootFromJson<RootPj>($"{jsonDataPath}/pj.json");
        public static RootProfil GetRootProfil() => LoadRootFromJson<RootProfil>($"{jsonDataPath}/profil.json");
        public static RootRace GetRootRace() => LoadRootFromJson<RootRace>($"{jsonDataPath}/race.json");
        public static RootReference GetRootReference() => LoadRootFromJson<RootReference>($"{jsonDataPath}/reference.json");
        public static RootTable GetRootTable() => LoadRootFromJson<RootTable>($"{jsonDataPath}/table.json");
        public static RootRegle GetRootRegle() => LoadRootFromJson<RootRegle>($"{jsonDataPath}/regle.json");
        public static RootTableCarriereInitiale GetRootTableCarriereInitiale() => LoadRootFromJson<RootTableCarriereInitiale>($"{jsonDataPath}/tablecarriereinitiale.json");
        public static RootTalent GetRootTalent() => LoadRootFromJson<RootTalent>($"{jsonDataPath}/talent.json");
        public static RootTrait GetRootTrait() => LoadRootFromJson<RootTrait>($"{jsonDataPath}/trait.json");
    }
}
*/