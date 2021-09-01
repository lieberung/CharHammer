using System.Diagnostics;

namespace BlazorWjdr.Services
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DataSource.JsonDto;

    public class CarrieresService
    {
        private readonly List<JsonCarriere> _dataCarrieres;
        private Dictionary<int, CarriereDto>? _cacheCarrieres;

        private readonly ProfilsService _profilsService;
        private readonly CompetencesEtTalentsService _competencesEtTalentsService;
        private readonly ReferencesService _referencesService;
        private readonly TraitsService _traitsService;

        public CarrieresService(
            List<JsonCarriere> dataCarrieres,
            ProfilsService profilsService,
            CompetencesEtTalentsService competencesEtTalentsService,
            ReferencesService referencesService,
            TraitsService traitsService)
        {
            _dataCarrieres = dataCarrieres;
            _profilsService = profilsService;
            _competencesEtTalentsService = competencesEtTalentsService;
            _referencesService = referencesService;
            _traitsService = traitsService;
        }

        public List<CarriereDto> AllCarrieres
        {
            get
            {
                Initialize();
                Debug.Assert(_cacheCarrieres != null, nameof(_cacheCarrieres) + " != null");
                return _cacheCarrieres.Values.OrderBy(t => t.Nom).ToList();
            }
        }

        public IEnumerable<CarriereDto> GetCarrieres(IEnumerable<int> ids) => ids.Select(GetCarriere).ToArray();

        public CarriereDto GetCarriere(int id)
        {
            Initialize();
            Debug.Assert(_cacheCarrieres != null, nameof(_cacheCarrieres) + " != null");
            return _cacheCarrieres[id];
        }

        public CarriereDto[] GetCarrieresProposant(CompetenceDto competence)
            => AllCarrieres
                .Where(c => c.CompetencesPourScore.Any(c => c.Id == competence.Id))
                .ToArray();

        public CarriereDto[] GetCarrieresProposant(TalentDto talent)
            => AllCarrieres
                .Where(c => c.TalentsPourScore.Any(t => t.Id == talent.Id))
                .ToArray();

        public CarriereDto[] GetCarrieresParuesDans(ReferenceDto reference)
            => AllCarrieres
                .Where(c => c.SourceLivre?.Id == reference.Id)
                .ToArray();

        private void Initialize()
        {
            if (_cacheCarrieres != null)
                return;
            _cacheCarrieres = _dataCarrieres
                .Select(c => new CarriereDto
                {
                    Id = c.id,
                    Groupe = c.groupe ?? "",
                    Nom = c.nom,
                    MotsClefDeRecherche = GenericService.MotsClefsDeRecherche(GenericService.ConvertirCaracteres(c.nom)),
                    Description = c.description,
                    Ambiance = c.ambiance ?? Array.Empty<string>(),
                    CarriereMereId = c.parent,
                    DebouchesIds = c.debouch ?? Array.Empty<int>(),
                    AvancementsIds = c.avancements ?? Array.Empty<int>(),
                    Dotations = c.dotations,
                    EstUneCarriereAvancee = c.avancee,
                    Image = $"images/careers/{c.id}.png",
                    PlanDeCarriere = _profilsService.GetProfil(c.plan),
                    Restriction = c.restriction ?? "",
                    Source = c.source_page ?? "",
                    SourceLivre = c.source_livre == null ? null : _referencesService.GetReference(c.source_livre.Value),
                    Competences = c.competences != null
                        ? _competencesEtTalentsService.GetCompetences(c.competences).ToList()
                        : new List<CompetenceDto>(),
                    Talents = c.talents != null
                        ? _competencesEtTalentsService.GetTalents(c.talents).ToList()
                        : new List<TalentDto>(),
                    ChoixCompetences = c.competenceschoix != null
                        ? c.competenceschoix.Select(choix => _competencesEtTalentsService.GetCompetences(choix).ToArray()).ToList()
                        : new List<CompetenceDto[]>(),
                    ChoixTalents = c.talentschoix != null
                        ? c.talentschoix.Select(choix => _competencesEtTalentsService.GetTalents(choix).ToArray()).ToList()
                        : new List<TalentDto[]>(),
                    Traits = c.traits != null
                        ? c.traits.Select(id => _traitsService.GetTrait(id)).ToList()
                        : new List<TraitDto>(),
                })
                .ToDictionary(k => k.Id, v => v);

            foreach (var carriere in _cacheCarrieres.Values.Where(c => c.CarriereMereId.HasValue))
            {
                Debug.Assert(carriere.CarriereMereId != null, "carriere.CarriereMereId != null");
                carriere.Parent = _cacheCarrieres[carriere.CarriereMereId.Value];
            }

            foreach (var carriere in _cacheCarrieres.Values.Where(c => c.DebouchesIds.Any()))
                carriere.Debouches = GetCarrieres(carriere.DebouchesIds).ToList();
            foreach (var carriere in _cacheCarrieres.Values.Where(c => c.AvancementsIds.Any()))
                carriere.Avancements = GetCarrieres(carriere.AvancementsIds).ToList();

            foreach (var carriere in _cacheCarrieres.Values)
            {
                carriere.Filieres = _cacheCarrieres.Values
                    .Where(c => c.Debouches.Contains(carriere))
                    .OrderBy(c => c.Nom)
                    .ToList();
                carriere.Origines = _cacheCarrieres.Values
                    .Where(c => c.Avancements.Contains(carriere))
                    .OrderBy(c => c.Nom)
                    .ToList();
                carriere.SousElements.AddRange(_cacheCarrieres.Values
                    .Where(c => c.Parent == carriere)
                    .OrderBy(c => c.Nom));
                /*
                if (carriere.AvancementId.HasValue)
                    carriere.Avancement = _cacheCarrieres[carriere.AvancementId.Value];
                */
                carriere.ScoreAcademique = CalculScoreAcademique(carriere);
                carriere.ScoreArcanique = CalculScoreArcanique(carriere);
                carriere.ScoreArtisanat = CalculScoreArtisanat(carriere);
                carriere.ScoreMartialAuContact = CalculScoreMartialAuContact(carriere);
                carriere.ScoreMartialADistance = CalculScoreMartialADistance(carriere);
                carriere.ScoreCavalerie = CalculScoreCavalerie(carriere);
                carriere.ScoreDeLOmbre = CalculScoreDeLOmbre(carriere);
                carriere.ScoreSocial = CalculScoreSocial(carriere);
                carriere.ScoreCommerce = CalculScoreCommerce(carriere);
                carriere.ScoreRodeur = CalculScoreRodeur(carriere);
                carriere.ScoreMaritime = CalculScoreMaritime(carriere);
                carriere.ScorePoudreNoire = CalculScorePoudreNoire(carriere);
                carriere.ScoreAmiDesBetes = CalculScoreAmiDesBetes(carriere);
            }

            //DirectoryInfo d = new DirectoryInfo("./wwwroot/images/careers/");
            //var images = d.GetFiles("*-*.png").Select(f => f.Name);
            //foreach (var carriere in _allCarrieres)
            //{
            //    var list = new List<string> { $"{carriere.Id}.png" };
            //    list.AddRange(images.Where(img => img.StartsWith($"{carriere.Id}-")));
            //    carriere.Images = list.ToArray();
            //}

            //_dataCarrieres.Clear();
        }

        #region Calcul Bonus de Caractéristique

        private int CalculBonusCapaciteDeTir(CarriereDto carriere)
        {
            var capaciteDeTir = carriere.PlanDeCarriere.Ct;
            if (carriere.TalentsPourScore.Any(t => t == _competencesEtTalentsService.TalentTireurDElite))
                capaciteDeTir += 5;
            if (carriere.EstUneCarriereAvancee)
                capaciteDeTir -= 10;
            var score = 0;
            if (capaciteDeTir > 0)
                score += capaciteDeTir / 5;

            return score;
        }

        private int CalculBonusIntelligence(CarriereDto carriere)
        {
            var intelligence = carriere.PlanDeCarriere.Int;
            if (carriere.TalentsPourScore.Any(t => t == _competencesEtTalentsService.TalentIntelligent))
                intelligence += 5;
            if (carriere.EstUneCarriereAvancee)
                intelligence -= 10;
            var score = 0;
            if (intelligence > 0)
                score += intelligence / 5;
            return score;
        }

        private int CalculBonusForceMentale(CarriereDto carriere)
        {
            var forceMentale = carriere.PlanDeCarriere.Fm;
            if (carriere.TalentsPourScore.Any(t => t == _competencesEtTalentsService.TalentSangFroid))
                forceMentale += 5;
            if (carriere.EstUneCarriereAvancee)
                forceMentale -= 10;
            var score = 0;
            if (forceMentale > 0)
                score += forceMentale / 5;
            return score;
        }

        private int CalculBonusAgilite(CarriereDto carriere)
        {
            var score = 0;
            var agilite = carriere.PlanDeCarriere.Ag;
            if (carriere.TalentsPourScore.Any(t => t == _competencesEtTalentsService.TalentReflexesEclairs))
                agilite += 5;
            if (carriere.EstUneCarriereAvancee)
                agilite -= 10;
            if (agilite > 0)
                score += agilite / 5;
            
            return score;
        }
        
        private int CalculBonusSociabilite(CarriereDto carriere)
        {
            var score = 0;
            var sociabilite = carriere.PlanDeCarriere.Soc;
            if (carriere.TalentsPourScore.Any(t => t == _competencesEtTalentsService.TalentSociable))
                sociabilite += 5;
            if (carriere.EstUneCarriereAvancee)
                sociabilite -= 10;
            if (sociabilite > 0)
                score += sociabilite / 5;
            
            return score;
        }
        
        #endregion
        #region Calcul Scores

        private int CalculScoreMartial(CarriereDto carriere)
        {
            var score = 0;

            score += carriere.CompetencesPourScore.Count(c =>
                c == _competencesEtTalentsService.CompetenceLangSecretBataille) * 2;

            if (carriere.TalentsPourScore.Contains(_competencesEtTalentsService.TalentMaitriseUneAuChoix))
                score += 3;
            if (carriere.TalentsPourScore.Contains(_competencesEtTalentsService.TalentMaitriseDeuxAuChoix))
                score += 6;
            
            score += carriere.TalentsPourScore.Count(c
                => c == _competencesEtTalentsService.TalentAmbidextrie
                   || c == _competencesEtTalentsService.TalentCoupsPrécis
                   || c == _competencesEtTalentsService.TalentDurACuir
                   || c == _competencesEtTalentsService.TalentSurSesGardes
                   || c == _competencesEtTalentsService.TalentValeureux
                   || c == _competencesEtTalentsService.TalentTroublant
                   || c == _competencesEtTalentsService.TalentForceAccrue
            ) * 2;

            score += carriere.PlanDeCarriere.A * 4;

            var force = carriere.PlanDeCarriere.F;
            if (carriere.TalentsPourScore.Any(t => t == _competencesEtTalentsService.TalentForceAccrue))
                force += 5;
            if (carriere.EstUneCarriereAvancee)
                force -= 10;
            if (force > 0)
                score += (force / 5) * 3;

            return score;
        }

        private int CalculScoreMartialAuContact(CarriereDto carriere)
        {
            var score = CalculScoreMartial(carriere);

            score += carriere.CompetencesPourScore.Count(c =>
                c == _competencesEtTalentsService.CompetenceEsquive ||
                c.Parent == _competencesEtTalentsService.CompetenceGroupeMelee
            ) * 2;

            score += carriere.TalentsPourScore.Count(c =>
                c == _competencesEtTalentsService.TalentCombatADeuxArmes
                || c == _competencesEtTalentsService.TalentDesarmement
                || c == _competencesEtTalentsService.TalentFrenesie
                || c == _competencesEtTalentsService.TalentCombattantVirevoltant
                || c == _competencesEtTalentsService.TalentCoupsAssomants
                || c == _competencesEtTalentsService.TalentCoupsPuissants
                || c == _competencesEtTalentsService.TalentCoupsAuBut
                || c == _competencesEtTalentsService.TalentCombatDeRue
                || c == _competencesEtTalentsService.TalentDurACuir
                || c == _competencesEtTalentsService.TalentGuerrierNe
                || c == _competencesEtTalentsService.TalentParadeEclair
                || c == _competencesEtTalentsService.TalentLutte
                || c == _competencesEtTalentsService.TalentResistanceAccrue
                || c == _competencesEtTalentsService.TalentRobuste
                || c == _competencesEtTalentsService.TalentDechainement
                || c == _competencesEtTalentsService.TalentPresenceImposante
                || c == _competencesEtTalentsService.TalentTueur
                || c.Parent == _competencesEtTalentsService.TalentGroupeVertu
            ) * 2;

            score += (carriere.PlanDeCarriere.Cc / 10) * (carriere.EstUneCarriereAvancee ? 1 : 2);
            score += (carriere.PlanDeCarriere.E / 10) * (carriere.EstUneCarriereAvancee ? 1 : 2);

            var blessures = carriere.PlanDeCarriere.B - (carriere.EstUneCarriereAvancee ? 4 : 2);
            if (blessures > 0)
                score += blessures;

            return score;
        }

        private int CalculScoreMartialADistance(CarriereDto carriere)
        {
            var score = CalculScoreMartial(carriere);

            score += carriere.CompetencesPourScore.Count(c =>
                c == _competencesEtTalentsService.CompetenceMetierArquebusier ||
                c.Parent == _competencesEtTalentsService.CompetenceGroupeTir
            ) * 2;

            score += carriere.TalentsPourScore.Count(c
                => c == _competencesEtTalentsService.TalentRechergementRapide
                   || c == _competencesEtTalentsService.TalentAdresseAuTir
                   || c == _competencesEtTalentsService.TalentTirDePrecision
                   || c == _competencesEtTalentsService.TalentTirEnPuissance
                   || c == _competencesEtTalentsService.TalentMaitreArtilleur
            ) * 2;

            score += CalculBonusCapaciteDeTir(carriere) * 3;

            return score;
        }

        private int CalculScoreCavalerie(CarriereDto carriere)
        {
            if (!carriere.CompetencesPourScore.Contains(_competencesEtTalentsService.CompetenceEquitation))
                return 0;
            
            var score = 0;

            score += carriere.CompetencesPourScore.Count(c
                => c == _competencesEtTalentsService.CompetenceExpressionArtistiqueAcrobatEquestre
            ) * 4;

            score += carriere.CompetencesPourScore.Count(c
                => c == _competencesEtTalentsService.CompetenceEmpriseSurLesAnimaux
                || c == _competencesEtTalentsService.CompetenceSoinsDesAnimaux
                || c == _competencesEtTalentsService.CompetenceMetierGarconDEcurie
                || c == _competencesEtTalentsService.CompetenceMetierVendeurDeChevaux
                || c == _competencesEtTalentsService.CompetenceDressage
                || c == _competencesEtTalentsService.CompetenceMeleeArmesDeCavalerie
            ) * 2;
            
            score += carriere.TalentsPourScore.Count(c
                => c == _competencesEtTalentsService.TalentMaitriseUneAuChoix
                || c == _competencesEtTalentsService.TalentMaitriseDeuxAuChoix
                || c == _competencesEtTalentsService.TalentAcrobateEquestre
            ) * 4;

            if (score == 0)
                return 0;

            score += CalculBonusAgilite(carriere);

            return score;
        }
        
        private int CalculScoreArcanique(CarriereDto carriere)
        {
            var score = carriere.PlanDeCarriere.Mag * 10;

            score += carriere.CompetencesPourScore.Count(c
                => c == _competencesEtTalentsService.CompetenceConnaissanceAcademiqueMagie
                   || c == _competencesEtTalentsService.CompetenceConnaissanceAcademiqueEsprits
                   || c == _competencesEtTalentsService.CompetenceConnaissanceAcademiqueNecromancie
                   || c == _competencesEtTalentsService.CompetenceLangueClassique
            ) * 1;
            
            score += carriere.CompetencesPourScore.Count(c
                => c == _competencesEtTalentsService.CompetenceFocalisation
                   || c == _competencesEtTalentsService.CompetenceLangageMystique
                   || c == _competencesEtTalentsService.CompetenceLangageMystiqueMagick
                   || c == _competencesEtTalentsService.CompetenceLangageMystiqueDemoniaque
                   || c == _competencesEtTalentsService.CompetenceLangageMystiqueElfeMystique
                   || c == _competencesEtTalentsService.CompetenceSensDeLaMagie
            ) * 2;
            
            score += carriere.TalentsPourScore.Count(c
                => c == _competencesEtTalentsService.TalentHarmonieAethyrique
                   || c == _competencesEtTalentsService.TalentMainsAgiles
                   || c == _competencesEtTalentsService.TalentMeditation
                   || c == _competencesEtTalentsService.TalentProjectilePuissant
                   || c == _competencesEtTalentsService.TalentMagieVulgaire
                   || c == _competencesEtTalentsService.TalentMagieNoire
                   || c.Parent != null && (
                       c.Parent == _competencesEtTalentsService.TalentGroupeMagieCommune
                       || c.Parent == _competencesEtTalentsService.TalentGroupeMagieMineure)
            ) * 2;
            
            score += CalculBonusIntelligence(carriere);
            score += CalculBonusForceMentale(carriere);

            return score;
        }

        private int CalculScoreArtisanat(CarriereDto carriere)
        {
            var score = 0;

            score += carriere.CompetencesPourScore.Count(c
                => c == _competencesEtTalentsService.CompetenceEvaluation
                   || c == _competencesEtTalentsService.CompetenceLangageSecretGuilde
                   || c.Parent == _competencesEtTalentsService.CompetenceGroupeMetier
                   || c == _competencesEtTalentsService.CompetenceConnaissancesAcademiquesArts
                   || c == _competencesEtTalentsService.CompetenceConnaissancesAcademiquesIngénierie
                   || c == _competencesEtTalentsService.CompetenceConnaissancesAcademiquesRunes
                   || c == _competencesEtTalentsService.CompetenceConnaissancesAcademiquesSciences
                   || c == _competencesEtTalentsService.CompetencePreparationDePoisons
                   || c == _competencesEtTalentsService.CompetenceCreationDeRunes
                   || c == _competencesEtTalentsService.CompetenceLangageMystiqueNain
            ) * 2;

            if (carriere.CompetencesPourScore.Contains(_competencesEtTalentsService.CompetenceMetierDeuxAuChoix))
                score += 2;
            if (carriere.CompetencesPourScore.Contains(_competencesEtTalentsService.CompetenceMetierTroisAuChoix))
                score += 4;
            
            score += carriere.TalentsPourScore.Count(c
                => c == _competencesEtTalentsService.TalentSavoirFaireNain
                   || c == _competencesEtTalentsService.TalentTalentArtistique
            ) * 2;

            if (carriere.TalentsPourScore.Contains(_competencesEtTalentsService.TalentRuneDeuxAuChoix))
                score += 4;
            if (carriere.TalentsPourScore.Contains(_competencesEtTalentsService.TalentRuneSixAuChoix))
                score += 5;
            if (carriere.TalentsPourScore.Contains(_competencesEtTalentsService.TalentRuneDixAuChoix))
                score += 6;
            if (carriere.TalentsPourScore.Contains(_competencesEtTalentsService.TalentRuneMajeureDeuxAuChoix))
                score += 7;
            
            score += CalculBonusIntelligence(carriere);
            score += CalculBonusAgilite(carriere) / 2;

            return score;
        }

        private int CalculScoreAcademique(CarriereDto carriere)
        {
            var score = 0;
            score += carriere.CompetencesPourScore.Count(c
                => c.Parent == _competencesEtTalentsService.CompetenceGroupeConnaissancesAcademiques
                || c.Parent == _competencesEtTalentsService.CompetenceGroupeLangue
                || c == _competencesEtTalentsService.CompetenceLireEcrire
            );
            if (carriere.CompetencesPourScore.Any(c => c == _competencesEtTalentsService.CompetenceConnaissancesAcademiquesDeuxAuChoix))
                score += 1;
            if (carriere.CompetencesPourScore.Any(c => c == _competencesEtTalentsService.CompetenceConnaissancesAcademiquesTroisAuChoix))
                score += 2;

            if (score < 2)
                return 0;
            
            score += (carriere.CompetencesPourScore.Count(c => c.Parent == _competencesEtTalentsService.CompetenceGroupeConnaissancesGenerales) + 1) / 2;

            score += carriere.TalentsPourScore.Count(c
                => c == _competencesEtTalentsService.TalentCalculMental
                || c == _competencesEtTalentsService.TalentLinguistique
            );

            score += CalculBonusIntelligence(carriere) * 2;

            return score;
        }

        private int CalculScoreCommerce(CarriereDto carriere)
        {
            var score = 0;
            
            score += carriere.CompetencesPourScore.Count(c
                => c == _competencesEtTalentsService.CompetenceMarchandage
                || c == _competencesEtTalentsService.CompetenceMetierMarchand
                || c == _competencesEtTalentsService.CompetenceMetierVendeurDeChevaux
            ) * 2;
            if (score == 0)
                return 0;

            score += carriere.CompetencesPourScore.Count(c
                => c == _competencesEtTalentsService.CompetenceEvaluation
                   || c == _competencesEtTalentsService.CompetenceBaratin
                   || c == _competencesEtTalentsService.CompetenceLireEcrire
                   || c == _competencesEtTalentsService.CompetenceExpressionArtistiqueConteur
            );
            
            score += carriere.TalentsPourScore.Count(c
                => c == _competencesEtTalentsService.TalentCalculMental
                   || c == _competencesEtTalentsService.TalentDurEnAffaires
            );

            score += CalculBonusIntelligence(carriere);
            score += CalculBonusSociabilite(carriere);
            
            return score;
        }

        private int CalculScoreDeLOmbre(CarriereDto carriere)
        {
            var score = 0;
            score += carriere.CompetencesPourScore.Count(c
                => c == _competencesEtTalentsService.CompetenceAlphSecretVoleurs
                   || c == _competencesEtTalentsService.CompetenceDeplacementSilencieux
                   || c == _competencesEtTalentsService.CompetenceDissimulation
                   || c == _competencesEtTalentsService.CompetenceFouille
                   || c == _competencesEtTalentsService.CompetencePerception
                   || c == _competencesEtTalentsService.CompetenceEscalade
                   || c == _competencesEtTalentsService.CompetenceCrochetage
                   || c == _competencesEtTalentsService.CompetenceDeguisement
                   || c == _competencesEtTalentsService.CompetenceEscamotage
                   || c == _competencesEtTalentsService.CompetencePreparationDePoisons
            );
            if (score < 2)
                return 0;

            score += carriere.TalentsPourScore.Count(c
                => c == _competencesEtTalentsService.TalentConnaissanceDesPieges
                   || c == _competencesEtTalentsService.TalentCamouflageRural
                   || c == _competencesEtTalentsService.TalentCamouflageSouterrain
                   || c == _competencesEtTalentsService.TalentCamouflageUrbain
                   || c == _competencesEtTalentsService.TalentCodeDeLaRue
                   || c == _competencesEtTalentsService.TalentImitation
                   || c == _competencesEtTalentsService.TalentSensAiguises
                   || c == _competencesEtTalentsService.TalentAccuiteAuditive
                   || c == _competencesEtTalentsService.TalentAccuiteVisuelle
                   || c == _competencesEtTalentsService.TalentFilature
                   || c == _competencesEtTalentsService.TalentLectureSurLesLevres
                   || c == _competencesEtTalentsService.TalentPistage
            );
            if (score < 4)
                return 0;
            
            score += CalculBonusAgilite(carriere);

            return score;
        }

        private int CalculScoreRodeur(CarriereDto carriere)
        {
            var score = 0;

            score += carriere.CompetencesPourScore.Count(c
                => c == _competencesEtTalentsService.CompetenceAlphabetSecretPisteurs
                || c == _competencesEtTalentsService.CompetenceLangageSecretRodeurs
                || c == _competencesEtTalentsService.CompetenceLangageSecretTroisAuChoix
                || c == _competencesEtTalentsService.CompetenceSurvie
                || c == _competencesEtTalentsService.CompetencePistage
            ) * 4;

            score += carriere.CompetencesPourScore.Count(c
                => c == _competencesEtTalentsService.CompetenceBraconnage
                   || c == _competencesEtTalentsService.CompetenceDeplacementSilencieux
                   || c == _competencesEtTalentsService.CompetenceDissimulation
                   || c == _competencesEtTalentsService.CompetenceEmpriseSurLesAnimaux
                   || c == _competencesEtTalentsService.CompetenceNatation
                   || c == _competencesEtTalentsService.CompetenceOrientation
                   || c == _competencesEtTalentsService.CompetencePerception
                   || c == _competencesEtTalentsService.CompetenceEscalade
            ) * 2;

            score += carriere.CompetencesPourScore.Count(c
                => c.Parent == _competencesEtTalentsService.CompetenceGroupeConnaissancesGenerales
                || c.Parent == _competencesEtTalentsService.CompetenceGroupeLangue
                || c == _competencesEtTalentsService.CompetenceFouille
                || c == _competencesEtTalentsService.CompetenceMetierCartographe
                || c == _competencesEtTalentsService.CompetenceSoinsDesAnimaux
                || c == _competencesEtTalentsService.CompetenceTirArcsLongs
                || c == _competencesEtTalentsService.CompetenceMeleeArmesParalisantes
            );

            score += carriere.TalentsPourScore.Count(c
               => c == _competencesEtTalentsService.TalentSensAiguises
               || c == _competencesEtTalentsService.TalentAccuiteAuditive
               || c == _competencesEtTalentsService.TalentAccuiteVisuelle
               || c == _competencesEtTalentsService.TalentSensDeLOrientation
               || c == _competencesEtTalentsService.TalentCamouflageRural
               || c == _competencesEtTalentsService.TalentGrandVoyageur
               || c == _competencesEtTalentsService.TalentLinguistique
               || c == _competencesEtTalentsService.TalentConnaissanceDesPieges
               || c == _competencesEtTalentsService.TalentSixiemeSens
               || c == _competencesEtTalentsService.TalentTireurDElite
            ) * 2;

            if (carriere.TalentsPourScore.Contains(_competencesEtTalentsService.TalentCourseAPied))
                score += 4;
            
            score += CalculBonusIntelligence(carriere) / 2;
            score += CalculBonusAgilite(carriere) / 2;

            return score;
        }

        private int CalculScoreSocial(CarriereDto carriere)
        {
            var score = 0;

            score += carriere.CompetencesPourScore.Count(c
                => c == _competencesEtTalentsService.CompetenceBaratin
                   || c == _competencesEtTalentsService.CompetenceCharisme
                   || c == _competencesEtTalentsService.CompetenceCommandement
                   || c == _competencesEtTalentsService.CompetenceCommérage
                   || c == _competencesEtTalentsService.CompetenceIntimidation
            );

            score += carriere.TalentsPourScore.Count(c
                => c == _competencesEtTalentsService.TalentEloquence
                   || c == _competencesEtTalentsService.TalentOrateurNe
                   || c == _competencesEtTalentsService.TalentPolitique
                   || c == _competencesEtTalentsService.TalentCodeDeLaRue
                   || c == _competencesEtTalentsService.TalentEtiquette
                   || c == _competencesEtTalentsService.TalentIntriguant
            );

            if (score < 3)
                return 0;

            score += CalculBonusIntelligence(carriere) / 2;
            score += CalculBonusSociabilite(carriere) * 2;

            return score;
        }

        private int CalculScoreMaritime(CarriereDto carriere)
        {
            if (!carriere.CompetencesPourScore.Any(c
                => c == _competencesEtTalentsService.CompetenceCanotage
                   || c == _competencesEtTalentsService.CompetenceNavigation))
                return 0;
            
            var score = 0;
            score += carriere.CompetencesPourScore.Count(c
                => c == _competencesEtTalentsService.CompetenceCanotage
                || c == _competencesEtTalentsService.CompetenceNatation
                || c == _competencesEtTalentsService.CompetenceNavigation
                || c == _competencesEtTalentsService.CompetenceEruditionAstronomie
                || c == _competencesEtTalentsService.CompetenceEruditionPotamologie
                || c == _competencesEtTalentsService.CompetenceOrientation
                || c == _competencesEtTalentsService.CompetenceMetierCartographe
                || c == _competencesEtTalentsService.CompetenceMetierCharpentierNaval
            ) * 2;

            score += carriere.TalentsPourScore.Count(c
                => c == _competencesEtTalentsService.TalentGrandVoyageur
                || c == _competencesEtTalentsService.TalentSensDeLOrientation
            ) * 2;

            return score;
        }

        private int CalculScorePoudreNoire(CarriereDto carriere)
        {
            if (!carriere.CompetencesPourScore.Any(c
               => c == _competencesEtTalentsService.CompetenceTirArmesAFeu
               || c == _competencesEtTalentsService.CompetenceGroupeExplosifs
               || c == _competencesEtTalentsService.CompetenceTirArmesMecaniques
            ))
                return 0;
            
            if (!carriere.TalentsPourScore.Contains(_competencesEtTalentsService.TalentMaitreArtilleur))
                return 0;
            
            var score = 0;
            score += carriere.CompetencesPourScore.Count(c
                => c == _competencesEtTalentsService.CompetenceMetierArquebusier
                || c == _competencesEtTalentsService.CompetenceTirArmesAFeu
                || c == _competencesEtTalentsService.CompetenceTirArmesMecaniques
                || c == _competencesEtTalentsService.CompetenceGroupeExplosifs
            );

            score += carriere.TalentsPourScore.Count(c
                => c == _competencesEtTalentsService.TalentMaitreArtilleur
                || c == _competencesEtTalentsService.TalentAdresseAuTir
                || c == _competencesEtTalentsService.TalentRechergementRapide
                || c == _competencesEtTalentsService.TalentSurSesGardes
                || c == _competencesEtTalentsService.TalentTirDePrecision
                || c == _competencesEtTalentsService.TalentTirEnPuissance
            );

            score += CalculBonusCapaciteDeTir(carriere);
            
            return score;
        }

        private int CalculScoreAmiDesBetes(CarriereDto carriere)
        {
            var score = 0;
            score += carriere.CompetencesPourScore.Count(c
                => c == _competencesEtTalentsService.CompetenceDressage
                || c == _competencesEtTalentsService.CompetenceSoinsDesAnimaux
                || c == _competencesEtTalentsService.CompetenceEmpriseSurLesAnimaux
            ) * 2;
            
            score += carriere.CompetencesPourScore.Count(c
                => c == _competencesEtTalentsService.CompetenceEquitation
                || c == _competencesEtTalentsService.CompetenceEquitationCochonDeGuerre
                || c == _competencesEtTalentsService.CompetenceMetierVendeurDeChevaux
                || c == _competencesEtTalentsService.CompetenceMetierMaitreChien
                || c == _competencesEtTalentsService.CompetenceMetierFauconnerie
                || c == _competencesEtTalentsService.CompetenceConnaissancesAcademiquesZoologie
                || c == _competencesEtTalentsService.CompetenceMetierGarconDEcurie
                || c == _competencesEtTalentsService.CompetenceMetierFermier
                || c == _competencesEtTalentsService.CompetenceConduiteDAttelage
            ) * 1;

            return score;
        }

        #endregion

        public CarriereDto[] Recherche(string searchText)
        {
            searchText = GenericService.ConvertirCaracteres(searchText);
            var motsClefRecherches = GenericService.MotsClefsDeRecherche(searchText);

            return AllCarrieres
                .Where(c => GenericService.ConvertirCaracteres(c.Nom).Contains(searchText)
                            || c.MotsClefDeRecherche.Intersect(motsClefRecherches).Any())
                .OrderByDescending(c => c.MotsClefDeRecherche.Intersect(motsClefRecherches).Count())
                .ToArray();
        }

        public IEnumerable<CarriereDto> CarrieresDeBretonnie => AllCarrieres
            .Where(c => c.SourceLivre?.Id == _referencesService.LivreLesChevaliersDuGraal.Id
                        || c.SourceLivre?.Id == _referencesService.LivreLeDucheDesDamnes.Id)
            .ToList();

        public IEnumerable<CarriereDto> CarrieresDuKislev => AllCarrieres
            .Where(c => c.Id == 53 || c.SourceLivre?.Id == _referencesService.LivreLaReineDesGlaces.Id)
            .ToList();

        public List<int> CarrieresSkaven => AllCarrieres
            .Where(c => c.SourceLivre?.Id == _referencesService.LivreLesFilsDuRatCornu.Id)
            .Select(c => c.Id)
            .ToList();

        // Tueur de morts, Fouet de dieu, Flagellant, Fanatique, Pénitent, Prêcheur de rue, Tueur de démons/géants/trolls, Exécuteur, Mystique, Cénobite
        public IEnumerable<CarriereDto> CarrieresFanatiques => GetCarrieres(new[] {209, 213, 170, 45, 212, 88, 198, 199, 77, 87, 268, 267, 194, 1, 314 });
        public IEnumerable<CarriereDto> CarrieresDeNorsca => GetCarrieres(new [] { 26, 89, 302, 303, 90, 91, 92, 93, 94, 95, 304 });
        public List<int> CarrieresChaos => new() { 305, 307, 309, 311, 306, 308, 310, 312, 293, 294 };
        public IEnumerable<CarriereDto> CarrieresCriminelles => GetCarrieres(new [] { 21, 141, 149, 151, 152, 158, 37, 38, 165, 43, 168, 280, 111, 51, 265, 183, 127, 145, 295, 66, 188, 191, 192, 193, 258, 76, 80 });
        public IEnumerable<CarriereDto> CarrieresBureaucratie => GetCarrieres(new [] { 315, 201, 251, 35, 86, 87, 97, 125, 46, 129, 49, 175, 281, 177, 58, 59, 63, 128, 146, 147, 69, 70 });
        public IEnumerable<CarriereDto> CarrieresMilitaires => GetCarrieres(new [] { 296, 316, 297, 154, 155, 319, 156, 161, 252, 162, 253, 254, 286, 36, 274, 40, 41, 301, 171, 46, 129, 48, 203, 112, 256, 53, 54, 185, 187, 197, 73, 259 });


        private CarriereDto CarriereInitie => GetCarriere(52);
        private CarriereDto CarrierePretre => GetCarriere(189);
        private CarriereDto CarrierePretreConsacre => GetCarriere(190);
        private CarriereDto CarriereGrandPretre => GetCarriere(172);
        
        private CarriereDto CarriereExorciste => GetCarriere(282);
        private CarriereDto CarriereFanatique => GetCarriere(45);
        private CarriereDto CarriereFlagellant => GetCarriere(170);
        private CarriereDto CarriereAnachorete => GetCarriere(266);
        private CarriereDto CarriereMystique => GetCarriere(268);
        private CarriereDto CarriereLayPriest => GetCarriere(143);
        private CarriereDto CarrierePrelat => GetCarriere(144);
        
        private CarriereDto CarriereChevalierDeLEmpire => GetCarriere(161);
        
        public IEnumerable<CarriereDto> CarrieresDevotes {
            get
            {
                var list = new List<CarriereDto>
                {
                    CarriereInitie, CarrierePretre, CarriereGrandPretre, CarrierePretreConsacre, CarriereChevalierDeLEmpire
                };
                list.AddRange(AllCarrieres.Where(c 
                    => (c.SourceLivre == _referencesService.LivreLeTomeDeLaRedemption) 
                    || (c.Parent != null && list.Contains(c.Parent))));
                list.AddRange(new []
                {
                    CarriereFanatique, CarriereFlagellant, CarriereAnachorete, CarriereMystique, CarriereExorciste, CarriereLayPriest, CarrierePrelat
                });
                
                return list;
            }
        }

        public IEnumerable<string> GallerieComplete()
        {
            Random rnd = new ();
            return AllCarrieres.SelectMany(c => c.Images).OrderBy(x => rnd.Next()).ToArray();
        }
    }
}
