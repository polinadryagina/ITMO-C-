using Avalonia.Media;

namespace CalculatorAvalona.ViewModels;

using CalculatorLab.Calculator;
using CalculatorLab.Repository;
using System.Collections.ObjectModel;
using System.Globalization;
using ReactiveUI;
using System.Reactive;

public class MainWindowViewModel : ViewModelBase
{
    private readonly ICalculator _calculator;
    private INumsRepository _repository;

    public ObservableCollection<LogEntry> LogEntries { get; }

    public MainWindowViewModel()
    {
        _calculator = new Calculator();
        _repository = new NumsRepository();
        LogEntries = new ObservableCollection<LogEntry>();
        ExecuteCommand("> 0");
    }

    public void ExecuteCommand(string command)
    {
        var operand = 0d;
        if (command.Length > 1)
        {
            operand = double.Parse(command.Substring(1));
        }
        switch (command[0])
        {
            case '+':
                _calculator.Add(operand);
                break;
            case '-':
                _calculator.Sub(operand);
                break;
            case '*':
                _calculator.Mul(operand);
                break;
            case '/':
                _calculator.Div(operand);
                break;
            case 's':
                _repository.Save(_calculator.GetNums(), (int)operand);
                break;
            case 'l':
                _calculator.SetNums(_repository.Load((int)operand));
                _calculator.ChangeCur(_calculator.GetNums().Count);
                break;
            case '>':
                _calculator.SetCur(operand);
                break;
            case '#':
                _calculator.ChangeCur((int)operand);
                break;
            default:
                LogEntries.Insert(0, new LogEntry { Text = $"{command}\nInvalid command", Color = Brushes.Red });
                return;
        }
        LogEntries.Insert(0, new LogEntry { Text = $"{command}\n{_calculator.CurToString()}", Color = Brushes.Black });
    }
}

public class LogEntry
{
    public string Text { get; set; }
    public IImmutableSolidColorBrush Color { get; set; }
}