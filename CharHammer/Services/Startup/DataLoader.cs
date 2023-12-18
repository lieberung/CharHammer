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

        var response = await httpClient.GetAsync("data/data.json.gz");
        var gzipData = await response.Content.ReadAsByteArrayAsync();
        ArgumentNullException.ThrowIfNull(gzipData);

        var jsonData = DecompressGzip(gzipData);
        var data = JsonConvert.DeserializeObject<DataJson>(jsonData);
        /*
        var data = await httpClient.GetFromJsonAsync<DataJson>("data/data.json");
        */
        ArgumentNullException.ThrowIfNull(data);
        Console.WriteLine($"{DateTime.Now.Subtract(startTime).TotalSeconds}sec.");
        return data;
    }

    public static string DecompressGzip(byte[] buffer)
    {
        byte[] decompressed;
        using (var inputStream = new MemoryStream(buffer))
        {
            using var outputStream = new MemoryStream();
            using (var gzip = new GZipStream(inputStream, CompressionMode.Decompress, leaveOpen: true))
            {
                gzip.CopyTo(outputStream);
            }
            decompressed = outputStream.ToArray();
        }
        return Encoding.UTF8.GetString(decompressed);
    }

    //static string DecompressGzip(byte[] inputBytes)
    //{
    //    using var inputStream = new MemoryStream(inputBytes);
    //    using var gZipStream = new GZipStream(inputStream, CompressionMode.Decompress);
    //    using var streamReader = new StreamReader(gZipStream);
    //    return streamReader.ReadToEnd();
    //}
}
