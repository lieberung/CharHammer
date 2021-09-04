using System.Diagnostics;

namespace BlazorWjdr.Services
{
    using BlazorWjdr.DataSource.JsonDto;
    using Models;
    using System.Collections.Generic;
    using System.Linq;

    public class ProfilsService
    {
        private readonly List<JsonProfil> _dataProfils;
        private Dictionary<int, ProfilDto>? _cacheProfils;

        public ProfilsService(List<JsonProfil> dataProfils)
        {
            _dataProfils = dataProfils;
        }

        public ProfilDto GetProfil(int id)
        {
            if (_cacheProfils == null)
                Initialize();
            Debug.Assert(_cacheProfils != null, nameof(_cacheProfils) + " != null");
            return _cacheProfils[id];
        }

        private void Initialize()
        {
            _cacheProfils = _dataProfils
                .Select(c => new ProfilDto
                {
                    Id = c.id,
                    A = c.a,
                    Ag = c.ag,
                    //B = c.b,
                    Cc = c.cc,
                    Ct = c.ct,
                    Dex = c.dex,
                    E = c.e,
                    F = c.f,
                    Fm = c.fm,
                    I = c.i,
                    Int = c.intel,
                    //M = c.m,
                    //Mag = c.mag,
                    //Pd = c.pd,
                    //Pf = c.pf,
                    Soc = c.soc
                })
                .ToDictionary(k => k.Id, v => v);

            //_dataProfils.Clear();
        }
    }
}
