using System.Data;
using BlazorWjdr.DataSource.JsonDto;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorWjdr.Services
{
    public class ADataClassToRuleThemAllService
    {
        private readonly HttpClient _httpClient;

        public ADataClassToRuleThemAllService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task InitializeDataAsync()
        {
            Armes = await GetRootArme();
            ArmesAttributs = await GetRootArmeAttribut();
            Bestioles = await GetRootBestiole();
            Carrieres = await GetRootCarriere();
            CarrieresInitiales = await GetRootTableCarriereInitiale();
            Chrono = new RootChrono(); //await GetRootChrono();
            Competences = await GetRootCompetence();
            Dieux = await GetRootDieu();
            Domaines = await GetRootDomaine();
            Lieux = await GetRootLieu();
            LieuxTypes = await GetRootLieuType();
            Personnages = await GetRootPersonnage();
            Pjs = await GetRootPj();
            Profils = await GetRootProfil();
            Races = await GetRootRace();
            References = await GetRootReference();
            Regles = await GetRootRegle();
            Tables = await GetRootTable();
            Talents = await GetRootTalent();
            Traits = await GetRootTrait();
        }

        public RootBestiole? Bestioles { get; private set; }
        public RootCarriere Carrieres { get; private set; }
        public RootChrono Chrono { get; private set; }
        public RootCompetence Competences { get; private set; }
        public RootDieu Dieux { get; private set; }
        public RootDomaine Domaines { get; private set; }
        public RootLieu Lieux { get; private set; }
        public RootLieuType LieuxTypes { get; private set; }
        public RootArme Armes { get; private set; }
        public RootArmeAttribut ArmesAttributs { get; private set; }
        public RootPersonnage Personnages { get; private set; }
        public RootPj Pjs { get; private set; }
        public RootProfil Profils { get; private set; }
        public RootRace Races { get; private set; }
        public RootReference References { get; private set; }
        public RootTable Tables { get; private set; }
        public RootRegle Regles { get; private set; }
        public RootTableCarriereInitiale CarrieresInitiales { get; private set; }
        public RootTalent Talents { get; private set; }
        public RootTrait Traits { get; private set; }

        private const string JsonDataPath = "json-data";

        private async Task<T> LoadRootFromJson<T>(string path)
        {
            var data = await _httpClient.GetFromJsonAsync<T>(path);
            return data ?? throw new NoNullAllowedException(nameof(data));
        }

        private async Task<RootBestiole> GetRootBestiole() => await LoadRootFromJson<RootBestiole>($"{JsonDataPath}/bestiole.json");
        private async Task<RootCarriere> GetRootCarriere() => await LoadRootFromJson<RootCarriere>($"{JsonDataPath}/carriere.json");
        private async Task<RootChrono> GetRootChrono() => await LoadRootFromJson<RootChrono>($"{JsonDataPath}/chrono.json");
        private async Task<RootCompetence> GetRootCompetence() => await LoadRootFromJson<RootCompetence>($"{JsonDataPath}/competence.json");
        private async Task<RootDieu> GetRootDieu() => await LoadRootFromJson<RootDieu>($"{JsonDataPath}/dieu.json");
        private async Task<RootDomaine> GetRootDomaine() => await LoadRootFromJson<RootDomaine>($"{JsonDataPath}/domaine.json");
        private async Task<RootLieu> GetRootLieu() => await LoadRootFromJson<RootLieu>($"{JsonDataPath}/lieu.json");
        private async Task<RootLieuType> GetRootLieuType() => await LoadRootFromJson<RootLieuType>($"{JsonDataPath}/lieutype.json");
        private async Task<RootArme> GetRootArme() => await LoadRootFromJson<RootArme>($"{JsonDataPath}/arme.json");
        private async Task<RootArmeAttribut> GetRootArmeAttribut() => await LoadRootFromJson<RootArmeAttribut>($"{JsonDataPath}/armeattribut.json");
        private async Task<RootPersonnage> GetRootPersonnage() => await LoadRootFromJson<RootPersonnage>($"{JsonDataPath}/personnage.json");
        private async Task<RootPj> GetRootPj() => await LoadRootFromJson<RootPj>($"{JsonDataPath}/pj.json");
        private async Task<RootProfil> GetRootProfil() => await LoadRootFromJson<RootProfil>($"{JsonDataPath}/profil.json");
        private async Task<RootRace> GetRootRace() => await LoadRootFromJson<RootRace>($"{JsonDataPath}/race.json");
        private async Task<RootReference> GetRootReference() => await LoadRootFromJson<RootReference>($"{JsonDataPath}/reference.json");
        private async Task<RootTable> GetRootTable() => await LoadRootFromJson<RootTable>($"{JsonDataPath}/table.json");
        private async Task<RootRegle> GetRootRegle() => await LoadRootFromJson<RootRegle>($"{JsonDataPath}/regle.json");
        private async Task<RootTableCarriereInitiale> GetRootTableCarriereInitiale() => await LoadRootFromJson<RootTableCarriereInitiale>($"{JsonDataPath}/tablecarriereinitiale.json");
        private async Task<RootTalent> GetRootTalent() => await LoadRootFromJson<RootTalent>($"{JsonDataPath}/talent.json");
        private async Task<RootTrait> GetRootTrait() => await LoadRootFromJson<RootTrait>($"{JsonDataPath}/trait.json");
    }
}
