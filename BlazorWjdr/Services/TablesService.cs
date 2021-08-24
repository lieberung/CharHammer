﻿using System;
using System.Diagnostics;

namespace BlazorWjdr.Services
{
    using BlazorWjdr.DataSource.JsonDto;
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class TablesService
    {
        private readonly List<JsonTable> _dataTables;
        private Dictionary<int, TableDto>? _cacheTable;
        private List<TableDto>? _allTables;

        public TablesService(List<JsonTable> dataTables)
        {
            _dataTables = dataTables;
        }

        public List<TableDto> AllTables
        {
            get
            {
                if (_allTables == null)
                    Initialize();
                Debug.Assert(_allTables != null, nameof(_allTables) + " != null");
                return _allTables;
            }
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

            _allTables = _cacheTable.Values.ToList();

            //_dataTables.Clear();
        }
    }
}
