/*
namespace CharHammer.Services;

public class CarrieresScoreService(CarriereDto[] carrieres, AptitudesService aptitudesService)
{ 
  private void InitializeScores()
  {
      foreach (var carriere in carrieres)
      {
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
          if (carriere.TalentsPourScore.Any(t => t == aptitudesService.TalentTireurDElite))
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
          if (carriere.TalentsPourScore.Any(t => t == aptitudesService.TalentIntelligent))
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
          if (carriere.TalentsPourScore.Any(t => t == aptitudesService.TalentSangFroid))
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
          if (carriere.TalentsPourScore.Any(t => t == aptitudesService.TalentReflexesEclairs))
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
          if (carriere.TalentsPourScore.Any(t => t == aptitudesService.TalentSociable))
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
              c == aptitudesService.CompetenceLangSecretBataille) * 2;

          if (carriere.TalentsPourScore.Contains(aptitudesService.TalentMaitriseUneAuChoix))
              score += 3;
          if (carriere.TalentsPourScore.Contains(aptitudesService.TalentMaitriseDeuxAuChoix))
              score += 6;

          score += carriere.TalentsPourScore.Count(c
              => c == aptitudesService.TalentAmbidextrie
                 || c == aptitudesService.TalentCoupsPrécis
                 || c == aptitudesService.TalentDurACuir
                 || c == aptitudesService.TalentSurSesGardes
                 || c == aptitudesService.TalentValeureux
                 || c == aptitudesService.TalentTroublant
                 || c == aptitudesService.TalentForceAccrue
          ) * 2;

          score += carriere.PlanDeCarriere.A * 4;

          var force = carriere.PlanDeCarriere.F;
          if (carriere.TalentsPourScore.Any(t => t == aptitudesService.TalentForceAccrue))
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
              c == aptitudesService.CompetenceEsquive ||
              c.Parent == aptitudesService.CompetenceGroupeMelee
          ) * 2;

          score += carriere.TalentsPourScore.Count(c =>
              c == aptitudesService.TalentCombatADeuxArmes
              || c == aptitudesService.TalentDesarmement
              || c == aptitudesService.TalentFrenesie
              || c == aptitudesService.TalentCombattantVirevoltant
              || c == aptitudesService.TalentCoupsAssomants
              || c == aptitudesService.TalentCoupsPuissants
              || c == aptitudesService.TalentCoupsAuBut
              || c == aptitudesService.TalentCombatDeRue
              || c == aptitudesService.TalentDurACuir
              || c == aptitudesService.TalentGuerrierNe
              || c == aptitudesService.TalentParadeEclair
              || c == aptitudesService.TalentLutte
              || c == aptitudesService.TalentResistanceAccrue
              || c == aptitudesService.TalentRobuste
              || c == aptitudesService.TalentDechainement
              || c == aptitudesService.TalentPresenceImposante
              || c == aptitudesService.TalentTueur
              || c.Parent == aptitudesService.TalentGroupeVertu
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
              c == aptitudesService.CompetenceMetierArquebusier ||
              c.Parent == aptitudesService.CompetenceGroupeTir
          ) * 2;

          score += carriere.TalentsPourScore.Count(c
              => c == aptitudesService.TalentRechergementRapide
                 || c == aptitudesService.TalentAdresseAuTir
                 || c == aptitudesService.TalentTirDePrecision
                 || c == aptitudesService.TalentTirEnPuissance
                 || c == aptitudesService.TalentMaitreArtilleur
          ) * 2;

          score += CalculBonusCapaciteDeTir(carriere) * 3;

          return score;
      }

      private int CalculScoreCavalerie(CarriereDto carriere)
      {
          if (!carriere.CompetencesPourScore.Contains(aptitudesService.CompetenceEquitation))
              return 0;

          var score = 0;

          score += carriere.CompetencesPourScore.Count(c
              => c == aptitudesService.CompetenceExpressionArtistiqueAcrobatEquestre
          ) * 4;

          score += carriere.CompetencesPourScore.Count(c
              => c == aptitudesService.CompetenceEmpriseSurLesAnimaux
              || c == aptitudesService.CompetenceSoinsDesAnimaux
              || c == aptitudesService.CompetenceMetierGarconDEcurie
              || c == aptitudesService.CompetenceMetierVendeurDeChevaux
              || c == aptitudesService.CompetenceDressage
              || c == aptitudesService.CompetenceMeleeArmesDeCavalerie
          ) * 2;

          score += carriere.TalentsPourScore.Count(c
              => c == aptitudesService.TalentMaitriseUneAuChoix
              || c == aptitudesService.TalentMaitriseDeuxAuChoix
              || c == aptitudesService.TalentAcrobateEquestre
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
              => c == aptitudesService.CompetenceConnaissanceAcademiqueMagie
                 || c == aptitudesService.CompetenceConnaissanceAcademiqueEsprits
                 || c == aptitudesService.CompetenceConnaissanceAcademiqueNecromancie
                 || c == aptitudesService.CompetenceLangueClassique
          ) * 1;

          score += carriere.CompetencesPourScore.Count(c
              => c == aptitudesService.CompetenceFocalisation
                 || c == aptitudesService.CompetenceLangageMystique
                 || c == aptitudesService.CompetenceLangageMystiqueMagick
                 || c == aptitudesService.CompetenceLangageMystiqueDemoniaque
                 || c == aptitudesService.CompetenceLangageMystiqueElfeMystique
                 || c == aptitudesService.CompetenceSensDeLaMagie
          ) * 2;

          score += carriere.TalentsPourScore.Count(c
              => c == aptitudesService.TalentHarmonieAethyrique
                 || c == aptitudesService.TalentMainsAgiles
                 || c == aptitudesService.TalentMeditation
                 || c == aptitudesService.TalentProjectilePuissant
                 || c == aptitudesService.TalentMagieVulgaire
                 || c == aptitudesService.TalentMagieNoire
                 || c.Parent != null && (
                     c.Parent == aptitudesService.TalentGroupeMagieCommune
                     || c.Parent == aptitudesService.TalentGroupeMagieMineure)
          ) * 2;

          score += CalculBonusIntelligence(carriere);
          score += CalculBonusForceMentale(carriere);

          return score;
      }

      private int CalculScoreArtisanat(CarriereDto carriere)
      {
          var score = 0;

          score += carriere.CompetencesPourScore.Count(c
              => c == aptitudesService.CompetenceEvaluation
                 || c == aptitudesService.CompetenceLangageSecretGuilde
                 || c.Parent == aptitudesService.CompetenceGroupeMetier
                 || c == aptitudesService.CompetenceConnaissancesAcademiquesArts
                 || c == aptitudesService.CompetenceConnaissancesAcademiquesIngénierie
                 || c == aptitudesService.CompetenceConnaissancesAcademiquesRunes
                 || c == aptitudesService.CompetenceConnaissancesAcademiquesSciences
                 || c == aptitudesService.CompetencePreparationDePoisons
                 || c == aptitudesService.CompetenceCreationDeRunes
                 || c == aptitudesService.CompetenceLangageMystiqueNain
          ) * 2;

          if (carriere.CompetencesPourScore.Contains(aptitudesService.CompetenceMetierDeuxAuChoix))
              score += 2;
          if (carriere.CompetencesPourScore.Contains(aptitudesService.CompetenceMetierTroisAuChoix))
              score += 4;

          score += carriere.TalentsPourScore.Count(c
              => c == aptitudesService.TalentSavoirFaireNain
                 || c == aptitudesService.TalentTalentArtistique
          ) * 2;

          if (carriere.TalentsPourScore.Contains(aptitudesService.TalentRuneDeuxAuChoix))
              score += 4;
          if (carriere.TalentsPourScore.Contains(aptitudesService.TalentRuneSixAuChoix))
              score += 5;
          if (carriere.TalentsPourScore.Contains(aptitudesService.TalentRuneDixAuChoix))
              score += 6;
          if (carriere.TalentsPourScore.Contains(aptitudesService.TalentRuneMajeureDeuxAuChoix))
              score += 7;

          score += CalculBonusIntelligence(carriere);
          score += CalculBonusAgilite(carriere) / 2;

          return score;
      }

      private int CalculScoreAcademique(CarriereDto carriere)
      {
          var score = 0;
          score += carriere.CompetencesPourScore.Count(c
              => c.Parent == aptitudesService.CompetenceGroupeConnaissancesAcademiques
              || c.Parent == aptitudesService.CompetenceGroupeLangue
              || c == aptitudesService.CompetenceLireEcrire
          );
          if (carriere.CompetencesPourScore.Any(c => c == aptitudesService.CompetenceConnaissancesAcademiquesDeuxAuChoix))
              score += 1;
          if (carriere.CompetencesPourScore.Any(c => c == aptitudesService.CompetenceConnaissancesAcademiquesTroisAuChoix))
              score += 2;

          if (score < 2)
              return 0;

          score += (carriere.CompetencesPourScore.Count(c => c.Parent == aptitudesService.CompetenceGroupeConnaissancesGenerales) + 1) / 2;

          score += carriere.TalentsPourScore.Count(c
              => c == aptitudesService.TalentCalculMental
              || c == aptitudesService.TalentLinguistique
          );

          score += CalculBonusIntelligence(carriere) * 2;

          return score;
      }

      private int CalculScoreCommerce(CarriereDto carriere)
      {
          var score = 0;

          score += carriere.CompetencesPourScore.Count(c
              => c == aptitudesService.CompetenceMarchandage
              || c == aptitudesService.CompetenceMetierMarchand
              || c == aptitudesService.CompetenceMetierVendeurDeChevaux
          ) * 2;
          if (score == 0)
              return 0;

          score += carriere.CompetencesPourScore.Count(c
              => c == aptitudesService.CompetenceEvaluation
                 || c == aptitudesService.CompetenceBaratin
                 || c == aptitudesService.CompetenceLireEcrire
                 || c == aptitudesService.CompetenceExpressionArtistiqueConteur
          );

          score += carriere.TalentsPourScore.Count(c
              => c == aptitudesService.TalentCalculMental
                 || c == aptitudesService.TalentDurEnAffaires
          );

          score += CalculBonusIntelligence(carriere);
          score += CalculBonusSociabilite(carriere);

          return score;
      }

      private int CalculScoreDeLOmbre(CarriereDto carriere)
      {
          var score = 0;
          score += carriere.CompetencesPourScore.Count(c
              => c == aptitudesService.CompetenceAlphSecretVoleurs
                 || c == aptitudesService.CompetenceDeplacementSilencieux
                 || c == aptitudesService.CompetenceDissimulation
                 || c == aptitudesService.CompetenceFouille
                 || c == aptitudesService.CompetencePerception
                 || c == aptitudesService.CompetenceEscalade
                 || c == aptitudesService.CompetenceCrochetage
                 || c == aptitudesService.CompetenceDeguisement
                 || c == aptitudesService.CompetenceEscamotage
                 || c == aptitudesService.CompetenceLectureSurLesLevres
                 || c == aptitudesService.CompetencePreparationDePoisons
          );
          if (score < 2)
              return 0;

          score += carriere.TalentsPourScore.Count(c
              => c == aptitudesService.TalentConnaissanceDesPieges
                 || c == aptitudesService.TalentCamouflageRural
                 || c == aptitudesService.TalentCamouflageSouterrain
                 || c == aptitudesService.TalentCamouflageUrbain
                 || c == aptitudesService.TalentCodeDeLaRue
                 || c == aptitudesService.TalentImitation
                 || c == aptitudesService.TalentSensAiguises
                 || c == aptitudesService.TalentAccuiteAuditive
                 || c == aptitudesService.TalentAccuiteVisuelle
                 || c == aptitudesService.TalentFilature
                 || c == aptitudesService.TalentPistage
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
              => c == aptitudesService.CompetenceAlphabetSecretPisteurs
              || c == aptitudesService.CompetenceLangageSecretRodeurs
              || c == aptitudesService.CompetenceLangageSecretTroisAuChoix
              || c == aptitudesService.CompetenceSurvie
              || c == aptitudesService.CompetencePistage
          ) * 4;

          score += carriere.CompetencesPourScore.Count(c
              => c == aptitudesService.CompetenceBraconnage
                 || c == aptitudesService.CompetenceDeplacementSilencieux
                 || c == aptitudesService.CompetenceDissimulation
                 || c == aptitudesService.CompetenceEmpriseSurLesAnimaux
                 || c == aptitudesService.CompetenceNatation
                 || c == aptitudesService.CompetenceOrientation
                 || c == aptitudesService.CompetencePerception
                 || c == aptitudesService.CompetenceEscalade
          ) * 2;

          score += carriere.CompetencesPourScore.Count(c
              => c.Parent == aptitudesService.CompetenceGroupeConnaissancesGenerales
              || c.Parent == aptitudesService.CompetenceGroupeLangue
              || c == aptitudesService.CompetenceFouille
              || c == aptitudesService.CompetenceMetierCartographe
              || c == aptitudesService.CompetenceSoinsDesAnimaux
              || c == aptitudesService.CompetenceTirArcsLongs
              || c == aptitudesService.CompetenceMeleeArmesParalisantes
          );

          score += carriere.TalentsPourScore.Count(c
             => c == aptitudesService.TalentSensAiguises
             || c == aptitudesService.TalentAccuiteAuditive
             || c == aptitudesService.TalentAccuiteVisuelle
             || c == aptitudesService.TalentSensDeLOrientation
             || c == aptitudesService.TalentCamouflageRural
             || c == aptitudesService.TalentGrandVoyageur
             || c == aptitudesService.TalentLinguistique
             || c == aptitudesService.TalentConnaissanceDesPieges
             || c == aptitudesService.TalentSixiemeSens
             || c == aptitudesService.TalentTireurDElite
          ) * 2;

          if (carriere.TalentsPourScore.Contains(aptitudesService.TalentCourseAPied))
              score += 4;

          score += CalculBonusIntelligence(carriere) / 2;
          score += CalculBonusAgilite(carriere) / 2;

          return score;
      }

      private int CalculScoreSocial(CarriereDto carriere)
      {
          var score = 0;

          score += carriere.CompetencesPourScore.Count(c
              => c == aptitudesService.CompetenceBaratin
                 || c == aptitudesService.CompetenceCharisme
                 || c == aptitudesService.CompetenceCommandement
                 || c == aptitudesService.CompetenceCommérage
                 || c == aptitudesService.CompetenceIntimidation
          );

          score += carriere.TalentsPourScore.Count(c
              => c == aptitudesService.TalentEloquence
                 || c == aptitudesService.TalentOrateurNe
                 || c == aptitudesService.TalentPolitique
                 || c == aptitudesService.TalentCodeDeLaRue
                 || c == aptitudesService.TalentEtiquette
                 || c == aptitudesService.TalentIntriguant
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
              => c == aptitudesService.CompetenceCanotage
                 || c == aptitudesService.CompetenceNavigation))
              return 0;

          var score = 0;
          score += carriere.CompetencesPourScore.Count(c
              => c == aptitudesService.CompetenceCanotage
              || c == aptitudesService.CompetenceNatation
              || c == aptitudesService.CompetenceNavigation
              || c == aptitudesService.CompetenceEruditionAstronomie
              || c == aptitudesService.CompetenceEruditionPotamologie
              || c == aptitudesService.CompetenceOrientation
              || c == aptitudesService.CompetenceMetierCartographe
              || c == aptitudesService.CompetenceMetierCharpentierNaval
          ) * 2;

          score += carriere.TalentsPourScore.Count(c
              => c == aptitudesService.TalentGrandVoyageur
              || c == aptitudesService.TalentSensDeLOrientation
          ) * 2;

          return score;
      }

      private int CalculScorePoudreNoire(CarriereDto carriere)
      {
          if (!carriere.CompetencesPourScore.Any(c
             => c == aptitudesService.CompetenceTirArmesAFeu
             || c == aptitudesService.CompetenceGroupeExplosifs
             || c == aptitudesService.CompetenceTirArmesMecaniques
          ))
              return 0;

          if (!carriere.TalentsPourScore.Contains(aptitudesService.TalentMaitreArtilleur))
              return 0;

          var score = 0;
          score += carriere.CompetencesPourScore.Count(c
              => c == aptitudesService.CompetenceMetierArquebusier
              || c == aptitudesService.CompetenceTirArmesAFeu
              || c == aptitudesService.CompetenceTirArmesMecaniques
              || c == aptitudesService.CompetenceGroupeExplosifs
          );

          score += carriere.TalentsPourScore.Count(c
              => c == aptitudesService.TalentMaitreArtilleur
              || c == aptitudesService.TalentAdresseAuTir
              || c == aptitudesService.TalentRechergementRapide
              || c == aptitudesService.TalentSurSesGardes
              || c == aptitudesService.TalentTirDePrecision
              || c == aptitudesService.TalentTirEnPuissance
          );

          score += CalculBonusCapaciteDeTir(carriere);

          return score;
      }

      private int CalculScoreAmiDesBetes(CarriereDto carriere)
      {
          var score = 0;
          score += carriere.CompetencesPourScore.Count(c
              => c == aptitudesService.CompetenceDressage
              || c == aptitudesService.CompetenceSoinsDesAnimaux
              || c == aptitudesService.CompetenceEmpriseSurLesAnimaux
          ) * 2;

          score += carriere.CompetencesPourScore.Count(c
              => c == aptitudesService.CompetenceEquitation
              || c == aptitudesService.CompetenceEquitationCochonDeGuerre
              || c == aptitudesService.CompetenceMetierVendeurDeChevaux
              || c == aptitudesService.CompetenceMetierMaitreChien
              || c == aptitudesService.CompetenceMetierFauconnerie
              || c == aptitudesService.CompetenceConnaissancesAcademiquesZoologie
              || c == aptitudesService.CompetenceMetierGarconDEcurie
              || c == aptitudesService.CompetenceMetierFermier
              || c == aptitudesService.CompetenceConduiteDAttelage
          ) * 1;

          return score;
      }
  
  #endregion
}
*/