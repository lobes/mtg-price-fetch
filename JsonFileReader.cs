using Newtonsoft.Json;
using System.IO;

public static class JsonFileReader
{
    public static T? Read<T>(string? filePath)
    {
        if (filePath is null) filePath = "./cards.json";

        using StreamReader r = new StreamReader(filePath);
        string json = r.ReadToEnd();
        var cards = JsonConvert.DeserializeObject<T?>(json);
        return cards;
    }
}