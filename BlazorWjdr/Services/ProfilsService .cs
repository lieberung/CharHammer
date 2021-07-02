namespace BlazorWjdr.Services
{
    using BlazorWjdr.Models;
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
                    Id = c.id,
                    A = c.a,
                    Ag = c.ag,
                    B = c.b,
                    BE = c.be,
                    BF = c.bf,
                    CC = c.cc,
                    CT = c.ct,
                    E = c.e,
                    F = c.f,
                    FM = c.fm,
                    Int = c.intel,
                    M = c.m,
                    Mag = c.mag,
                    PD = c.pd,
                    PF = c.pf,
                    Soc = c.soc
                })
                .ToDictionary(k => k.Id, v => v);
        }
    }
}
