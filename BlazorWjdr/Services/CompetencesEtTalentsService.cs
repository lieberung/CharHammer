using System.Diagnostics;
using BlazorWjdr.DataSource.JsonDto;

namespace BlazorWjdr.Services
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CompetencesEtTalentsService
    {
        private readonly Dictionary<int, CompetenceDto> _cacheCompetences;
        private readonly Dictionary<int, TalentDto> _cacheTalents;

        public CompetencesEtTalentsService(Dictionary<int, CompetenceDto> dataCompetences, Dictionary<int, TalentDto> dataTalents)
        {
            _cacheCompetences = dataCompetences;
            _cacheTalents = dataTalents;
        }
        
        public IEnumerable<CompetenceDto> GetCompetences(IEnumerable<int> ids) => ids.Select(GetCompetence).OrderBy(c => c.Nom).ToArray();
        public CompetenceDto GetCompetence(int id)
        {
            return _cacheCompetences[id];
        }

        public IEnumerable<CompetenceDto> AllCompetences
        {
            get
            {
                return _cacheCompetences.Values.OrderBy(c => c.Nom).ThenBy(c => c.Specialisation);
            }
        }

        public IEnumerable<TalentDto> GetTalents(IEnumerable<int> ids) => ids.Select(GetTalent).OrderBy(t => t.Nom).ToArray();
        public TalentDto GetTalent(int id)
        {
            return _cacheTalents[id];
        }

        public IEnumerable<TalentDto> AllTalents
        {
            get
            {
                return _cacheTalents.Values.OrderBy(c => c.Nom).ThenBy(c => c.Specialisation);
            }
        }

        private void Initialize()
        {
            //if (_dataCompetences.Count == 0) throw new System.Exception("tototototototototototototototot");
            /*
            _cacheTalents = _dataTalents
                .Select(t => new TalentDto
                {
                    Id = t.id,
                    Nom = $"{t.nom}{(!string.IsNullOrWhiteSpace(t.spe) ? $" : {t.spe}" : "")}",
                    Description = t.description,
                    Ignore = t.ignorer,
                    Resume = t.resume,
                    Specialisation = t.spe ?? "",
                    TalentParentId = t.parent_id,
                    Trait = t.trait,
                    Max = t.max ?? "",
                    Tests = t.tests ?? ""
                })
                .ToDictionary(k => k.Id, v => v);

            foreach (var talent in _cacheTalents.Values.Where(t => t.TalentParentId.HasValue))
            {
                talent.Parent = _cacheTalents[talent.TalentParentId!.Value];
            }

            foreach (var talent in _cacheTalents.Values.Where(t => t.Parent != null).Select(t => t.Parent))
                talent!.SousElements.AddRange(_cacheTalents.Values
                    .Where(c=>c.Parent == talent)
                    .OrderBy(c => c.Nom));

            _cacheCompetences = _dataCompetences
                .Select(c => new CompetenceDto
                {
                    Id = c.id,
                    Ignore = c.ignorer,
                    Nom = $"{c.nom}{(!string.IsNullOrWhiteSpace(c.spe) ? $" : {c.spe}" : "")}",
                    Resume = c.resume,
                    Specialisation = c.spe ?? "",
                    CaracteristiqueAssociee = c.carac,
                    EstUneCompetenceDeBase = c.de_base,
                    CompetenceMereId = c.parent,
                    TalentsLies = (c.talents ?? Array.Empty<int>())
                        .Select(id => _cacheTalents[id])
                        .OrderBy(t => t.Nom)
                        .ToList()
                })
                .ToDictionary(k => k.Id, v => v);

            foreach (var competence in _cacheCompetences.Values.Where(c => c.CompetenceMereId.HasValue))
            {
                competence.TalentsLies = competence.TalentsLiesIds.Select(id => _cacheTalents[id]).ToList();
                competence.Parent = _cacheCompetences[competence.CompetenceMereId!.Value];
            }

            foreach (var competence in _cacheCompetences.Values)
            {
                competence.NomPourRecherche = GenericService.ConvertirCaracteres(competence.Nom);
                competence.MotsClefDeRecherche = GenericService.MotsClefsDeRecherche(competence.NomPourRecherche);
                competence.SetResume();
            }

            foreach (var competence in _cacheCompetences.Values.Where(t => t.Parent != null).Select(t => t.Parent))
            {
                competence!.SousElements.AddRange(_cacheCompetences.Values
                    .Where(c=>c.Parent == competence)
                    .OrderBy(c => c.Nom));
            }

            foreach (var talent in _cacheTalents.Values)
            {
                talent.NomPourRecherche = GenericService.ConvertirCaracteres(talent.Nom);
                talent.MotsClefDeRecherche = GenericService.MotsClefsDeRecherche(talent.NomPourRecherche);
                talent.CompetencesLiees = _cacheCompetences.Values
                    .Where(c => c.TalentsLies.Contains(talent) && c.Ignore == false)
                    .ToList();
            }
            */
            
            //_dataCompetences.Clear();
            //_dataTalents.Clear();
        }

        #region Competences & Talents
        
        // Caractéristiques
        public TalentDto TalentGuerrierNe => GetTalent(34);
        public TalentDto TalentTireurDElite => GetTalent(90);
        public TalentDto TalentForceAccrue => GetTalent(29);
        public TalentDto TalentResistanceAccrue => GetTalent(69);
        public TalentDto TalentVivacite => GetTalent(216);
        public TalentDto TalentDoigtsVifs => GetTalent(233);
        public TalentDto TalentReflexesEclairs => GetTalent(68);
        public TalentDto TalentIntelligent => GetTalent(39);
        public TalentDto TalentSangFroid => GetTalent(76);
        public TalentDto TalentSociable => GetTalent(83);
        

        // Académique
        public CompetenceDto CompetenceGroupeConnaissancesAcademiques => GetCompetence(13);
        public CompetenceDto CompetenceGroupeConnaissancesGenerales => GetCompetence(14);
        public CompetenceDto CompetenceGroupeLangue => GetCompetence(39);
        public CompetenceDto CompetenceConnaissancesAcademiquesDeuxAuChoix => GetCompetence(169);
        public CompetenceDto CompetenceConnaissancesAcademiquesTroisAuChoix => GetCompetence(166);
        public CompetenceDto CompetenceLireEcrire => GetCompetence(42);
        public TalentDto TalentCalculMental => GetTalent(7);
        public TalentDto TalentLinguistique => GetTalent(42);

        // Arcanique
        public CompetenceDto CompetenceConnaissanceAcademiqueEsprits => GetCompetence(141);
        public CompetenceDto CompetenceConnaissanceAcademiqueMagie => GetCompetence(109);
        public CompetenceDto CompetenceConnaissanceAcademiqueNecromancie => GetCompetence(110);
        public CompetenceDto CompetenceFocalisation => GetCompetence(31);
        public CompetenceDto CompetenceLangageMystique => GetCompetence(36);
        public CompetenceDto CompetenceLangageMystiqueMagick => GetCompetence(162);
        public CompetenceDto CompetenceLangageMystiqueDemoniaque => GetCompetence(170);
        public CompetenceDto CompetenceLangageMystiqueElfeMystique => GetCompetence(171);
        public CompetenceDto CompetenceLangueClassique => GetCompetence(142);
        public CompetenceDto CompetenceSensDeLaMagie => GetCompetence(52);
        public TalentDto TalentHarmonieAethyrique => GetTalent(35);
        public TalentDto TalentMainsAgiles => GetTalent(48);
        public TalentDto TalentMeditation => GetTalent(63);
        public TalentDto TalentProjectilePuissant => GetTalent(66);
        public TalentDto TalentGroupeMagieCommune => GetTalent(44);
        public TalentDto TalentGroupeMagieMineure => GetTalent(45);
        public TalentDto TalentMagieNoire => GetTalent(46);
        public TalentDto TalentMagieVulgaire => GetTalent(47);

        // Martial
        public CompetenceDto CompetenceLangSecretBataille => GetCompetence(148);
        public TalentDto TalentAmbidextrie => GetTalent(5);
        public TalentDto TalentCoupsPrécis => GetTalent(20);
        public TalentDto TalentSurSesGardes => GetTalent(85);
        public TalentDto TalentTroublant => GetTalent(91);
        public TalentDto TalentMaitriseUneAuChoix => GetTalent(153);
        public TalentDto TalentMaitriseDeuxAuChoix => GetTalent(152);
        public TalentDto TalentReflexesDeCombat => GetTalent(218);
        
        // Martial CaC
        public CompetenceDto CompetenceGroupeMelee => GetCompetence(600);
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
        public TalentDto TalentParadeEclair => GetTalent(65);
        public TalentDto TalentLutte => GetTalent(43);
        public TalentDto TalentRobuste => GetTalent(74);
        public TalentDto TalentValeureux => GetTalent(92);
        public TalentDto TalentGroupeVertu => GetTalent(206);
        public TalentDto TalentPresenceImposante => GetTalent(217);
        public TalentDto TalentDechainement => GetTalent(237);
        public TalentDto TalentTueur => GetTalent(232);
        public TalentDto TalentCombatRapproche => GetTalent(228);
        public TalentDto TalentHommeBouclier => GetTalent(231);
        public TalentDto TalentAssautBrutal => GetTalent(223);

        public CompetenceDto CompetenceMeleeArmesDEscrime => GetCompetence(611);
        public CompetenceDto CompetenceMeleeArmesDeCavalerie => GetCompetence(609);
        public CompetenceDto CompetenceMeleeArmesDeParade => GetCompetence(607);
        public CompetenceDto CompetenceMeleeArmesLourdes => GetCompetence(612);
        public CompetenceDto CompetenceMeleeArmesParalisantes => GetCompetence(605);
        public CompetenceDto CompetenceMeleeFléaux => GetCompetence(614);

        
        // Martial Distance
        public CompetenceDto CompetenceTirArbaletes => GetCompetence(604);
        public CompetenceDto CompetenceTirArcsLongs => GetCompetence(615);
        public CompetenceDto CompetenceTirArmesAFeu => GetCompetence(616);
        public CompetenceDto CompetenceTirArmesDeJet => GetCompetence(610);
        public CompetenceDto CompetenceTirArmesMecaniques => GetCompetence(606);
        public CompetenceDto CompetenceTirLancePierres => GetCompetence(608);
        public CompetenceDto CompetenceGroupeExplosifs => GetCompetence(617);
        public CompetenceDto CompetenceGroupeTir => GetCompetence(620);
        public CompetenceDto CompetenceMetierArquebusier => GetCompetence(59);
        public TalentDto TalentAdresseAuTir => GetTalent(4);
        public TalentDto TalentRechergementRapide => GetTalent(67);
        public TalentDto TalentTirDePrecision => GetTalent(88);
        public TalentDto TalentTirEnPuissance => GetTalent(89);
        public TalentDto TalentMaitreArtilleur => GetTalent(49);
        public TalentDto TalentTirEclair => GetTalent(220);
        
        // De l'ombre
        public CompetenceDto CompetenceAlphSecretVoleurs => GetCompetence(89);
        public CompetenceDto CompetenceDeplacementSilencieux => GetCompetence(19);
        public CompetenceDto CompetenceDissimulation => GetCompetence(20);
        public CompetenceDto CompetenceFouille => GetCompetence(32);
        public CompetenceDto CompetencePerception => GetCompetence(48);
        public CompetenceDto CompetenceEscalade => GetCompetence(24);
        public CompetenceDto CompetenceCrochetage => GetCompetence(16);
        public CompetenceDto CompetenceDeguisement => GetCompetence(18);
        public CompetenceDto CompetenceEscamotage => GetCompetence(25);
        public CompetenceDto CompetencePreparationDePoisons => GetCompetence(50);
        public CompetenceDto CompetenceLectureSurLesLevres => GetCompetence(41);
        public TalentDto TalentConnaissanceDesPieges => GetTalent(16);
        public TalentDto TalentCamouflageRural => GetTalent(8);
        public TalentDto TalentCamouflageSouterrain => GetTalent(9);
        public TalentDto TalentCamouflageUrbain => GetTalent(10);
        public TalentDto TalentCodeDeLaRue => GetTalent(13);
        public TalentDto TalentImitation => GetTalent(36);
        public TalentDto TalentSensAiguises => GetTalent(80);
        public TalentDto TalentAccuiteAuditive => GetTalent(2);
        public TalentDto TalentAccuiteGustativeEtOlfactive => GetTalent(209);
        public TalentDto TalentAccuiteVisuelle => GetTalent(3);
        public TalentDto TalentFilature => GetTalent(30);
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
        
        // Commerce  + TalentCalculMental
        public CompetenceDto CompetenceMarchandage => GetCompetence(43);
        public CompetenceDto CompetenceMetierMarchand => GetCompetence(78);
        public CompetenceDto CompetenceEvaluation => GetCompetence(179);
        public CompetenceDto CompetenceExpressionArtistiqueConteur => GetCompetence(123);
        public TalentDto TalentDurEnAffaires => GetTalent(25);

        // Cavalerie  + CompetenceMetierVendeurDeChevaux, TalentMaitriseArmesDeCavalerie
        public CompetenceDto CompetenceEquitation => GetCompetence(23);
        public CompetenceDto CompetenceEquitationCochonDeGuerre => GetCompetence(152);
        public CompetenceDto CompetenceExpressionArtistiqueAcrobatEquestre => GetCompetence(153);
        public CompetenceDto CompetenceEmpriseSurLesAnimaux => GetCompetence(22);
        public CompetenceDto CompetenceSoinsDesAnimaux => GetCompetence(54);
        public CompetenceDto CompetenceMetierGarconDEcurie => GetCompetence(180);
        public CompetenceDto CompetenceDressage => GetCompetence(21);
        public TalentDto TalentAcrobateEquestre => GetTalent(1);

        // Artisanat  + CompetencePreparationDePoisons, CompetenceEvaluation
        public CompetenceDto CompetenceLangageSecretGuilde => GetCompetence(158);
        public CompetenceDto CompetenceGroupeMetier => GetCompetence(44);
        public CompetenceDto CompetenceMetierDeuxAuChoix => GetCompetence(159);
        public CompetenceDto CompetenceMetierTroisAuChoix => GetCompetence(172);
        public CompetenceDto CompetenceConnaissancesAcademiquesArts => GetCompetence(102);
        public CompetenceDto CompetenceConnaissancesAcademiquesIngénierie => GetCompetence(108);
        public CompetenceDto CompetenceConnaissancesAcademiquesRunes => GetCompetence(112);
        public CompetenceDto CompetenceConnaissancesAcademiquesSciences => GetCompetence(113);
        public CompetenceDto CompetenceLangageMystiqueNain => GetCompetence(3);
        public TalentDto TalentRuneDeuxAuChoix => GetTalent(170);
        public TalentDto TalentRuneSixAuChoix => GetTalent(171);
        public TalentDto TalentRuneDixAuChoix => GetTalent(173);
        public TalentDto TalentRuneMajeureDeuxAuChoix => GetTalent(172);
        public CompetenceDto CompetenceCreationDeRunes => GetCompetence(6);
        public TalentDto TalentSavoirFaireNain => GetTalent(78);
        public TalentDto TalentTalentArtistique => GetTalent(86);
        
        // Rôdeurs  + CompetenceDeplacementSilencieux, CompetenceDissimulation, CompetenceEmpriseSurLesAnimaux, CompetenceGroupeLangue
        //          , CompetencePerception, CompetenceFouille, CompetenceEscalade, TalentLinguistique, TalentConnaissanceDesPieges
        public CompetenceDto CompetenceBraconnage => GetCompetence(5);
        public CompetenceDto CompetenceAlphabetSecretPisteurs => GetCompetence(86);
        public CompetenceDto CompetenceLangageSecretRodeurs => GetCompetence(87);
        public CompetenceDto CompetenceLangageSecretTroisAuChoix => GetCompetence(38);
        public CompetenceDto CompetenceOrientation => GetCompetence(47);
        public CompetenceDto CompetenceMetierCartographe => GetCompetence(63);
        public CompetenceDto CompetenceSurvie => GetCompetence(55);
        public CompetenceDto CompetencePistage => GetCompetence(49);
        public TalentDto TalentSensDeLOrientation => GetTalent(81);
        public TalentDto TalentGrandVoyageur => GetTalent(33);
        public TalentDto TalentSixiemeSens => GetTalent(82);
        public TalentDto TalentCourseAPied => GetTalent(22);

        // Maritimes  + CompetenceOrientation, CompetenceMetierCartographe, TalentGrandVoyageur
        public CompetenceDto CompetenceCanotage => GetCompetence(7);
        public CompetenceDto CompetenceNatation => GetCompetence(45);
        public CompetenceDto CompetenceNavigation => GetCompetence(46);
        public CompetenceDto CompetenceEruditionAstronomie => GetCompetence(103);
        public CompetenceDto CompetenceEruditionPotamologie => GetCompetence(189);
        public CompetenceDto CompetenceMetierCharpentierNaval => GetCompetence(65);
        
        // Poudre noire  + CompetenceMetierArquebusier, TalentMaitriseArmesAFeu

        // Ami des bêtes  + CompetenceMetierGarconDEcurie
        public CompetenceDto CompetenceMetierVendeurDeChevaux => GetCompetence(177);
        public CompetenceDto CompetenceMetierMaitreChien => GetCompetence(178);
        public CompetenceDto CompetenceMetierFauconnerie => GetCompetence(179);
        public CompetenceDto CompetenceConnaissancesAcademiquesZoologie => GetCompetence(188);
        public CompetenceDto CompetenceMetierFermier => GetCompetence(74);
        public CompetenceDto CompetenceConduiteDAttelage => GetCompetence(12);
        
        public CompetenceDto CompetenceIntuition => GetCompetence(210);
        public CompetenceDto CompetenceSangFroid => GetCompetence(219);
        public CompetenceDto CompetenceResistance => GetCompetence(220);
        public CompetenceDto CompetenceAthletisme => GetCompetence(209);
        public CompetenceDto CompetenceSoins => GetCompetence(53);
        public CompetenceDto CompetenceMetierApothicaire => GetCompetence(58);
            
        public TalentDto TalentChirurgie => GetTalent(12);
        public TalentDto TalentChance => GetTalent(11);
        public TalentDto TalentResistanceALaMagie => GetTalent(70);
        public TalentDto TalentResistanceAuxMaladies => GetTalent(72);
        public TalentDto TalentResistanceAuxPoisons => GetTalent(73);
        public TalentDto TalentSainDEsprit => GetTalent(75);
        public TalentDto TalentVisionNocturne => GetTalent(93);
        public TalentDto TalentSensAiguisés => GetTalent(80);
        
        #endregion
        
        public CompetenceDto[] RechercheCompetences(string searchText)
        {
            searchText = GenericService.ConvertirCaracteres(searchText);
            var motsClefRecherches = GenericService.MotsClefsDeRecherche(searchText);

            return AllCompetences
                .Where(c => c.Ignore == false && (
                            c.NomPourRecherche.Contains(searchText) || c.MotsClefDeRecherche.Intersect(motsClefRecherches).Any()
                    )
                )
                .OrderByDescending(c => c.MotsClefDeRecherche.Intersect(motsClefRecherches).Count())
                .ToArray();
        }
        
        public TalentDto[] RechercheTalents(string searchText)
        {
            searchText = GenericService.ConvertirCaracteres(searchText);
            var motsClefRecherches = GenericService.MotsClefsDeRecherche(searchText);

            return AllTalents
                .Where(t => t.Ignore == false && (
                        t.NomPourRecherche.Contains(searchText) || t.MotsClefDeRecherche.Intersect(motsClefRecherches).Any()
                    )
                )
                .OrderByDescending(t => t.MotsClefDeRecherche.Intersect(motsClefRecherches).Count())
                .ToArray();
        }
        
        public List<TalentDto> TalentsInitiaux => new List<TalentDto> {
             TalentAccuiteAuditive,
             TalentAccuiteGustativeEtOlfactive,
             TalentAccuiteVisuelle,
             TalentAmbidextrie,
             TalentCalculMental,
             TalentChance,
             TalentCourseAPied,
             TalentDoigtsVifs,    
             TalentDurACuir,
             TalentForceAccrue,
             TalentGuerrierNe,
             TalentImitation,
             TalentIntelligent,
             TalentReflexesEclairs,
             TalentResistanceALaMagie,
             TalentResistanceAccrue,
             TalentResistanceAuxMaladies,
             TalentResistanceAuxPoisons,
             TalentRobuste,
             TalentSainDEsprit,
             TalentSangFroid,
             TalentSixiemeSens,
             TalentSociable,
             TalentTireurDElite,
             TalentVisionNocturne,
             TalentVivacite
        };
    }
}
