using System.Text.Json;
using ClientsManager.Application.Services.Interfaces;

namespace ClientsManager.Application.Services;

public class SerializationService : ISerializationService
{
    public async Task SerializeToFileAsync<T>(T obj, string path)
    {
        using var stream = File.Open(path, FileMode.CreateNew);
        await JsonSerializer.SerializeAsync<T>(stream, obj);
    }

    public async Task<T?> DeserializeFromFileAsync<T>(string path)
    {
        using var stream = File.OpenRead(path);
        return await JsonSerializer.DeserializeAsync<T>(stream);
    }
}
