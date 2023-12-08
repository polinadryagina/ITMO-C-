/******************************************************************************

                            Online C# Compiler.
                Code, Compile, Run and Debug C# program online.
Write your code in this editor and press "Run" button to execute it.

*******************************************************************************/

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Xml.Serialization;
using System.IO;
using System.Data.SQLite;

class Program
{
    static void Main()
    {
        var history = new List<double>();
        double current = 0;
        string input;
        Console.WriteLine("Usage:\nwhen a first symbol on line is ‘>’ – enter operand (number)\nwhen a first symbol on line is ‘@’ – enter operation\noperation is one of ‘+’, ‘-‘, ‘/’, ‘*’ or\n‘#’ followed with number of evaluation step\n‘q’ to exit\n's' to save\n'l' to load");

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
                else if (operation == "s")
                {
                    Console.WriteLine("Choose save method: 1 - JSON, 2 - XML, 3 - SQLite");
                    var method = Console.ReadLine();
                    Save(history, method);
                }
                else if (operation == "l")
                {
                    Console.WriteLine("Choose load method: 1 - JSON, 2 - XML, 3 - SQLite");
                    var method = Console.ReadLine();
                    history = Load(method);
                    current = history[history.Count - 1];
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
                using (var stream = new FileStream("history.xml", FileMode.Create));
                serializer.Serialize(stream, history);
                break;
            case "3":
                using (var connection = new SQLiteConnection("Data Source=history.db"));
                connection.Open();
                using (var command = new SQLiteCommand(connection));
                command.CommandText = "CREATE TABLE IF NOT EXISTS History (Value REAL)";
                command.ExecuteNonQuery();
                command.CommandText = "DELETE FROM History";
                command.ExecuteNonQuery();
                foreach (var value in history)
                {
                    command.CommandText = $"INSERT INTO History (Value) VALUES ({value})";
                    command.ExecuteNonQuery();
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
                using (var stream = new FileStream("history.xml", FileMode.Open));
                return (List<double>)serializer.Deserialize(stream);
            case "3":
                using (var connection = new SQLiteConnection("Data Source=history.db"));
                connection.Open();
                using (var command = new SQLiteCommand("SELECT * FROM History", connection));
                using (var reader = command.ExecuteReader());
                var history = new List<double>();
                while (reader.Read())
                {
                    history.Add(reader.GetDouble(0));
                }
                return history;
            default:
                return new List<double>();
        }
    }
}
