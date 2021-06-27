namespace BlazorWjdr.Services
{
    using BlazorWjdr.DomainModel;
    using System.Collections.Generic;
    using System.Linq;

    public class ProfilsService
    {
        private Dictionary<int, ProfilDto>? _cacheProfils = null;

        public ProfilsService()
        {
        }

        public ProfilDto GetProfil(int id)
        {
            if (_cacheProfils == null)
                Initialize();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            return _cacheProfils[id];
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        private void Initialize()
        {
            _cacheProfils = DataSource.JsonLoader
                .GetRootProfil()
                .items
                .Select(c => new ProfilDto
                {
                    Id = c.profilid,
                    A = c.profila,
                    Ag = c.profilag,
                    B = c.profilb,
                    BE = c.profilbe,
                    BF = c.profilbf,
                    CC = c.profilcc,
                    CT = c.profilct,
                    E = c.profile,
                    F = c.profilf,
                    FM = c.profilfm,
                    Int = c.profilint,
                    M = c.profilm,
                    Mag = c.profilmag,
                    PD = c.profilpd,
                    PF = c.profilpf,
                    Soc = c.profilsoc
                })
                .ToDictionary(k => k.Id, v => v);
        }
    }
}
