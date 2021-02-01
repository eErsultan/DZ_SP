using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Thread_DZ
{
    class Program
    {
        private static int startNumber = 0;

        static void Main(string[] args)
        {
            while (true)
            {
                ErrorMessage("Нажмите клавишу Esc для остановки потока");
                Console.Write("Введите число, которое начинает генерировать простые числа: ");
                int.TryParse(Console.ReadLine(), out startNumber);
                while (startNumber < 0)
                {
                    Console.Write("Число не может быть меньше 0: ");
                    int.TryParse(Console.ReadLine(), out startNumber);
                }
                Thread thread = new Thread(OutputAndWritingPrimeNumbers);
                thread.Start();
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    thread.Abort();
                    SuccessMessage("Поток остановлен");
                    break;
                }
                else
                {
                    while (keyInfo.Key != ConsoleKey.Escape)
                    {
                        ErrorMessage("Нажмите клавишу Esc для остановки потока");
                        keyInfo = Console.ReadKey();
                    }
                    thread.Abort();
                    SuccessMessage("Поток остановлен");
                }
            }   
        }

        static void SuccessMessage(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n" + text + "\n");
            Console.ResetColor();
        }

        static void ErrorMessage(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n" + text + "\n");
            Console.ResetColor();
        }

        static void OutputAndWritingPrimeNumbers()
        {
            int primeNumber;
            while (true)
            {
                primeNumber = GeneratePrimeNumbers();
                WriteNumberInFile(primeNumber);
                OutputOnConsole(primeNumber);
                Thread.Sleep(1000);
            }
        }

        static void WriteNumberInFile(int number)
        {
            string pathFile = @"C:\test\PrimeNumbers.txt";
            try
            {
                using (StreamWriter sw = new StreamWriter(pathFile, true))
                {
                    sw.WriteLine(number);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void OutputOnConsole(int number)
        {
            Console.WriteLine(number);
        }

        static int GeneratePrimeNumbers()
        {
            int primeNumber;
            while (true)
            {
                primeNumber = startNumber;
                if (primeNumber == 1 || primeNumber == 2 ||
                    primeNumber == 3 || primeNumber == 5 ||
                    primeNumber == 7)
                {
                    startNumber++;
                    return primeNumber;
                }
                if (primeNumber % 2 != 0 && primeNumber % 3 != 0 && 
                    primeNumber % 5 != 0 && primeNumber % 7 != 0)
                {
                    startNumber++;
                    break;
                }
                else
                {
                    startNumber++;
                }
            }
            return primeNumber;
        }

    }
}
