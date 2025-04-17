using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_c__Lab_1
{
    class ThreadController
    {
        private readonly int[] runTimes;
        private readonly NumberCalculator[] workers;

        public ThreadController(int[] runTimes, NumberCalculator[] workers)
        {
            this.runTimes = runTimes;
            this.workers = workers;
        }

        public void Run()
        {
            DateTime startTime = DateTime.Now;
            bool[] isFinished = new bool[workers.Length];
            int completedCount = 0;

            while (completedCount < workers.Length)
            {
                TimeSpan elapsed = DateTime.Now - startTime;
                long elapsedSeconds = (long)elapsed.TotalSeconds;

                for (int i = 0; i < workers.Length; i++)
                {
                    if (!isFinished[i] && elapsedSeconds >= runTimes[i])
                    {
                        workers[i].SetStopped(true);
                        isFinished[i] = true;
                        completedCount++;
                    }
                }

                Thread.Sleep(200);
            }
        }
    }
}