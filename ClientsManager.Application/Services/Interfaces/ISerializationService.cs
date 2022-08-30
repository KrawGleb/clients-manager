namespace ClientsManager.Application.Services.Interfaces;

public interface ISerializationService
{
    Task SerializeToFileAsync<T>(T obj, string path);
    Task<T?> DeserializeFromFileAsync<T>(string path);
}