using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task8
{
    class Program
    {

        static bool NextSet(int[] a, int n, int[,] matrix)
        {
            int j = n - 2;
            while (j != -1 && a[j] >= a[j + 1]) j--;
            if (j == -1)
                return false; // больше перестановок нет
            int k = n - 1;
            while (a[j] >= a[k]) k--;
            int s = a[k];
            a[k] = a[j];
            a[j] = s;
            Swap(matrix, j, k);
            int l = j + 1, r = n - 1; // сортируем оставшуюся часть последовательности
            while (l < r)
            {
                s = a[l];
                a[l] = a[r];
                a[r] = s;
                Swap(matrix, l, r);
                l++;r--;
            }
            return true;
        }
        static void PrintMatrix(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
        static double Input(string sentence, double minBorder = double.MinValue, double maxBorder = double.MaxValue)
        {
            double result = 0;
            bool ok = true;
            do
            {
                Console.Write(sentence);
                ok = double.TryParse(Console.ReadLine(), out result);
                if (result < minBorder || result > maxBorder)
                {
                    ok = false;
                }
            }
            while (!ok);
            return result;
        }
        static int IntInput(string sentence, int minBorder = int.MinValue, int maxBorder = int.MaxValue)
        {
            int result = 0;
            bool ok = true;
            do
            {
                Console.Write(sentence);
                ok = int.TryParse(Console.ReadLine(), out result);
                if (result < minBorder || result > maxBorder)
                {
                    ok = false;
                }
            }
            while (!ok);
            return result;
        }
        static int[] InputGraph(string sentence, int minBorder = int.MinValue, int maxBorder = int.MaxValue)
        {
            int[] result = new int[2];
            bool ok = true;

            do
            {
                try
                {
                    Console.WriteLine(sentence);
                    result = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
                    ok = true;
                    if (result.Max() > maxBorder || result.Min() < minBorder)
                    {
                        ok = false;
                        Console.WriteLine("Вершины не входят в заданную область");
                    }
                    else if (result[0] == result[1])
                    {
                        ok = false;
                        Console.WriteLine("Вершины графа не могут совпадать");
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Неверный формат ввода");
                    ok = false;
                }
            }
            while (!ok);
            return result;
        }
        static void Swap(int[,] matrix, int p1, int p2)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                int temp = matrix[i, p1];
                matrix[i, p1] = matrix[i, p2];
                matrix[i, p2] = temp;

                
            }
            for(int i = 0; i < matrix.GetLength(0); i++)
            {
                int temp = matrix[p1, i];
                matrix[p1, i] = matrix[p2, i];
                matrix[p2, i] = temp;
            }
        }
        static bool Equals(int[,] arr1, int[,] arr2)
        {
            for(int i = 0; i < arr1.GetLength(0); i++)
            {
                for(int j = 0; j < arr1.GetLength(1); j++)
                {
                    if (arr1[i,j] != arr2[i,j])
                        return false;
                }
            }
            return true;
        }

        static void Main(string[] args)
        {
            
            int n1 = IntInput("Введите количество вершин первого графа: ", 1);
            int k1 = IntInput("Введите количество ребер первого графа: ", 0);
            int[][] arrayOfBorders1 = new int[k1][];
            Console.WriteLine("Введите ребро графа в виде 'вершина 1 - пробел - вершина 2', причем " +
                "наибольшее значение из двух не превосходит заданное количество вершин");
            Console.WriteLine("Например, 1 2");
            for (int i = 0; i < k1; i++)
            {
                arrayOfBorders1[i] = InputGraph($"Введите {i + 1}-е ребро графа", 1, n1);
            }

            int n2 = IntInput("Введите количество вершин второго графа: ", 1);
            int k2 = IntInput("Введите количество ребер второго графа: ", 0);
            int[][] arrayOfBorders2 = new int[k2][];

            for (int i = 0; i < k2; i++)
            {
                arrayOfBorders2[i] = InputGraph($"Введите {i + 1}-е ребро графа", 1, n2);
            }
            //Инициализация первой матрицы
            int[,] matrix1 = new int[n1, n1];
            for (int i = 0; i < k1; i++)
            {
                matrix1[arrayOfBorders1[i][0] - 1, arrayOfBorders1[i][1] - 1] = 1;
                matrix1[arrayOfBorders1[i][1] - 1, arrayOfBorders1[i][0] - 1] = 1;
            }
            //Инициализация второй матрицы
            int[,] matrix2 = new int[n1, n1];
            for (int i = 0; i < k2; i++)
            {
                matrix2[arrayOfBorders2[i][0] - 1, arrayOfBorders2[i][1] - 1] = 1;
                matrix2[arrayOfBorders2[i][1] - 1, arrayOfBorders2[i][0] - 1] = 1;
            }

            PrintMatrix(matrix1);
            Console.WriteLine();
            PrintMatrix(matrix2);

            if(n1!=n2 || k1 != k2)
            {
                Console.WriteLine("Графы не изоморфны");
            }
            else
            {
                int[] array = new int[matrix1.GetLength(0)];
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = i + 1;
                }
                bool isomorph = false;
                do
                {
                    if (Equals(matrix1, matrix2))
                    {
                        
                        isomorph = true;
                        break;
                    }

                } while (NextSet(array, array.Length, matrix2));

                if (isomorph)
                {
                    Console.WriteLine("Графы изоморфны");

                }
                else
                {
                    Console.WriteLine("Графы не изоморфны");
                }
            }
            
            Console.ReadKey();


        }
    }
}
