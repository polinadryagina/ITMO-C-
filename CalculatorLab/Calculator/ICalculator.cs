namespace CalculatorLab.Calculator;

public interface ICalculator
{
    void Add(double num);
    void Sub(double num);
    void Mul(double num);
    void Div(double num);
    void SetCur(double num);
    void ChangeCur(int ind);
    double GetCur();
    string CurToString();
    List<double> GetNums();
    void SetNums(List<double> nums);
}