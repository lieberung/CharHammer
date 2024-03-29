﻿namespace CharHammer.Services;

public class AptitudesService
{
  private readonly IReadOnlyDictionary<int, AptitudeDto> _data;

  public AptitudesService(IReadOnlyDictionary<int, AptitudeDto> data)
  {
    _data = data;
    AllAptitudes = _data.Values.OrderBy(c => c.Nom).ThenBy(c => c.Spe).ToArray();

    AllCompetences = AllAptitudes.Where(a => a.EstUneCompetence);
    AllTalents = AllAptitudes.Where(a => a.EstUnTalent);
    AllTraits = AllAptitudes.Where(a => a.EstUnTrait).ToArray();

    AllTraitsRoot = AllTraits.Where(t => t is { Ignore: false, EstUnTrait: true, Parent: null });
    SignesDistinctifs = AllTraits.Where(t => t.EstUnSigneDistinctif);
    Folies = AllTraits.Where(t => t.CategSpe == "folie");
    Maladies = AllTraits.Where(t => t.CategSpe == "maladie");
    Mutations = AllTraits.Where(t => t.CategSpe == "mutation");
    Nevroses = AllTraits.Where(t => t.CategSpe == "nevrose");
    Addictions = AllTraits.Where(t => t.CategSpe == "addiction");
    Alergies = AllTraits.Where(t => t.CategSpe == "allergie");
    Phobies = AllTraits.Where(t => t.CategSpe == "phobie");
    Conditions = AllTraits.Where(t => t.CategSpe == "condition").OrderBy(t => t.NomComplet);
    TroublesMineurs = Alergies.Where(t => t.Severite == 1)
        .Union(Nevroses.Where(t => t.Severite == 1))
        .Union(Phobies.Where(t => t.Severite == 1));
  }

  public AptitudeDto GetAptitude(int id) => _data[id];
  public AptitudeDto GetTalent(int id) => _data[id];
  private AptitudeDto GetTrait(int id) => _data[id];

  public IEnumerable<AptitudeDto> AllAptitudes { get; }

  public IEnumerable<AptitudeDto> AllCompetences { get; }
  public IEnumerable<AptitudeDto> AllTalents { get; }
  private IEnumerable<AptitudeDto> AllTraits { get; }

  public IEnumerable<AptitudeDto> AllTraitsRoot { get; }
  public IEnumerable<AptitudeDto> SignesDistinctifs { get; }
  public IEnumerable<AptitudeDto> TroublesMineurs { get; }
  public IEnumerable<AptitudeDto> Folies { get; }
  public IEnumerable<AptitudeDto> Maladies { get; }
  public IEnumerable<AptitudeDto> Mutations { get; }
  private IEnumerable<AptitudeDto> Nevroses { get; }
  public IEnumerable<AptitudeDto> Addictions { get; }
  private IEnumerable<AptitudeDto> Alergies { get; }
  private IEnumerable<AptitudeDto> Phobies { get; }
  public IEnumerable<AptitudeDto> Conditions { get; }

  public IEnumerable<AptitudeDto> AllArmesSpecialisations => AllMeleeSpecialisations.Union(AllTirSpecialisations);

  public IEnumerable<AptitudeDto> AllMeleeSpecialisations =>
      CompetenceGroupeMelee.SousElements.Where(s => !s.Ignore).OrderBy(a => a.Spe);
  public IEnumerable<AptitudeDto> AllTirSpecialisations =>
      CompetenceGroupeTir.SousElements.Where(s => !s.Ignore).OrderBy(a => a.Spe);

  public bool DonneAccesADesArmes(AptitudeDto a)
      => a.Parent == CompetenceGroupeMelee || a.Parent == CompetenceGroupeTir || a == CompetenceGroupeExplosifs;
  public static bool DonneAccesADesSortileges(AptitudeDto a)
      => a.Parent?.Id is AptitudeGroupeInspirationDivineId or AptitudeGroupeScienceDeLaMagieId;

  public IEnumerable<AptitudeDto> RechercheAptitudes(string searchText)
  {
    searchText = GenericService.NettoyerPourRecherche(searchText);
    var motsClefRecherches = GenericService.MotsClefsDeRecherche(searchText);

    return AllAptitudes
        .Where(c => c.Ignore == false && (
                    c.NomPourRecherche.Contains(searchText) || c.MotsClefDeRecherche.Intersect(motsClefRecherches).Any()
            )
        )
        .OrderByDescending(c => c.MotsClefDeRecherche.Intersect(motsClefRecherches).Count());
  }

  //public IEnumerable<AptitudeDto> TalentsInitiaux => [
  //     TalentAccuiteAuditive,
  //     TalentAccuiteGustativeEtOlfactive,
  //     TalentAccuiteVisuelle,
  //     TalentAmbidextrie,
  //     TalentCalculMental,
  //     TalentChance,
  //     TalentRobuste,
  //     TalentSainDEsprit,
  //     TalentImitation,
  //     TalentResistanceALaMagie,
  //     TalentResistanceAuxMaladies,
  //     TalentResistanceAuxPoisons,
  //     TalentSixiemeSens,
  //     /*
  //     TraitCourseAPied,
  //     TraitHabileDeSesMains,    
  //     TraitDurACuir,
  //     TraitForceAccrue,
  //     TraitGuerrierNe,
  //     TraitIntelligent,
  //     TraitReflexesEclairs,
  //     TraitResistanceAccrue,
  //     TraitSangFroid,
  //     TraitSociable,
  //     TraitTireurDElite,
  //     //TraitVisionNocturne,
  //     TraitVivacite
  //     */
  //];

  #region Mélée et Tir

  public const int IdMeleeGroupe = 1600;
  public const int IdMeleeOrdinaires = 1618;
  public const int IdMeleeFleaux = 1614;
  public const int IdMeleeParade = 1607;
  public const int IdMeleeCavalerie = 1609;
  public const int IdMeleeParalysantes = 1605;
  public const int IdMeleeLourdes = 1612;
  public const int IdMeleeEscrime = 1611;
  public const int IdMeleeBagarre = 1624;

  public const int IdTirGroupe = 1620;
  public const int IdTirArbaletes = 1604;
  public const int IdTirArcs = 1615;
  public const int IdTirArmesAFeu = 1616;
  public const int IdTirArmesDeJet = 1610;
  public const int IdTirMecaniques = 1606;
  public const int IdTirLancePierres = 1608;
  public const int IdTirExplosifs = 1617;

  #endregion

  #region Aptitudes & Talents

  public const int AptitudeGroupeInspirationDivineId = 2278;
  public const int AptitudeGroupeScienceDeLaMagieId = 2079;

  public const int TraitDurACuirId = 3021;
  public const int AptitudeGroupeGabarit = 3830;

  public const int TraitGabaritMinusculeId = 3835;
  public const int TraitGabaritToutPetitId = 3837;
  public const int TraitGabaritPetitId = 3831;
  public const int TraitGabaritMoyenId = 3836;
  public const int TraitGabaritLargeId = 3832;
  public const int TraitGabaritEnormeId = 3833;
  public const int TraitGabaritMonstrueuxId = 3834;

  // Caractéristiques
  public AptitudeDto TraitGuerrierNe => GetTrait(3010);
  public AptitudeDto TraitTireurDElite => GetTrait(3011);
  public AptitudeDto TraitForceAccrue => GetTrait(3012);
  public AptitudeDto TraitResistanceAccrue => GetTrait(3013);
  public AptitudeDto TraitVivacite => GetTrait(3014);
  public AptitudeDto TraitHabileDeSesMains => GetTrait(3016);
  public AptitudeDto TraitReflexesEclairs => GetTrait(3015);
  public AptitudeDto TraitIntelligent => GetTrait(3017);
  public AptitudeDto TraitCalme => GetTrait(3018);
  public AptitudeDto TraitAffable => GetTrait(3019);

  public AptitudeDto TraitCourseAPied => GetTrait(3020);
  public AptitudeDto TraitDurACuir => GetTrait(TraitDurACuirId);

  public AptitudeDto TalentCostaud => GetTrait(2239);
  public AptitudeDto TalentRobuste => GetTalent(2074);

  // Académique
  public AptitudeDto CompetenceGroupeSavoir => GetAptitude(1013);
  public AptitudeDto CompetenceGroupeLangue => GetAptitude(1039);
  public AptitudeDto CompetenceSavoirDeuxAuChoix => GetAptitude(1151);
  public AptitudeDto CompetenceLireEcrire => GetAptitude(1042);
  public AptitudeDto TalentCalculMental => GetTalent(2007);
  public AptitudeDto TalentLinguistique => GetTalent(2042);

  // Arcanique
  public AptitudeDto CompetenceConnaissanceAcademiqueEsprits => GetAptitude(1141);
  public AptitudeDto CompetenceConnaissanceAcademiqueMagie => GetAptitude(1109);
  public AptitudeDto CompetenceConnaissanceAcademiqueNecromancie => GetAptitude(1110);
  public AptitudeDto CompetenceFocalisation => GetAptitude(1031);
  public AptitudeDto CompetenceLangageMystique => GetAptitude(1036);
  public AptitudeDto CompetenceLangageMystiqueMagick => GetAptitude(1162);
  public AptitudeDto CompetenceScienceDeLaMagie => GetAptitude(AptitudeGroupeScienceDeLaMagieId);

  public AptitudeDto InspirationDivineManann => GetAptitude(2281);
  public AptitudeDto InspirationDivineMorr => GetAptitude(2282);
  public AptitudeDto InspirationDivineMyrmidia => GetAptitude(2280);
  public AptitudeDto InspirationDivineRanald => GetAptitude(2283);
  public AptitudeDto InspirationDivineRhya => GetAptitude(2284);
  public AptitudeDto InspirationDivineShallya => GetAptitude(2285);
  public AptitudeDto InspirationDivineSigmar => GetAptitude(2286);
  public AptitudeDto InspirationDivineTaal => GetAptitude(2287);
  public AptitudeDto InspirationDivineUlric => GetAptitude(2288);
  public AptitudeDto InspirationDivineVerena => GetAptitude(2289);

  public AptitudeDto CompetenceLangageMystiqueDemoniaque => GetAptitude(1170);
  public AptitudeDto CompetenceLangageMystiqueElfeMystique => GetAptitude(1171);
  public AptitudeDto CompetenceLangueClassique => GetAptitude(1142);
  public AptitudeDto CompetenceSensDeLaMagie => GetAptitude(1052);
  public AptitudeDto TalentHarmonieAethyrique => GetTalent(2035);
  public AptitudeDto TalentMainsAgiles => GetTalent(2048);
  public AptitudeDto TalentMeditation => GetTalent(2063);
  public AptitudeDto TalentProjectilePuissant => GetTalent(2066);
  public AptitudeDto TalentMagieMineureVulgaire => GetTalent(2163);
  public AptitudeDto TalentMagieArcaniqueCommune => GetTalent(2151);
  public AptitudeDto TalentTraditionMagieNaturelle => GetTalent(2189);
  public AptitudeDto TalentTraditionSorcellerie => GetTalent(2164);
  public AptitudeDto TalentMagieNoireDemonologie => GetTalent(2046);
  public AptitudeDto TalentMagieNoireNecromancie => GetTalent(2459);
  public AptitudeDto TalentMagieCommuneVulgaire => GetTalent(2163);

  // Martial
  public AptitudeDto CompetenceLangSecretBataille => GetAptitude(1148);
  public AptitudeDto TalentAmbidextrie => GetTalent(2005);
  public AptitudeDto TalentCoupsPrécis => GetTalent(2020);
  public AptitudeDto TalentSurSesGardes => GetTalent(2085);
  public AptitudeDto TalentTroublant => GetTalent(2091);
  public AptitudeDto TalentMaitriseUneAuChoix => GetTalent(2153);
  public AptitudeDto TalentReflexesDeCombat => GetTalent(TalentReflexesDeCombatId);
  public static int TalentReflexesDeCombatId => 2218;

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
  public AptitudeDto TalentFrenesie => GetTalent(2020);
  public AptitudeDto TalentParadeEclair => GetTalent(2065);
  public AptitudeDto TalentLutte => GetTalent(2043);
  public AptitudeDto TraitValeureux => GetTalent(3906);
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
  public AptitudeDto TalentFrappeReactive => GetTalent(2213);
  public AptitudeDto TalentRetournement => GetTalent(2242);

  public AptitudeDto CompetenceMeleeArmesDEscrime => GetAptitude(IdMeleeEscrime);
  public AptitudeDto CompetenceMeleeArmesDeCavalerie => GetAptitude(IdMeleeCavalerie);
  public AptitudeDto CompetenceMeleeArmesDeParade => GetAptitude(IdMeleeParade);
  public AptitudeDto CompetenceMeleeArmesLourdes => GetAptitude(IdMeleeLourdes);
  public AptitudeDto CompetenceMeleeArmesParalisantes => GetAptitude(IdMeleeParalysantes);
  public AptitudeDto CompetenceMeleeFléaux => GetAptitude(IdMeleeFleaux);
  public AptitudeDto CompetenceMeleeBagarre => GetAptitude(IdMeleeBagarre);

  // Martial Distance
  public AptitudeDto CompetenceTirArbaletes => GetAptitude(IdTirArbaletes);
  public AptitudeDto CompetenceTirArcs => GetAptitude(IdTirArcs);
  public AptitudeDto CompetenceTirArmesAFeu => GetAptitude(IdTirArmesAFeu);
  public AptitudeDto CompetenceTirArmesDeJet => GetAptitude(IdTirArmesDeJet);
  public AptitudeDto CompetenceTirArmesMecaniques => GetAptitude(IdTirMecaniques);
  public AptitudeDto CompetenceTirLancePierres => GetAptitude(IdTirLancePierres);
  public AptitudeDto CompetenceGroupeExplosifs => GetAptitude(IdTirExplosifs);
  public AptitudeDto CompetenceGroupeTir => GetAptitude(1620);
  public AptitudeDto CompetenceMetierArquebusier => GetAptitude(1059);
  public AptitudeDto TalentAdresseAuTir => GetTalent(2004);
  public AptitudeDto TalentRechergementRapide => GetTalent(2067);
  public AptitudeDto TalentTirDePrecision => GetTalent(2088);
  public AptitudeDto TalentTirEnPuissance => GetTalent(2089);
  public AptitudeDto TalentMaitreArtilleur => GetTalent(2049);
  public AptitudeDto TalentTirEclair => GetTalent(2220);
  public AptitudeDto TalentTirParfaits => GetTalent(3920);

  // De l'ombre
  public AptitudeDto CompetenceAlphSecretVoleurs => GetAptitude(1089);
  public AptitudeDto CompetenceDissimulation => GetAptitude(1020);
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
  public AptitudeDto TalentAffiniteAnimale => GetTalent(2298);
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
  public AptitudeDto CompetenceSavoirArts => GetAptitude(1102);
  public AptitudeDto CompetenceSavoirIngénierie => GetAptitude(1108);
  public AptitudeDto CompetenceSavoirRunes => GetAptitude(1112);
  public AptitudeDto CompetenceSavoirSciences => GetAptitude(1113);
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
  public AptitudeDto TalentSixiemeSens => GetTalent(3907);

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

  public AptitudeDto TalentDictionInstinctive => GetAptitude(2226);
  public AptitudeDto TalentMagieArcaniqueMetal => GetAptitude(2455);
  public AptitudeDto TalentMagieArcaniqueBete => GetAptitude(2451);
  public AptitudeDto TalentMagieArcaniqueFeu => GetAptitude(2452);
  public AptitudeDto TalentMagieArcaniqueCieux => GetAptitude(2453);
  public AptitudeDto TalentMagieArcaniqueLumiere => GetAptitude(2457);
  public AptitudeDto TalentMagieArcaniqueOmbres => GetAptitude(2456);
  public AptitudeDto TalentMagieArcaniqueMort => GetAptitude(2454);
  public AptitudeDto TalentMagieArcaniqueVie => GetAptitude(2458);
  public AptitudeDto TalentSombreSavoirSlaanesh => GetAptitude(2179);
  public AptitudeDto TalentSombreSavoirTzeench => GetAptitude(2180);
  public AptitudeDto TalentSombreSavoirNurgle => GetAptitude(2178);

  public AptitudeDto CompetencePriere => GetAptitude(1225);
  public AptitudeDto TalentBenediction => GetTalent(2277);
  public AptitudeDto TalentInspirationDivine => GetTalent(AptitudeGroupeInspirationDivineId);

  #endregion

  #region Traits

  public AptitudeDto ConditionSurpris => GetTrait(3460);
  public AptitudeDto ConditionDemoralise => GetTrait(3453);
  public AptitudeDto ConditionATerre => GetTrait(3458);
  public AptitudeDto ConditionEtourdi => GetTrait(3459);
  public AptitudeDto ConditionInconscient => GetTrait(3461);
  public AptitudeDto ConditionExtenue => GetTrait(3456);

  public AptitudeDto TraitPsychoHaine => GetTrait(3217);
  public AptitudeDto TraitPsychoAnimosite => GetTrait(3215);
  public AptitudeDto TraitEffrayant => GetTrait(3199);

  #endregion
}
