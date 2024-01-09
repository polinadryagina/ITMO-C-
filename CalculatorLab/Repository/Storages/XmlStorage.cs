namespace CalculatorLab.Repository.Storages;

using System.Xml.Serialization;

public class XmlStorage : IStorage
{
    public void Save(List<double> nums)
    {
        var serializer = new XmlSerializer(typeof(List<double>));
        using (var stream = new FileStream("history.xml", FileMode.Create))
        {
            serializer.Serialize(stream, nums);
        }
    }

    public List<double> Load()
    {
        var serializer = new XmlSerializer(typeof(List<double>));
        using (var stream = new FileStream("history.xml", FileMode.Open))
        {
            return (List<double>)serializer.Deserialize(stream);
        }
    }
}