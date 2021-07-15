using System;
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
        private readonly RacesService _racesService = null!;
        private readonly CompetencesEtTalentsService _competencesEtTalentsService = null!;
        private readonly LieuxService _lieuxService = null!;
        private readonly ProfilsService _profilsService = null!;
        private readonly CarrieresService _carrieresService = null!;

        public BestiolesService(
            RacesService racesService,
            CompetencesEtTalentsService competencesEtTalentsService,
            LieuxService lieuxService,
            ProfilsService profilsService,
            CarrieresService carrieresService)
        {
            _racesService = racesService;
            _competencesEtTalentsService = competencesEtTalentsService;
            _lieuxService = lieuxService;
            _profilsService = profilsService;
            _carrieresService = carrieresService;
        }
        
        private Dictionary<int, BestioleDto>? _cacheBestiole;
        private List<BestioleDto>? _allBestioles;

        private List<BestioleDto> AllBestioles
        {
            get
            {
                if (_allBestioles == null)
                    Initialize();
#pragma warning disable CS8603 // Possible null Bestiole return.
                return _allBestioles;
#pragma warning restore CS8603 // Possible null Bestiole return.
            }
        }
        
        public Task<BestioleDto[]> Items()
        {
            return Task.FromResult(AllBestioles.ToArray());
        }

        public BestioleDto GetBestiole(int id)
        {
            if (_cacheBestiole == null)
                Initialize();
#pragma warning disable CS8602 // DeBestiole of a possibly null Bestiole.
            return _cacheBestiole[id];
#pragma warning restore CS8602 // DeBestiole of a possibly null Bestiole.
        }

        private void Initialize()
        {
            var cachePersonnage = DataSource.JsonLoader
                .GetRootPersonnage()
                .items
                .ToDictionary(k => k.id);
            var cachePj = DataSource.JsonLoader
                .GetRootPj()
                .items
                .ToDictionary(k => k.id);

            _cacheBestiole = DataSource.JsonLoader
                .GetRootBestiole()
                .items
                .Select(c => new BestioleDto
                {
                    Id = c.id,
                    EstUnPersonnage = cachePersonnage.ContainsKey(c.id),
                    EstUnPersonnageJoueur = cachePj.ContainsKey(c.id),
                    Userid = c.fk_userid,
                    MembreDe = c.membrede,
                    Age = c.age,
                    Commentaire = c.comment,
                    Histoire = c.histoire,
                    Nom = c.nom,
                    Poids = c.poids,
                    Psychologie = c.psycho,
                    Race = _racesService.GetRace(c.fk_raceid),
                    Sexe = c.sexe,
                    Taille = c.taille,
                    ProfilActuel = _profilsService.GetProfil(c.fk_profilactuelid),
                    Competences = c.fk_competences  != null ?
                        _competencesEtTalentsService.GetCompetences(c.fk_competences).ToArray()
                        : Array.Empty<CompetenceDto>(),
                    Talents = c.fk_talents != null ?
                        _competencesEtTalentsService.GetTalents(c.fk_talents).ToArray()
                        : Array.Empty<TalentDto>(),
                    Origines = c.fk_origines != null ?
                        _lieuxService.GetLieux(c.fk_origines).ToArray()
                        : Array.Empty<LieuDto>(),
                    // Personnage
                    fk_signeastralid = cachePersonnage.ContainsKey(c.id) ? cachePersonnage[c.id].fk_signeastralid : null,
                    Cheveux = cachePersonnage.ContainsKey(c.id) ? cachePersonnage[c.id].cheveux : "",
                    Yeux = cachePersonnage.ContainsKey(c.id) ? cachePersonnage[c.id].yeux : "",
                    FreresEtSoeurs = cachePersonnage.ContainsKey(c.id) ? cachePersonnage[c.id].freres_et_soeurs : "",
                    MainDirectrice = cachePersonnage.ContainsKey(c.id) ? cachePersonnage[c.id].main_directrice : 0,
                    Mort = cachePersonnage.ContainsKey(c.id) && cachePersonnage[c.id].mort,
                    CarriereDuPere = cachePersonnage.ContainsKey(c.id) && cachePersonnage[c.id].fk_carrierepereid.HasValue ?
                        _carrieresService.GetCarriere(cachePersonnage[c.id].fk_carrierepereid.Value) : null,
                    CarriereDeLaMere = cachePersonnage.ContainsKey(c.id) && cachePersonnage[c.id].fk_carrieremereid.HasValue ?
                        _carrieresService.GetCarriere(cachePersonnage[c.id].fk_carrieremereid.Value) : null,
                    // PJ
                    Joueur = cachePj.ContainsKey(c.id) ? cachePj[c.id].nom_joueur : "",
                    CheminementPro = cachePersonnage.ContainsKey(c.id) ?
                        cachePersonnage[c.id].fk_cheminprofess != null ?
                            _carrieresService.GetCarrieres(cachePersonnage[c.id].fk_cheminprofess).ToArray()
                            : Array.Empty<CarriereDto>()
                        : Array.Empty<CarriereDto>(),
                    
                    ProfilInitial = cachePj.ContainsKey(c.id) ? _profilsService.GetProfil(cachePj[c.id].fk_profilinitialid) : null,
                    XpActuel = cachePj.ContainsKey(c.id) ? cachePj[c.id].xp_actuel : 0,
                    XpTotal = cachePj.ContainsKey(c.id) ? cachePj[c.id].xp_total : 0,
                })
                .ToDictionary(k => k.Id);
            
            _allBestioles = _cacheBestiole.Values.ToList();
        }
    }
}
