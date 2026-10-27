using CharHammer.DataSource;
using System.Diagnostics;
using System.IO.Compression;
using System.Text.Json;

namespace CharHammer.Services.Startup;

public class DataLoader(HttpClient httpClient)
{
  public async Task<DataJson> LoadData()
  {
    Console.Write("Loading json data... ");
    var chrono = Stopwatch.StartNew();

    using var response = await httpClient.GetAsync("data/data.json.gz", HttpCompletionOption.ResponseHeadersRead);
    response.EnsureSuccessStatusCode();

    // Lecture 100 % asynchrone : flux réseau -> GZipStream -> System.Text.Json (contexte généré, sans réflexion).
    // Ni le fichier décompressé, ni la chaîne JSON complète ne sont matérialisés en mémoire.
    await using var network = await response.Content.ReadAsStreamAsync();
    await using var gzip = new GZipStream(network, CompressionMode.Decompress);
    var data = await JsonSerializer.DeserializeAsync(gzip, DataJsonContext.Default.DataJson);

    ArgumentNullException.ThrowIfNull(data);
    Console.WriteLine($"{chrono.ElapsedMilliseconds} ms.");
    return data;
  }
}
