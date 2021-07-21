namespace BlazorWjdr.Services
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CompetencesEtTalentsService
    {
        private List<CompetenceDto>? _allCompetences;
        private Dictionary<int, CompetenceDto>? _cacheCompetences;

        private List<TalentDto>? _allTalents;
        private Dictionary<int, TalentDto>? _cacheTalents;

        public IEnumerable<CompetenceDto> GetCompetences(IEnumerable<int> ids) => ids.Select(GetCompetence).OrderBy(c => c.Nom).ToArray();
        public CompetenceDto GetCompetence(int id)
        {
            if (_cacheCompetences == null)
                Initialize();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            return _cacheCompetences[id];
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        public Task<CompetenceDto[]> ItemsCompetences()
        {
            if (_allCompetences == null)
                Initialize();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            return Task.FromResult(_allCompetences.ToArray());
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        public IEnumerable<TalentDto> GetTalents(IEnumerable<int> ids) => ids.Select(GetTalent).OrderBy(t => t.Nom).ToArray();
        public TalentDto GetTalent(int id)
        {
            if (_cacheTalents == null)
                Initialize();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            return _cacheTalents[id];
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        public Task<TalentDto[]> ItemsTalents()
        {
            if (_allTalents == null)
                Initialize();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            return Task.FromResult(_allTalents.ToArray());
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        private void Initialize()
        {
            _allTalents = DataSource.JsonLoader
                .GetRootTalent()
                .items
                .Select(t => new TalentDto
                {
                    Id = t.id,
                    Nom = $"{t.nom}{(!string.IsNullOrWhiteSpace(t.specialisation) ? $" : {t.specialisation}" : "")}",
                    Description = t.description,
                    Ignore = t.ignorer,
                    Resume = t.resume,
                    Specialisation = t.specialisation,
                    TalentParentId = t.parent_id,
                    Trait = t.trait
                })
                .OrderBy(t => t.Nom)
                .ToList();

            _cacheTalents = _allTalents.ToDictionary(k => k.Id, v => v);

            foreach (var talent in _allTalents.Where(t => t.TalentParentId.HasValue))
#pragma warning disable CS8629 // Nullable value type may be null.
                talent.Parent = GetTalent(talent.TalentParentId.Value);
#pragma warning restore CS8629 // Nullable value type may be null.
            foreach (var talent in _allTalents)
                talent.SousElements.AddRange(_allTalents
                    .Where(c=>c.Parent == talent)
                    .OrderBy(c => c.Nom));

            _allCompetences = DataSource.JsonLoader
                .GetRootCompetence()
                .items
                .Select(c => new CompetenceDto
                {
                    Id = c.id,
                    Ignore = c.ignorer,
                    Nom = $"{c.nom}{(!string.IsNullOrWhiteSpace(c.specialisation) ? $" : {c.specialisation}" : "")}",
                    Resume = c.resume,
                    Specialisation = c.specialisation,
                    CaracteristiqueAssociee = c.carac,
                    EstUneCompetenceDeBase = c.de_base,
                    CompetenceMereId = c.fk_competencemereid,
                    TalentsLies = (c.fk_talentslies ?? Array.Empty<int>())
                        .Select(id => _cacheTalents[id])
                        .OrderBy(t => t.Nom)
                        .ToList()
                })
                .OrderBy(t => t.Nom)
                .ToList();

            _cacheCompetences = _allCompetences.ToDictionary(k => k.Id, v => v);

            foreach (var competence in _allCompetences.Where(c => c.CompetenceMereId.HasValue))
#pragma warning disable CS8629 // Nullable value type may be null.
                competence.Parent = GetCompetence(competence.CompetenceMereId.Value);
#pragma warning restore CS8629 // Nullable value type may be null.

            foreach (var competence in _allCompetences)
            {
                competence.SousElements.AddRange(_allCompetences
                    .Where(c=>c.Parent == competence)
                    .OrderBy(c => c.Nom));
                competence.SetResume();
            }

            foreach (var talent in _allTalents)
            {
                talent.CompetencesLiees = _allCompetences
                    .Where(c => c.TalentsLies.Contains(talent))
                    .ToList();
            }
        }

        // Académique
        public CompetenceDto CompetenceGroupeConnaissancesAcademiques => GetCompetence(13);
        public CompetenceDto CompetenceGroupeConnaissancesGenerales => GetCompetence(14);
        public CompetenceDto CompetenceGroupeLangue => GetCompetence(39);
        public CompetenceDto CompetenceConnaissancesAcademiquesDeuxAuChoix => GetCompetence(169);
        public CompetenceDto CompetenceConnaissancesAcademiquesTroisAuChoix => GetCompetence(166);
        public CompetenceDto CompetenceLireEcrire => GetCompetence(42);
        public TalentDto TalentIntelligent => GetTalent(39);
        public TalentDto TalentCalculMental => GetTalent(7);
        public TalentDto TalentLinguistique => GetTalent(42);

        // Martial
        public CompetenceDto CompetenceLangSecretBataille => GetCompetence(148);
        public TalentDto TalentAmbidextrie => GetTalent(5);
        public TalentDto TalentCoupsPrécis => GetTalent(20);
        public TalentDto TalentGroupeMaitrise => GetTalent(50);
        public TalentDto TalentSurSesGardes => GetTalent(85);
        public TalentDto TalentForceAccrue => GetTalent(29);
        public TalentDto TalentTroublant => GetTalent(91);
        
        // Martial CaC
        public CompetenceDto CompetenceEsquive => GetCompetence(26);
        public TalentDto TalentCombatADeuxArmes => GetTalent(155);
        public TalentDto TalentCombatDeRue => GetTalent(14);
        public TalentDto TalentCombattantVirevoltant => GetTalent(15);
        public TalentDto TalentCoupsAuBut => GetTalent(18);
        public TalentDto TalentCoupsPuissants => GetTalent(21);
        public TalentDto TalentCoupsAssomants => GetTalent(19);
        public TalentDto TalentDesarmement => GetTalent(23);
        public TalentDto TalentDurACuir => GetTalent(24);
        public TalentDto TalentFrenesie => GetTalent(30);
        public TalentDto TalentGuerrierNe => GetTalent(34);
        public TalentDto TalentParadeEclair => GetTalent(65);
        public TalentDto TalentLutte => GetTalent(43);
        public TalentDto TalentEffrayant => GetTalent(26);
        public TalentDto TalentResistanceAccrue => GetTalent(69);
        public TalentDto TalentRobuste => GetTalent(74);
        public TalentDto TalentValeureux => GetTalent(92);
        public TalentDto TalentGroupeVertu => GetTalent(206);
        public TalentDto TalentMaitriseArmesDEscrime => GetTalent(57);
        public TalentDto TalentMaitriseArmesDeCavalerie => GetTalent(54);
        public TalentDto TalentMaitriseArmesDeParade => GetTalent(56);
        public TalentDto TalentMaitriseArmesLourdes => GetTalent(58);
        public TalentDto TalentMaitriseArmesParalisantes => GetTalent(60);
        public TalentDto TalentMaitriseFléaux => GetTalent(61);
        public List<TalentDto> TalentsMaitriseAuContact => new() {TalentMaitriseArmesDEscrime,TalentMaitriseArmesDeCavalerie,TalentMaitriseArmesDeParade,TalentMaitriseArmesLourdes,TalentMaitriseArmesParalisantes,TalentMaitriseFléaux}; 
        
        // Martial Distance
        public CompetenceDto CompetenceMetierArquebusier => GetCompetence(59);
        public TalentDto TalentAdresseAuTir => GetTalent(4);
        public TalentDto TalentTireurDElite => GetTalent(90);
        public TalentDto TalentRechergementRapide => GetTalent(67);
        public TalentDto TalentTirDePrecision => GetTalent(88);
        public TalentDto TalentTirEnPuissance => GetTalent(89);
        public TalentDto TalentMaitreArtilleur => GetTalent(49);
        public TalentDto TalentMaitriseArbaletes => GetTalent(51);
        public TalentDto TalentMaitriseArcsLongs => GetTalent(52);
        public TalentDto TalentMaitriseArmesAFeu => GetTalent(53);
        public TalentDto TalentMaitriseArmesDeJet => GetTalent(55);
        public TalentDto TalentMaitriseArmesMecaniques => GetTalent(59);
        public TalentDto TalentMaitriseLancePierres => GetTalent(62);
        
        // De l'ombre
        public CompetenceDto CompetenceAlphSecretVoleurs => GetCompetence(89);
        public CompetenceDto CompetenceLangSecretVoleurs => GetCompetence(147);
        public CompetenceDto CompetenceDeplacementSilencieux => GetCompetence(19);
        public CompetenceDto CompetenceDissimulation => GetCompetence(20);
        public CompetenceDto CompetenceFouille => GetCompetence(32);
        public CompetenceDto CompetencePerception => GetCompetence(48);
        public CompetenceDto CompetenceEscalade => GetCompetence(24);
        public CompetenceDto CompetenceCrochetage => GetCompetence(16);
        public CompetenceDto CompetenceDeguisement => GetCompetence(18);
        public CompetenceDto CompetenceEscamotage => GetCompetence(25);
        public CompetenceDto CompetencePreparationDePoisons => GetCompetence(50);
        public TalentDto TalentConnaissanceDesPieges => GetTalent(16);
        public TalentDto TalentCamouflageRural => GetTalent(8);
        public TalentDto TalentCamouflageSouterrain => GetTalent(9);
        public TalentDto TalentCamouflageUrbain => GetTalent(10);
        public TalentDto TalentCodeDeLaRue => GetTalent(13);
        public TalentDto TalentImitation => GetTalent(36);
        public TalentDto TalentSensAiguises => GetTalent(80);
        public TalentDto TalentAccuiteAuditive => GetTalent(2);
        public TalentDto TalentAccuiteVisuelle => GetTalent(3);
        public TalentDto TalentFilature => GetTalent(30);
        public TalentDto TalentReflexesEclairs => GetTalent(80);
        public TalentDto TalentLectureSurLesLevres => GetTalent(41);
        public TalentDto TalentPistage => GetTalent(49);
        
        // Sociales ( + TalentCodeDeLaRue)
        public CompetenceDto CompetenceBaratin => GetCompetence(4);
        public CompetenceDto CompetenceCharisme => GetCompetence(8);
        public CompetenceDto CompetenceCommandement => GetCompetence(9);
        public CompetenceDto CompetenceCommérage => GetCompetence(10);
        public CompetenceDto CompetenceIntimidation => GetCompetence(34);
        public TalentDto TalentEloquence => GetTalent(27);
        public TalentDto TalentOrateurNe => GetTalent(64);
        public TalentDto TalentPolitique => GetTalent(174);
        public TalentDto TalentEtiquette => GetTalent(28);
        public TalentDto TalentIntriguant => GetTalent(40);
        public TalentDto TalentSociable => GetTalent(83);

    }
}
