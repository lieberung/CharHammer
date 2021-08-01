namespace BlazorWjdr.Services
{
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class TablesService
    {
        private Dictionary<int, TableDto>? _cacheTable;
        private List<TableDto>? _allTables;

        private List<TableDto> AllTables
        {
            get
            {
                if (_allTables == null)
                    Initialize();
#pragma warning disable CS8603 // Possible null Table return.
                return _allTables;
#pragma warning restore CS8603 // Possible null Table return.
            }
        }
        
        public Task<TableDto[]> Items()
        {
            return Task.FromResult(AllTables.ToArray());
        }

        public TableDto GetTable(int id)
        {
            if (_cacheTable == null)
                Initialize();
#pragma warning disable CS8602 // DeTable of a possibly null Table.
            return _cacheTable[id];
#pragma warning restore CS8602 // DeTable of a possibly null Table.
        }

        private void Initialize()
        {
            _cacheTable = DataSource.JsonLoader
                .GetRootTable()
                .items
                .Select(c => new TableDto
                {
                    Id = c.id,
                    Titre = c.titre,
                    Description = c.description,
                    Lignes = c.lignes
                })
                .ToDictionary(k => k.Id, v => v);
            _allTables = _cacheTable.Values.ToList();
        }
    }
}
