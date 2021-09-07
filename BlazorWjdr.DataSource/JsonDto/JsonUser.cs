// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Global
namespace BlazorWjdr.DataSource.JsonDto
{
    using System.Collections.Generic;

    public class JsonUser
    {
        public int id { get; set; }
        public string email { get; set; } = null!;
        public string pseudo { get; set; } = null!;
        public string password { get; set; } = null!;
    }

    public class RootUser
    {
        public List<JsonUser> items { get; set; } = null!;
    }
}
