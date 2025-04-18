
namespace Code_c__Lab_1
{
    class NumberCalculator
    {
        private Thread thread;
        private readonly int threadId;
        private readonly int increment;
        private volatile bool stopped = false;

        public NumberCalculator(int threadId, int increment)
        {
            this.threadId = threadId;
            this.increment = increment;
            thread = new Thread(Run);
        }

        public void Start() => thread.Start();

        public void SetStopped(bool stopped) => this.stopped = stopped;

        private void Run()
        {
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
