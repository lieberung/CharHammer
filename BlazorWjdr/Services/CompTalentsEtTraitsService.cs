namespace BlazorWjdr.Services
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CompTalentsEtTraitsService
    {
        private readonly Dictionary<int, AptitudeDto> _cacheAptitudes;

        public CompTalentsEtTraitsService(Dictionary<int, AptitudeDto> dataAptitudes)
        {
            _cacheAptitudes = dataAptitudes;
        }

        public IEnumerable<AptitudeDto> GetAptitudes(IEnumerable<int> ids) => ids.Select(GetAptitude).OrderBy(c => c.Nom).ToArray();
        public AptitudeDto GetAptitude(int id) => _cacheAptitudes[id];
        public IEnumerable<AptitudeDto> AllAptitudes => _cacheAptitudes.Values
            .OrderBy(c => c.Nom).ThenBy(c => c.Spe);

        public IEnumerable<AptitudeDto> AllCompetences => _cacheAptitudes.Values
            .Where(a => a.EstUneCompetence)
            .OrderBy(c => c.Nom).ThenBy(c => c.Spe);

        public IEnumerable<AptitudeDto> GetTalents(IEnumerable<int> ids) => ids.Select(GetTalent).OrderBy(t => t.Nom).ToArray();
        public AptitudeDto GetTalent(int id) => _cacheAptitudes[id];

        public IEnumerable<AptitudeDto> AllTalents => _cacheAptitudes.Values
            .Where(a => a.EstUnTalent)
            .OrderBy(c => c.Nom).ThenBy(c => c.Spe);

        public IEnumerable<AptitudeDto> GetTraits(IEnumerable<int> ids) => ids.Select(GetTrait).OrderBy(t => t.Nom).ToArray();
        public AptitudeDto GetTrait(int id) => _cacheAptitudes[id];

        public IEnumerable<AptitudeDto> AllTraits => _cacheAptitudes.Values
            .Where(a => a.EstUnTrait)
            .OrderBy(c => c.Nom).ThenBy(c => c.Spe);

        public List<AptitudeDto> AllMeleeSpecialisations =>
            CompetenceGroupeMelee.SousElements.Where(s => s.Ignore == false).ToList();
        public List<AptitudeDto> AllTirSpecialisations =>
            CompetenceGroupeTir.SousElements.Where(s => s.Ignore == false).ToList();

        #region Aptitudes & Talents

        // Caractéristiques
        public AptitudeDto TraitGuerrierNe => GetTrait(3010);
        public AptitudeDto TraitTireurDElite => GetTrait(3011);
        public AptitudeDto TraitForceAccrue => GetTrait(3012);
        public AptitudeDto TraitResistanceAccrue => GetTrait(3013);
        public AptitudeDto TraitVivacite => GetTrait(3014);
        public AptitudeDto TraitHabileDeSesMains => GetTrait(3016);
        public AptitudeDto TraitReflexesEclairs => GetTrait(3015);
        public AptitudeDto TraitIntelligent => GetTrait(3017);
        public AptitudeDto TraitSangFroid => GetTrait(3018);
        public AptitudeDto TraitSociable => GetTrait(3019);

        public AptitudeDto TraitCourseAPied => GetTrait(3020);
        public AptitudeDto TraitDurACuir => GetTrait(3021);


        // Académique
        public AptitudeDto CompetenceGroupeConnaissancesAcademiques => GetAptitude(1013);
        public AptitudeDto CompetenceGroupeConnaissancesGenerales => GetAptitude(1014);
        public AptitudeDto CompetenceGroupeLangue => GetAptitude(1039);
        public AptitudeDto CompetenceConnaissancesAcademiquesDeuxAuChoix => GetAptitude(1169);
        public AptitudeDto CompetenceConnaissancesAcademiquesTroisAuChoix => GetAptitude(1166);
        public AptitudeDto CompetenceLireEcrire => GetAptitude(1042);
        public AptitudeDto TalentCalculMental => GetTalent(207);
        public AptitudeDto TalentLinguistique => GetTalent(2042);

        // Arcanique
        public AptitudeDto CompetenceConnaissanceAcademiqueEsprits => GetAptitude(1141);
        public AptitudeDto CompetenceConnaissanceAcademiqueMagie => GetAptitude(1109);
        public AptitudeDto CompetenceConnaissanceAcademiqueNecromancie => GetAptitude(1110);
        public AptitudeDto CompetenceFocalisation => GetAptitude(1031);
        public AptitudeDto CompetenceLangageMystique => GetAptitude(1036);
        public AptitudeDto CompetenceLangageMystiqueMagick => GetAptitude(1162);
        public AptitudeDto CompetenceLangageMystiqueDemoniaque => GetAptitude(1170);
        public AptitudeDto CompetenceLangageMystiqueElfeMystique => GetAptitude(1171);
        public AptitudeDto CompetenceLangueClassique => GetAptitude(1142);
        public AptitudeDto CompetenceSensDeLaMagie => GetAptitude(1052);
        public AptitudeDto TalentHarmonieAethyrique => GetTalent(2035);
        public AptitudeDto TalentMainsAgiles => GetTalent(2048);
        public AptitudeDto TalentMeditation => GetTalent(2063);
        public AptitudeDto TalentProjectilePuissant => GetTalent(2066);
        public AptitudeDto TalentGroupeMagieCommune => GetTalent(2044);
        public AptitudeDto TalentGroupeMagieMineure => GetTalent(2045);
        public AptitudeDto TalentMagieNoire => GetTalent(2046);
        public AptitudeDto TalentMagieVulgaire => GetTalent(2047);

        // Martial
        public AptitudeDto CompetenceLangSecretBataille => GetAptitude(1148);
        public AptitudeDto TalentAmbidextrie => GetTalent(205);
        public AptitudeDto TalentCoupsPrécis => GetTalent(2020);
        public AptitudeDto TalentSurSesGardes => GetTalent(2085);
        public AptitudeDto TalentTroublant => GetTalent(2091);
        public AptitudeDto TalentMaitriseUneAuChoix => GetTalent(2153);
        public AptitudeDto TalentMaitriseDeuxAuChoix => GetTalent(2152);
        public AptitudeDto TalentReflexesDeCombat => GetTalent(2218);

        // Martial CaC
        public AptitudeDto CompetenceGroupeMelee => GetAptitude(1600);
        public AptitudeDto CompetenceEsquive => GetAptitude(1026);
        public AptitudeDto TalentCombatADeuxArmes => GetTalent(2155);
        public AptitudeDto TalentCombatDeRue => GetTalent(2014);
        public AptitudeDto TalentCombattantVirevoltant => GetTalent(2015);
        public AptitudeDto TalentCoupsAuBut => GetTalent(2018);
        public AptitudeDto TalentCoupsPuissants => GetTalent(2021);
        public AptitudeDto TalentCoupsAssomants => GetTalent(2019);
        public AptitudeDto TalentDesarmement => GetTalent(2023);
        public AptitudeDto TalentDurACuir => GetTalent(2024);
        public AptitudeDto TalentFrenesie => GetTalent(20);
        public AptitudeDto TalentParadeEclair => GetTalent(2065);
        public AptitudeDto TalentLutte => GetTalent(2043);
        public AptitudeDto TalentRobuste => GetTalent(2074);
        public AptitudeDto TalentValeureux => GetTalent(292);
        public AptitudeDto TalentGroupeVertu => GetTalent(2206);
        public AptitudeDto TalentPresenceImposante => GetTalent(2217);
        public AptitudeDto TalentDechainement => GetTalent(2237);
        public AptitudeDto TalentTueur => GetTalent(2232);
        public AptitudeDto TalentCombatRapproche => GetTalent(2228);
        public AptitudeDto TalentHommeBouclier => GetTalent(2231);
        public AptitudeDto TalentAssautBrutal => GetTalent(2223);
        public AptitudeDto TalentChargeBerserk => GetTalent(2245);
        public AptitudeDto TalentDetermine => GetTalent(2273);
        public AptitudeDto TalentRiposte => GetTalent(2241);
        public AptitudeDto TalentFrappeReactive => GetTalent(20213);
        public AptitudeDto TalentRetournement => GetTalent(20242);

        public AptitudeDto CompetenceMeleeArmesDEscrime => GetAptitude(1611);
        public AptitudeDto CompetenceMeleeArmesDeCavalerie => GetAptitude(1609);
        public AptitudeDto CompetenceMeleeArmesDeParade => GetAptitude(1607);
        public AptitudeDto CompetenceMeleeArmesLourdes => GetAptitude(1612);
        public AptitudeDto CompetenceMeleeArmesParalisantes => GetAptitude(1605);
        public AptitudeDto CompetenceMeleeFléaux => GetAptitude(1614);


        // Martial Distance
        public AptitudeDto CompetenceTirArbaletes => GetAptitude(1604);
        public AptitudeDto CompetenceTirArcsLongs => GetAptitude(1615);
        public AptitudeDto CompetenceTirArmesAFeu => GetAptitude(1616);
        public AptitudeDto CompetenceTirArmesDeJet => GetAptitude(1610);
        public AptitudeDto CompetenceTirArmesMecaniques => GetAptitude(1606);
        public AptitudeDto CompetenceTirLancePierres => GetAptitude(1608);
        public AptitudeDto CompetenceGroupeExplosifs => GetAptitude(1617);
        public AptitudeDto CompetenceGroupeTir => GetAptitude(1620);
        public AptitudeDto CompetenceMetierArquebusier => GetAptitude(1059);
        public AptitudeDto TalentAdresseAuTir => GetTalent(2004);
        public AptitudeDto TalentRechergementRapide => GetTalent(2067);
        public AptitudeDto TalentTirDePrecision => GetTalent(2088);
        public AptitudeDto TalentTirEnPuissance => GetTalent(2089);
        public AptitudeDto TalentMaitreArtilleur => GetTalent(2049);
        public AptitudeDto TalentTirEclair => GetTalent(2220);

        // De l'ombre
        public AptitudeDto CompetenceAlphSecretVoleurs => GetAptitude(1089);
        public AptitudeDto CompetenceDeplacementSilencieux => GetAptitude(1019);
        public AptitudeDto CompetenceDissimulation => GetAptitude(1020);
        public AptitudeDto CompetenceFouille => GetAptitude(1032);
        public AptitudeDto CompetencePerception => GetAptitude(1048);
        public AptitudeDto CompetenceEscalade => GetAptitude(1024);
        public AptitudeDto CompetenceCrochetage => GetAptitude(1016);
        public AptitudeDto CompetenceDeguisement => GetAptitude(1018);
        public AptitudeDto CompetenceEscamotage => GetAptitude(1025);
        public AptitudeDto CompetencePreparationDePoisons => GetAptitude(1050);
        public AptitudeDto CompetenceLectureSurLesLevres => GetAptitude(1041);
        public AptitudeDto TalentConnaissanceDesPieges => GetTalent(2016);
        public AptitudeDto TalentCamouflageRural => GetTalent(2008);
        public AptitudeDto TalentCamouflageSouterrain => GetTalent(2009);
        public AptitudeDto TalentCamouflageUrbain => GetTalent(2010);
        public AptitudeDto TalentCodeDeLaRue => GetTalent(2013);
        public AptitudeDto TalentImitation => GetTalent(2036);
        public AptitudeDto TalentSensAiguises => GetTalent(2080);
        public AptitudeDto TalentAccuiteAuditive => GetTalent(2002);
        public AptitudeDto TalentAccuiteGustativeEtOlfactive => GetTalent(2209);
        public AptitudeDto TalentAccuiteVisuelle => GetTalent(2003);
        public AptitudeDto TalentFilature => GetTalent(2020);
        public AptitudeDto TalentPistage => GetTalent(2049);

        // Sociales ( + TalentCodeDeLaRue)
        public AptitudeDto CompetenceBaratin => GetAptitude(1004);
        public AptitudeDto CompetenceCharisme => GetAptitude(1008);
        public AptitudeDto CompetenceCommandement => GetAptitude(1009);
        public AptitudeDto CompetenceCommérage => GetAptitude(1010);
        public AptitudeDto CompetenceIntimidation => GetAptitude(1034);
        public AptitudeDto TalentEloquence => GetTalent(2027);
        public AptitudeDto TalentOrateurNe => GetTalent(2064);
        public AptitudeDto TalentPolitique => GetTalent(2174);
        public AptitudeDto TalentEtiquette => GetTalent(2028);
        public AptitudeDto TalentIntriguant => GetTalent(2040);

        // Commerce  + TalentCalculMental
        public AptitudeDto CompetenceMarchandage => GetAptitude(1043);
        public AptitudeDto CompetenceMetierMarchand => GetAptitude(1078);
        public AptitudeDto CompetenceEvaluation => GetAptitude(1179);
        public AptitudeDto CompetenceExpressionArtistiqueConteur => GetAptitude(1123);
        public AptitudeDto TalentDurEnAffaires => GetTalent(2025);

        // Cavalerie  + AptitudeMetierVendeurDeChevaux, TalentMaitriseArmesDeCavalerie
        public AptitudeDto CompetenceEquitation => GetAptitude(1023);
        public AptitudeDto CompetenceEquitationCochonDeGuerre => GetAptitude(1152);
        public AptitudeDto CompetenceExpressionArtistiqueAcrobatEquestre => GetAptitude(1153);
        public AptitudeDto CompetenceEmpriseSurLesAnimaux => GetAptitude(1022);
        public AptitudeDto CompetenceSoinsDesAnimaux => GetAptitude(1054);
        public AptitudeDto CompetenceMetierGarconDEcurie => GetAptitude(1180);
        public AptitudeDto CompetenceDressage => GetAptitude(1021);
        public AptitudeDto TalentAcrobateEquestre => GetTalent(2001);

        // Artisanat  + AptitudePreparationDePoisons, AptitudeEvaluation
        public AptitudeDto CompetenceLangageSecretGuilde => GetAptitude(1158);
        public AptitudeDto CompetenceGroupeMetier => GetAptitude(1044);
        public AptitudeDto CompetenceMetierDeuxAuChoix => GetAptitude(1159);
        public AptitudeDto CompetenceMetierTroisAuChoix => GetAptitude(1172);
        public AptitudeDto CompetenceConnaissancesAcademiquesArts => GetAptitude(1102);
        public AptitudeDto CompetenceConnaissancesAcademiquesIngénierie => GetAptitude(1108);
        public AptitudeDto CompetenceConnaissancesAcademiquesRunes => GetAptitude(1112);
        public AptitudeDto CompetenceConnaissancesAcademiquesSciences => GetAptitude(1113);
        public AptitudeDto CompetenceLangageMystiqueNain => GetAptitude(1003);
        public AptitudeDto TalentRuneDeuxAuChoix => GetTalent(2170);
        public AptitudeDto TalentRuneSixAuChoix => GetTalent(2171);
        public AptitudeDto TalentRuneDixAuChoix => GetTalent(2173);
        public AptitudeDto TalentRuneMajeureDeuxAuChoix => GetTalent(2172);
        public AptitudeDto CompetenceCreationDeRunes => GetAptitude(1006);
        public AptitudeDto TalentSavoirFaireNain => GetTalent(2078);
        public AptitudeDto TalentTalentArtistique => GetTalent(2086);

        // Rôdeurs  + AptitudeDeplacementSilencieux, AptitudeDissimulation, AptitudeEmpriseSurLesAnimaux, AptitudeGroupeLangue
        //          , AptitudePerception, AptitudeFouille, AptitudeEscalade, TalentLinguistique, TalentConnaissanceDesPieges
        public AptitudeDto CompetenceBraconnage => GetAptitude(105);
        public AptitudeDto CompetenceAlphabetSecretPisteurs => GetAptitude(1086);
        public AptitudeDto CompetenceLangageSecretRodeurs => GetAptitude(1087);
        public AptitudeDto CompetenceLangageSecretTroisAuChoix => GetAptitude(1038);
        public AptitudeDto CompetenceOrientation => GetAptitude(1047);
        public AptitudeDto CompetenceMetierCartographe => GetAptitude(1063);
        public AptitudeDto CompetenceSurvie => GetAptitude(1055);
        public AptitudeDto CompetencePistage => GetAptitude(1049);
        public AptitudeDto TalentSensDeLOrientation => GetTalent(2081);
        public AptitudeDto TalentGrandVoyageur => GetTalent(2033);
        public AptitudeDto TalentSixiemeSens => GetTalent(2082);

        // Maritimes  + AptitudeOrientation, AptitudeMetierCartographe, TalentGrandVoyageur
        public AptitudeDto CompetenceCanotage => GetAptitude(1007);
        public AptitudeDto CompetenceNatation => GetAptitude(1045);
        public AptitudeDto CompetenceNavigation => GetAptitude(1046);
        public AptitudeDto CompetenceEruditionAstronomie => GetAptitude(1103);
        public AptitudeDto CompetenceEruditionPotamologie => GetAptitude(1189);
        public AptitudeDto CompetenceMetierCharpentierNaval => GetAptitude(1065);

        // Poudre noire  + AptitudeMetierArquebusier, TalentMaitriseArmesAFeu

        // Ami des bêtes  + AptitudeMetierGarconDEcurie
        public AptitudeDto CompetenceMetierVendeurDeChevaux => GetAptitude(1177);
        public AptitudeDto CompetenceMetierMaitreChien => GetAptitude(1178);
        public AptitudeDto CompetenceMetierFauconnerie => GetAptitude(1179);
        public AptitudeDto CompetenceConnaissancesAcademiquesZoologie => GetAptitude(1188);
        public AptitudeDto CompetenceMetierFermier => GetAptitude(1074);
        public AptitudeDto CompetenceConduiteDAttelage => GetAptitude(1012);

        public AptitudeDto CompetenceIntuition => GetAptitude(1210);
        public AptitudeDto CompetenceSangFroid => GetAptitude(1219);
        public AptitudeDto CompetenceResistance => GetAptitude(1220);
        public AptitudeDto CompetenceAthletisme => GetAptitude(1209);
        public AptitudeDto CompetenceSoins => GetAptitude(1053);
        public AptitudeDto CompetenceMetierApothicaire => GetAptitude(1058);

        public AptitudeDto TalentChirurgie => GetTalent(2012);
        public AptitudeDto TalentChance => GetTalent(2011);
        public AptitudeDto TalentResistanceALaMagie => GetTalent(2070);
        public AptitudeDto TalentResistanceAuxMaladies => GetTalent(2072);
        public AptitudeDto TalentResistanceAuxPoisons => GetTalent(2073);
        public AptitudeDto TalentSainDEsprit => GetTalent(2075);
        public AptitudeDto TraitVisionNocturne => GetTrait(3903);
        public AptitudeDto TalentSensAiguisés => GetTalent(2080);

        public AptitudeDto CompetencePriere => GetAptitude(1225);
        public AptitudeDto TalentBenediction => GetTalent(2277);
        public AptitudeDto TalentInspirationDivine => GetTalent(2278);

        #endregion

        public AptitudeDto[] RechercheAptitudes(string searchText)
        {
            searchText = GenericService.ConvertirCaracteres(searchText);
            var motsClefRecherches = GenericService.MotsClefsDeRecherche(searchText);

            return AllAptitudes
                .Where(c => c.Ignore == false && (
                            c.NomPourRecherche.Contains(searchText) || c.MotsClefDeRecherche.Intersect(motsClefRecherches).Any()
                    )
                )
                .OrderByDescending(c => c.MotsClefDeRecherche.Intersect(motsClefRecherches).Count())
                .ToArray();
        }

        public List<AptitudeDto> TalentsInitiaux => new List<AptitudeDto> {
             TalentAccuiteAuditive,
             TalentAccuiteGustativeEtOlfactive,
             TalentAccuiteVisuelle,
             TalentAmbidextrie,
             TalentCalculMental,
             TalentChance,
             TalentRobuste,
             TalentSainDEsprit,
             TalentImitation,
             TalentResistanceALaMagie,
             TalentResistanceAuxMaladies,
             TalentResistanceAuxPoisons,
             TalentSixiemeSens,
             /*
             TraitCourseAPied,
             TraitHabileDeSesMains,    
             TraitDurACuir,
             TraitForceAccrue,
             TraitGuerrierNe,
             TraitIntelligent,
             TraitReflexesEclairs,
             TraitResistanceAccrue,
             TraitSangFroid,
             TraitSociable,
             TraitTireurDElite,
             //TraitVisionNocturne,
             TraitVivacite
             */
        };

        #region Traits

        public List<AptitudeDto> SignesDistinctifs => AllTraits.Where(t => t.CategSpe == "trait").OrderBy(t => t.NomComplet).ToList();
        public List<AptitudeDto> Folies => AllTraits.Where(t => t.CategSpe == "folie").ToList();
        public List<AptitudeDto> Maladies => AllTraits.Where(t => t.CategSpe == "maladie").ToList();
        public List<AptitudeDto> Mutations => AllTraits.Where(t => t.CategSpe == "mutation").ToList();
        public List<AptitudeDto> Nevroses => AllTraits.Where(t => t.CategSpe == "nevrose").ToList();
        public List<AptitudeDto> Addictions => AllTraits.Where(t => t.CategSpe == "addiction").ToList();
        public List<AptitudeDto> Alergies => AllTraits.Where(t => t.CategSpe == "allergie").ToList();
        public List<AptitudeDto> Phobies => AllTraits.Where(t => t.CategSpe == "phobie").ToList();
        public List<AptitudeDto> Conditions => AllTraits.Where(t => t.CategSpe == "condition").OrderBy(t => t.NomComplet).ToList();

        public AptitudeDto ConditionSurpris => GetTrait(303460);
        public AptitudeDto ConditionDemoralise => GetTrait(303453);
        public AptitudeDto ConditionATerre => GetTrait(303458);
        public AptitudeDto ConditionEtourdi => GetTrait(303459);
        public AptitudeDto ConditionInconscient => GetTrait(303461);

        public AptitudeDto TraitPsychoHaine => GetTrait(303217);
        public AptitudeDto TraitPsychoAnimosite => GetTrait(303215);
        public AptitudeDto TraitEffrayant => GetTrait(303199);

        public List<AptitudeDto> TroublesMineurs()
        {
            var list = new List<AptitudeDto>();
            list.AddRange(Alergies.Where(t => t.Severite == 1));
            list.AddRange(Nevroses.Where(t => t.Severite == 1));
            list.AddRange(Phobies.Where(t => t.Severite == 1));
            return list.OrderBy(t => t.CategSpe).ThenBy(t => t.Nom).ToList();
        }

        public AptitudeDto TirerUnSigneAleatoire(List<AptitudeDto> traitsDejaObtenus)
        {
            AptitudeDto? ta = null;
            while (ta == null
                   || traitsDejaObtenus.Contains(ta)
                   || ta.Incompatibles.Intersect(traitsDejaObtenus).Any()
                   || traitsDejaObtenus.Any(to => to.Incompatibles.Contains(ta))
            )
            {
                var sd = SignesDistinctifs;
                var i = new Random().Next(0, sd.Count);
                ta = sd[i];
            }
            return ta;
        }

        #endregion
    }
}
