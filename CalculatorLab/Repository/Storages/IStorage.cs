namespace CalculatorLab.Repository.Storages;

public interface IStorage
{
    void Save(List<double> nums);
    List<double> Load();
}