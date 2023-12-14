using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Text.Json;
using System.Data.SQLite;

class Test
{
    static void Main()
    {
        Console.WriteLine(TestCode(
                            new string[] {
                                "> 3",
                                "@: s",
                                "2",
                                "@: +",
                                "> 5",
                                "@: /",
                                "> 2",
                                "@: s",
                                "1",
                                "@: #2",
                                "@: l",
                                "2",
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
                                "Choose save method: 1 - JSON, 2 - XML, 3 - SQLite",
                                "[#2] = 8",
                                "[#3] = 4",
                                "Choose save method: 1 - JSON, 2 - XML, 3 - SQLite",
                                "[#4] = 8",
                                "Choose load method: 1 - JSON, 2 - XML, 3 - SQLite",
                                "[#2] = 30"
                            }
        ));
        Console.WriteLine(TestCode(
                            new string[] {
                                "> 5",
                                "@: s",
                                "3",
                                "@: *",
                                "> 0",
                                "@: /",
                                "> 2",
                                "@: #2",
                                "@: #1",
                                "@: *",
                                "> 10",
                                "@: l",
                                "3",
                                "@: +",
                                "> 2",
                                "@: q"
                            }, 
                            new string[] {
                                "Usage:\nwhen a first symbol on line is ‘>’" + 
                                " – enter operand (number)\nwhen a first symbol" + 
                                " on line is ‘@’ – enter operation\noperation is" + 
                                " one of ‘+’, ‘-‘, ‘/’, ‘*’ or\n‘#’ followed with" + 
                                " number of evaluation step\n‘q’ to exit",
                                "[#1] = 5",
                                "Choose save method: 1 - JSON, 2 - XML, 3 - SQLite",
                                "[#2] = 0",
                                "[#3] = 0",
                                "[#4] = 0",
                                "[#5] = 5",
                                "[#6] = 50",
                                "Choose load method: 1 - JSON, 2 - XML, 3 - SQLite",
                                "[#2] = 7"
                            }
        ));
    }
    
    static void TestCode(string[] inputs, string[] output)
    {
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
                else if (operation == "s")
                {
                    if (output[outputInd++] != "Choose save method: 1 - JSON, 2 - XML, 3 - SQLite") {
                        return false;
                    }
                    var method = inputs[inputInd++];
                    Save(history, method);
                }
                else if (operation == "l")
                {
                    if (output[outputInd++] != "Choose load method: 1 - JSON, 2 - XML, 3 - SQLite") {
                        return false;
                    }
                    var method = inputs[inputInd++];
                    history = Load(method);
                    current = history[history.Count - 1];
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

     static void Save(List<double> history, string method)
    {
        switch (method)
        {
            case "1":
                var json = JsonSerializer.Serialize(history);
                File.WriteAllText("history.json", json);
                break;
            case "2":
                var serializer = new XmlSerializer(typeof(List<double>));
                using (var stream = new FileStream("history.xml", FileMode.Create))
                {
                    serializer.Serialize(stream, history);
                }
                break;
            case "3":
                using (var connection = new SQLiteConnection("Data Source=history.db"))
                {
                    connection.Open();
                    using (var command = new SQLiteCommand(connection))
                    {
                        command.CommandText = "CREATE TABLE IF NOT EXISTS History (Value REAL)";
                        command.ExecuteNonQuery();
                        command.CommandText = "DELETE FROM History";
                        command.ExecuteNonQuery();
                        foreach (var value in history)
                        {
                            command.CommandText = $"INSERT INTO History (Value) VALUES ({value})";
                            command.ExecuteNonQuery();
                        }
                    }
                }
                break;
        }
    }

    static List<double> Load(string method)
    {
        switch (method)
        {
            case "1":
                var json = File.ReadAllText("history.json");
                return JsonSerializer.Deserialize<List<double>>(json);
            case "2":
                var serializer = new XmlSerializer(typeof(List<double>));
                using (var stream = new FileStream("history.xml", FileMode.Open))
                {
                    return (List<double>)serializer.Deserialize(stream);
                }
                break;
            case "3":
                using (var connection = new SQLiteConnection("Data Source=history.db"))
                {
                    connection.Open();
                    using (var command = new SQLiteCommand("SELECT * FROM History", connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            var history = new List<double>();
                            while (reader.Read())
                            {
                                history.Add(reader.GetDouble(0));
                            }
                            return history;
                        }
                    }
                }
                break;
        }
        return new List<double>();
    }
}