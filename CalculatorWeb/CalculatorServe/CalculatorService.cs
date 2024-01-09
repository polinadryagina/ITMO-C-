using CalculatorLab.Calculator;
using CalculatorLab.Repository;

namespace LabCalculator.Calculator;

public class CalculatorService : ICalculatorService
{
    private ICalculator calculator;
    private INumsRepository repository;

    public CalculatorService()
    {
        calculator = new CalculatorLab.Calculator.Calculator();
        repository = new NumsRepository();
    }

    public double SetCur(double num)
    {
        calculator.SetCur(num);
        return calculator.GetCur();
    }

    public double ChangeCur(int ind)
    {
        calculator.ChangeCur(ind);
        return calculator.GetCur();
    }

    public double Operate(char operation, double operand)
    {
        switch (operation)
        {
            case '+':
                calculator.Add(operand);
                break;
            case '-':
                calculator.Sub(operand);
                break;
            case '*':
                calculator.Mul(operand);
                break;
            case '/':
                calculator.Div(operand);
                break;
        }

        return calculator.GetCur();
    }

    public async void Save(string method)
    {
        var id = method switch
        {
            "json" => 1,
            "xml" => 2,
            "sqlite" => 3,
            _ => 1
        };
        await Task.Run(() => repository.Save(calculator.GetNums(), id));
    }

    public async void Load(string method)
    {
        var id = method switch
        {
            "json" => 1,
            "xml" => 2,
            "sqlite" => 3,
            _ => 1
        };
        await Task.Run(() => calculator.SetNums(repository.Load(id)));
        calculator.ChangeCur(calculator.GetNums().Count);
    }
}