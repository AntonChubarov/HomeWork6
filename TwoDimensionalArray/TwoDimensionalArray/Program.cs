using System;

namespace TwoDimensionalArray
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                Console.Clear();
                Console.WriteLine("Type the number of array rows (a positive number between 2 and 10) and press Enter.");
                int rows = AskForSize();
                Console.WriteLine("Type the number of array columns (a positive number between 2 and 10) and press Enter.");
                int columns = AskForSize();
                Console.WriteLine("The array will be filled with random integers.");
                Console.WriteLine("Type a lower bound of random numbers (an integer number) and press Enter.");
                int lower = AskForInteger();
                Console.WriteLine("Type an upper bound of random numbers (an integer number) and press Enter.");
                int upper = AskForInteger();
                if (lower > upper)
                {
                    lower = lower + upper;
                    upper = lower - upper;
                    lower = lower - upper;
                }
                int[,] arr = new int[rows, columns];
                FillArrayRandomly(arr, lower, upper);
                Console.WriteLine("\nThe array is:");
                PrintArray(arr);
                int minval = ArrayMinVal(arr);
                Console.WriteLine($"Min. element of array is: {minval}\n");
                int maxval = ArrayMaxVal(arr);
                Console.WriteLine($"Max. element of array is: {maxval}\n");
                Console.Write("Index(es) of min. element(s) in array: ");
                PrintIndexes(arr, minval);
                Console.Write("Index(es) of max. element(s) in array: ");
                PrintIndexes(arr, maxval);
                Console.WriteLine($"Count of elements, biger than all their neighbors (in row): {CountOfBigerThanNeighbors(arr)}\n");
                Console.WriteLine("\nPress Enter to restart, type \"exit\" and press Enter to exit");
            } while (Console.ReadLine() != "exit");
        }
        private static int AskForSize()
        {
            int i;
            while (!Int32.TryParse(Console.ReadLine(), out i))
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.Write("Incorrrect input. Please, type a positive number between 2 and 20\n");
            }
            if (i < 0)
            {
                i *= -1;
                Console.WriteLine($"The negative number was converted to positive {i}");
            }
            if (i == 1 || i == 0)
            {
                i = 2;
                Console.WriteLine($"Array size 0 or 1 does not make sense, size was set to minimum value {i}");
            }
            else if (i > 10)
            {
                i = 10;
                Console.WriteLine($"Too big array size for console, size was set to maximum value {i}");
            }
            return i;
        }
        private static int AskForInteger()
        {
            int i;
            while (!Int32.TryParse(Console.ReadLine(), out i))
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.Write("Incorrrect input. Please, type an integer number\n");
            }
            return i;
        }
        private static void FillArrayRandomly(int[,] arr, int lower, int upper)
        {
            Random rand = new Random();
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    arr[i, j] = rand.Next(lower, upper);
                }
            }
        }
        private static void PrintArray(int[,] arr)
        {
            Console.WriteLine("");
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    Console.Write($"{arr[i, j]}\t");
                }
                Console.WriteLine("\n");
            }
        }
        private static int ArrayMinVal(int[,] arr)
        {
            int min = arr[0,0];
            foreach (int item in arr)
            {
                if (item < min)
                    min = item;
            }
            return min;
        }
        private static int ArrayMaxVal(int[,] arr)
        {
            int max = arr[0, 0];
            foreach (int item in arr)
            {
                if (item > max)
                    max = item;
            }
            return max;
        }
        private static int[,] IndexOf(int[,] arr, int item)
        {
            int[,] indexes = new int[arr.Length, 2];
            int count = 0;
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (arr[i,j] == item)
                    {
                        indexes[count, 0] = i;
                        indexes[count, 1] = j;
                        count++;
                    }
                }
            }
            int[,] outIndexes = new int[count, 2];
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    outIndexes[i, j] = indexes[i, j];
                }
            }
            return outIndexes;
        }
        private static void PrintIndexes(int[,] arr, int item)
        {
            int[,] indexes = IndexOf(arr, item);
            for (int i = 0; i < indexes.GetLength(0); i++)
            {
                Console.Write($"{{ {indexes[i,0]}, {indexes[i,1]} }}   ");
            }
            Console.WriteLine("\n");
        }
        private static int CountOfBigerThanNeighbors(int[,] arr)
        {
            int count = 0;
            int columns = arr.GetLength(1);
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (j == 0)
                    {
                        if (arr[i, j] > arr[i, j + 1])
                            count++;
                    } else if (j == columns - 1)
                    {
                        if (arr[i, j] > arr[i, j - 1])
                            count++;
                    } else
                    {
                        if (arr[i, j] > arr[i, j - 1] && arr[i, j] > arr[i, j + 1])
                            count++;
                    }
                }
            }
            return count;
        }
    }
}
