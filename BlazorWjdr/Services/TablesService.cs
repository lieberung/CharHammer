namespace BlazorWjdr.Services
{
    using System;
    using System.Diagnostics;
    using DataSource.JsonDto;
    using Models;
    using System.Collections.Generic;
    using System.Linq;

    public class TablesService
    {
        private readonly List<JsonTable> _dataTables;
        private Dictionary<int, TableDto>? _cacheTable;

        public TablesService(List<JsonTable> dataTables)
        {
            _dataTables = dataTables;
        }

        public TableDto GetTable(int id)
        {
            if (_cacheTable == null)
                Initialize();
            Debug.Assert(_cacheTable != null, nameof(_cacheTable) + " != null");
            return _cacheTable[id];
        }

        private void Initialize()
        {
            _cacheTable = _dataTables
                .Select(c => new TableDto
                {
                    Id = c.id,
                    Titre = c.titre,
                    Description = c.description,
                    StylesHeader = c.styles_th ?? Array.Empty<string>(),
                    StylesRows = c.styles_td ?? Array.Empty<string>(),
                    Lignes = c.lignes
                })
                .ToDictionary(k => k.Id, v => v);
        }
    }
}
