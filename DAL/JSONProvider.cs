using System.Text.Json;
using System.Text.Json.Serialization;
namespace DAL;
public class JSONProvider<T>
{
    JsonSerializerOptions? options = new()
    {
        ReferenceHandler = ReferenceHandler.IgnoreCycles,
        WriteIndented = true
    };
    public JSONProvider() {}
    public void Write(List<T>? list, string file)
    {
        File.WriteAllText(file, string.Empty);
        using (FileStream fileStream = new FileStream(file, FileMode.OpenOrCreate))
        {
            JsonSerializer.Serialize(fileStream, list, options);
        }
    }
    public List<T>? Read(string file)
    {
        using (FileStream fileStream = new FileStream(file, FileMode.OpenOrCreate))
        {
            return (List<T>?)JsonSerializer.Deserialize(fileStream, typeof(List<T>), options);
        }
    }
    public void DeleteAllFromFile(string file)
    {
        File.WriteAllText(file, string.Empty);
    }
}