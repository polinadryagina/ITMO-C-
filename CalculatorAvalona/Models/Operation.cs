namespace CalculatorAvalona.Models;

public class Operation
{
    public string Expression { get; set; }
    public double Result { get; set; }
    public bool IsSuccessful { get; set; }
    public string ErrorMessage { get; set; }
}
