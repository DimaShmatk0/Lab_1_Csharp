using System;
using System.Linq;
using System.Threading;

namespace Code_c__Lab_1
{
    class Program
    {
        static void Main()
        {
            int minDuration = 4;
            int maxDuration = 10;
            int minStep = 2;
            int maxStep = 6;

            for (int threadCount = 8; threadCount <= 8; threadCount *= 2)
            {
                RunTestWithThreads(threadCount, minDuration, maxDuration, minStep, maxStep);
            }
        }

        static int[] GenerateRandomArray(int size, int min, int max)
        {
            Random rand = new Random();
            int[] result = new int[size];
            for (int i = 0; i < size; i++)
            {
                result[i] = rand.Next(min, max + 1);
            }
            return result;
        }

        static void PrintArray(int[] array)
        {
            foreach (var value in array)
            {
                Console.Write(value + " ");
            }
            Console.WriteLine();
        }

        static void RunTestWithThreads(int threadCount, int minDuration, int maxDuration, int minStep, int maxStep)
        {
            Console.WriteLine($"\nКількість потоків: {threadCount}");

            int[] threadDurations = GenerateRandomArray(threadCount, minDuration, maxDuration);
            int[] incrementSteps = GenerateRandomArray(threadCount, minStep, maxStep);

            Console.Write("Тривалість потоків: ");
            PrintArray(threadDurations);
            Console.Write("Кроки додавання: ");
            PrintArray(incrementSteps);

            var launchSignal = new CountdownEvent(1);
            var calculators = new NumberCalculator[threadCount];

            for (int i = 0; i < threadCount; i++)
            {
                calculators[i] = new NumberCalculator(i, incrementSteps[i], launchSignal);
                calculators[i].Start();
            }

            var controller = new ThreadController(threadDurations, calculators);
            Thread controllerThread = new Thread(controller.Run);

            Thread.Sleep(1000);
            Console.WriteLine(">>> Початок!");
            var startTime = DateTime.Now;
            controllerThread.Start();
            launchSignal.Signal(); // даємо сигнал на старт

            controllerThread.Join();
            var durationMs = (DateTime.Now - startTime).TotalMilliseconds;
            Console.WriteLine($"Загальний час виконання: {durationMs:F2} мс");
        }
    }
}