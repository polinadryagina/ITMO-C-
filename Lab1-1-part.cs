using System;

class Program
{
    static void Main()
    {
        Test(null, 0, 0, null);
        Console.WriteLine();
        Test(new double[] { -5, -4, -3, -2, -1, 0, 1, 2, 3, 4 }, 0, 9, new double[] { -2, -1, 0, 1, 2, -5, -4, -3, 3, 4 });
        Console.WriteLine();
        Test(new double[] { 5, 4, 3, 2, 1, 0, -1, -2, -3, -4 }, 0, 0, new double[] { 2, 1, 0, -1, -2, 5, 4, 3, -3, -4 });
        Console.WriteLine();
        Test(new double[] { -1, -2, -3, -4, -5, 0, 1, 2, 3, 4 }, 4, 9, new double[] { -1, -2, 0, 1, 2, -3, -4, -5, 3, 4 });
    }

    static void Test(double[] array, int expectedMaxIndex, double expectedSum, double[] expectedArray)
    {
        int N = 10;
        if (array == null)
        {
            array = new double[N];
            Random rand = new Random();

            // Заполнение массива случайными числами
            for (int i = 0; i < N; i++)
            {
                array[i] = rand.NextDouble() * 10 - 5; // числа от -5 до 5
            }
        }
        
        Console.WriteLine("Данный массив: " + string.Join(", ", array));

        int maxIndex = 0;
        double sum = 0;
        bool foundPositive = false;
        for (int i = 0; i < N; i++)
        {
            if (Math.Abs(array[i]) > Math.Abs(array[maxIndex]))
            {
                maxIndex = i;
            }

            if (foundPositive)
            {
                sum += array[i];
            }
            else if (array[i] > 0)
            {
                foundPositive = true;
            }
        }
        
        if (expectedArray != null) {
            if (maxIndex != expectedMaxIndex) {
                Console.WriteLine("Номер максимального по модулю элемента: " + maxIndex + ", ожидается: " + expectedMaxIndex);
                return;
            }
            if (sum != expectedSum) {
                Console.WriteLine("Сумма элементов после первого положительного: " + sum + ", ожидается: " + expectedSum);
                return;
            }
        }
        else
        {
            Console.WriteLine("Номер максимального по модулю элемента: " + maxIndex);
            Console.WriteLine("Сумма элементов после первого положительного: " + sum);
        }

        // Преобразование массива
        double a = -2, b = 2;
        double[] newArray = new double[N];
        int index = 0;
        for (int i = 0; i < N; i++)
        {
            if (Math.Floor(array[i]) >= a && Math.Floor(array[i]) <= b)
            {
                newArray[index++] = array[i];
            }
        }
        for (int i = 0; i < N; i++)
        {
            if (Math.Floor(array[i]) < a || Math.Floor(array[i]) > b)
            {
                newArray[index++] = array[i];
            }
        }
        
        if (expectedArray != null) {
            bool equal = true;
            for (int i = 0; i < N; i++)
            {
                if (newArray[i] != expectedArray[i])
                {
                    Console.WriteLine("Преобразованный массив: " + string.Join(", ", newArray));
                    Console.WriteLine("Ожидается: " + string.Join(", ", expectedArray));
                    return;
                }
            }
            Console.WriteLine("Тест прошел!");
        }
        else
        {
            Console.WriteLine("Преобразованный массив: " + string.Join(", ", newArray));
        }
    }
}