using System;

class Program
{
    static void Main()
    {
        Test(null, 0, null);
        Console.WriteLine();
        Test(new int[,] { { 0, 0, 0, 0, 0 }, { 0, -1, -2, -3, -4 }, { 0, 1, 2, 3, 4 }, { 0, -1, -2, -3, -4 }, { 0, 1, 2, 3, 4 } }, 2, new int[,] { { -1, -2, -3, -4 }, { 1, 2, 3, 4 }, { -1, -2, -3, -4 }, { 1, 2, 3, 4 } });
        Console.WriteLine();
        Test(new int[,] { { -1, -2, -3, -4, -5 }, { 0, 0, 0, 0, 0 }, { 1, 2, 3, 4, 5 }, { -1, -2, -3, -4, -5 }, { 1, 2, 3, 4, 5 } }, 2, new int[,] { { -1, -2, -3, -4, -5 }, { 1, 2, 3, 4, 5 }, { -1, -2, -3, -4, -5 }, { 1, 2, 3, 4, 5 } });
        Console.WriteLine();
        Test(new int[,] { { -1, -2, -3, -4, -5 }, { -1, -2, -3, -4, -5 }, { 0, 0, 0, 0, 0 }, { 1, 2, 3, 4, 5 }, { 1, 2, 3, 4, 5 } }, 3, new int[,] { { -1, -2, -3, -4, -5 }, { -1, -2, -3, -4, -5 }, { 1, 2, 3, 4, 5 }, { 1, 2, 3, 4, 5 } });
    }

    static void Test(int[,] matrix, int expectedPositiveRowIndex, int[,] expectedMatrix)
    {
        int N;
        
        if (matrix == null) 
        {
            N = 5;
            matrix = new int[N, N];
            Random rand = new Random();

            // Заполнение матрицы случайными числами
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    matrix[i, j] = rand.Next(-5, 5);
                }
            }
        }
        N = matrix.GetLength(0);
        
        Console.WriteLine("Данная матрица:");
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                Console.Write(matrix[i, j] + " ");
            }
            Console.WriteLine();
        }

        // Поиск строк и столбцов, заполненных только нулями
        bool[] zeroRows = new bool[N];
        bool[] zeroCols = new bool[N];
        for (int i = 0; i < N; i++)
        {
            zeroRows[i] = true;
            zeroCols[i] = true;
            for (int j = 0; j < N; j++)
            {
                if (matrix[i, j] != 0)
                {
                    zeroRows[i] = false;
                }
                if (matrix[j, i] != 0)
                {
                    zeroCols[i] = false;
                }
            }
        }

        // Создание новой матрицы без строк и столбцов, заполненных нулями
        int[,] newMatrix = new int[N, N];
        int newRow = 0, newCol = 0;
        for (int i = 0; i < N; i++)
        {
            if (!zeroRows[i])
            {
                newCol = 0;
                for (int j = 0; j < N; j++)
                {
                    if (!zeroCols[j])
                    {
                        newMatrix[newRow, newCol] = matrix[i, j];
                        newCol++;
                    }
                }
                newRow++;
            }
        }

        // Поиск номера первой строки с положительным элементом
        int positiveRowIndex = -1;
        for (int i = 0; i < N && positiveRowIndex == -1; i++)
        {
            for (int j = 0; j < N; j++)
            {
                if (matrix[i, j] > 0)
                {
                    positiveRowIndex = i;
                    break;
                }
            }
        }

        if (expectedMatrix != null) 
        {
            if (positiveRowIndex != expectedPositiveRowIndex) 
            {
                Console.WriteLine("Номер первой строки с положительным элементом: " + positiveRowIndex + ", ожидается: " + expectedPositiveRowIndex);
                return;
            }
        }
        else
        {
            Console.WriteLine("Номер первой строки с положительным элементом: " + positiveRowIndex);
        }
        
        if (expectedMatrix != null) {
            bool equal = true;
            for (int i = 0; i < newRow && equal; i++)
            {
                for (int j = 0; j < newCol; j++)
                {
                    if (newMatrix[i, j] != expectedMatrix[i, j]) 
                    {
                        equal = false;
                        break;
                    }
                }
            } 
            
            if (!equal) {
                Console.WriteLine("Измененная матрица:");
                for (int i = 0; i < newRow; i++)
                {
                    for (int j = 0; j < newCol; j++)
                    {
                        Console.Write(newMatrix[i, j] + " ");
                    }
                    Console.WriteLine();
                }   

                Console.WriteLine("Ожидаемая матрица:");
                for (int i = 0; i < newRow; i++)
                {
                    for (int j = 0; j < newCol; j++)
                    {
                        Console.Write(expectedMatrix[i, j] + " ");
                    }
                    Console.WriteLine();
                }
                return;
            }
            Console.WriteLine("Тест пройден!");
        }
        else
        {
            Console.WriteLine("Измененная матрица:");
            for (int i = 0; i < newRow; i++)
            {
                for (int j = 0; j < newCol; j++)
                {
                    Console.Write(newMatrix[i, j] + " ");
                }
                Console.WriteLine();
            } 
        }
    }
}