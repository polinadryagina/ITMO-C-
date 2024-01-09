namespace CalculatorLab.Calculator;

public class Calculator : ICalculator
{
    private List<double> nums;
    private double cur;

    public Calculator()
    {
        nums = new List<double>();
        cur = 0;
    }

    public void Add(double num)
    {
        cur += num;
        ProcessRes();
    }

    public void Sub(double num)
    {
        cur -= num;
        ProcessRes();
    }

    public void Mul(double num)
    {
        cur *= num;
        ProcessRes();
    }

    public void Div(double num)
    {
        cur /= num;
        ProcessRes();
    }

    public void SetCur(double num)
    {
        cur = num;
        ProcessRes();
    }

    public void ChangeCur(int ind)
    {
        cur = nums[ind - 1];
        ProcessRes();
    }

    public double GetCur()
    {
        return cur;
    }

    public List<double> GetNums()
    {
        return nums;
    }

    public void SetNums(List<double> nums)
    {
        this.nums = nums;
    }

    private void ProcessRes()
    {
        nums.Add(cur);
        Console.WriteLine($"[#{nums.Count}] = {cur}");
    }
}