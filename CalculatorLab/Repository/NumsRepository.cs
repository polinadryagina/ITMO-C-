using CalculatorLab.Repository.Storages;

namespace CalculatorLab.Repository;

public class NumsRepository : INumsRepository
{
    private IStorage[] storages;

    public NumsRepository()
    {
        storages = new IStorage[] { new JsonStorage(), new XmlStorage(), new SQLiteStorage() };
    }

    public void Save(List<double> nums, int method)
    {
        storages[method - 1].Save(nums);
    }

    public List<double> Load(int method)
    {
        return storages[method - 1].Load();
    }
}