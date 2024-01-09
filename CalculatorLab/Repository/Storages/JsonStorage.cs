namespace CalculatorLab.Repository.Storages;

using System.Text.Json;

public class JsonStorage : IStorage
{
    public void Save(List<double> nums)
    {
        var json = JsonSerializer.Serialize(nums);
        File.WriteAllText("history.json", json);
    }

    public List<double> Load()
    {
        var json = File.ReadAllText("history.json");
        return JsonSerializer.Deserialize<List<double>>(json);
    }
}