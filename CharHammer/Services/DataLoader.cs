using CharHammer.DataSource;
using System.Net.Http.Json;

namespace CharHammer.Services;

public class DataLoader(HttpClient httpClient)
{
    public async Task<DataJson> LoadData()
    {
        Console.Write("Loading json data... ");
        var startTime = DateTime.Now;
        var data = await httpClient.GetFromJsonAsync<DataJson>("json-data/data.json");
        ArgumentNullException.ThrowIfNull(data);
        Console.WriteLine($"{DateTime.Now.Subtract(startTime).TotalSeconds}sec.");
        return data;
    }
}
