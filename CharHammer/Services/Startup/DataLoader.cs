using CharHammer.DataSource;
using Newtonsoft.Json;
using System.IO.Compression;
using System.Text;

namespace CharHammer.Services.Startup;

public class DataLoader(HttpClient httpClient)
{
  public async Task<DataJson> LoadData()
  {
    Console.Write("Loading json data... ");
    var startTime = DateTime.Now;

    using var response = await httpClient.GetAsync("data/data.json.gz", HttpCompletionOption.ResponseHeadersRead);
    response.EnsureSuccessStatusCode();

    // En WebAssembly, le flux réseau n'accepte pas les lectures synchrones (celles de GZipStream et de Newtonsoft) :
    // on recopie donc de façon asynchrone le fichier compressé (≈ 570 Ko) dans un MemoryStream, puis on décompresse
    // et on désérialise en flux, sans matérialiser ni le JSON décompressé en byte[], ni la chaîne complète.
    using var compressed = new MemoryStream();
    await response.Content.CopyToAsync(compressed);
    compressed.Position = 0;

    var data = Deserialize(compressed);
    ArgumentNullException.ThrowIfNull(data);
    Console.WriteLine($"{DateTime.Now.Subtract(startTime).TotalSeconds}sec.");
    return data;
  }

  private static DataJson? Deserialize(Stream gzipStream)
  {
    using var gzip = new GZipStream(gzipStream, CompressionMode.Decompress, leaveOpen: true);
    using var streamReader = new StreamReader(gzip, Encoding.UTF8);
    using var jsonReader = new JsonTextReader(streamReader);
    return JsonSerializer.CreateDefault().Deserialize<DataJson>(jsonReader);
  }
}
