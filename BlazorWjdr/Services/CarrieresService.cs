using System.IO;

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
                .Where(c => c.Competences.Contains(competence)) // ToDo: choix
                .ToArray());

        public Task<CarriereDto[]> GetCarrieresProposant(TalentDto talent)
            => Task.FromResult(AllCarrieres
                .Where(c => c.Talents.Contains(talent)) // ToDo: choix
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
                carriere.ScoreMartialAuContact = CalculScoreMartialAuContact(carriere);
                carriere.ScoreMartialADistance = CalculScoreMartialADistance(carriere);
                carriere.ScoreCavalerie = CalculScoreCavalerie(carriere);
                carriere.ScoreDeLOmbre = CalculScoreDeLOmbre(carriere);
                carriere.ScoreSocial = CalculScoreSocial(carriere);
                carriere.ScoreCommerce = CalculScoreCommerce(carriere);
            }
        }

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

            var capaciteDeTir = carriere.PlanDeCarriere.Ct;
            if (carriere.TalentsPourScore.Any(t => t == _competencesEtTalentsService.TalentTireurDElite))
                capaciteDeTir += 5;
            if (carriere.EstUneCarriereAvancee)
                capaciteDeTir -= 10;
            if (capaciteDeTir > 0)
                score += (capaciteDeTir / 5) * 3;

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
                || c == _competencesEtTalentsService.CompetenceMetierVendeurDeCheveaux
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
            
            var agilite = carriere.PlanDeCarriere.Ag;
            if (carriere.TalentsPourScore.Any(t => t == _competencesEtTalentsService.TalentReflexesEclairs))
                agilite += 5;
            if (carriere.EstUneCarriereAvancee)
                agilite -= 10;
            if (agilite > 0)
                score += (agilite / 5);

            return score;
        }

        private int CalculScoreAcademique(CarriereDto carriere)
        {
            var score = 0;
            score += carriere.CompetencesPourScore.Count(c
                => c.Parent == _competencesEtTalentsService.CompetenceGroupeConnaissancesAcademiques
                   || c.Parent == _competencesEtTalentsService.CompetenceGroupeLangue
                   || c == _competencesEtTalentsService.CompetenceLireEcrire
            ) * 2;
            score += (carriere.CompetencesPourScore.Count(c =>
                c.Parent == _competencesEtTalentsService.CompetenceGroupeConnaissancesGenerales) + 1) / 2;

            if (carriere.CompetencesPourScore.Any(c =>
                c == _competencesEtTalentsService.CompetenceConnaissancesAcademiquesDeuxAuChoix))
                score += 2;
            if (carriere.CompetencesPourScore.Any(c =>
                c == _competencesEtTalentsService.CompetenceConnaissancesAcademiquesDeuxAuChoix))
                score += 4;

            score += carriere.TalentsPourScore.Count(c
                => c == _competencesEtTalentsService.TalentCalculMental
                   || c == _competencesEtTalentsService.TalentLinguistique
            ) * 2;

            var intelligence = carriere.PlanDeCarriere.Int;
            if (carriere.TalentsPourScore.Any(t => t == _competencesEtTalentsService.TalentIntelligent))
                intelligence += 5;
            if (carriere.EstUneCarriereAvancee)
                intelligence -= 10;
            if (intelligence > 0)
                score += (intelligence / 5) * 3;

            return score;
        }

        private int CalculScoreCommerce(CarriereDto carriere)
        {
            var score = 0;
            
            score += carriere.CompetencesPourScore.Count(c
                => c == _competencesEtTalentsService.CompetenceMarchandage
                || c == _competencesEtTalentsService.CompetenceMetierMarchand
            ) * 3;

            score += carriere.CompetencesPourScore.Count(c
                => c == _competencesEtTalentsService.CompetenceEvaluation
                   || c == _competencesEtTalentsService.CompetenceBaratin
                   || c == _competencesEtTalentsService.CompetenceLireEcrire
                   || c == _competencesEtTalentsService.CompetenceExpressionArtistiqueConteur
            ) * 2;
            
            score += carriere.TalentsPourScore.Count(c
                => c == _competencesEtTalentsService.TalentCalculMental
                   || c == _competencesEtTalentsService.TalentDurEnAffaires
            ) * 2;

            var intelligence = carriere.PlanDeCarriere.Int;
            if (carriere.TalentsPourScore.Any(t => t == _competencesEtTalentsService.TalentIntelligent))
                intelligence += 5;
            if (carriere.EstUneCarriereAvancee)
                intelligence -= 10;
            if (intelligence > 0)
                score += (intelligence / 5) * 3;

            var sociabilite = carriere.PlanDeCarriere.Soc;
            if (carriere.TalentsPourScore.Any(t => t == _competencesEtTalentsService.TalentSociable))
                sociabilite += 5;
            if (carriere.EstUneCarriereAvancee)
                sociabilite -= 10;
            if (sociabilite > 0)
                score += (sociabilite / 5) * 3;
            
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
            ) * 2;

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
            ) * 2;

            var agilite = carriere.PlanDeCarriere.Ag;
            if (carriere.TalentsPourScore.Any(t => t == _competencesEtTalentsService.TalentReflexesEclairs))
                agilite += 5;
            if (carriere.EstUneCarriereAvancee)
                agilite -= 10;
            if (agilite > 0)
                score += (agilite / 5) * 3;
            return score;
        }

        private int CalculScoreSocial(CarriereDto carriere)
        {
            var score = 0;

            score += carriere.CompetencesPourScore.Count(c
                => c == _competencesEtTalentsService.CompetenceBaratin
                   && c == _competencesEtTalentsService.CompetenceCharisme
                   && c == _competencesEtTalentsService.CompetenceCommandement
                   && c == _competencesEtTalentsService.CompetenceCommérage
                   && c == _competencesEtTalentsService.CompetenceIntimidation
            ) * 2;

            score += carriere.TalentsPourScore.Count(c
                => c == _competencesEtTalentsService.TalentEloquence
                   || c == _competencesEtTalentsService.TalentOrateurNe
                   || c == _competencesEtTalentsService.TalentPolitique
                   || c == _competencesEtTalentsService.TalentCodeDeLaRue
                   || c == _competencesEtTalentsService.TalentEtiquette
                   || c == _competencesEtTalentsService.TalentIntriguant
            ) * 2;

            var intelligence = carriere.PlanDeCarriere.Int;
            if (carriere.TalentsPourScore.Any(t => t == _competencesEtTalentsService.TalentIntelligent))
                intelligence += 5;
            if (carriere.EstUneCarriereAvancee)
                intelligence -= 10;
            if (intelligence > 0)
                score += (intelligence / 5);

            var sociabilite = carriere.PlanDeCarriere.Soc;
            if (carriere.TalentsPourScore.Any(t => t == _competencesEtTalentsService.TalentSociable))
                sociabilite += 5;
            if (carriere.EstUneCarriereAvancee)
                sociabilite -= 10;
            if (sociabilite > 0)
                score += (sociabilite / 5) * 3;

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
        public IEnumerable<CarriereDto> CarrieresFolie => GetCarrieres(new[] {209, 213, 170, 45, 212, 88, 198, 199, 77, 87, 268, 267});
        public IEnumerable<CarriereDto> CarrieresDeNorsca => GetCarrieres(new [] { 26, 89, 302, 303, 90, 91, 92, 93, 94, 95, 304 });
        public IEnumerable<CarriereDto> CarrieresChaos => GetCarrieres(new [] { 305, 307, 309, 311, 306, 308, 310, 312, 293, 294 });
    }
}
