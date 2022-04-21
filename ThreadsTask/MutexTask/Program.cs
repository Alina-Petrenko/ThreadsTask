using System;
using System.Threading;

namespace MutexTask
{
    /// <summary>
    /// Represents working with 'Mutex'
    /// </summary>
    public class Program
    {
        #region Fields

        /// <summary>
        /// Mutex
        /// </summary>
        public static Mutex mutex = new();

        #endregion

        #region Private Methods

        /// <summary>
        /// Entry point in project
        /// </summary>
        static void Main()
        {
            Console.WriteLine("Using 'Mutex'");
            for (var i = 0; i < 5; i++)
            {
                Thread myThread = new(Division);
                myThread.Name = $"Thread {i}";
                myThread.Start();
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Divides a value in half
        /// </summary>
        public static void Division()
        {
            double value = 40d;
            mutex.WaitOne();
            for (var i = 0; i < 5; i++)
            {
                value = Math.Round((value / 2), 2);
                Console.WriteLine($"{Thread.CurrentThread.Name}: {value}");
            }
            mutex.ReleaseMutex();
        }

        #endregion
    }
}
