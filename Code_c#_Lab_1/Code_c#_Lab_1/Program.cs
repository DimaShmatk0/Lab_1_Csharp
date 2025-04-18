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

            int threadCount = 8;

            RunTestWithThreads(threadCount, minDuration, maxDuration, minStep, maxStep);
            
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

            var calculators = new NumberCalculator[threadCount];

            for (int i = 0; i < threadCount; i++)
            {
                calculators[i] = new NumberCalculator(i, incrementSteps[i]);
                calculators[i].Start();
            }

            var controller = new ThreadController(threadDurations, calculators);
            Thread controllerThread = new Thread(controller.Run);

            controllerThread.Start();
        }
    }
}
