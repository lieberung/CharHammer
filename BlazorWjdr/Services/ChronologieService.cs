using BlazorWjdr.Models;
using System.Collections.Generic;
using System.Linq;

namespace BlazorWjdr.Services
{
    public class ChronologieService
    {
        private readonly ChronologieDto[] _chronologie;

        public ChronologieService(IEnumerable<ChronologieDto> chronologie)
        {
            _chronologie = chronologie.ToArray();
        }

        public ChronologieDto[] AllChronologie() => _chronologie;
    }
}
