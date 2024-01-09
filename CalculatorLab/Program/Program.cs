using System;
using System.Collections.Generic;
using System.IO;
using CalculatorLab.Calculator;
using CalculatorLab.Repository;

class Program
{
    static void Main()
    {
        ICalculator calculator = new Calculator();
        INumsRepository repository = new NumsRepository();
        string input;
        Console.WriteLine(
            "Usage:\nwhen a first symbol on line is ‘>’ – enter operand (number)\nwhen a first symbol on line is ‘@’ – enter operation\noperation is one of ‘+’, ‘-‘, ‘/’, ‘*’ or\n‘#’ followed with number of evaluation step\n‘q’ to exit\n's' to save\n'l' to load");

        while (true)
        {
            input = Console.ReadLine();
            if (input.StartsWith(">"))
            {
                calculator.SetCur(double.Parse(input.Substring(1)));
            }
            else if (input.StartsWith("@"))
            {
                var operation = input.Substring(3);
                if (operation == "q")
                {
                    break;
                }
                
                if (operation.StartsWith("#"))
                {
                    var step = int.Parse(operation.Substring(1));
                    calculator.ChangeCur(step);
                }
                else switch (operation)
                {
                    case "s":
                    {
                        Console.WriteLine("Choose save method: 1 - JSON, 2 - XML, 3 - SQLite");
                        var method = Convert.ToInt32(Console.ReadLine());
                        repository.Save(calculator.GetNums(), method);
                        break;
                    }
                    case "l":
                    {
                        Console.WriteLine("Choose load method: 1 - JSON, 2 - XML, 3 - SQLite");
                        var method = Convert.ToInt32(Console.ReadLine());
                        calculator.SetNums(repository.Load(method));
                        calculator.ChangeCur(calculator.GetNums().Count);
                        break;
                    }
                    default:
                    {
                        var operand = double.Parse(Console.ReadLine().Substring(1));
                        switch (operation)
                        {
                            case "+":
                                calculator.Add(operand);
                                break;
                            case "-":
                                calculator.Sub(operand);
                                break;
                            case "*":
                                calculator.Mul(operand);
                                break;
                            case "/":
                                calculator.Div(operand);
                                break;
                        }
                        break;
                    }
                }
            }
        }
    }
}