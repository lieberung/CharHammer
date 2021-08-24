using System;
using System.Diagnostics;
using BlazorWjdr.DataSource.JsonDto;
using BlazorWjdr.Pages;

namespace BlazorWjdr.Services
{
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class BestiolesService
    {
        private readonly List<JsonBestiole> _dataBestioles;
        private readonly List<JsonPj> _dataPjs;
        private readonly List<JsonPersonnage> _dataPersonnages;

        private readonly RacesService _racesService;
        private readonly CompetencesEtTalentsService _competencesEtTalentsService;
        private readonly LieuxService _lieuxService;
        private readonly ProfilsService _profilsService;
        private readonly CarrieresService _carrieresService;
        private readonly TraitsService _traitsService;

        public BestiolesService(
            List<JsonBestiole> dataBestioles,
            List<JsonPj> dataPjs,
            List<JsonPersonnage> dataPersonnages,
            RacesService racesService,
            CompetencesEtTalentsService competencesEtTalentsService,
            LieuxService lieuxService,
            ProfilsService profilsService,
            CarrieresService carrieresService,
            TraitsService traitsService)
        {
            _dataBestioles = dataBestioles;
            _dataPersonnages = dataPersonnages;
            _dataPjs = dataPjs;

            _racesService = racesService;
            _competencesEtTalentsService = competencesEtTalentsService;
            _lieuxService = lieuxService;
            _profilsService = profilsService;
            _carrieresService = carrieresService;
            _traitsService = traitsService;
        }
        
        private Dictionary<int, BestioleDto>? _cacheBestiole;
        private List<BestioleDto>? _allBestioles;

        public List<BestioleDto> AllBestioles
        {
            get
            {
                if (_allBestioles == null)
                    Initialize();
                Debug.Assert(_allBestioles != null, nameof(_allBestioles) + " != null");
                return _allBestioles;

            }
        }
        
        public BestioleDto GetBestiole(int id)
        {
            if (_cacheBestiole == null)
                Initialize();
            Debug.Assert(_cacheBestiole != null, nameof(_cacheBestiole) + " != null");
            return _cacheBestiole[id];
        }

        private void Initialize()
        {
            var cachePersonnage = _dataPersonnages.ToDictionary(k => k.id);
            var cachePj = _dataPjs.ToDictionary(k => k.id);

            _cacheBestiole = _dataBestioles
                .Select(c => new BestioleDto
                {
                    Id = c.id,
                    EstUnPersonnage = cachePersonnage.ContainsKey(c.id),
                    EstUnPersonnageJoueur = cachePj.ContainsKey(c.id),
                    Userid = c.user,
                    MembreDe = c.membrede,
                    Age = c.age,
                    Commentaire = c.comment,
                    Histoire = c.histoire,
                    Nom = c.nom,
                    Poids = c.poids,
                    Psychologie = c.psycho,
                    Race = _racesService.GetRace(c.race),
                    Sexe = c.sexe,
                    Taille = c.taille,
                    ProfilActuel = _profilsService.GetProfil(c.profil_actuel),
                    CompetencesAcquises = CompetenceAcquise.GetList(
                        (c.competences ?? Array.Empty<int>()).Select(id => _competencesEtTalentsService.GetCompetence(id)).ToArray()
                    ),
                    Talents = (c.talents ?? Array.Empty<int>()).Select(id => _competencesEtTalentsService.GetTalent(id)).ToArray(),
                    Origines = (c.origines ?? Array.Empty<int>()).Select(id => _lieuxService.GetLieu(id)).ToArray(),
                    Traits = (c.traits ?? Array.Empty<int>()).Select(id => _traitsService.GetTrait(id)).ToArray(),
                    // Personnage
                    fk_signeastralid = cachePersonnage.ContainsKey(c.id) ? cachePersonnage[c.id].fk_signeastralid : null,
                    Cheveux = cachePersonnage.ContainsKey(c.id) ? cachePersonnage[c.id].cheveux : "",
                    Yeux = cachePersonnage.ContainsKey(c.id) ? cachePersonnage[c.id].yeux : "",
                    FreresEtSoeurs = cachePersonnage.ContainsKey(c.id) ? cachePersonnage[c.id].freres_et_soeurs : "",
                    MainDirectrice = cachePersonnage.ContainsKey(c.id) ? cachePersonnage[c.id].main_directrice : 0,
                    Mort = cachePersonnage.ContainsKey(c.id) && cachePersonnage[c.id].mort,
                    CarriereDuPere = cachePersonnage.ContainsKey(c.id) && cachePersonnage[c.id].fk_carrierepereid.HasValue ?
                        _carrieresService.GetCarriere(cachePersonnage[c.id].fk_carrierepereid!.Value) : null,
                    CarriereDeLaMere = cachePersonnage.ContainsKey(c.id) && cachePersonnage[c.id].fk_carrieremereid.HasValue ?
                        _carrieresService.GetCarriere(cachePersonnage[c.id].fk_carrieremereid!.Value) : null,
                    // PJ
                    Joueur = cachePj.ContainsKey(c.id) ? cachePj[c.id].nom_joueur : "",
                    CheminementPro = cachePersonnage.ContainsKey(c.id) ?
                        cachePersonnage[c.id].fk_cheminprofess != null ?
                            _carrieresService.GetCarrieres(cachePersonnage[c.id].fk_cheminprofess!).ToArray()
                            : Array.Empty<CarriereDto>()
                        : Array.Empty<CarriereDto>(),
                    
                    ProfilInitial = cachePj.ContainsKey(c.id) ? _profilsService.GetProfil(cachePj[c.id].fk_profilinitialid) : null,
                    XpActuel = cachePj.ContainsKey(c.id) ? cachePj[c.id].xp_actuel : 0,
                    XpTotal = cachePj.ContainsKey(c.id) ? cachePj[c.id].xp_total : 0,
                })
                .ToDictionary(k => k.Id);
            
            _allBestioles = _cacheBestiole.Values.ToList();

            //_dataBestioles.Clear();
            //_dataPersonnages.Clear();
            //_dataPjs.Clear();
        }
    }
}
