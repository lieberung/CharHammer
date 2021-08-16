namespace BlazorWjdr.Services
{
    using Models;
    using System.Collections.Generic;
    using System.Linq;

    public class ProfilsService
    {
        private Dictionary<int, ProfilDto>? _cacheProfils;

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
                    Cc = c.cc,
                    Ct = c.ct,
                    Dex = c.dex,
                    E = c.e,
                    F = c.f,
                    Fm = c.fm,
                    I = c.i,
                    Int = c.intel,
                    M = c.m,
                    Mag = c.mag,
                    Pd = c.pd,
                    Pf = c.pf,
                    Soc = c.soc
                })
                .ToDictionary(k => k.Id, v => v);
        }
    }
}
