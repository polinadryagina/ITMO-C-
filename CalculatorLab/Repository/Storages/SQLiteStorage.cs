namespace CalculatorLab.Repository.Storages;

// using System.Data.SQLite;

public class SQLiteStorage : IStorage
{
    public void Save(List<double> nums)
    {
        // using (var connection = new SQLiteConnection("Data Source=history.db"))
        // {
        //     connection.Open();
        //     using (var command = new SQLiteCommand(connection))
        //     {
        //         command.CommandText = "CREATE TABLE IF NOT EXISTS History (Value REAL)";
        //         command.ExecuteNonQuery();
        //         command.CommandText = "DELETE FROM History";
        //         command.ExecuteNonQuery();
        //         foreach (var value in nums)
        //         {
        //             command.CommandText = $"INSERT INTO History (Value) VALUES ({value})";
        //             command.ExecuteNonQuery();
        //         }
        //     }
        // }
    }

    public List<double> Load()
    {
        // using (var connection = new SQLiteConnection("Data Source=history.db"))
        // {
        //     connection.Open();
        //     using (var command = new SQLiteCommand("SELECT * FROM History", connection))
        //     {
        //         using (var reader = command.ExecuteReader())
        //         {
        //             var nums = new List<double>();
        //             while (reader.Read())
        //             {
        //                 nums.Add(reader.GetDouble(0));
        //             }
        //             return nums;
        //         }
        //     }
        // }
        return null;
    }
}