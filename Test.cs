using System;
using System.Collections.Generic;

class Test
{
    static void Main()
    {
        Console.WriteLine(TestCode(
                            new string[] {
                                "> 3",
                                "@: +",
                                "> 5",
                                "@: /",
                                "> 2",
                                "@: #2",
                                "@: *",
                                "> 10",
                                "@: q"
                            }, 
                            new string[] {
                                "Usage:\nwhen a first symbol on line is ‘>’" + 
                                " – enter operand (number)\nwhen a first symbol" + 
                                " on line is ‘@’ – enter operation\noperation is" + 
                                " one of ‘+’, ‘-‘, ‘/’, ‘*’ or\n‘#’ followed with" + 
                                " number of evaluation step\n‘q’ to exit",
                                "[#1] = 3",
                                "[#2] = 8",
                                "[#3] = 4",
                                "[#4] = 8",
                                "[#5] = 80"
                            }
        ));
        Console.WriteLine(TestCode(
                            new string[] {
                                "> 5",
                                "@: *",
                                "> 0",
                                "@: /",
                                "> 2",
                                "@: #2",
                                "@: #1",
                                "@: *",
                                "> 10",
                                "@: q"
                            }, 
                            new string[] {
                                "Usage:\nwhen a first symbol on line is ‘>’" + 
                                " – enter operand (number)\nwhen a first symbol" + 
                                " on line is ‘@’ – enter operation\noperation is" + 
                                " one of ‘+’, ‘-‘, ‘/’, ‘*’ or\n‘#’ followed with" + 
                                " number of evaluation step\n‘q’ to exit",
                                "[#1] = 5",
                                "[#2] = 0",
                                "[#3] = 0",
                                "[#4] = 0",
                                "[#5] = 5",
                                "[#6] = 50"
                            }
        ));
    }
    
    static bool TestCode(string[] inputs, string[] output) {
        var history = new List<double>();
        double current = 0;
        string input;
        int inputInd = 0;
        int outputInd = 0;
        if (
            output[outputInd++] != "Usage:\nwhen a first symbol on line is ‘>’" + 
                                    " – enter operand (number)\nwhen a first symbol" + 
                                    " on line is ‘@’ – enter operation\noperation is" + 
                                    " one of ‘+’, ‘-‘, ‘/’, ‘*’ or\n‘#’ followed with" + 
                                    " number of evaluation step\n‘q’ to exit"
        ) {
            return false;
        }

        while (true)
        {
            input = inputs[inputInd++];
            if (input.StartsWith(">"))
            {
                current = double.Parse(input.Substring(1));
                history.Add(current);
                if (output[outputInd++] != $"[#{history.Count}] = {current}") {
                    return false;
                }
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
                    if (output[outputInd++] != $"[#{history.Count}] = {current}") {
                        return false;
                    }
                }
                else
                {
                    var operand = double.Parse(inputs[inputInd++].Substring(1));
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
                    if (output[outputInd++] != $"[#{history.Count}] = {current}") {
                        return false;
                    }
                }
            }
        }
        return true;
    }
}
