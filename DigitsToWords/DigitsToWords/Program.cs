using System;

namespace NumsToWords
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                Console.Clear();
                Console.WriteLine("Type 3-digit number (between 0 and 999) and press Enter to convert it to words");
                Digit2Number();
                Console.WriteLine("type 3-digit number by words to convert it to digits");
                int nDigit = Text2Digits();
                Console.WriteLine($"\nYour number is: {nDigit}\n");
                Console.WriteLine("Press Enter to restart application, type \"exit\" and press Enter to exit");
            } while (Console.ReadLine().ToLower() != "exit");
        }
        private static void Digit2Number()
        {
            int[] digits = GetDigits();
            if (digits[0] + digits[1] + digits[2] == 0)
            {
                Console.WriteLine("Your number is: zero");
                return;
            }
            string shundreds = Hundreds2String(digits[0]);
            if (digits[0] != 0 && digits[1] + digits[2] != 0)
                shundreds += " and";
            string sDecades, sOnes;
            if (digits[1] == 1)
            {
                sDecades = Umpteens2String(digits[2]);
                sOnes = "";
            }
            else
            {
                sDecades = Decades2String(digits[1]);
                sOnes = Ones2String(digits[2]);
            }
            Console.WriteLine($"\nYour number is: {shundreds} {sDecades} {sOnes}\n".Replace("  ", " "));
        }
        private static int GetInteger()
        {
            int i;
            while (!Int32.TryParse(Console.ReadLine(), out i))
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.Write("Incorrrect input. Please, type a non-negative number between 0 and 999\n");
            }
            if (i < 0)
            {
                i *= -1;
                Console.WriteLine($"The negative number was converted to positive {i}");
            }
            else if (i > 999)
            {
                i = 999;
                Console.WriteLine($"Too big number, it was set to maximum value {i}");
            }
            return i;
        }
        private static int[] GetDigits()
        {
            int i = GetInteger();
            int hundreds = i / 100;
            int decades = (i - 100 * hundreds) / 10;
            int ones = i - 100 * hundreds - 10 * decades;
            int[] digits = new int[3] { hundreds, decades, ones };
            return digits;
        }
        private static string Ones2String(int digit)
        {
            string[] ones = new string[10] { "", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            string sOnes = ones[digit];
            return sOnes;
        }
        private static string Hundreds2String(int digit)
        {
            string shundreds = Ones2String(digit);
            if (digit != 0)
            {
                if (digit == 1)
                    shundreds += " hundred";
                else
                    shundreds += " hundreds";
            }
            return shundreds;
        }
        private static string Umpteens2String(int digit)
        {
            string[] umpteens = new string[10] { "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
            string sUmpteens = umpteens[digit];
            return sUmpteens;
        }
        private static string Decades2String(int digit)
        {
            string[] decades = new string[10] { "", "", "twenty", "thirty", "fourty", "fifty", "sixty", "seventy", "eighty", "ninety" };
            string sDecades = decades[digit];
            return sDecades;
        }
        private static int Text2Digits()
        {
            string[] sDigits = GetWords();
            string[,] replacements = { {"zero", "0"}, {"one", "01"}, { "two", "02" }, { "three", "03" }, { "four", "04" }, { "five", "05" },
                { "six", "06" }, { "seven", "07" }, { "eight", "08" }, { "nine", "09" }, { "ten", "10" }, { "eleven", "11" }, { "twelve", "12" },
                { "thirteen", "13" }, { "fourteen", "14" }, { "fifteen", "15" }, { "sixteen", "16" }, { "seventeen", "17" }, { "eighteen", "18" },
                { "nineteen", "19" }, { "twenty", "20" }, { "thirty", "30" }, { "forty", "40" }, { "fifty", "50" }, { "sixty", "60" },
                { "seventy", "70" }, { "eighty", "80" }, { "ninety", "90" } };
            for (int i = 0; i < sDigits.Length; i++)
            {
                for (int j = 0; j < replacements.GetLength(0); j++)
                {
                    if (sDigits[i] == replacements[j, 0])
                    {
                        sDigits[i] = replacements[j, 1];
                    }
                }
            }
            string digit = "";
            foreach (string item in sDigits)
                digit += item;
            if (digit[0] == '0' && digit.Length > 1)
                digit = digit.Substring(1);
            if (digit.Length > 3)
                digit = digit.Replace("00", "");
            int nDigit;
            if (!Int32.TryParse(digit, out nDigit) || nDigit > 999)
            {
                Console.WriteLine("Input error possible");
                return -1;
            }
            return nDigit;
        }
        private static string[] GetWords()
        {
            string input = Console.ReadLine().ToLower();
            string output = input;
            output = output.Replace(" hundreds", "");
            output = output.Replace(" hundred", "");
            output = output.Replace(" and ", " ");
            output = output.Replace("  ", " ");
            output = output.Trim();
            string[] digitsWords = output.Split(' ');
            if (input.Contains("hun") && digitsWords.Length == 1)
            {
                Array.Resize(ref digitsWords, 3);
                digitsWords[1] = "zero";
                digitsWords[2] = "zero";
            }
            return digitsWords;
        }
    }
}
