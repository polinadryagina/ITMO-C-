/******************************************************************************

                            Online C# Compiler.
                Code, Compile, Run and Debug C# program online.
Write your code in this editor and press "Run" button to execute it.

*******************************************************************************/

using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        var history = new List<double>();
        double current = 0;
        string input;
        Console.WriteLine("Usage:\nwhen a first symbol on line is ‘>’ – enter operand (number)\nwhen a first symbol on line is ‘@’ – enter operation\noperation is one of ‘+’, ‘-‘, ‘/’, ‘*’ or\n‘#’ followed with number of evaluation step\n‘q’ to exit");

        while (true)
        {
            input = Console.ReadLine();
            if (input.StartsWith(">"))
            {
                current = double.Parse(input.Substring(1));
                history.Add(current);
                Console.WriteLine($"[#{history.Count}] = {current}");
            }
            else if (input.StartsWith("@"))
            {
                var operation = input.Substring(3);
                if (operation == "q")
                {
                    break;
                }
                else if (operation.StartsWith("#"))
                {
                    var step = int.Parse(operation.Substring(1));
                    current = history[step - 1];
                    history.Add(current);
                    Console.WriteLine($"[#{history.Count}] = {current}");
                }
                else
                {
                    var operand = double.Parse(Console.ReadLine().Substring(1));
                    switch (operation)
                    {
                        case "+":
                            current += operand;
                            break;
                        case "-":
                            current -= operand;
                            break;
                        case "*":
                            current *= operand;
                            break;
                        case "/":
                            current /= operand;
                            break;
                    }
                    history.Add(current);
                    Console.WriteLine($"[#{history.Count}] = {current}");
                }
            }
        }
    }
}
