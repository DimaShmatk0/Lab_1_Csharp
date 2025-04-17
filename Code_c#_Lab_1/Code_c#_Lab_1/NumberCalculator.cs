using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_c__Lab_1
{
    class NumberCalculator
    {
        private Thread thread;
        private readonly int threadId;
        private readonly int increment;
        private readonly CountdownEvent startLatch;
        private volatile bool stopped = false;

        public NumberCalculator(int threadId, int increment, CountdownEvent startLatch)
        {
            this.threadId = threadId;
            this.increment = increment;
            this.startLatch = startLatch;
            thread = new Thread(Run);
        }

        public void Start() => thread.Start();

        public void SetStopped(bool stopped) => this.stopped = stopped;

        private void Run()
        {
            startLatch.Wait(); // Очікуємо сигналу запуску

            long totalSum = 0;
            long termsCount = 0;

            while (!stopped)
            {
                totalSum += increment;
                termsCount++;
            }

            Console.WriteLine($"Thread ID: {threadId}, Sum: {totalSum}, Terms: {termsCount}, Step: {increment}");
        }
    }
}
