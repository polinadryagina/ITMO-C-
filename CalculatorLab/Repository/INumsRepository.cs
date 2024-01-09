namespace CalculatorLab.Repository;

public interface INumsRepository
{
    void Save(List<double> nums, int method);
    List<double> Load(int method);
}