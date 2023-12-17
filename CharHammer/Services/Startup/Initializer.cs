using CharHammer.DataSource;
using CharHammer.Models;

namespace CharHammer.Services.Startup;

internal static class Initializer
{
    internal static IEnumerable<ScenarioDto> InitializeScenarios(
        IEnumerable<ScenarioJson> items,
        IReadOnlyDictionary<int, LieuDto> lieux,
        IReadOnlyDictionary<int, LieuTypeDto> lieuxTypes)
        => items
            .Select(s => new ScenarioDto(
                Difficulte: s.difficulte ?? "",
                Duree: s.duree ?? "",
                Lieux: (s.lieux ?? []).Select(id => lieux[id]),
                LieuxTypes: (s.lieuxtypes ?? []).Select(id => lieuxTypes[id]),
                Nom: s.nom,
                Lien: s.lien ?? "",
                Note: s.note,
                Resume: s.resume ?? "",
                Source: s.source ?? "",
                Auteurs: s.auteurs ?? [],
                Styles: s.styles ?? [],
                DejaJoue: s.deja_joue ?? ""));

    internal static IEnumerable<CampagneDto> InitializeCampagnes(
        IReadOnlyDictionary<int, UserDto> users,
        IReadOnlyDictionary<int, TeamDto> teams,
        IEnumerable<CampagneJson> campagnes,
        IEnumerable<ScenarioDto> scenarios,
        IReadOnlyDictionary<int, BestioleDto> bestioles,
        IReadOnlyDictionary<int, CarriereDto> carrieres,
        IReadOnlyDictionary<int, LieuDto> lieux)
        => campagnes
            .Select(c => new CampagneDto(
                Id: c.id,
                Mj: users[c.mj],
                Seances: (c.seances ?? []).Select(s => GetSeanceDtoFromJson(s, bestioles, scenarios, lieux)),
                Contacts: (c.contacts ?? []).Select(s => GetContactDeCampagneDtoFromJson(s, bestioles, carrieres, lieux)),
                Team: teams[c.team],
                Titre: c.titre));

    private static SeanceDto GetSeanceDtoFromJson(
        SeanceJson s,
        IReadOnlyDictionary<int, BestioleDto> bestioles,
        IEnumerable<ScenarioDto> scenarios,
        IReadOnlyDictionary<int, LieuDto> lieux)
        => new(
            Acte: s.acte,
            Debut: s.debut ?? "",
            Duree: s.duree,
            Lieux: (s.lieux ?? []).Select(id => lieux[id]),
            Facts: (s.facts ?? []).Select(f => new FactDto(
                Fact: f.fact,
                Pjs: (f.pjs ?? []).Select(id => bestioles[id]),
                Tri: f.tri
            )),
            Pjs: (s.pjs ?? []).Select(id => bestioles[id]),
            Quand: s.quand,
            Resume: s.resume ?? "",
            Secret: s.secret,
            Titre: s.titre,
            Scenario: s.scenario is null ? null : scenarios.SingleOrDefault(sc => string.Equals(sc.Nom, s.scenario, StringComparison.CurrentCultureIgnoreCase)),
            Xp: s.xp,
            XpComment: s.xp_comment,
            Rencontres: (s.rencontres ?? []).Select(r => GetRencontreDtoFromJson(r, bestioles)));

    private static ContactDeCampagneDto GetContactDeCampagneDtoFromJson(
        ContactDeCampagneJson s,
        IReadOnlyDictionary<int, BestioleDto> bestioles,
        IReadOnlyDictionary<int, CarriereDto> carrieres,
        IReadOnlyDictionary<int, LieuDto> lieux)
        => new(
            Description: s.description ?? "",
            Notes: s.notes ?? [],
            Pnj: bestioles[s.pnj],
            LieuDeRencontre: lieux[s.lieu_de_rencontre],
            LieuDeResidence: s.lieu_de_residence == 0 ? null : lieux[s.lieu_de_residence],
            ProposeLesCarrieres: (s.employeur ?? []).Select(id => carrieres[id]));

    private static RencontreDto GetRencontreDtoFromJson(RencontreJson r, IReadOnlyDictionary<int, BestioleDto> bestioles)
        => new(
            Groupe: r.groupe,
            Allies: (r.allies ?? []).Select(a => GetRencontreCombattantDtoFromJson(a, false, bestioles)),
            Ennemis: (r.ennemis ?? []).Select(a => GetRencontreCombattantDtoFromJson(a, true, bestioles)));

    private static CombattantDto GetRencontreCombattantDtoFromJson(CombattantJson c, bool ennemi, IReadOnlyDictionary<int, BestioleDto> bestioles)
        => new(Combattant: bestioles[c.id], Nom: c.nom ?? bestioles[c.id].Nom, Ennemi: ennemi);

    internal static IReadOnlyDictionary<int, TeamDto> InitializeTeams(IEnumerable<TeamJson> teams)
        => teams.Select(t => new TeamDto(t.id, t.nom)).ToDictionary(k => k.Id);

    internal static IReadOnlyDictionary<int, UserDto> InitializeUsers(IEnumerable<UserJson> users)
        => users.Select(t => new UserDto(t.id, t.pseudo, t.email)).ToDictionary(k => k.Id);

    internal static IReadOnlyDictionary<int, SortilegeDto> InitializeSortileges(IEnumerable<SortilegeJson> items, IReadOnlyDictionary<int, AptitudeDto> dataAptitudes)
        => items
            .Select(i => new SortilegeDto(
                Id: i.id,
                Aptitudes: (i.aptitudes ?? []).Select(id => dataAptitudes[id]),
                Cible: i.cible,
                Distance: i.distance,
                Duree: i.duree,
                Ingredient: i.ingredient,
                Effet: i.effet,
                Nom: i.nom,
                Type: i.type,
                NS: i.ns
            ))
            .ToDictionary(k => k.Id);

    internal static IReadOnlyDictionary<int, RegleDto> InitializeRegles(
        IEnumerable<RegleJson> items,
        IReadOnlyDictionary<int, TableDto> tables,
        IReadOnlyDictionary<int, BestioleDto> bestioles,
        IReadOnlyDictionary<int, AptitudeDto> aptitudes,
        IReadOnlyDictionary<int, LieuDto> lieux,
        IReadOnlyDictionary<int, CarriereDto> carrieres)
    {
        var cacheRegle = items
            .Select(r => new RegleDto(
                Id: r.id,
                Html: r.html,
                Titre: r.titre,
                ReglesId: r.regles ?? [],
                Carrieres: (r.carrieres ?? []).Select(id => carrieres[id]),
                Aptitudes: GetAptitudes(r.aptitudes, aptitudes),
                AptitudesChoix: (r.aptitudes_choix ?? []).Select(choix => GetAptitudes(choix, aptitudes)),
                Bestioles: (r.bestioles ?? []).Select(id => bestioles[id]),
                Tables: (r.tables ?? []).Select(id => tables[id]),
                Lieux: (r.lieux ?? []).Select(id => lieux[id]),
                Regle: r.regle
            ))
            .ToDictionary(k => k.Id);

        foreach (var regle in cacheRegle.Values)
        {
            regle.SousRegles = regle.ReglesId.Select(id => cacheRegle[id]);
        }
        return cacheRegle;
    }

    internal static IReadOnlyDictionary<int, BestioleDto> InitializeCreatures(
        IEnumerable<CreatureJson> items,
        IReadOnlyDictionary<int, RaceDto> races,
        IReadOnlyDictionary<int, AptitudeDto> aptitudes,
        IReadOnlyDictionary<int, LieuDto> lieux,
        IReadOnlyDictionary<int, CarriereDto> carrieres,
        IReadOnlyDictionary<int, ArmeDto> armes,
        IReadOnlyDictionary<int, ArmureDto> armures,
        IReadOnlyDictionary<int, EquipementDto> equipement,
        IReadOnlyDictionary<int, SortilegeDto> sorts,
        IReadOnlyDictionary<int, UserDto> users)
    {
        var bestioles = items
            .Select(c => new BestioleDto(
                Id: c.id,
                UserId: c.user ?? 0,
                MembreDe: c.membrede ?? [],
                Age: c.age,
                DateDeCreation: c.date_creation ?? "",
                Notes: c.notes ?? string.Empty,
                Histoire: c.histoire ?? string.Empty,
                Ambitions: c.ambitions ?? [],
                Description: c.description ?? string.Empty,
                Nom: c.nom,
                NomCourt: c.nom_court ?? c.nom,
                Poids: c.poids,
                Psychologie: c.psycho ?? "",
                Race: races[c.race],
                Sexe: c.sexe,
                Taille: c.taille,
                Masquer: c.masquer,
                ProfilActuel: JsonToDto(c.profil_actuel),
                ProfilInitial: c.profil_initial is not null ? JsonToDto(c.profil_initial) : null,
                AptitudesAcquises: AptitudeAcquiseDto.GetList(
                    (c.aptitudes ?? []).Select(id => aptitudes[id]),
                    JsonToDto(c.profil_actuel)
                ),
                AptitudesOptionnels: AptitudeAcquiseDto.GetList(
                    (c.aptitudes_facultatives ?? []).Select(id => aptitudes[id]),
                    JsonToDto(c.profil_actuel)
                ),
                Origines: (c.origines ?? []).Select(id => lieux[id]),
                Armes: (c.armes ?? []).Select(id => armes[id]),
                Armures: (c.armures ?? []).Select(id => armures[id]),
                Equipement: (c.equipement ?? []).Select(id => equipement[id]),
                Sorts: (c.sorts ?? []).Select(id => sorts[id]),
                SigneAstralId: c.fk_signeastralid,
                Cheveux: c.cheveux,
                Yeux: c.yeux,
                FreresEtSoeurs: c.freres_et_soeurs,
                MainDirectrice: c.main_directrice ?? -1,
                Mort: c.mort,
                CarriereDuPere: c.carriere_du_pere.HasValue ? carrieres[c.carriere_du_pere.Value] : null,
                CarriereDeLaMere: c.carriere_de_la_mere.HasValue ? carrieres[c.carriere_de_la_mere.Value] : null,
                Joueur: c.user is not null ? users[c.user.Value].Pseudo : "",
                CheminementPro: (c.cheminement ?? []).Select(id => carrieres[id]),
                XpActuel: c.xp_actuel ?? 0,
                XpTotal: c.xp_total ?? 0
            ))
            .ToDictionary(k => k.Id);

        foreach (var bestiole in bestioles.Values)
        {
            bestiole.Gabarit = BestiolesService.GetGabarit(bestiole) ?? aptitudes[AptitudesService.TraitGabaritMoyenId];
            bestiole.Blessures = bestiole.ProfilActuel.B == 0 ? BestiolesService.CalculBlessures(bestiole.Gabarit, bestiole.ProfilActuel,
                bestiole.AptitudesAcquises.Any(aa => aa.Aptitude.Id == AptitudesService.TraitDurACuirId)) : bestiole.ProfilActuel.B;
            bestiole.BlessuresDetailDuCalcul = BestiolesService.GetBlessuresDetailDuCalcul(bestiole.Gabarit, bestiole.ProfilActuel,
                bestiole.AptitudesAcquises.Any(aa => aa.Aptitude.Id == AptitudesService.TraitDurACuirId));
            bestiole.BlessuresFormuleDeCalcul = BestiolesService.GetBlessuresFormuleDeCalcul(bestiole.Gabarit,
                bestiole.AptitudesAcquises.Any(aa => aa.Aptitude.Id == AptitudesService.TraitDurACuirId));
        }

        return bestioles;
    }

    internal static IReadOnlyDictionary<int, IEnumerable<LigneDeCarriereInitialeDto>> InitializeTablesCarrieresInitiales(
        IReadOnlyDictionary<int, CarriereDto> carrieres)
    {
        //ToDo: automatiser ?
        var gather = new Dictionary<int, List<LigneDeCarriereInitialeDto>>
        {
            { 1,  [] },
            { 4,  [] },
            { 7,  [] },
            { 22, [] },
            { 23, [] },
            { 26, [] },
            { 27, [] },
            { 28, [] },
            { 63, [] },
            { 65, [] },
            { 66, [] }
        };

        var carrieresInitiales = carrieres.Values.Where(c => c.TirageInitial.Any());

        foreach (var ci in carrieresInitiales)
        {
            foreach (var ti in ci.TirageInitial)
            {
                gather[ti.Race.Id].Add(new LigneDeCarriereInitialeDto(Carriere: ci, Facteur: ti.Facteur));
            }
        }

        var result = new Dictionary<int, IEnumerable<LigneDeCarriereInitialeDto>>();
        foreach (var key in gather.Keys)
            result.Add(key, gather[key].OrderBy(l => l.Carriere.Groupe).ThenBy(l => l.Carriere.Nom));

        return result;
    }

    internal static IReadOnlyDictionary<int, RaceDto> InitializeRaces(
        IEnumerable<RaceJson> items,
        IReadOnlyDictionary<int, AptitudeDto> aptitudes,
        IReadOnlyDictionary<int, LieuDto> lieux)
    {
        var cache = items
            .Select(r => new RaceDto(
                Id: r.id,
                Description: r.description ?? "",
                Aptitudes: (r.aptitudes ?? []).Select(id => aptitudes[id]).OrderBy(a => a.NomComplet),
                Lieux: (r.lieux ?? []).Select(id => lieux[id]).OrderBy(l => l.Nom),
                GroupOnly: r.group_only,
                NomFeminin: r.nom_feminin ?? "",
                NomMasculin: r.nom_masculin,
                Autochtones: r.nom_autoch ?? "",
                ParentId: r.parent,
                PourPj: r.pj,
                Opinions: (r.opinions ?? []).Select(o => new OpinionDto(RaceId: o.race, Ambiance: o.ambiance)),
                Infos: (r.infos ?? []).Select(i => new RaceInfoDto(Titre: i.titre, Detail: i.detail))
            ))
            .ToDictionary(k => k.Id);

        foreach (var race in cache.Values.Where(d => d.ParentId.HasValue))
        {
            race.Parent = cache[race.ParentId!.Value];
        }

        foreach (var race in cache.Values)
        {
            race.SousElements = cache.Values.Where(c => c.Parent == race).OrderBy(c => c.NomMasculin);

            foreach (var opinion in race.Opinions.Where(opinion => opinion.RaceId != 0))
            {
                opinion.Race = cache[opinion.RaceId];
            }
        }

        return cache;
    }

    internal static IReadOnlyDictionary<int, ArmureDto> InitializeArmures(IEnumerable<ArmureJson> items, IReadOnlyDictionary<int, ArmeAttributDto> attributs)
    {
        var result = items
            .Select(a => new ArmureDto(
                a.id,
                a.parent,
                a.nom,
                a.type,
                a.pa,
                a.zones,
                a.prix,
                a.enc,
                a.dispo,
                a.description,
                (a.attributs ?? []).Select(id => attributs[id]).OrderBy(at => at.Nom))
            ).ToDictionary(k => k.Id);

        //foreach (var arme in result.Values.Where(a => a.ParentId.HasValue))
        //{
        //    arme.Parent = result[arme.ParentId!.Value];
        //}
        return result;
    }

    internal static IReadOnlyDictionary<int, EquipementDto> InitializeEquipements(
        IEnumerable<EquipementJson> items,
        IReadOnlyDictionary<int, LieuDto> lieux,
        IReadOnlyDictionary<int, LieuTypeDto> lieuxtypes)
    {
        var result = items
            .Select(t => new EquipementDto(
                Id: t.id,
                ParentId: t.parent,
                Dispo: t.dispo,
                Nom: t.nom,
                Description: t.description,
                Enc: t.enc ?? "-",
                Groupes: t.groupes ?? [],
                Prix: t.prix,
                Contenance: t.contenance,
                Addiction: t.addiction,
                Lieux: (t.lieux ?? []).Select(id => lieux[id]),
                LieuxTypes: (t.lieuxtypes ?? []).Select(id => lieuxtypes[id]),
                Potion: t.potion is null ? null : GetPotionDtoFromJson(t.potion)
            ))
            .ToDictionary(k => k.Id);

        foreach (var equipement in result.Values.Where(e => e.ParentId.HasValue))
        {
            equipement.Parent = result[equipement.ParentId!.Value];
        }
        return result;
    }

    private static PotionDto GetPotionDtoFromJson(PotionJson item)
        => new(
            Creation: new CreationDto(Difficulte: item.creation.difficulte, Temps: item.creation.temps),
            Ingredients: new IngredientsDto(
                Localisation: item.ingredients.localisation,
                Prix: item.ingredients.prix,
                Difficulte: item.ingredients.difficulte
            ),
            Instabilite: item.instabilite,
            Reaction: item.reaction);

    internal static IReadOnlyDictionary<int, ArmeAttributDto> InitializeArmesAttributs(IEnumerable<ArmeAttributJson> items)
        => items
            .Select(t => new ArmeAttributDto(t.id, t.type, t.nom, t.description))
            .ToDictionary(k => k.Id);

    internal static IReadOnlyDictionary<int, ArmeDto> InitializeArmes(
        IEnumerable<ArmeJson> items,
        IReadOnlyDictionary<int, ArmeAttributDto> cacheAttributs,
        IReadOnlyDictionary<int, AptitudeDto> cacheCompetences)
    {
        var result = items
            .Select(l => new ArmeDto(
                Id: l.id,
                ParentId: l.parent,
                Nom: l.nom,
                Description: l.description ?? "",
                Attributs: (l.attributs ?? []).Select(id => cacheAttributs[id]),
                Degats: l.degats,
                Disponibilite: l.dispo,
                Encombrement: l.enc,
                Groupes: (l.groupes ?? []).Select(id => cacheAttributs[id]),
                Allonge: l.allonge ?? "",
                Portee: l.portee ?? "",
                Prix: l.prix,
                Rechargement: l.rechargement ?? "",
                CompetencesDeMaitrise: l.competences.Select(id => cacheCompetences[id])
            ))
            .ToDictionary(k => k.Id);

        //foreach (var arme in result.Values.Where(a => a.ParentId.HasValue))
        //{
        //    arme.Parent = result[arme.ParentId!.Value];
        //}
        return result;
    }

    internal static IReadOnlyDictionary<int, TableDto> InitializeTables(IEnumerable<TableJson> items)
    {
        return items
            .Select(c => new TableDto(c.id,
                Titre: c.titre,
                Description: c.description,
                StylesHeader: c.styles_th ?? [],
                StylesRows: c.styles_td ?? [],
                Lignes: c.lignes
            ))
            .ToDictionary(k => k.Id);
    }

    internal static IReadOnlyDictionary<int, DieuDto> InitializeDieux(
        IEnumerable<DieuJson> items,
        IReadOnlyDictionary<int, AptitudeDto> aptitudes,
        IReadOnlyDictionary<int, LieuDto> lieux)
    {
        var cache = items
            .Select(c => new DieuDto(
                Id: c.id,
                Nom: c.nom,
                Commentaire: c.comment ?? "",
                Domaines: c.domaines ?? "",
                Fideles: c.fideles ?? "",
                Histoire: c.histoire ?? "",
                Symboles: c.symboles ?? "",
                PatronId: c.patron,
                Ambiance: c.ambiance ?? [],
                Aptitudes: new AptitudesAssocieesDto(
                    Inities: (c.aptitudes?.initie ?? []).Select(id => aptitudes[id]).OrderBy(a => a.NomComplet),
                    PretesSansOrdre: (c.aptitudes?.pretre_sans_ordre ?? []).Select(id => aptitudes[id]).OrderBy(a => a.NomComplet)
                ),
                Chef: c.chef ?? "",
                Commandements: c.commandements ?? [],
                Culte: c.culte ?? "",
                Cultistes: c.cultistes ?? "",
                Dogme: c.dogme ?? "",
                Fetes: c.fetes ?? "",
                Initiation: c.initiation ?? "",
                Intro: c.intro ?? "",
                Penitences: c.penitences ?? "",
                Personnalites: (c.personnalites ?? []).Select(p => new PersonnaliteDto(p.nom ?? "", p.description ?? "")),
                Pretrise: c.pretrise ?? "",
                Regles: (c.regles ?? []).Select(r => new RegleAssocieeDto(r.titre ?? "", r.description ?? "")),
                Sectes: (c.sectes ?? []).Select(p => new SecteDto(p.nom ?? "", p.description ?? "")),
                Siege: c.siege.HasValue ? lieux[c.siege.Value] : null,
                Structure: c.structure ?? "",
                Temples: (c.temples ?? []).Select(t => new TempleDto(t.nom ?? "", t.description ?? "")),
                LivresSaints: c.livres ?? "aucun.",
                Ordres: (c.ordres ?? []).Select(jc => new CulteDto(
                    Ambiance: jc.ambiance ?? "",
                    Aptitudes: (jc.aptitudes ?? []).Select(id => aptitudes[id]),
                    Description: jc.description ?? "",
                    Id: jc.id,
                    Mineur: jc.mineur,
                    Nom: jc.nom ?? ""
                ))
            ))
            .ToDictionary(k => k.Id);

        foreach (var dieu in cache.Values.Where(d => d.PatronId.HasValue))
        {
            dieu.Patron = cache[dieu.PatronId!.Value];
        }
        return cache;
    }

    internal static IReadOnlyDictionary<int, LieuTypeDto> InitializeLieuxTypes(IEnumerable<LieuTypeJson> items)
    {
        var result = items
            .Select(t => new LieuTypeDto(
                Id: t.id,
                Nom: t.libelle,
                ParentId: t.parentid
            ))
            .ToDictionary(k => k.Id);

        foreach (var lieuType in result.Values.Where(t => t.ParentId.HasValue))
        {
            lieuType.Parent = result[lieuType.ParentId!.Value];
        }
        return result;
    }

    internal static IReadOnlyDictionary<int, LieuDto> InitializeLieux(
        IEnumerable<LieuJson> items,
        IReadOnlyDictionary<int, LieuTypeDto> cacheTypesDeLieu)
    {
        var result = items
            .Select(l => new LieuDto(
                Id: l.id,
                Nom: l.nom,
                Population: l.population ?? "",
                Allegeance: l.allegeance ?? "",
                Industrie: l.industrie ?? "",
                Description: l.description ?? "",
                ParentId: l.parent,
                TypeDeLieu: cacheTypesDeLieu[l.type],
                Ignorer: l.ignorer
            ))
            .ToDictionary(k => k.Id);

        var parents = new List<int>();
        foreach (var lieu in result.Values.Where(t => t.ParentId.HasValue))
        {
            var parentId = lieu.ParentId!.Value;
            if (!parents.Contains(parentId))
                parents.Add(parentId);
            lieu.Parent = result[parentId];
            lieu.Parent.SousElements.Add(lieu);
        }

        foreach (var lieuId in parents)
        {
            result[lieuId].SousElements = [.. result[lieuId].SousElements.OrderBy(c => c.Nom)];
        }

        return result;
    }

    internal static IReadOnlyDictionary<int, ReferenceDto> InitializeReferences(IEnumerable<ReferenceJson> items)
        => items
            .Select(c => new ReferenceDto(
                Id: c.id,
                AnneeDePublication: c.publishyear ?? 6666,
                Titre: c.titre,
                Version: c.version
            ))
            .ToDictionary(k => k.Id);

    internal static IReadOnlyDictionary<int, DomaineDto> InitializeDomaines(IEnumerable<DomaineJson> items)
        => items
            .Select(c => new DomaineDto(c.id, c.nom)).ToDictionary(k => k.Id);

    internal static IReadOnlyDictionary<int, AptitudeDto> InitializeAptitudes(IEnumerable<AptitudeJson> items)
    {
        var result = items
            .Select(c => new AptitudeDto(
                Id: c.id,
                Ignore: c.ignorer,
                Martial: c.martial,
                Nom: $"{c.nom}{(!string.IsNullOrWhiteSpace(c.spe) ? $" : {c.spe}" : "")}",
                NomEn: c.nom_en,
                Spe: c.spe ?? "",
                Max: c.max,
                Categ: c.categ,
                CategSpe: c.categ_spe ?? "",
                AptitudeMereId: c.parent,
                Contagieux: c.contagieux,
                Guerison: c.guerison ?? "",
                Severite: c.severite ?? 0,
                IncompatiblesIds: c.incompatibles ?? [],
                AptitudesLieesIds: c.aptitudes ?? []
            )
            {
                Tests = c.tests ?? "",
                CaracteristiqueAssociee = c.carac ?? "",
                Resume = c.resume ?? "",
                Description = c.description ?? "",
            })
            .ToDictionary(k => k.Id);

        foreach (var apt in result.Values)
        {
            apt.NomPourRecherche = GenericService.NettoyerPourRecherche(apt.Nom);
            apt.MotsClefDeRecherche = GenericService.MotsClefsDeRecherche(apt.NomPourRecherche);
            apt.AptitudesLiees = apt.AptitudesLieesIds.Select(id => result[id]).OrderBy(a => a.NomComplet).ToList();
            apt.Incompatibles = apt.IncompatiblesIds.Select(id => result[id]).OrderBy(a => a.NomComplet).ToList();
        }

        foreach (var apt in result.Values)
        {
            foreach (var aptLiee in apt.AptitudesLiees.Where(aptLiee => !aptLiee.AptitudesLiees.Contains(apt)))
                aptLiee.AptitudesLiees.Add(apt);
            foreach (var aptInc in apt.Incompatibles.Where(aptInc => !aptInc.Incompatibles.Contains(apt)))
                aptInc.Incompatibles.Add(apt);
        }

        foreach (var aptitude in result.Values.Where(c => c.AptitudeMereId.HasValue))
        {
            aptitude.Parent = result[aptitude.AptitudeMereId!.Value];
            if (aptitude.Description == "")
                aptitude.Description = aptitude.Parent.Description;
            if (aptitude.Resume == "")
                aptitude.Resume = aptitude.Parent.Resume;
            if (aptitude.CaracteristiqueAssociee == "" && aptitude.Parent.CaracteristiqueAssociee != "")
                aptitude.CaracteristiqueAssociee = aptitude.Parent.CaracteristiqueAssociee;
            if (aptitude.Tests == "" && aptitude.Parent.Tests != "")
                aptitude.Tests = aptitude.Parent.Tests;
            if (aptitude.AptitudesLiees.Count == 0)
                aptitude.AptitudesLiees = aptitude.Parent.AptitudesLiees;
        }

        foreach (var aptitude in result.Values.Where(t => t.Parent is not null).Select(t => t.Parent).Distinct())
        {
            aptitude!.SousElements = result.Values.Where(c => c.Parent == aptitude).OrderBy(c => c.Nom);
        }

        return result;
    }

    private static ProfilDto JsonToDto(ProfilJson p)
        => new(Cc: p.cc, Ct: p.ct, F: p.f, E: p.e, I: p.i, Ag: p.ag, Dex: p.dex, Int: p.intel, Fm: p.fm, Soc: p.soc, M: p.m, B: p.b);

    private static IEnumerable<CarriereDto> GetCarrieres(IEnumerable<int>? ids, IReadOnlyDictionary<int, CarriereDto> cache)
        => (ids ?? []).Select(id => cache[id]).OrderBy(c => c.Nom);

    internal static IReadOnlyDictionary<int, CarriereDto> InitializeCarrieres(
        IEnumerable<CarriereJson> carrieres,
        IReadOnlyDictionary<int, RaceDto> cacheRaces,
        IReadOnlyDictionary<int, AptitudeDto> cacheAptitudes,
        IReadOnlyDictionary<int, ReferenceDto> cacheReferences)
    {
        var cache = carrieres
            .Select(c => new CarriereDto(
                Id: c.id,
                Nom: c.nom,
                NomAnglais: c.nom_en ?? "",
                NiveauSpecifie: c.niveau,
                MotsClefDeRecherche: GenericService.MotsClefsDeRecherche(GenericService.NettoyerPourRecherche(c.nom)),
                CarriereMereId: c.parent,
                DebouchesIds: c.debouch ?? [],
                AvancementsIds: c.avancements ?? [],
                TirageInitial: (c.tirage ?? []).Select(ti => new TirageDto(Facteur: ti.f, Race: cacheRaces[ti.r])),
                Dotations: c.dotations,
                Image: $"images/careers/{c.id}.png",
                PlanDeCarriere: JsonToDto(c.profil),
                Source: JsonToDto(c.source, cacheReferences),
                Aptitudes: GetAptitudes(c.aptitudes, cacheAptitudes),
                AptitudesChoix: (c.aptitudes_choix ?? []).Select(choix => GetAptitudes(choix, cacheAptitudes))
            )
            {
                Ambiance = (c.ambiance ?? []).Select(ca => new CitationDto(ca.c, ca.a ?? "", ca.s ?? "")),
                CompetenceDeMetier = c.metier is null ? null : cacheAptitudes[c.metier.Value],
                Description = c.description,
                Groupe = c.groupe ?? "",
                Leitmotiv = c.leitmotiv ?? "",
                Restriction = c.restriction ?? "",
                Statut = c.revenu ?? "",
            })
            .ToDictionary(k => k.Id);

        foreach (var fille in cache.Values.Where(c => c.CarriereMereId.HasValue))
        {
            fille.Parent = cache[fille.CarriereMereId!.Value];
            if (!fille.Ambiance.Any())
                fille.Ambiance = fille.Parent.Ambiance;
            fille.CompetenceDeMetier ??= fille.Parent.CompetenceDeMetier;
            if (fille.Description == "")
                fille.Description = fille.Parent.Description;
            if (fille.Groupe == "")
                fille.Groupe = fille.Parent.Groupe;
            if (fille.Leitmotiv == "")
                fille.Leitmotiv = fille.Parent.Leitmotiv;
            if (fille.Restriction == "")
                fille.Restriction = fille.Parent.Restriction;
            if (fille.Statut == "")
                fille.Statut = fille.Parent.Statut;
        }

        foreach (var filiere in cache.Values.Where(c => c.DebouchesIds.Any()))
            filiere.Debouches = GetCarrieres(filiere.DebouchesIds, cache);

        foreach (var carriere in cache.Values.Where(c => c.AvancementsIds.Any()))
            carriere.DebouchesDAvancements = GetCarrieres(carriere.AvancementsIds, cache);

        var tousLesAvancements = cache.Values.SelectMany(c => c.DebouchesDAvancements).Distinct();
        foreach (var carriere in tousLesAvancements)
        {
            carriere.FilieresDAvancement = cache.Values.Where(c => c.DebouchesDAvancements.Contains(carriere)).OrderBy(c => c.Nom);
            SetDescriptionParFiliere(carriere);
        }

        foreach (var carriereMere in cache.Values.Where(c => c.Parent is not null).Select(c => c.Parent).Distinct())
            carriereMere!.SousElements = cache.Values
                .Where(c => c.Parent == carriereMere)
                .OrderBy(c => c.Nom);

        foreach (var carriere in cache.Values)
        {
            carriere.Filieres = cache.Values
                .Where(c => c.Debouches.Contains(carriere))
                .OrderBy(c => c.Nom);

            foreach (var apt in carriere.AptitudesPourScore)
            {
                apt.CarrieresLiees.Add(carriere);
            }
        }

        return cache;
    }

    private static void SetDescriptionParFiliere(CarriereDto avancement)
    {
        if (avancement.Description != "")
            return;

        var filiere = avancement.FilieresDAvancement.FirstOrDefault(f => f.Description != "");
        if (filiere is not null)
        {
            avancement.Description = filiere.Description;
            return;
        }

        // Désactivé car inutile actuellement
        //filiere = avancement.FilieresDAvancement.FirstOrDefault(f => f.FilieresDAvancement.Any());
        //if (filiere is not null)
        //{
        //    SetDescriptionParFiliere(filiere);
        //    avancement.Description = filiere.Description;
        //}
    }

    private static SourceDto? JsonToDto(SourceJson? source, IReadOnlyDictionary<int, ReferenceDto> references)
        => source?.id is null ? null : new SourceDto(Book: references[source.id!.Value], Info: source.info ?? "");

    private static IEnumerable<AptitudeDto> GetAptitudes(int[]? argAptitudes, IReadOnlyDictionary<int, AptitudeDto> cacheAptitudes)
        => (argAptitudes ?? []).Select(id => cacheAptitudes[id]).OrderBy(a => a.NomComplet);

    internal static IEnumerable<ChronologieDto> InitializeChronologie(IEnumerable<ChronoJson> chrono, IReadOnlyDictionary<int, ReferenceDto> references, IReadOnlyDictionary<int, DomaineDto> domaines)
        => chrono
                .Select(c => new ChronologieDto(
                    c.debut,
                    c.fin,
                    c.resume,
                    c.titre ?? "",
                    c.comment ?? "",
                    (c.sources ?? []).Select(id => references[id]),
                    (c.domaines ?? []).Select(id => domaines[id])
                ))
                .OrderBy(c => c.Debut).ThenBy(c => c.Fin);
}
