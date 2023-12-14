using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Задачи
{
    internal class Program
    {
        static void CountNumbers()
        {
            for (int i = 1; i <= 10; i++)
            {
                Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId}: {i}");
                Thread.Sleep(100); 
            }
        }

        static async Task<long> CalculateFactorialAsync(int number)
        {
            await Task.Delay(5000); // Задержка потока на 5 секунд
            long factorial = 1;
            for (int i = 1; i <= number; i++)
            {
                factorial *= i;
            }
            return factorial;
        }
        static int CalculateSquare(int number)
        {
            return number * number;
        }
        static void Main(string[] args)
        {
            // Задача 1 - Создать программу, где будет реализовано 3 потока. В каждом потоке числа от 1 до 10.
            Console.WriteLine("Задача 1 - Создать программу, где будет реализовано 3 потока. В каждом потоке числа от 1 до 10.\n");

            Thread thread1 = new Thread(CountNumbers);
            Thread thread2 = new Thread(CountNumbers);
            Thread thread3 = new Thread(CountNumbers);

            thread1.Start();
            thread2.Start();
            thread3.Start();
            
            thread1.Join();
            thread2.Join();
            thread3.Join();

            Console.WriteLine("Все потоки завершились.");

            // Задача 2 - Факториал,квадрат введенного числа.Факториал ассинхронно, квадрат синхронно
            Console.WriteLine("\nЗадача 2 - Факториал,квадрат введенного числа.Факториал ассинхронно, квадрат синхронно\n");
            Console.Write("Введите число: ");
            int number;
            bool a = int.TryParse(Console.ReadLine(), out number); 
            if (a)
            {
                // Синхронное вычисление квадрата числa
                int square = CalculateSquare(number);
                Console.WriteLine($"Квадрат числа {number}: {square}");
                // Асинхронное вычисление факториала
                Task<long> factorialTask = CalculateFactorialAsync(number);
                Console.WriteLine("Вычисление факториала запущено...");
                // Ожидаем завершения вычисления факториала
                factorialTask.Wait();
                long factorial = factorialTask.Result;
                Console.WriteLine($"Факториал числа {number}: {factorial}");
                Console.WriteLine("Программа завершена.");
            }
            else
            {
                Console.WriteLine("Вы некорректно ввели число");
            }

            // Задача 3 - Получаем объект и должны вернуть имена всех методов, которые нашли для этого объекта
            Console.WriteLine("\nЗадача 3 - Получаем объект и должны вернуть имена всех методов, которые нашли для этого объекта\n");
            Type type = typeof(Refl);
            MethodInfo[] methods = type.GetMethods();

            foreach (MethodInfo method in methods)
            {
                Console.WriteLine(method.Name);
            }
        }                
    }
}
