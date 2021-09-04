namespace BlazorWjdr.Services
{
    using Models;
    using System.Collections.Generic;

    public class ProfilsService
    {
        private readonly Dictionary<int, ProfilDto> _cacheProfils;

        public ProfilsService(Dictionary<int, ProfilDto> dataProfils)
        {
            _cacheProfils = dataProfils;
        }

        public ProfilDto GetProfil(int id)
        {
            return _cacheProfils[id];
        }
    }
}
