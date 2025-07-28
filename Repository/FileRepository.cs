using System.Text.Json;

namespace FTSAirportTicketBookingSystem.Repository;

public class FileRepository : IRepository
{
    private static FileRepository? _instance;

    public static FileRepository Instance
    {
        get => _instance ??= new FileRepository();
    }
    
    private static readonly string RootPath;
    static FileRepository()
    {
        RootPath = FindProjectPath();
    }
    
    public async Task<ICollection<T>> ReadAsync<T>() where T : class
    {
        var collectionName = typeof(T).Name;
        var filePath = GetFilePath(collectionName);
        await CreateFileIfDoesNotExist(filePath);
        return await ReadAsCollectionFromFileAsync<T>(filePath);
    }

    public async Task WriteAsync<T>(T data) where T : class
    {
        var collectionName = typeof(T).Name;
        var filePath = GetFilePath(collectionName);
        await CreateFileIfDoesNotExist(filePath);
        var fileContent = await File.ReadAllTextAsync(filePath);
        var items = JsonSerializer.Deserialize<List<T>>(fileContent) ?? new List<T>();
        items.Add(data);
        JsonSerializerOptions options = new()
        {
            WriteIndented = true
        };

        JsonSerializerOptions optionsCopy = new(options);
        var updatedJson = JsonSerializer.Serialize(items, optionsCopy);
        await File.WriteAllTextAsync(filePath, updatedJson);
    }
    
    private static async Task CreateFileIfDoesNotExist(string filePath)
    {
        if (File.Exists(filePath)) return;
        Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
        await File.WriteAllTextAsync(filePath, "[]");
    }
    private static string FindProjectPath()
    {
        var current = Directory.GetCurrentDirectory();
        while (current != null && !File.Exists(Path.Combine(current, "Program.cs")))
        {
            current = Directory.GetParent(current)?.FullName;
        }
        return current!;
    }

    private static string GetFilePath(string collectionName)
    {
        return Path.Combine(RootPath, "Data", $"{collectionName}.json");
    }

    private static async Task<ICollection<T>> ReadAsCollectionFromFileAsync<T>(string filePath)
    where T : class
    {
        var fileContent = await File.ReadAllTextAsync(filePath);
        var serializedContent = JsonSerializer.Deserialize<List<T>>(fileContent);
        return serializedContent ?? [];
    }
}