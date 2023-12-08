using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Text.Json;
using System.Xml.Serialization;
using System.IO;
using System.Data.SQLite;

namespace WpfCalculator
{
    public partial class MainWindow : Window
    {
        private List<double> history = new List<double>();
        private double current = 0;

        public MainWindow()
        {
            InitializeComponent();
            txtInput.KeyDown += TxtInput_KeyDown;
        }

        private void TxtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ProcessCommand();
            }
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            ProcessCommand();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var method = MessageBox.Show("Choose save method: Yes - JSON, No - XML, Cancel - SQLite", "Save", MessageBoxButton.YesNoCancel);
            Save(history, method.ToString());
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            var method = MessageBox.Show("Choose load method: Yes - JSON, No - XML, Cancel - SQLite", "Load", MessageBoxButton.YesNoCancel);
            history = Load(method.ToString());
            current = history[^1];
        }

        private void ProcessCommand()
        {
            var input = txtInput.Text;
            if (input.StartsWith(">"))
            {
                current = double.Parse(input.Substring(1));
                history.Add(current);
                lstHistory.Items.Add($"[#{history.Count}] = {current}");
            }
            else if (input.StartsWith("@"))
            {
                var operation = input.Substring(3);
                if (operation.StartsWith("#"))
                {
                    var step = int.Parse(operation.Substring(1));
                    current = history[step - 1];
                    history.Add(current);
                    lstHistory.Items.Add($"[#{history.Count}] = {current}");
                }
                else
                {
                    var operand = double.Parse(txtInput.Text.Substring(1));
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
                    lstHistory.Items.Add($"[#{history.Count}] = {current}");
                }
            }
            txtInput.Clear();
        }

        private void Save(List<double> history, string method)
        {
            switch (method)
            {
                case "Yes":
                    var json = JsonSerializer.Serialize(history);
                    File.WriteAllText("history.json", json);
                    break;
                case "No":
                    var serializer = new XmlSerializer(typeof(List<double>));
                    using var stream = new FileStream("history.xml", FileMode.Create);
                    serializer.Serialize(stream, history);
                    break;
                case "Cancel":
                    using var connection = new SQLiteConnection("Data Source=history.db");
                    connection.Open();
                    using var command = new SQLiteCommand(connection);
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

        private List<double> Load(string method)
        {
            switch (method)
            {
                case "Yes":
                    var json = File.ReadAllText("history.json");
                    return JsonSerializer.Deserialize<List<double>>(json);
                case "No":
                    var serializer = new XmlSerializer(typeof(List<double>));
                    using var stream = new FileStream("history.xml", FileMode.Open);
                    return (List<double>)serializer.Deserialize(stream);
                case "Cancel":
                    using var connection = new SQLiteConnection("Data Source=history.db");
                    connection.Open();
                    using var command = new SQLiteCommand("SELECT * FROM History", connection);
                    using var reader = command.ExecuteReader();
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
}