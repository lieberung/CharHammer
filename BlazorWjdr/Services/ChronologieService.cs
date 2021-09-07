using BlazorWjdr.Models;
using System.Collections.Generic;
using System.Linq;

namespace BlazorWjdr.Services
{
    public class ChronologieService
    {
        private readonly ChronologieDto[] _chronologie;

        public ChronologieService(IEnumerable<ChronologieDto> dataChrono)
        {
            _chronologie = dataChrono.ToArray();
        }

        public ChronologieDto[] AllChronologie()
        {
            return _chronologie;
        }
    }
}
