namespace BlazorWjdr.Services
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CarrieresService
    {
        private Dictionary<int, CarriereDto> _cacheCarrieres;

        public CarrieresService(Dictionary<int, CarriereDto> dataCarrieres)
        {
            _cacheCarrieres = dataCarrieres;
            //Initialize();
        }

        public List<CarriereDto> AllCarrieres => _cacheCarrieres.Values.OrderBy(t => t.Nom).ToList();

        public IEnumerable<CarriereDto> GetCarrieres(IEnumerable<int> ids) => ids.Select(GetCarriere).ToArray();

        public CarriereDto GetCarriere(int id) => _cacheCarrieres[id];

        public CarriereDto[] GetCarrieresProposant(CompetenceDto competence)
            => AllCarrieres
                .Where(c => c.CompetencesPourScore.Any(comp => comp.Id == competence.Id))
                .ToArray();

        public CarriereDto[] GetCarrieresProposant(TalentDto talent)
            => AllCarrieres
                .Where(c => c.TalentsPourScore.Any(t => t.Id == talent.Id))
                .ToArray();

        public CarriereDto[] GetCarrieresParuesDans(ReferenceDto reference)
            => AllCarrieres
                .Where(c => c.SourceLivre?.Id == reference.Id)
                .ToArray();

        /*
        private void InitializeScores()
        {
            foreach (var carriere in _cacheCarrieres.Values)
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
            
            //DirectoryInfo d = new DirectoryInfo("./wwwroot/images/careers/");
            //var images = d.GetFiles("*-*.png").Select(f => f.Name);
            //foreach (var carriere in _allCarrieres)
            //{
            //    var list = new List<string> { $"{carriere.Id}.png" };
            //    list.AddRange(images.Where(img => img.StartsWith($"{carriere.Id}-")));
            //    carriere.Images = list.ToArray();
            //}
        }
        */
        
        #region Calcul Bonus de Caractéristique
/*
        private int CalculBonusCapaciteDeTir(CarriereDto carriere)
        {
            var capaciteDeTir = carriere.PlanDeCarriere.Ct;
            if (carriere.TalentsPourScore.Any(t => t == _compTalentsEtTraitsService.TalentTireurDElite))
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
            if (carriere.TalentsPourScore.Any(t => t == _compTalentsEtTraitsService.TalentIntelligent))
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
            if (carriere.TalentsPourScore.Any(t => t == _compTalentsEtTraitsService.TalentSangFroid))
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
            if (carriere.TalentsPourScore.Any(t => t == _compTalentsEtTraitsService.TalentReflexesEclairs))
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
            if (carriere.TalentsPourScore.Any(t => t == _compTalentsEtTraitsService.TalentSociable))
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
                c == _compTalentsEtTraitsService.CompetenceLangSecretBataille) * 2;

            if (carriere.TalentsPourScore.Contains(_compTalentsEtTraitsService.TalentMaitriseUneAuChoix))
                score += 3;
            if (carriere.TalentsPourScore.Contains(_compTalentsEtTraitsService.TalentMaitriseDeuxAuChoix))
                score += 6;
            
            score += carriere.TalentsPourScore.Count(c
                => c == _compTalentsEtTraitsService.TalentAmbidextrie
                   || c == _compTalentsEtTraitsService.TalentCoupsPrécis
                   || c == _compTalentsEtTraitsService.TalentDurACuir
                   || c == _compTalentsEtTraitsService.TalentSurSesGardes
                   || c == _compTalentsEtTraitsService.TalentValeureux
                   || c == _compTalentsEtTraitsService.TalentTroublant
                   || c == _compTalentsEtTraitsService.TalentForceAccrue
            ) * 2;

            score += carriere.PlanDeCarriere.A * 4;

            var force = carriere.PlanDeCarriere.F;
            if (carriere.TalentsPourScore.Any(t => t == _compTalentsEtTraitsService.TalentForceAccrue))
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
                c == _compTalentsEtTraitsService.CompetenceEsquive ||
                c.Parent == _compTalentsEtTraitsService.CompetenceGroupeMelee
            ) * 2;

            score += carriere.TalentsPourScore.Count(c =>
                c == _compTalentsEtTraitsService.TalentCombatADeuxArmes
                || c == _compTalentsEtTraitsService.TalentDesarmement
                || c == _compTalentsEtTraitsService.TalentFrenesie
                || c == _compTalentsEtTraitsService.TalentCombattantVirevoltant
                || c == _compTalentsEtTraitsService.TalentCoupsAssomants
                || c == _compTalentsEtTraitsService.TalentCoupsPuissants
                || c == _compTalentsEtTraitsService.TalentCoupsAuBut
                || c == _compTalentsEtTraitsService.TalentCombatDeRue
                || c == _compTalentsEtTraitsService.TalentDurACuir
                || c == _compTalentsEtTraitsService.TalentGuerrierNe
                || c == _compTalentsEtTraitsService.TalentParadeEclair
                || c == _compTalentsEtTraitsService.TalentLutte
                || c == _compTalentsEtTraitsService.TalentResistanceAccrue
                || c == _compTalentsEtTraitsService.TalentRobuste
                || c == _compTalentsEtTraitsService.TalentDechainement
                || c == _compTalentsEtTraitsService.TalentPresenceImposante
                || c == _compTalentsEtTraitsService.TalentTueur
                || c.Parent == _compTalentsEtTraitsService.TalentGroupeVertu
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
                c == _compTalentsEtTraitsService.CompetenceMetierArquebusier ||
                c.Parent == _compTalentsEtTraitsService.CompetenceGroupeTir
            ) * 2;

            score += carriere.TalentsPourScore.Count(c
                => c == _compTalentsEtTraitsService.TalentRechergementRapide
                   || c == _compTalentsEtTraitsService.TalentAdresseAuTir
                   || c == _compTalentsEtTraitsService.TalentTirDePrecision
                   || c == _compTalentsEtTraitsService.TalentTirEnPuissance
                   || c == _compTalentsEtTraitsService.TalentMaitreArtilleur
            ) * 2;

            score += CalculBonusCapaciteDeTir(carriere) * 3;

            return score;
        }

        private int CalculScoreCavalerie(CarriereDto carriere)
        {
            if (!carriere.CompetencesPourScore.Contains(_compTalentsEtTraitsService.CompetenceEquitation))
                return 0;
            
            var score = 0;

            score += carriere.CompetencesPourScore.Count(c
                => c == _compTalentsEtTraitsService.CompetenceExpressionArtistiqueAcrobatEquestre
            ) * 4;

            score += carriere.CompetencesPourScore.Count(c
                => c == _compTalentsEtTraitsService.CompetenceEmpriseSurLesAnimaux
                || c == _compTalentsEtTraitsService.CompetenceSoinsDesAnimaux
                || c == _compTalentsEtTraitsService.CompetenceMetierGarconDEcurie
                || c == _compTalentsEtTraitsService.CompetenceMetierVendeurDeChevaux
                || c == _compTalentsEtTraitsService.CompetenceDressage
                || c == _compTalentsEtTraitsService.CompetenceMeleeArmesDeCavalerie
            ) * 2;
            
            score += carriere.TalentsPourScore.Count(c
                => c == _compTalentsEtTraitsService.TalentMaitriseUneAuChoix
                || c == _compTalentsEtTraitsService.TalentMaitriseDeuxAuChoix
                || c == _compTalentsEtTraitsService.TalentAcrobateEquestre
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
                => c == _compTalentsEtTraitsService.CompetenceConnaissanceAcademiqueMagie
                   || c == _compTalentsEtTraitsService.CompetenceConnaissanceAcademiqueEsprits
                   || c == _compTalentsEtTraitsService.CompetenceConnaissanceAcademiqueNecromancie
                   || c == _compTalentsEtTraitsService.CompetenceLangueClassique
            ) * 1;
            
            score += carriere.CompetencesPourScore.Count(c
                => c == _compTalentsEtTraitsService.CompetenceFocalisation
                   || c == _compTalentsEtTraitsService.CompetenceLangageMystique
                   || c == _compTalentsEtTraitsService.CompetenceLangageMystiqueMagick
                   || c == _compTalentsEtTraitsService.CompetenceLangageMystiqueDemoniaque
                   || c == _compTalentsEtTraitsService.CompetenceLangageMystiqueElfeMystique
                   || c == _compTalentsEtTraitsService.CompetenceSensDeLaMagie
            ) * 2;
            
            score += carriere.TalentsPourScore.Count(c
                => c == _compTalentsEtTraitsService.TalentHarmonieAethyrique
                   || c == _compTalentsEtTraitsService.TalentMainsAgiles
                   || c == _compTalentsEtTraitsService.TalentMeditation
                   || c == _compTalentsEtTraitsService.TalentProjectilePuissant
                   || c == _compTalentsEtTraitsService.TalentMagieVulgaire
                   || c == _compTalentsEtTraitsService.TalentMagieNoire
                   || c.Parent != null && (
                       c.Parent == _compTalentsEtTraitsService.TalentGroupeMagieCommune
                       || c.Parent == _compTalentsEtTraitsService.TalentGroupeMagieMineure)
            ) * 2;
            
            score += CalculBonusIntelligence(carriere);
            score += CalculBonusForceMentale(carriere);

            return score;
        }

        private int CalculScoreArtisanat(CarriereDto carriere)
        {
            var score = 0;

            score += carriere.CompetencesPourScore.Count(c
                => c == _compTalentsEtTraitsService.CompetenceEvaluation
                   || c == _compTalentsEtTraitsService.CompetenceLangageSecretGuilde
                   || c.Parent == _compTalentsEtTraitsService.CompetenceGroupeMetier
                   || c == _compTalentsEtTraitsService.CompetenceConnaissancesAcademiquesArts
                   || c == _compTalentsEtTraitsService.CompetenceConnaissancesAcademiquesIngénierie
                   || c == _compTalentsEtTraitsService.CompetenceConnaissancesAcademiquesRunes
                   || c == _compTalentsEtTraitsService.CompetenceConnaissancesAcademiquesSciences
                   || c == _compTalentsEtTraitsService.CompetencePreparationDePoisons
                   || c == _compTalentsEtTraitsService.CompetenceCreationDeRunes
                   || c == _compTalentsEtTraitsService.CompetenceLangageMystiqueNain
            ) * 2;

            if (carriere.CompetencesPourScore.Contains(_compTalentsEtTraitsService.CompetenceMetierDeuxAuChoix))
                score += 2;
            if (carriere.CompetencesPourScore.Contains(_compTalentsEtTraitsService.CompetenceMetierTroisAuChoix))
                score += 4;
            
            score += carriere.TalentsPourScore.Count(c
                => c == _compTalentsEtTraitsService.TalentSavoirFaireNain
                   || c == _compTalentsEtTraitsService.TalentTalentArtistique
            ) * 2;

            if (carriere.TalentsPourScore.Contains(_compTalentsEtTraitsService.TalentRuneDeuxAuChoix))
                score += 4;
            if (carriere.TalentsPourScore.Contains(_compTalentsEtTraitsService.TalentRuneSixAuChoix))
                score += 5;
            if (carriere.TalentsPourScore.Contains(_compTalentsEtTraitsService.TalentRuneDixAuChoix))
                score += 6;
            if (carriere.TalentsPourScore.Contains(_compTalentsEtTraitsService.TalentRuneMajeureDeuxAuChoix))
                score += 7;
            
            score += CalculBonusIntelligence(carriere);
            score += CalculBonusAgilite(carriere) / 2;

            return score;
        }

        private int CalculScoreAcademique(CarriereDto carriere)
        {
            var score = 0;
            score += carriere.CompetencesPourScore.Count(c
                => c.Parent == _compTalentsEtTraitsService.CompetenceGroupeConnaissancesAcademiques
                || c.Parent == _compTalentsEtTraitsService.CompetenceGroupeLangue
                || c == _compTalentsEtTraitsService.CompetenceLireEcrire
            );
            if (carriere.CompetencesPourScore.Any(c => c == _compTalentsEtTraitsService.CompetenceConnaissancesAcademiquesDeuxAuChoix))
                score += 1;
            if (carriere.CompetencesPourScore.Any(c => c == _compTalentsEtTraitsService.CompetenceConnaissancesAcademiquesTroisAuChoix))
                score += 2;

            if (score < 2)
                return 0;
            
            score += (carriere.CompetencesPourScore.Count(c => c.Parent == _compTalentsEtTraitsService.CompetenceGroupeConnaissancesGenerales) + 1) / 2;

            score += carriere.TalentsPourScore.Count(c
                => c == _compTalentsEtTraitsService.TalentCalculMental
                || c == _compTalentsEtTraitsService.TalentLinguistique
            );

            score += CalculBonusIntelligence(carriere) * 2;

            return score;
        }

        private int CalculScoreCommerce(CarriereDto carriere)
        {
            var score = 0;
            
            score += carriere.CompetencesPourScore.Count(c
                => c == _compTalentsEtTraitsService.CompetenceMarchandage
                || c == _compTalentsEtTraitsService.CompetenceMetierMarchand
                || c == _compTalentsEtTraitsService.CompetenceMetierVendeurDeChevaux
            ) * 2;
            if (score == 0)
                return 0;

            score += carriere.CompetencesPourScore.Count(c
                => c == _compTalentsEtTraitsService.CompetenceEvaluation
                   || c == _compTalentsEtTraitsService.CompetenceBaratin
                   || c == _compTalentsEtTraitsService.CompetenceLireEcrire
                   || c == _compTalentsEtTraitsService.CompetenceExpressionArtistiqueConteur
            );
            
            score += carriere.TalentsPourScore.Count(c
                => c == _compTalentsEtTraitsService.TalentCalculMental
                   || c == _compTalentsEtTraitsService.TalentDurEnAffaires
            );

            score += CalculBonusIntelligence(carriere);
            score += CalculBonusSociabilite(carriere);
            
            return score;
        }

        private int CalculScoreDeLOmbre(CarriereDto carriere)
        {
            var score = 0;
            score += carriere.CompetencesPourScore.Count(c
                => c == _compTalentsEtTraitsService.CompetenceAlphSecretVoleurs
                   || c == _compTalentsEtTraitsService.CompetenceDeplacementSilencieux
                   || c == _compTalentsEtTraitsService.CompetenceDissimulation
                   || c == _compTalentsEtTraitsService.CompetenceFouille
                   || c == _compTalentsEtTraitsService.CompetencePerception
                   || c == _compTalentsEtTraitsService.CompetenceEscalade
                   || c == _compTalentsEtTraitsService.CompetenceCrochetage
                   || c == _compTalentsEtTraitsService.CompetenceDeguisement
                   || c == _compTalentsEtTraitsService.CompetenceEscamotage
                   || c == _compTalentsEtTraitsService.CompetenceLectureSurLesLevres
                   || c == _compTalentsEtTraitsService.CompetencePreparationDePoisons
            );
            if (score < 2)
                return 0;

            score += carriere.TalentsPourScore.Count(c
                => c == _compTalentsEtTraitsService.TalentConnaissanceDesPieges
                   || c == _compTalentsEtTraitsService.TalentCamouflageRural
                   || c == _compTalentsEtTraitsService.TalentCamouflageSouterrain
                   || c == _compTalentsEtTraitsService.TalentCamouflageUrbain
                   || c == _compTalentsEtTraitsService.TalentCodeDeLaRue
                   || c == _compTalentsEtTraitsService.TalentImitation
                   || c == _compTalentsEtTraitsService.TalentSensAiguises
                   || c == _compTalentsEtTraitsService.TalentAccuiteAuditive
                   || c == _compTalentsEtTraitsService.TalentAccuiteVisuelle
                   || c == _compTalentsEtTraitsService.TalentFilature
                   || c == _compTalentsEtTraitsService.TalentPistage
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
                => c == _compTalentsEtTraitsService.CompetenceAlphabetSecretPisteurs
                || c == _compTalentsEtTraitsService.CompetenceLangageSecretRodeurs
                || c == _compTalentsEtTraitsService.CompetenceLangageSecretTroisAuChoix
                || c == _compTalentsEtTraitsService.CompetenceSurvie
                || c == _compTalentsEtTraitsService.CompetencePistage
            ) * 4;

            score += carriere.CompetencesPourScore.Count(c
                => c == _compTalentsEtTraitsService.CompetenceBraconnage
                   || c == _compTalentsEtTraitsService.CompetenceDeplacementSilencieux
                   || c == _compTalentsEtTraitsService.CompetenceDissimulation
                   || c == _compTalentsEtTraitsService.CompetenceEmpriseSurLesAnimaux
                   || c == _compTalentsEtTraitsService.CompetenceNatation
                   || c == _compTalentsEtTraitsService.CompetenceOrientation
                   || c == _compTalentsEtTraitsService.CompetencePerception
                   || c == _compTalentsEtTraitsService.CompetenceEscalade
            ) * 2;

            score += carriere.CompetencesPourScore.Count(c
                => c.Parent == _compTalentsEtTraitsService.CompetenceGroupeConnaissancesGenerales
                || c.Parent == _compTalentsEtTraitsService.CompetenceGroupeLangue
                || c == _compTalentsEtTraitsService.CompetenceFouille
                || c == _compTalentsEtTraitsService.CompetenceMetierCartographe
                || c == _compTalentsEtTraitsService.CompetenceSoinsDesAnimaux
                || c == _compTalentsEtTraitsService.CompetenceTirArcsLongs
                || c == _compTalentsEtTraitsService.CompetenceMeleeArmesParalisantes
            );

            score += carriere.TalentsPourScore.Count(c
               => c == _compTalentsEtTraitsService.TalentSensAiguises
               || c == _compTalentsEtTraitsService.TalentAccuiteAuditive
               || c == _compTalentsEtTraitsService.TalentAccuiteVisuelle
               || c == _compTalentsEtTraitsService.TalentSensDeLOrientation
               || c == _compTalentsEtTraitsService.TalentCamouflageRural
               || c == _compTalentsEtTraitsService.TalentGrandVoyageur
               || c == _compTalentsEtTraitsService.TalentLinguistique
               || c == _compTalentsEtTraitsService.TalentConnaissanceDesPieges
               || c == _compTalentsEtTraitsService.TalentSixiemeSens
               || c == _compTalentsEtTraitsService.TalentTireurDElite
            ) * 2;

            if (carriere.TalentsPourScore.Contains(_compTalentsEtTraitsService.TalentCourseAPied))
                score += 4;
            
            score += CalculBonusIntelligence(carriere) / 2;
            score += CalculBonusAgilite(carriere) / 2;

            return score;
        }

        private int CalculScoreSocial(CarriereDto carriere)
        {
            var score = 0;

            score += carriere.CompetencesPourScore.Count(c
                => c == _compTalentsEtTraitsService.CompetenceBaratin
                   || c == _compTalentsEtTraitsService.CompetenceCharisme
                   || c == _compTalentsEtTraitsService.CompetenceCommandement
                   || c == _compTalentsEtTraitsService.CompetenceCommérage
                   || c == _compTalentsEtTraitsService.CompetenceIntimidation
            );

            score += carriere.TalentsPourScore.Count(c
                => c == _compTalentsEtTraitsService.TalentEloquence
                   || c == _compTalentsEtTraitsService.TalentOrateurNe
                   || c == _compTalentsEtTraitsService.TalentPolitique
                   || c == _compTalentsEtTraitsService.TalentCodeDeLaRue
                   || c == _compTalentsEtTraitsService.TalentEtiquette
                   || c == _compTalentsEtTraitsService.TalentIntriguant
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
                => c == _compTalentsEtTraitsService.CompetenceCanotage
                   || c == _compTalentsEtTraitsService.CompetenceNavigation))
                return 0;
            
            var score = 0;
            score += carriere.CompetencesPourScore.Count(c
                => c == _compTalentsEtTraitsService.CompetenceCanotage
                || c == _compTalentsEtTraitsService.CompetenceNatation
                || c == _compTalentsEtTraitsService.CompetenceNavigation
                || c == _compTalentsEtTraitsService.CompetenceEruditionAstronomie
                || c == _compTalentsEtTraitsService.CompetenceEruditionPotamologie
                || c == _compTalentsEtTraitsService.CompetenceOrientation
                || c == _compTalentsEtTraitsService.CompetenceMetierCartographe
                || c == _compTalentsEtTraitsService.CompetenceMetierCharpentierNaval
            ) * 2;

            score += carriere.TalentsPourScore.Count(c
                => c == _compTalentsEtTraitsService.TalentGrandVoyageur
                || c == _compTalentsEtTraitsService.TalentSensDeLOrientation
            ) * 2;

            return score;
        }

        private int CalculScorePoudreNoire(CarriereDto carriere)
        {
            if (!carriere.CompetencesPourScore.Any(c
               => c == _compTalentsEtTraitsService.CompetenceTirArmesAFeu
               || c == _compTalentsEtTraitsService.CompetenceGroupeExplosifs
               || c == _compTalentsEtTraitsService.CompetenceTirArmesMecaniques
            ))
                return 0;
            
            if (!carriere.TalentsPourScore.Contains(_compTalentsEtTraitsService.TalentMaitreArtilleur))
                return 0;
            
            var score = 0;
            score += carriere.CompetencesPourScore.Count(c
                => c == _compTalentsEtTraitsService.CompetenceMetierArquebusier
                || c == _compTalentsEtTraitsService.CompetenceTirArmesAFeu
                || c == _compTalentsEtTraitsService.CompetenceTirArmesMecaniques
                || c == _compTalentsEtTraitsService.CompetenceGroupeExplosifs
            );

            score += carriere.TalentsPourScore.Count(c
                => c == _compTalentsEtTraitsService.TalentMaitreArtilleur
                || c == _compTalentsEtTraitsService.TalentAdresseAuTir
                || c == _compTalentsEtTraitsService.TalentRechergementRapide
                || c == _compTalentsEtTraitsService.TalentSurSesGardes
                || c == _compTalentsEtTraitsService.TalentTirDePrecision
                || c == _compTalentsEtTraitsService.TalentTirEnPuissance
            );

            score += CalculBonusCapaciteDeTir(carriere);
            
            return score;
        }

        private int CalculScoreAmiDesBetes(CarriereDto carriere)
        {
            var score = 0;
            score += carriere.CompetencesPourScore.Count(c
                => c == _compTalentsEtTraitsService.CompetenceDressage
                || c == _compTalentsEtTraitsService.CompetenceSoinsDesAnimaux
                || c == _compTalentsEtTraitsService.CompetenceEmpriseSurLesAnimaux
            ) * 2;
            
            score += carriere.CompetencesPourScore.Count(c
                => c == _compTalentsEtTraitsService.CompetenceEquitation
                || c == _compTalentsEtTraitsService.CompetenceEquitationCochonDeGuerre
                || c == _compTalentsEtTraitsService.CompetenceMetierVendeurDeChevaux
                || c == _compTalentsEtTraitsService.CompetenceMetierMaitreChien
                || c == _compTalentsEtTraitsService.CompetenceMetierFauconnerie
                || c == _compTalentsEtTraitsService.CompetenceConnaissancesAcademiquesZoologie
                || c == _compTalentsEtTraitsService.CompetenceMetierGarconDEcurie
                || c == _compTalentsEtTraitsService.CompetenceMetierFermier
                || c == _compTalentsEtTraitsService.CompetenceConduiteDAttelage
            ) * 1;

            return score;
        }
*/
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
            .Where(c => c.SourceLivre?.Id == 15 || c.SourceLivre?.Id == 16)
            .ToList();

        public IEnumerable<CarriereDto> CarrieresDuKislev => AllCarrieres
            .Where(c => c.Id == 53 || c.SourceLivre?.Id == 14)
            .ToList();

        public List<int> CarrieresSkaven => AllCarrieres
            .Where(c => c.SourceLivre?.Id == 17)
            .Select(c => c.Id)
            .ToList();

        // Tueur de morts, Fouet de dieu, Flagellant, Fanatique, Pénitent, Prêcheur de rue, Tueur de démons/géants/trolls, Exécuteur, Mystique, Cénobite
        public IEnumerable<CarriereDto> CarrieresFanatiques => GetCarrieres(new[] {209, 213, 170, 45, 212, 88, 198, 199, 77, 87, 268, 267, 194, 1, 314 });
        public IEnumerable<CarriereDto> CarrieresDeNorsca => GetCarrieres(new [] { 26, 89, 302, 303, 90, 91, 92, 93, 94, 95, 304 });
        public List<int> CarrieresChaos => new() { 305, 307, 309, 311, 306, 308, 310, 312, 293, 294 };
        public IEnumerable<CarriereDto> CarrieresCriminelles => GetCarrieres(new [] { 21, 141, 149, 151, 152, 158, 37, 38, 165, 43, 168, 280, 111, 51, 265, 183, 127, 145, 295, 66, 188, 191, 192, 193, 258, 76, 80 });
        public IEnumerable<CarriereDto> CarrieresBureaucratie => GetCarrieres(new [] { 315, 251, 35, 86, 87, 97, 125, 46, 129, 49, 175, 281, 177, 58, 59, 63, 128, 146, 147, 69, 70 });
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
                list.AddRange(AllCarrieres.Where(c => c.SourceLivre?.Id == 13 || c.Parent != null && list.Contains(c.Parent)));
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
            return AllCarrieres.SelectMany(c => c.Images).OrderBy(_ => rnd.Next()).ToArray();
        }
    }
}
