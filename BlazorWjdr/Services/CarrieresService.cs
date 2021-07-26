namespace BlazorWjdr.Services
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CarrieresService
    {
        private List<CarriereDto>? _allCarrieres;
        private Dictionary<int, CarriereDto>? _cacheCarrieres;

        private readonly ProfilsService _profilsService;
        private readonly CompetencesEtTalentsService _competencesEtTalentsService;
        private readonly ChoixCompetencesEtTalentsService _choixCompetencesEtTalentsService;
        private readonly ReferencesService _referencesService;

        public CarrieresService(
            ProfilsService profilsService,
            CompetencesEtTalentsService competencesEtTalentsService,
            ChoixCompetencesEtTalentsService choixCompetencesEtTalentsService,
            ReferencesService referencesService)
        {
            _profilsService = profilsService;
            _competencesEtTalentsService = competencesEtTalentsService;
            _choixCompetencesEtTalentsService = choixCompetencesEtTalentsService;
            _referencesService = referencesService;
        }

        private List<CarriereDto> AllCarrieres
        {
            get
            {
                if (_allCarrieres == null)
                    Initialize();
#pragma warning disable CS8603 // Possible null reference return.
                return _allCarrieres;
#pragma warning restore CS8603 // Possible null reference return.
            }
        }

        public IEnumerable<CarriereDto> GetCarrieres(IEnumerable<int> ids) => ids.Select(GetCarriere).ToArray();

        public CarriereDto GetCarriere(int id)
        {
            if (_cacheCarrieres == null)
                Initialize();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            return _cacheCarrieres[id];
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        public Task<CarriereDto[]> GetCarrieresProposant(CompetenceDto competence)
            => Task.FromResult(AllCarrieres
                .Where(c => c.CompetencesPourScore.Contains(competence)) // ToDo: choix
                .ToArray());

        public Task<CarriereDto[]> GetCarrieresProposant(TalentDto talent)
            => Task.FromResult(AllCarrieres
                .Where(c => c.TalentsPourScore.Contains(talent)) // ToDo: choix
                .ToArray());

        public Task<CarriereDto[]> GetCarrieresParuesDans(ReferenceDto reference)
            => Task.FromResult(AllCarrieres
                .Where(c => c.SourceLivre == reference)
                .ToArray());

        public Task<CarriereDto[]> ItemsCarrieres()
        {
            return Task.FromResult(AllCarrieres.ToArray());
        }

        private void Initialize()
        {
            _allCarrieres = DataSource.JsonLoader
                .GetRootCarriere()
                .items
                .Select(c => new CarriereDto
                {
                    Id = c.id,
                    Nom = c.libelle,
                    MotsClefDeRecherche = ConvertirCaracteres(c.libelle).ToLower().Split(' ').ToList(),
                    Description = c.description,
                    CarriereMereId = c.fk_parentcarriereid,
                    DebouchesIds = c.fk_debouches ?? Array.Empty<int>(),
                    Dotations = c.dotations,
                    EstUneCarriereAvancee = c.avancee,
                    Image = $"/images/careers/{c.id}.png",
                    PlanDeCarriere = _profilsService.GetProfil(c.fk_plandecarriereid),
                    Restriction = c.restriction ?? "",
                    Source = c.source ?? "",
                    SourceLivre = c.fk_sourceid == null ? null : _referencesService.GetReference(c.fk_sourceid.Value),
#pragma warning disable CS8604 // Possible null reference argument.
                    Competences = c.fk_competences != null
                        ? _competencesEtTalentsService.GetCompetences(c.fk_competences).ToList()
                        : new List<CompetenceDto>(),
                    Talents = c.fk_talents != null
                        ? _competencesEtTalentsService.GetTalents(c.fk_talents).ToList()
                        : new List<TalentDto>(),
                    ChoixCompetences = c.fk_choixcompetences != null
                        ? _choixCompetencesEtTalentsService.GetChoixCompetences(c.fk_choixcompetences).ToList()
                        : new List<CompetenceDto[]>(),
                    ChoixTalents = c.fk_choixtalents != null
                        ? _choixCompetencesEtTalentsService.GetChoixTalents(c.fk_choixtalents).ToList()
                        : new List<TalentDto[]>()
#pragma warning restore CS8604 // Possible null reference argument.
                })
                .OrderBy(t => t.Nom)
                .ToList();

            _cacheCarrieres = _allCarrieres.ToDictionary(k => k.Id, v => v);

            foreach (var carriere in _allCarrieres.Where(c => c.CarriereMereId.HasValue))
#pragma warning disable CS8629 // Nullable value type may be null.
                carriere.Parent = _cacheCarrieres[carriere.CarriereMereId.Value];
#pragma warning restore CS8629 // Nullable value type may be null.

            foreach (var carriere in _allCarrieres.Where(c => c.DebouchesIds.Any()))
                carriere.Debouches = GetCarrieres(carriere.DebouchesIds).ToList();

            foreach (var carriere in _allCarrieres)
            {
                carriere.Filieres = _allCarrieres
                    .Where(c => c.Debouches.Contains(carriere))
                    .OrderBy(c => c.Nom)
                    .ToList();
                carriere.SousElements.AddRange(_allCarrieres
                    .Where(c => c.Parent == carriere)
                    .OrderBy(c => c.Nom));

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
                   || c == _competencesEtTalentsService.TalentEffrayant
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

            score += carriere.CompetencesPourScore.Count(c => c == _competencesEtTalentsService.CompetenceEsquive) * 2;

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
                || c.Parent == _competencesEtTalentsService.TalentGroupeVertu
                || (c.Parent == _competencesEtTalentsService.TalentGroupeMaitrise &&
                    _competencesEtTalentsService.TalentsMaitriseAuContact.Contains(c))
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

            score += carriere.CompetencesPourScore.Count(c
                => c == _competencesEtTalentsService.CompetenceMetierArquebusier) * 2;

            score += carriere.TalentsPourScore.Count(c
                => c == _competencesEtTalentsService.TalentRechergementRapide
                   || c == _competencesEtTalentsService.TalentAdresseAuTir
                   || c == _competencesEtTalentsService.TalentTirDePrecision
                   || c == _competencesEtTalentsService.TalentTirEnPuissance
                   || c == _competencesEtTalentsService.TalentMaitreArtilleur
                   || (c.Parent == _competencesEtTalentsService.TalentGroupeMaitrise &&
                       !_competencesEtTalentsService.TalentsMaitriseAuContact.Contains(c))
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
            ) * 2;
            
            score += carriere.TalentsPourScore.Count(c
                => c == _competencesEtTalentsService.TalentMaitriseArmesDeCavalerie
                || c == _competencesEtTalentsService.TalentMaitriseUneAuChoix
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
                   || c == _competencesEtTalentsService.CompetenceLangSecretVoleurs
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
                || c == _competencesEtTalentsService.CompetenceAlphabetSecretDeuxAuChoix
                || c == _competencesEtTalentsService.CompetenceLangageSecretRodeurs
                || c == _competencesEtTalentsService.CompetenceLangageSecretDeuxAuChoix
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
                   || c == _competencesEtTalentsService.TalentMaitriseArcsLongs
                   || c == _competencesEtTalentsService.TalentMaitriseArmesParalisantes
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
                || c == _competencesEtTalentsService.CompetenceConnaissancesAcademiquesAstronomie
                || c == _competencesEtTalentsService.CompetenceConnaissancesAcademiquesPotamologie
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
            if (!carriere.TalentsPourScore.Any(c
                => c == _competencesEtTalentsService.TalentMaitreArtilleur
                   || c == _competencesEtTalentsService.TalentMaitriseArmesAFeu))
                return 0;
            
            var score = 0;
            score += carriere.CompetencesPourScore.Count(c
                => c == _competencesEtTalentsService.CompetenceMetierArquebusier
            ) * 2;

            score += carriere.TalentsPourScore.Count(c
                => c == _competencesEtTalentsService.TalentMaitreArtilleur
                || c == _competencesEtTalentsService.TalentMaitriseArmesAFeu
                || c == _competencesEtTalentsService.TalentAdresseAuTir
                || c == _competencesEtTalentsService.TalentRechergementRapide
                || c == _competencesEtTalentsService.TalentSurSesGardes
                || c == _competencesEtTalentsService.TalentTirDePrecision
                || c == _competencesEtTalentsService.TalentTirEnPuissance
            ) * 2;

            score += CalculBonusCapaciteDeTir(carriere) * 2;
            
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

        #region Supprimer les caractères indésirables pour le nom du fichier

        private const string CaracteresARemplacer = "ÀÁÂÃÄÅàáâãäåÒÓÔÕÖØòóôõöøÈÉÊËèéêëÌÍÎÏìíîïÙÚÛÜùúûüÿÑñÇç-'";
        private const string CaracteresDeRemplacement = "aaaaaaaaaaaaooooooooooooeeeeeeeeiiiiiiiiuuuuuuuuynncc  ";

        private static string ConvertirCaracteres(string chaineANettoyer)
        {
            char[] tableauFind = CaracteresDeRemplacement.ToCharArray();
            char[] tableauReplace = CaracteresARemplacer.ToCharArray();

            for (var i = 0; i < tableauReplace.Length; i++)
                chaineANettoyer = chaineANettoyer.Replace(tableauReplace[i], tableauFind[i]);

            return chaineANettoyer;
        }

        #endregion

        public CarriereDto[] Recherche(string searchText)
        {
            searchText = ConvertirCaracteres(searchText).ToLower();
            var motsClefRecherches = searchText.Split(' ').Where(c => c != "").ToList();

            return AllCarrieres
                .Where(c => ConvertirCaracteres(c.Nom).ToLower().Contains(searchText)
                            || c.MotsClefDeRecherche.Intersect(motsClefRecherches).Any())
                .ToArray();
        }

        public IEnumerable<CarriereDto> CarrieresDeBretonnie => AllCarrieres
            .Where(c => c.SourceLivre == _referencesService.LivreLesChevaliersDuGraal
                        || c.SourceLivre == _referencesService.LivreLeDucheDesDamnes)
            .ToList();

        public IEnumerable<CarriereDto> CarrieresDuKislev => AllCarrieres
            .Where(c => c.Id == 53 || c.SourceLivre == _referencesService.LivreLaReineDesGlaces)
            .ToList();

        public IEnumerable<CarriereDto> CarrieresSkaven => AllCarrieres
            .Where(c => c.SourceLivre == _referencesService.LivreLesFilsDuRatCornu)
            .ToList();

        // Tueur de morts, Fouet de dieu, Flagellant, Fanatique, Pénitent, Prêcheur de rue, Tueur de démons/géants/trolls, Exécuteur, Mystique, Cénobite
        public IEnumerable<CarriereDto> CarrieresFanatiques => GetCarrieres(new[] {209, 213, 170, 45, 212, 88, 198, 199, 77, 87, 268, 267, 194, 1, 314 });
        public IEnumerable<CarriereDto> CarrieresDeNorsca => GetCarrieres(new [] { 26, 89, 302, 303, 90, 91, 92, 93, 94, 95, 304 });
        public IEnumerable<CarriereDto> CarrieresChaos => GetCarrieres(new [] { 305, 307, 309, 311, 306, 308, 310, 312, 293, 294 });
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
    }
}
