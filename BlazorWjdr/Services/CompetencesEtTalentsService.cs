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

        public IEnumerable<CompetenceDto> GetCompetences(IEnumerable<int> ids) => ids.Select(GetCompetence).ToArray();
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

        public IEnumerable<TalentDto> GetTalents(IEnumerable<int> ids) => ids.Select(GetTalent).ToArray();
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
                    Nom = $"{t.nom}{(!string.IsNullOrWhiteSpace(t.specialisation) ? $" ({t.specialisation})" : "")}",
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
                    Nom = $"{c.nom}{(!string.IsNullOrWhiteSpace(c.specialisation) ? $" ({c.specialisation})" : "")}",
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
                talent.SetResumeComplet();
                talent.CompetencesLiees = _allCompetences
                    .Where(c => c.TalentsLies.Contains(talent))
                    .ToList();
            }
        }
    }
}
