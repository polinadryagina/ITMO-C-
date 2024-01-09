namespace LabCalculator.Calculator;

public interface ICalculatorService
{
    double SetCur(double num);
    double ChangeCur(int ind);
    double Operate(char operation, double operand);
    void Save(string method);
    void Load(string method);
}